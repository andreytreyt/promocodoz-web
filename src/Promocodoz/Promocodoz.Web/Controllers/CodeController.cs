using System.Linq;
using System.Transactions;
using System.Web.Http;
using Promocodoz.Domain.Core.Entities;
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
            if (string.IsNullOrWhiteSpace(model?.Sid) || 
                string.IsNullOrWhiteSpace(model.Secret) || 
                string.IsNullOrWhiteSpace(model.Code))
                return BadRequest();

            var user = _repository.GetById<ApplicationUser>(model.Sid);

            if (user == null)
                return BadRequest();

            if (user.SecretKey != model.Secret)
                return BadRequest();

            var code = _repository.GetAll<Code>().FirstOrDefault(x => !x.IsActivated && x.Key == model.Code);

            if (code == null)
                return NotFound();

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
