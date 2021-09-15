using MassTransit;
using MassTransit.Testing;

using MessageComponets.SmsConsumers;

using MessageContracts.SMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit;
using NUnit.Framework;
using MessageComponets.StateMachine;
using Moq;
using Microsoft.Extensions.Logging;

namespace SmsMicroserviceTests
{
    [TestFixture]
   public class SendSmsTest
    {
        [Test]
        public async Task  Should_Receive_Published_Events_And_Consume_It()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<SendSmsConsumer>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send<SendSms > (new
                {
                    PhoneNumber = "+254702831844",
                    SmsText = "Hey",
                    IdempotenceKey = Guid.Parse("5fd8e1b9-8dfc-4a59-8702-fb998651aa3b")

                });

                // did the endpoint consume the message
                Assert.That(await harness.Consumed.Any<SendSms>());

                // did the actual consumer consume the message
                Assert.That(await consumerHarness.Consumed.Any<SendSms>());

            }
            finally
            {
                await harness.Stop();
            }
        }
        [Test]
        public async Task Should_create_a_state_machine_instance()
        {
            // Mass Transit test harness has in-memory transport and in-memory saga repository
            Mock<ILogger<SendSmsStateMachine>> _logger = new();
            var harness = new InMemoryTestHarness();
            var orderStateMachine = new SendSmsStateMachine(_logger.Object);
            var saga = harness.StateMachineSaga<SendSmsState, SendSmsStateMachine>(orderStateMachine);

            await harness.Start();
            try
            {
                Guid idempotencekey = Guid.NewGuid();
                  await harness.Bus.Publish<SendSms>(new
                {
                    PhoneNumber = "+254702831844",
                    SmsText = "Hey",
                    IdempotenceKey = idempotencekey,

                });

                // Expect a saga instance to be created
                Assert.That(saga.Created.Select(x => x.CorrelationId == idempotencekey).Any(), Is.True);

     

            }
            finally
            {
                await harness.Stop();
            }

        }
        [Test]
        public async Task Should_Publish_sms_sent_event_when_sms_is_sent()
        {
            
                // Mass Transit test harness has in-memory transport and in-memory saga repository
                Mock<ILogger<SendSmsStateMachine>> _logger = new();
                var harness = new InMemoryTestHarness();
                var orderStateMachine = new SendSmsStateMachine(_logger.Object);
                var saga = harness.StateMachineSaga<SendSmsState, SendSmsStateMachine>(orderStateMachine);

                await harness.Start();
                try
                {
                    Guid idempotencekey = Guid.NewGuid();
                    await harness.Bus.Publish<SendSms>(new
                    {
                        PhoneNumber = "+254702831844",
                        SmsText = "Hey",
                        IdempotenceKey = idempotencekey,

                    });

                    // Expect a saga instance to be created
                    Assert.That(saga.Created.Select(x => x.CorrelationId == idempotencekey).Any(), Is.True);



                
                Assert.That(saga.Created.Select(x => x.CorrelationId == idempotencekey).Any(), Is.True);

                // Avoid race condition and check if sms has been sent
                var instanceId = await saga.Exists(idempotencekey, x => x.SmsSubmitted);
              

            }
            finally
            {
                await harness.Stop();
            }

        }


    }
}
