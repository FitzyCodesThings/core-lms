using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreLMS.Core.Entities;
using CoreLMS.Persistence;
using CoreLMS.Core.Interfaces;
using CoreLMS.Core.DataTransferObjects;

namespace CoreLMS.Web.Areas.Admin.Pages.Authors
{
    public class CreateModel : PageModel
    {
        private readonly IAuthorService authorService;

        public CreateModel(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateAuthorDto AuthorDto { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await authorService.AddAuthorAsync(AuthorDto);

            return RedirectToPage("./Index");
        }
    }
}
