using CoreLMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreLMS.Core.Interfaces
{
    public partial interface IAppDbContext
    {
        public Task<Author> CreateAuthorAsync(Author author);
        public Task<Author> SelectAuthorByIdAsync(int id);
        public Task<List<Author>> SelectAuthorsAsync();
        public Task<Author> UpdateAuthorAsync(Author author);
        public Task<Author> DeleteAuthorAsync(Author author);
    }
}
