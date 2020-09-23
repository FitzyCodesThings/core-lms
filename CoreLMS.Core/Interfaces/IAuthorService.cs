using CoreLMS.Core.DataTransferObjects;
using CoreLMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreLMS.Core.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAuthorsAsync();

        Task<Author> GetAuthorAsync(int id);

        Task<Author> AddAuthorAsync(CreateAuthorDto authorDto);

        Task<Author> UpdateAuthorAsync(UpdateAuthorDto authorDto);

        Task<Author> DeleteAuthorAsync(int id);
    }
}
