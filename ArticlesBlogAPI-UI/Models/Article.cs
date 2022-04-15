namespace ArticlesBlogAPI_UI.Models
{
    public class Article
    {
        public int IdArticle { get; set; }

        public string? Title { get; set; }

        public int? IdAuthor { get; set; }

        public string? DatePublic { get; set; }

        public string? TextArticle { get; set; }

        public List<string>? Tags { get; set; }

        public string? UrlBanner { get; set; }

        public string? Summary { get; set; }

        public int getIdArticle()
        {
            return IdArticle;
        }

        public bool hasTag(string tagText)
        {
            if (Tags == null) return false;
            if (Tags.Contains(tagText)) return true;
            return false;
        }
    }
}