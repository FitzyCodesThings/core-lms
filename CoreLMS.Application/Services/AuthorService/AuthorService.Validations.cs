using CoreLMS.Application.Validators;
using CoreLMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLMS.Application.Services
{
    public partial class AuthorService
    {
        private void ValidateAuthorOnCreate(Author author)
        {
            ModelValidator.ValidateModel(author);
        }

        private void ValidateAuthorOnUpdate(Author author)
        {         
            ModelValidator.ValidateModel(author);         
        }
    }
}
