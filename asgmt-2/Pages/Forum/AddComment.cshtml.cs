using asgmt_2.Models;
using asgmt_2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;

namespace asgmt_2.Pages
{

    [ValidateAntiForgeryToken]
    public class AddCommentModel : PageModel
    {
        private readonly IDbService _context;
        private readonly List<HTMLTag> _allowedTags;

        public AddCommentModel(IDbService context)
        {
            Comment = new();
            _context = context;

            _allowedTags = new()
            {
                new HTMLTag("b"),
                new HTMLTag("i"),
                new HTMLTag("u"),
            };
        }

        public void OnGet() { }

        [BindProperty]
        public Comment Comment { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || Comment == null || string.IsNullOrEmpty(Comment.Content))
            {
                ViewData["Error"] = "Invalid input. Please try again.";
                return Page();
            }

            Comment.Content = ParseInput(Comment.Content);
            if (string.IsNullOrEmpty(Comment.Content))
                return Page();
            _context.AddComment(Comment);

            return RedirectToPage("Index");
        }

        /// <summary>
        /// Parses input to allow tags disclosed in '_allowedTags' and HTML-encode the rest.
        /// Uses regex to ensure that all opened tags are closed, otherwise they are not decoded.
        /// </summary>
        /// <param name="content">Content to be parsed</param>
        /// <returns></returns>
        private string ParseInput(string content)
        {
            var encoded = HttpUtility.HtmlEncode(content);
            foreach (HTMLTag? tag in _allowedTags)
            {
                encoded = tag.Pattern.Replace(
                    encoded,
                    (match) => tag.RawOpen + match.Groups[1].Value + tag.RawClose
                );
            }
            return encoded;
        }
    }
}
