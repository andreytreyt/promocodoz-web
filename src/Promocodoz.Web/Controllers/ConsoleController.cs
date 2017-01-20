using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Promocodoz.Domain.Core.Entities;
using Promocodoz.Domain.Core.TransactionScopeFactory;
using Promocodoz.Domain.Interfaces;
using Promocodoz.Web.ViewModel;

namespace Promocodoz.Web.Controllers
{
    [Authorize]
    public class ConsoleController : Controller
    {
        private readonly IRepository _repository;

        public ConsoleController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = _repository.GetById<ApplicationUser>(userId);

            var model = new ConsoleViewModel
            {
                Sid = userId,
                Secret = user.SecretKey
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ConsoleViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = _repository.GetById<ApplicationUser>(userId);

            model.Sid = userId;
            model.Secret = user.SecretKey;

            if (!ModelState.IsValid) return View(model);

            using (TransactionScope transactionScope = TransactionScopeFactory.Serializable())
            {
                for (int i = 0; i < model.CodesCount; i++)
                {
                    user.Codes.Add(new Code(model.Value, model.ExpiredDate, model.Platform, model.Comment));
                }

                _repository.Save();
                transactionScope.Complete();
            }

            return RedirectToAction("Index");
        }

        public JsonResult Codes()
        {
            var userId = User.Identity.GetUserId();
            var user = _repository.GetById<ApplicationUser>(userId);

            var codes = user.Codes
                .OrderByDescending(x => x.Id)
                .Select(x => new
                {
                    Code = x.Key,
                    Value = x.Value,
                    IsActivated = x.IsActivated ? "Yes" : "No",
                    ActivationDate = x.ActivationDate?.ToShortDateString(),
                    ExpiredDate = x.ExpiredDate?.ToShortDateString(),
                    Platform = x.Platform.ToString(),
                    Comment = x.Comment
                })
                .ToList();

            var json = Json(codes, JsonRequestBehavior.AllowGet);

            return json;
        }
    }
}