using CoreLMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLMS.Core.Entities
{
    public class Person : IAuditableEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }        

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
