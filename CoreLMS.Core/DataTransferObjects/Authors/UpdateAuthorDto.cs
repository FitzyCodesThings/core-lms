using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreLMS.Core.DataTransferObjects
{
    public class UpdateAuthorDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Suffix { get; set; }

        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [Phone]
        public string ContactPhoneNumber { get; set; }

        public string Description { get; set; }
        public string WebsiteURL { get; set; }
    }
}
