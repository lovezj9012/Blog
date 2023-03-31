using SqlSugar;

namespace Blog.Model
{
    public class BlogType : BaseId
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar(20)")]
        public string TypeName { get; set; }
    }
}
