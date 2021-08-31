﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NeoServer.Game.Common.Contracts.Creatures;
using NeoServer.Game.Common.Creatures;
using NeoServer.Game.Creatures.Model.Players;
using NeoServer.Game.Tests.Helpers;
using Xunit;

namespace NeoServer.Game.Creatures.Tests.Players
{
    public class PlayerSkillBonusesTests
    {
        [Fact]
        public void AddSkillBonus_0_KeepBonusTheSame()
        {
            var sut = PlayerTestDataBuilder.BuildPlayer(skills: new Dictionary<SkillType, ISkill>()
            {
                [SkillType.Axe] = new Skill(SkillType.Axe,1,10,0)
            });

            sut.AddSkillBonus(SkillType.Axe, 0);

            sut.GetSkillBonus(SkillType.Axe).Should().Be(0);
        }
        [Fact]
        public void AddSkillBonus_0_DoNotCallEvent()
        {
            var sut = PlayerTestDataBuilder.BuildPlayer(skills: new Dictionary<SkillType, ISkill>()
            {
                [SkillType.Axe] = new Skill(SkillType.Axe, 1, 10, 0)
            });

            var called = false;
            sut.OnAddedSkillBonus += (player, increased) =>
            {
                called = true;
            };

            sut.AddSkillBonus(SkillType.Axe, 0);

            called.Should().BeFalse();
        }
        [Fact]
        public void AddSkillBonus_10_IncreaseBonusBy10()
        {
            var sut = PlayerTestDataBuilder.BuildPlayer(skills: new Dictionary<SkillType, ISkill>()
            {
                [SkillType.Axe] = new Skill(SkillType.Axe, 1, 10, 0)
            });
            
            sut.AddSkillBonus(SkillType.Axe, 10);
            sut.GetSkillBonus(SkillType.Axe).Should().Be(10);

            sut.AddSkillBonus(SkillType.Axe, 5);
            sut.GetSkillBonus(SkillType.Axe).Should().Be(15);
        }
        [Fact]
        public void AddSkillBonus_10_CallEvent()
        {
            var sut = PlayerTestDataBuilder.BuildPlayer(skills: new Dictionary<SkillType, ISkill>()
            {
                [SkillType.Axe] = new Skill(SkillType.Axe, 1, 10, 0)
            });

            sut.AddSkillBonus(SkillType.Axe, 10);
            sut.GetSkillBonus(SkillType.Axe).Should().Be(10);

            var eventEncreased = 0;
            IPlayer eventPlayer = null;
            sut.OnAddedSkillBonus += (player, increased) =>
            {
                eventPlayer = player;
                eventEncreased = increased;
            };

            sut.AddSkillBonus(SkillType.Axe, 5);
            eventEncreased.Should().Be(5);
            eventPlayer.Should().BeEquivalentTo(sut);
        }

        [Fact]
        public void RemoveSkillBonus_0_KeepBonusTheSame()
        {
            var sut = PlayerTestDataBuilder.BuildPlayer(skills: new Dictionary<SkillType, ISkill>()
            {
                [SkillType.Axe] = new Skill(SkillType.Axe, 1, 10, 0)
            });

            sut.RemoveSkillBonus(SkillType.Axe, 0);

            sut.GetSkillBonus(SkillType.Axe).Should().Be(0);
        }
        [Fact]
        public void RemoveSkillBonus_0_DoNotCallEvent()
        {
            var sut = PlayerTestDataBuilder.BuildPlayer(skills: new Dictionary<SkillType, ISkill>()
            {
                [SkillType.Axe] = new Skill(SkillType.Axe, 1, 10, 0)
            });

            var called = false;
            sut.OnRemovedSkillBonus += (player, increased) =>
            {
                called = true;
            };

            sut.RemoveSkillBonus(SkillType.Axe, 0);

            called.Should().BeFalse();
        }
        [Fact]
        public void RemoveSkillBonus_50_DecreaseBonusBy50()
        {
            var sut = PlayerTestDataBuilder.BuildPlayer(skills: new Dictionary<SkillType, ISkill>()
            {
                [SkillType.Axe] = new Skill(SkillType.Axe, 1, 10, 0)
            });

            sut.AddSkillBonus(SkillType.Axe, 100);

            sut.RemoveSkillBonus(SkillType.Axe, 50);
            sut.GetSkillBonus(SkillType.Axe).Should().Be(50);
        }
        [Fact]
        public void RemoveSkillBonus_5_CallEvent()
        {
            var sut = PlayerTestDataBuilder.BuildPlayer(skills: new Dictionary<SkillType, ISkill>()
            {
                [SkillType.Axe] = new Skill(SkillType.Axe, 1, 10, 0)
            });

            sut.AddSkillBonus(SkillType.Axe, 100);

            var eventDecreased = 0;
            IPlayer eventPlayer = null;
            sut.OnRemovedSkillBonus += (player, decreased) =>
            {
                eventPlayer = player;
                eventDecreased = decreased;
            };

            sut.RemoveSkillBonus(SkillType.Axe, 5);
            eventDecreased.Should().Be(5);
            eventPlayer.Should().BeEquivalentTo(sut);
        }

        [Fact]
        public void RemoveSkillBonus_MoreThanAvailable_SetTo0()
        {
            var sut = PlayerTestDataBuilder.BuildPlayer(skills: new Dictionary<SkillType, ISkill>()
            {
                [SkillType.Axe] = new Skill(SkillType.Axe, 1, 10, 0)
            });

            sut.AddSkillBonus(SkillType.Axe, 10);
            
            sut.RemoveSkillBonus(SkillType.Axe, 20);
            sut.GetSkillBonus(SkillType.Axe).Should().Be(0);
        }
    }
}
