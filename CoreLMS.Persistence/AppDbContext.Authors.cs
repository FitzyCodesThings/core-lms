using CoreLMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreLMS.Persistence
{
    public partial class AppDbContext
    {
        public DbSet<Author> Authors { get; set; }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            EntityEntry<Author> authorEntry = await this.Authors.AddAsync(author);
            await this.SaveChangesAsync();
            return authorEntry.Entity;
        }

        public async Task<Author> SelectAuthorByIdAsync(int id) =>
            await this.Authors
                .AsNoTracking()                
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<List<Author>> SelectAuthorsAsync()
        {
            return await this.Authors                            
                            .ToListAsync();
        }

        public async Task<Author> UpdateAuthorAsync(Author author)
        {
            EntityEntry<Author> authorEntry = this.Authors.Update(author);
            await this.SaveChangesAsync();
            return authorEntry.Entity;
        }
        public async Task<Author> DeleteAuthorAsync(Author author)
        {
            EntityEntry<Author> authorEntry = this.Authors.Remove(author);
            await this.SaveChangesAsync();
            return authorEntry.Entity;
        }
    }
}
