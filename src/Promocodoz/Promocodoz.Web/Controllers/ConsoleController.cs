using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Promocodoz.Domain.Core.Entities;
using Promocodoz.Domain.Core.Enums;
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

            var accountInfoModel = new AccountInfoViewModel
            {
                Sid = userId,
                Secret = user.SecretKey
            };

            var codes = user.Codes
                .OrderByDescending(x => x.Id)
                .Select(x => new CodeViewModel
                {
                    Key = x.Key,
                    Value = x.Value,
                    IsActivated = x.IsActivated,
                    ActivationDate = x.ActivationDate,
                    Platform = x.Platform
                })
                .ToList();

            var model = new ConsoleViewModel(accountInfoModel)
            {
                Codes = codes
            };

            IEnumerable<Platform> platforms = (IEnumerable<Platform>)Enum.GetValues(typeof(Platform));

            ViewBag.Platforms = platforms;

            return View(model);
        }

        [HttpPost]
        public ActionResult GenerateCodes(int count, int value, Platform? platform = null)
        {
            var userId = User.Identity.GetUserId();
            var user = _repository.GetById<ApplicationUser>(userId);

            using (TransactionScope transactionScope = TransactionScopeFactory.Serializable())
            {
                for (int i = 0; i < count; i++)
                {
                    user.Codes.Add(new Code(value, platform));
                }

                _repository.Save();
                transactionScope.Complete();
            }

            return RedirectToAction("Index");
        }
    }
}