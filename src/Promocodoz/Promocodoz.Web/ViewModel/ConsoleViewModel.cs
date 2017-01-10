using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Promocodoz.Domain.Core.Enums;

namespace Promocodoz.Web.ViewModel
{
    public class ConsoleViewModel
    {
        public string Sid { get; set; }
        public string Secret { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Count of codes")]
        public int CodesCount { get; set; } = 1;

        [Required]
        [Display(Name = "Value")]
        public int Value { get; set; } = 1;

        [Display(Name = "Platform")]
        public Platform? Platform { get; set; } = null;

        [StringLength(32)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public IEnumerable<Platform> Platforms => (IEnumerable<Platform>)Enum.GetValues(typeof(Platform));
    }
}