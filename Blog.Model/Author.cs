using SqlSugar;

namespace Blog.Model
{
    public class Author : BaseId
    {
        /// <summary>
        /// 作者名称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar(10)")]
        public string Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar(20)")]
        public string AccountName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar(50)")]
        public string PassWord { get; set; }
    }
}
