#region Using
using AuditNow.Api.Resources.Transaction;
using AuditNow.Api.Validators.Transaction;
using AuditNow.Core.Models;
using AuditNow.Core.Models.Enums;
using AuditNow.Core.Models.ValueObjects;
using AuditNow.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
#endregion

namespace AuditNow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;


        public TransactionController(IHttpContextAccessor context, IUserService userService, ITransactionService transactionService, IMapper mapper) : base(context, userService)
        {
            _mapper = mapper;
            _transactionService = transactionService;
        }


        [HttpPost("")]
        public async Task<ActionResult<ReturnObject<TransactionResource>>> CreateTransaction([FromBody] CreateTransactionResource saveTransactionResource)
        {
            if (requestUser == null) return Unauthorized();

            var validationResult = await new CreateTransactionValidator().ValidateAsync(saveTransactionResource);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            try
            {
                ReturnObject<TransactionResource> response = new ReturnObject<TransactionResource>();

                var transactionToCreate = _mapper.Map<CreateTransactionResource, Transaction>(saveTransactionResource);
                transactionToCreate.UserId = requestUser.UserId;
                transactionToCreate.CreationUserId = requestUser.UserId;
                transactionToCreate.CreationDate = DateTime.UtcNow;
                transactionToCreate.ModificationUserId = requestUser.UserId;
                transactionToCreate.ModificationDate = DateTime.UtcNow;
                transactionToCreate.IsActive = true;

                ReturnObject<Transaction> ret = await _transactionService.CreateTransaction(transactionToCreate);
                if (ret.IsSuccessful == true)
                {
                    ReturnObject<Transaction> createdTransaction = _transactionService.GetTransactionById(ret.Data.First().TransactionId, true);
                    TransactionResource transactionResource = _mapper.Map<Transaction, TransactionResource>(createdTransaction.Data.First());
                    response.Data = new List<TransactionResource> { transactionResource };
                }

                response.IsSuccessful = ret.IsSuccessful;
                response.Message = ret.Message;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet("find")]
        public ActionResult<ReturnObject<TransactionResource>> GetTransactionByFilter([FromQuery] int transactionId, [FromQuery] int transactionType, [FromQuery] bool? isActive)
        {
            if (requestUser == null) return Unauthorized();

            try
            {
                ReturnObject<TransactionResource> response = new ReturnObject<TransactionResource>();

                Transaction transaction = new Transaction();
                transaction.TransactionId = transactionId;
                transaction.TransactionType = (TransactionType)Enum.Parse(typeof(TransactionType), transactionType.ToString());


                ReturnObject<Transaction> ret = _transactionService.GetTransactionByFilter(transaction, isActive, requestUser.UserId);
                if (ret.IsSuccessful == true)
                {
                    List<TransactionResource> transactionResources = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionResource>>(ret.Data).ToList();
                    response.Data = transactionResources;
                }

                response.IsSuccessful = ret.IsSuccessful;
                response.Message = ret.Message;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet("transactionTypes")]
        public ActionResult<ReturnObject<BasicEntity>> GetTransactionsTypes()
        {
            if (requestUser == null) return Unauthorized();

            try
            {
                ReturnObject<BasicEntity> response = new ReturnObject<BasicEntity>();

                ICollection<BasicEntity> transactionTypes = new Collection<BasicEntity>();
                var values = (TransactionType[])Enum.GetValues(typeof(TransactionType));
                for (int i = 0; i < values.Length; i++)
                {
                    transactionTypes.Add(new BasicEntity { Id = values[i].GetHashCode(), Description = values[i].ToString() });
                }

                response.IsSuccessful = true;
                response.Data = transactionTypes.ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
