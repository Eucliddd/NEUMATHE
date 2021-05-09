namespace Mathe.Models
{
    /// <summary>
    /// 问题类。
    /// </summary>
    [SQLite.Table("hm")]
    public class Problem
    {
        /// <summary>
        /// 主键。
        /// </summary>
        [SQLite.Column("id"),SQLite.PrimaryKey]
        public int id { get; set; }

        /// <summary>
        /// 问题。
        /// </summary>
        [SQLite.Column("img0")]
        public byte[] img0 { get; set; }

        /// <summary>
        /// 选项1。
        /// </summary>
        [SQLite.Column("img1")]
        public byte[] img1 { get; set; }

        /// <summary>
        /// 选项2。
        /// </summary>
        [SQLite.Column("img2")]
        public byte[] img2 { get; set; }

        /// <summary>
        /// 选项3。
        /// </summary>
        [SQLite.Column("img3")]
        public byte[] img3 { get; set; }

        /// <summary>
        /// 选项4。
        /// </summary>
        [SQLite.Column("img4")]
        public byte[] img4 { get; set; }

        /// <summary>
        /// 答案。
        /// </summary>
        [SQLite.Column("imgans")]
        public byte[] imgans { get; set; }

        /// <summary>
        /// 选哪个。
        /// </summary>
        [SQLite.Column("answer")]
        public int answer { get; set; }

        /// <summary>
        /// 章节（父节点）
        /// </summary>
        [SQLite.Column("chapter")]
        public string chapter { get; set; }

        /// <summary>
        /// 是否做过。
        /// </summary>
        [SQLite.Column("done")]
        public int done { get; set; }

        /// <summary>
        /// 是否标记。
        /// </summary>
        [SQLite.Column("mark")]
        public int mark { get; set; }
    }
}