﻿using Moq;
using NeoServer.Server.Contracts.Tasks;
using NeoServer.Server.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NeoServer.Server.Tests.Tasks
{
    public class OptimizedSchedulerTest
    {
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(500)]
        [Theory]
        public void Start_Must_Execute_Events_After_Delay(int delay)
        {
            var dispatcher = new Mock<IDispatcher>();
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            var sut = new OptimizedScheduler(dispatcher.Object);

            sut.Start(cancellationToken);

            for (int i = 0; i < 5_000; i++)
            {
                sut.AddEvent(new SchedulerEvent(delay, () => { }));
            }

            Thread.Sleep(1_000);
            //Thread.Sleep(delay * 10);
            Assert.Equal(5_000ul, sut.Count);
        }
        [Fact]
        public void Start_Must_Execute_Random_Delayed_Events()
        {
            var dispatcher = new Mock<IDispatcher>();
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            var sut = new OptimizedScheduler(dispatcher.Object);

            sut.Start(cancellationToken);

            Random random = new();
            for (int i = 0; i < 5_000; i++)
            {
                var delay = random.Next(1, 1000);
                sut.AddEvent(new SchedulerEvent(delay, () => { }));
            }

            Thread.Sleep(1_000);
            //Thread.Sleep(delay * 10);
            Assert.Equal(5_000ul, sut.Count);
        }

    }
}
