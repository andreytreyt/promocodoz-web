using System;
using Promocodoz.Domain.Core.Enums;

namespace Promocodoz.Web.ViewModel
{
    public class CodeViewModel
    {
        public string Key { get; set; }
        public int Value { get; set; }
        public bool IsActivated { get; set; }
        public DateTime? ActivationDate { get; set; }
        public Platform? Platform { get; set; }
    }
}