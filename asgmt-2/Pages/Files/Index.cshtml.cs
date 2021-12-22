using asgmt_2.Models;
using asgmt_2.Services;
using asgmt_2.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using System.Web;

namespace asgmt_2.Pages.Files
{
    public class IndexModel : PageModel
    {
        private readonly IDbService _context;
        private readonly string[] _extensions = { ".jpeg", ".jpg" };
        private readonly long _sizeLimit = 10 * 1024 * 1024;

        public IndexModel(IDbService context)
        {
            _context = context;
        }

        public IActionResult OnGet(Guid id)
        {
            if (id == Guid.Empty)
                return Page();

            AppFile? appFile = _context.GetFile(id);
            if (appFile == null || appFile.Data == null || appFile.Data.Length == 0)
                return NotFound();

            return File(appFile.Data, MediaTypeNames.Application.Octet, appFile.Name + appFile.FileExtension);
        }

        [BindProperty]
        public IFormFile? FormFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Request.ContentType == null ||
                !MediaTypeHeaderValue.TryParse(Request.ContentType, out MediaTypeHeaderValue? mediaTypeHeader) ||
                string.IsNullOrEmpty(mediaTypeHeader.Boundary.Value) ||
                FormFile == null)
            {
                ViewData["Error"] = "File could not be uploaded. Please try again.";
                ModelState.AddModelError("File", "Request could not be processed.");
                return Page();
            }

            var data = await FileHelpers.ProcessFormFile(FormFile, ModelState, _extensions, _sizeLimit);
            if (data == null || data.Length == 0)
            {
                ViewData["Error"] = "File could not be uploaded. Please try again.";
                ModelState.AddModelError("File", "Request could not be processed.");
                return Page();
            }

            AppFile appFile = new();
            appFile.UnsafeName = HttpUtility.HtmlEncode(FormFile.FileName);
            appFile.FileExtension = Path.GetExtension(appFile.UnsafeName);
            appFile.Data = data;
            appFile.Size = data.Length;
            _context.UploadFile(appFile);

            return Page();
        }
    }
}
