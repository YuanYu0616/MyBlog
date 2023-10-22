namespace MyBlog.Models
{
    public class Article
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 文章標題
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 文章內容
        /// </summary>
        public string ArticleContent { get; set; } = string.Empty;
        public bool isDelete { get; set; }
    }
}
