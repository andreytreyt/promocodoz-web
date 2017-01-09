using System.Collections.Generic;

namespace Promocodoz.Web.ViewModel
{
    public class ConsoleViewModel
    {
        public ConsoleViewModel(AccountInfoViewModel accountInfo)
        {
            AccountInfo = accountInfo;
        }

        public AccountInfoViewModel AccountInfo { get; protected set; }
        public List<CodeViewModel> Codes { get; set; } = new List<CodeViewModel>();
    }
}