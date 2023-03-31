using SqlSugar;

namespace Blog.Model
{
    public class BlogNews
    {
        /// <summary>
        /// 标题
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar(100)")]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(ColumnDataType = "text", IsNullable = true)]
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 类型ID
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int VisitCount { get; set; }

        /// <summary>
        /// 点赞量
        /// </summary>
        public int CollectCount { get; set; }

        /// <summary>
        /// 作者ID
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// 作者，IsIgnore表示该字段不映射到数据库
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Author Author { get; set; }


        [SugarColumn(IsIgnore = true)]
        public BlogType BlogType { get; set; }
    }
}
