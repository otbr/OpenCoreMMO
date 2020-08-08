﻿using NeoServer.Game.Contracts;
using NeoServer.Game.Contracts.Creatures;
using NeoServer.Game.Contracts.Items;
using NeoServer.Game.Contracts.World;
using NeoServer.Game.Contracts.World.Tiles;
using NeoServer.Game.Creature.Model;
using NeoServer.Game.Creatures.Enums;
using NeoServer.Game.Enums.Creatures;
using NeoServer.Game.Enums.Creatures.Players;
using NeoServer.Game.Enums.Location;
using NeoServer.Game.Enums.Location.Structs;
using NeoServer.Game.Model;
using NeoServer.Server.Helpers;
using NeoServer.Server.Model.Players.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeoServer.Game.Creatures.Model
{
    public abstract class Creature : MoveableThing, ICreature, ICombatActor
    {
        private static readonly object _idLock = new object();
        private static uint _idCounter = 1;

        private readonly object _enqueueWalkLock;

        public event RemoveCreature OnCreatureRemoved;

        public event OnTurnedToDirection OnTurnedToDirection;

        public event StopWalk OnStoppedWalking;

        public event Damage OnDamaged;

        public event Die OnKilled;

        private readonly ICreatureType _creatureType;

        public Creature(ICreatureType type, IOutfit outfit = null, uint healthPoints = 0)
        {
            if (string.IsNullOrWhiteSpace(type.Name))
            {
                throw new ArgumentNullException(nameof(type.Name));
            }

            _enqueueWalkLock = new object();

            _creatureType = type;

            CreatureId = RandomIdGenerator.Generate();
            Speed = type.Speed;
            HealthPoints = Math.Min(MaxHealthpoints, healthPoints == 0 ? MaxHealthpoints : healthPoints);
            Outfit = outfit ?? new Outfit()
            {
                LookType = type.Look[LookType.Type]
            };


            Cooldowns = new Dictionary<CooldownType, Tuple<DateTime, TimeSpan>>
            {
                { CooldownType.Move, new Tuple<DateTime, TimeSpan>(DateTime.Now, TimeSpan.Zero) },
                { CooldownType.Action, new Tuple<DateTime, TimeSpan>(DateTime.Now, TimeSpan.Zero) },
                { CooldownType.Combat, new Tuple<DateTime, TimeSpan>(DateTime.Now, TimeSpan.Zero) },
                { CooldownType.Talk, new Tuple<DateTime, TimeSpan>(DateTime.Now, TimeSpan.Zero) }
            };

            WalkingQueue = new ConcurrentQueue<Direction>();

            Hostiles = new HashSet<uint>();
            Friendly = new HashSet<uint>();
        }

        public void SetAsRemoved()
        {
            IsRemoved = true;
        }

        public new string Name => _creatureType.Name;

        public bool IsDead => HealthPoints <= 0;

        public event OnAttackTargetChange OnTargetChanged;

        //public override ushort ThingId => CreatureThingId;

        //public override byte Count => 0x01;
        public override string InspectionText => $"{Name}";

        public override string CloseInspectionText => InspectionText;

        public uint ActorId => CreatureId;

        public uint CreatureId { get; }

        public ushort Corpse => _creatureType.Look[LookType.Corpse];

        public uint HealthPoints { get; private set; }

        public uint MaxHealthpoints => _creatureType.MaxHealth;

        public abstract IOutfit Outfit { get; protected set; }

        public Direction Direction { get; protected set; }

        public List<ICondition> Conditions { get; set; } = new List<ICondition>();

        public bool InFight => Conditions.Any(x => x.Type == ConditionType.InFight);

        public Direction ClientSafeDirection
        {
            get
            {
                switch (Direction)
                {
                    case Direction.North:
                    case Direction.East:
                    case Direction.South:
                    case Direction.West:
                        return Direction;
                    case Direction.NorthEast:
                    case Direction.SouthEast:
                        return Direction.East;
                    case Direction.NorthWest:
                    case Direction.SouthWest:
                        return Direction.West;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }


        public byte LightBrightness { get; protected set; }

        public byte LightColor { get; protected set; }

        public ushort Speed { get; protected set; } = 220;

        public uint Flags { get; private set; }

        public BloodType Blood { get; protected set; } // TODO: implement.

        public abstract ushort AttackPower { get; }


        public abstract ushort ArmorRating { get; }

        public abstract ushort DefensePower { get; }

        public uint AutoAttackTargetId { get; protected set; }

        public abstract byte AutoAttackRange { get; }

        public byte AutoAttackCredits { get; }

        public byte AutoDefenseCredits { get; }

        public decimal BaseAttackSpeed { get; }

        public decimal BaseDefenseSpeed { get; }

        public TimeSpan CombatCooldownTimeRemaining => throw new NotImplementedException(); //TODO
                                                                                            //CalculateRemainingCooldownTime(CooldownType.Combat, Game.Instance.CombatSynchronizationTime);

        public DateTime LastAttackTime => Cooldowns[CooldownType.Combat].Item1;

        public TimeSpan LastAttackCost => Cooldowns[CooldownType.Combat].Item2;


        public IDictionary<CooldownType, Tuple<DateTime, TimeSpan>> Cooldowns { get; }

        // public IList<Condition> Conditions { get; protected set; } // TODO: implement.
        public bool IsInvisible { get; protected set; } // TODO: implement.

        public bool CanSeeInvisible { get; } // TODO: implement.

        public ConcurrentQueue<Direction> WalkingQueue { get; }

        public byte NextStepId { get; set; }

        public HashSet<uint> Hostiles { get; }

        public HashSet<uint> Friendly { get; }


        public bool IsRemoved { get; private set; }

        private uint lastStepCost = 1;

        public bool HasFlag(CreatureFlag flag)
        {
            var flagValue = (uint)flag;

            return (Flags & flagValue) == flagValue;
        }

        public void SetFlag(CreatureFlag flag)
        {
            Flags |= (uint)flag;
        }

        public void UnsetFlag(CreatureFlag flag)
        {
            Flags &= ~(uint)flag;
        }

        public bool CanSee(ICreature otherCreature)
        {
            return !otherCreature.IsInvisible || CanSeeInvisible;
        }

        public bool CanSee(Location pos)
        {
            if (Location.Z <= 7)
            {
                // we are on ground level or above (7 -> 0)
                // view is from 7 -> 0
                if (pos.Z > 7)
                {
                    return false;
                }
            }
            else if (Location.Z >= 8)
            {
                // we are underground (8 -> 15)
                // view is +/- 2 from the floor we stand on
                if (Math.Abs(Location.Z - pos.Z) > 2)
                {
                    return false;
                }
            }

            var offsetZ = Location.Z - pos.Z;

            if (pos.X >= Location.X - 8 + offsetZ && pos.X <= Location.X + 9 + offsetZ &&
                pos.Y >= Location.Y - 6 + offsetZ && pos.Y <= Location.Y + 7 + offsetZ)
            {
                return true;
            }

            return false;
        }

        public void TurnTo(Direction direction)
        {
            Direction = direction;
            OnTurnedToDirection?.Invoke(this, direction);
        }

        public byte Skull { get; protected set; } // TODO: implement.

        public byte Shield { get; protected set; } // TODO: implement.

        public byte[] GetRaw(IPlayer playerRequesting)
        {
            var cache = new List<byte>();

            var known = playerRequesting.KnowsCreatureWithId(CreatureId);

            if (known)
            {
                cache.AddRange(BitConverter.GetBytes((ushort)0x62));
                cache.AddRange(BitConverter.GetBytes(CreatureId));
            }
            else
            {
                cache.AddRange(BitConverter.GetBytes((ushort)0x61));
                cache.AddRange(BitConverter.GetBytes(playerRequesting.ChooseToRemoveFromKnownSet()));
                cache.AddRange(BitConverter.GetBytes(CreatureId));

                playerRequesting.AddKnownCreature(CreatureId);

                var creatureNameBytes = Encoding.Default.GetBytes(Name);
                cache.AddRange(BitConverter.GetBytes((ushort)creatureNameBytes.Length));
                cache.AddRange(creatureNameBytes);
            }

            cache.Add((byte)Math.Min(100, HealthPoints * 100 / MaxHealthpoints));
            cache.Add((byte)ClientSafeDirection);

            if (playerRequesting.CanSee(this))
            {
                // Add creature outfit
                cache.AddRange(BitConverter.GetBytes(Outfit.LookType));

                if (Outfit.LookType > 0)
                {
                    cache.Add(Outfit.Head);
                    cache.Add(Outfit.Body);
                    cache.Add(Outfit.Legs);
                    cache.Add(Outfit.Feet);
                    cache.Add(Outfit.Addon);
                }
                else
                {
                    cache.AddRange(BitConverter.GetBytes(Outfit.LookType));
                }
            }
            else
            {
                cache.AddRange(BitConverter.GetBytes((ushort)0));
                cache.AddRange(BitConverter.GetBytes((ushort)0));
            }

            cache.Add(LightBrightness);
            cache.Add(LightColor);

            cache.AddRange(BitConverter.GetBytes(Speed));

            cache.Add(Skull);
            cache.Add(Shield);

            if (!known)
            {
                cache.Add(0x00); //guild emblem
            }

            cache.Add(0x00);
            return cache.ToArray();
        }

        public void SetAttackTarget(uint targetId)
        {
            AutoAttackTargetId = targetId;

        }

        public void UpdateLastAttack(TimeSpan exahust)
        {
            Cooldowns[CooldownType.Combat] = new Tuple<DateTime, TimeSpan>(DateTime.Now, exahust);
        }

        public void CheckAutoAttack(IThing thingChanged, IThingStateChangedEventArgs eventAgrs)
        {

            if (AutoAttackTargetId == 0)
            {
                return;
            }


        }

        public uint LastCombatEvent { get; set; }


        private void ReduceHealth(ushort damage)
        {
            if (damage > HealthPoints)
            {
                HealthPoints = 0;
            }
            else
            {
                HealthPoints -= damage;
            }

            if (IsDead)
            {
                OnKilled?.Invoke(this);
            }

        }

        public abstract bool UsingDistanceWeapon { get; }

        public void ReceiveAttack(ICreature enemy, ushort damage)
        {
            ReduceHealth(damage);
            OnDamaged?.Invoke(enemy, this, damage);
        }

        private void StopAttack() => AutoAttackTargetId = 0;
        
        public void Attack(ICreature enemy)
        {
            if (!UsingDistanceWeapon && !Tile.IsNextTo(enemy.Tile))
            {
                return;
            }

            if (enemy.IsDead)
            {
                StopAttack();
                return;
            }
            var remainingCooldown = CalculateRemainingCooldownTime(CooldownType.Combat, DateTime.Now);
            if (remainingCooldown > 0)
            {
                return;
            }

            enemy.ReceiveAttack(this, CalculateDamage());
            UpdateLastAttack(TimeSpan.FromMilliseconds(2000));

        }

        private GaussianRandom _random = new GaussianRandom();

        private ushort CalculateDamage()
        {
            var diff = AttackPower - MinimumAttackPower;
            var gaussian = _random.Next(0.5f, 0.25f);

            double increment;
            if (gaussian < 0.0)
            {
                increment = diff / 2;
            }
            else if (gaussian > 1.0)
            {
                increment = (diff + 1) / 2;
            }
            else
            {
                increment = Math.Round(gaussian * diff);
            }
            return (ushort)(MinimumAttackPower + increment);
        }

        public bool StopWalkingRequested { get; set; }
        public void StopWalking()
        {
            StopWalkingRequested = true;
            WalkingQueue.Clear(); // reset the actual queue
            UpdateLastStepInfo(0);

            OnStoppedWalking?.Invoke(this);
        }

        public override void Moved(ITile fromTile, ITile toTile)
        {
            LastStep = DateTime.Now.TimeOfDay.TotalMilliseconds;

            lastStepCost = 1;

            if (fromTile.Location.Z != toTile.Location.Z)
            {
                lastStepCost = 2;
            }
            else if (fromTile.Location.IsDiagonalMovement(toTile.Location))
            {
                lastStepCost = 3;
            }
        }


        public virtual bool TryWalkTo(params Direction[] directions)
        {
            lock (_enqueueWalkLock)
            {
                if (!WalkingQueue.IsEmpty)
                {
                    WalkingQueue.Clear();
                }
                foreach (var direction in directions)
                {
                    WalkingQueue.Enqueue(direction);
                }
            }
            return true;
        }



        public double CalculateRemainingCooldownTime(CooldownType type, DateTime currentTime)
        {
            TimeSpan timeDiff = TimeSpan.Zero;

            try
            {
                timeDiff = Cooldowns[type].Item1 + Cooldowns[type].Item2 - currentTime;
            }
            catch
            {
                Console.WriteLine($"WARN: cooldown type {type} not found in {Name}'s cooldowns.");
            }

            return Math.Round((timeDiff > TimeSpan.Zero ? timeDiff : TimeSpan.Zero).TotalMilliseconds, 0);
        }

        public void UpdateLastStepInfo(byte lastStepId, bool wasDiagonal = true)
        {
            var tilePenalty = Tile?.MovementPenalty;
            var totalPenalty = (tilePenalty ?? 200) * (wasDiagonal ? 2 : 1);

            Cooldowns[CooldownType.Move] = new Tuple<DateTime, TimeSpan>(DateTime.Now, TimeSpan.FromMilliseconds(1000 * totalPenalty / (double)Math.Max(1, (int)Speed)));
            lastStepCost = 1;
            //NextStepId = (byte)(lastStepId + 1);
        }

        private int stepDelay
        {
            get
            {
                var stepDuration = CalculateStepDuration() * lastStepCost;
                return (int)(stepDuration - (DateTime.Now.TimeOfDay.TotalMilliseconds - LastStep));


            }
        }
        public int StepDelayMilliseconds
        {
            get
            {
                if (stepDelay > 0)
                {
                    return stepDelay;
                }

                return (int)(CalculateStepDuration() * lastStepCost);
            }
        }

        public double LastStep { get; private set; }


        public List<uint> NextSteps { get; set; }
        public bool CancelNextWalk { get; set; }
        public uint EventWalk { get; set; }
        public IWalkableTile Tile { get; set; }

        public abstract ushort MinimumAttackPower { get; }

        private int CalculateStepDuration()
        {
            var duration = Math.Floor((double)(1000 * Tile.StepSpeed / Speed));

            var stepDuration = (int)Math.Ceiling(duration / 50) * 50;

            //todo check monster creature.cpp 1367
            return stepDuration;
        }

        public bool TryGetNextStep(out Direction direction)
        {
            return WalkingQueue.TryDequeue(out direction);
        }

        //public bool operator ==(Creature creature1, Creature creature2) => creature1.CreatureId == creature2.CreatureId;
        //public bool operator !=(Creature creature1, Creature creature2) => creature1.CreatureId != creature2.CreatureId;

    }
}
