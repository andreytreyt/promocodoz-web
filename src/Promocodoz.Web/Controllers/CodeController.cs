using System;
using System.Linq;
using System.Transactions;
using System.Web.Http;
using Promocodoz.Domain.Core.Entities;
using Promocodoz.Domain.Core.Enums;
using Promocodoz.Domain.Core.TransactionScopeFactory;
using Promocodoz.Domain.Interfaces;
using Promocodoz.Infrastructure.Data;
using Promocodoz.Web.Models;

namespace Promocodoz.Web.Controllers
{
    public class CodeController : ApiController
    {
        private readonly IRepository _repository;

        public CodeController()
        {
            _repository = new Repository();
        }

        // POST: api/code
        public IHttpActionResult Post([FromBody]CodeJsonModel model)
        {
            if (model == null)
                return BadRequest("Invalid data.");

            if (string.IsNullOrWhiteSpace(model.Sid))
                return BadRequest("Sid is required.");

            if (string.IsNullOrWhiteSpace(model.Secret))
                return BadRequest("Secret is required.");

            if (string.IsNullOrWhiteSpace(model.Code))
                return BadRequest("Code is required.");

            var user = _repository.GetById<ApplicationUser>(model.Sid);

            if (user == null)
                return BadRequest($"Account with sid {model.Sid} is not found.");

            if (!user.EmailConfirmed)
                return BadRequest("Email is not confirmed");

            if (user.SecretKey != model.Secret)
                return BadRequest("Secret is an incorrect.");

            var code = _repository.GetAll<Code>()
                .FirstOrDefault(x => !x.IsActivated && x.Key == model.Code && 
                                     (x.Platform == Platform.All || x.Platform == model.Platform));

            if (code == null)
                return BadRequest($"Code {model.Code} for Platform {model.Platform} is not found.");

            if (code.ExpiredDate < DateTime.Now)
                return BadRequest($"Activation date of code {model.Code} is expired.");

            using (TransactionScope transactionScope = TransactionScopeFactory.Serializable())
            {
                code.Activate();

                _repository.Save();
                transactionScope.Complete();
            }

            return Ok(code.Value);
        }
    }
}
