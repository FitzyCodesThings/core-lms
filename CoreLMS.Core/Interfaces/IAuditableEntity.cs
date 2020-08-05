using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLMS.Core.Interfaces
{
    public interface IAuditableEntity
    {
        int Id { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        DateTime? DateDeleted { get; set; }        
    }
}
