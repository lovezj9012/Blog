namespace Blog.WebApi.Utils.Model
{
    public class BlogNewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public int TypeId { get; set; }

        public int AuthorId { get; set; }

        public string TypeName { get; set; }

        public string AuthorName { get; set; }
    }
}
