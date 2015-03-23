
namespace Blackbox.ViewModel
{
    public class HelpDataItem
    {
        public string Title { get; set; }
        public string Illustration { get; set; }
        public string Details { get; set; }

        public HelpDataItem()
        {
        }

        public HelpDataItem(string title, string details)
        {
            Title = title;
            Details = details;
        }

        public HelpDataItem(string title, string illustration, string details)
            : this(title, details)
        {
            Illustration = illustration;
        }
    }
}
