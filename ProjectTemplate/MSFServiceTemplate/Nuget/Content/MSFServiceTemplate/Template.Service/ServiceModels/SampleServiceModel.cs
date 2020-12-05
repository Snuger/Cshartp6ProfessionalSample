namespace Template.Service.ServiceModels
{
    public class SampleServiceModel
    {
        /// <summary>
        /// 已完成
        /// </summary>
        public int WanChengCount { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 所属类型（产品、开发、体验、测试）
        /// </summary>
        public string LeiXing { get; set; }
    }
}
