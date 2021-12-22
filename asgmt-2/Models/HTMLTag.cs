using System.Text.RegularExpressions;
using System.Web;

namespace asgmt_2.Models
{
    public class HTMLTag
    {
        public HTMLTag(string tag)
        {
            RawOpen = $"<{tag}>";
            RawClose = $"</{tag}>";
            EncodedOpen = HttpUtility.HtmlEncode(RawOpen);
            EncodedClose = HttpUtility.HtmlEncode(RawClose);
            Pattern = new Regex($"{EncodedOpen}(.+){EncodedClose}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        public string EncodedOpen { get; set; }
        public string EncodedClose { get; set; }
        public string RawOpen { get; set; }
        public string RawClose { get; set; }
        public Regex Pattern { get; set; }
    }
}
