using System;
using System.Collections.Generic;
using System.Text;

namespace Tahfeez.SharedKernal.Interfaces
{
    public interface IFullAudit
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
