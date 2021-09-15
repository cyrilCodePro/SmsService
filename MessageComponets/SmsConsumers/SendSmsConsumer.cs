using DataAccessLayer;

using MassTransit;

using MessageComponets.ApiService;


using MessageContracts.SMS;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageComponets.SmsConsumers
{
   public class SendSmsConsumer:IConsumer<SendSms>
    {
        private readonly ILogger<SendSmsConsumer> _logger;
        private readonly IApiSmsService _apiSmsService;
        private readonly ITransactionService _transactionService;

        public SendSmsConsumer(ILogger<SendSmsConsumer> logger, IApiSmsService apiSmsService, ITransactionService transactionService)
        {
            _logger = logger;
            _apiSmsService = apiSmsService;
            _transactionService = transactionService;
        }
        public SendSmsConsumer()
        {

        }

        public async Task Consume(ConsumeContext<SendSms> context)
        {

            var transactions = _transactionService == null ? false : await _transactionService?.CheckIfItExists(context.Message.IdempotenceKey);
           
            if (!transactions)
            {
                _logger?.LogInformation("Received Text message from: {PhoneNumber} with id {Id}", context.Message.PhoneNumber, context.Message.IdempotenceKey);
                await _apiSmsService?.SendSmsToApi(new { context.Message.PhoneNumber, context.Message.SmsText });
                await _transactionService?.AddNewTransactions(new TransactionsModel
                {
                    IdempotenceKey = context.Message.IdempotenceKey
                });
            }
            else
            {
                _logger?.LogWarning("Transaction with id {Id} exists", context.Message.IdempotenceKey);
            }
         
         }
    }
}
