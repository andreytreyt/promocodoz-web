using System;
using System.ComponentModel.DataAnnotations;
using Promocodoz.Domain.Core.Enums;

namespace Promocodoz.Domain.Core.Entities
{
    public class Code : IdentifiableEntity<int>
    {
        protected Code()
        { }

        public Code(int value, Platform platform, string comment = null)
        {
            var guid = Guid.NewGuid().ToString().ToUpper().Split('-');
            var key = $"{guid[1]}-{guid[2]}-{guid[3]}";

            Key = key;
            Value = value;
            IsActivated = false;
            Platform = platform;
            Comment = comment;
        }

        [Required]
        public string Key { get; protected set; }
        public int Value { get; protected set; }
        public bool IsActivated { get; protected set; }
        public DateTime? ActivationDate { get; protected set; }
        public Platform Platform { get; protected set; }
        [StringLength(32)]
        public string Comment { get; protected set; }

        public virtual ApplicationUser User { get; protected set; }

        public Code Activate()
        {
            IsActivated = true;
            ActivationDate = DateTime.UtcNow;
            return this;
        }
    }
}
