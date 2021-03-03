namespace GarlicBread.Helpers
{
    public static class UrlHelper
    {
        public static string CreateMarkdownUrl(string content, string url)
        {
            return $"[{content}]({url})";
        }
    }
}