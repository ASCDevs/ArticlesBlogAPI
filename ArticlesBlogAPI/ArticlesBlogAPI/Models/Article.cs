namespace ArticlesBlogAPI
{
    public class Article
    {
        public int idArticle { get; set; }

        public string title { get; set; }
        
        public int idAuthor { get; set; }

        public string datePublic { get; set; }

        public string textArticle { get ;set;}

        public IEnumerable<string> tags {get;set;}

        public int getIdArticle()
        {
            return idArticle;
        }

        public bool hasTag(string tagText)
        {
            if (tags == null) return false;
            if (tags.Contains(tagText)) return true;
            return false;
        }
        
    }
}