using MCRP.MSF.CRUD.ORM;
using MCRP.Utils.UID;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.ORM.Models.Sample;

namespace Template.ORM.EntityTypeConfigurations.Sample
{
    /// <summary>
    /// 项目需求
    /// </summary>
   public class SampleModelEntityTypeConfiguration: EntityConfigurationBase<SampleModel>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="worker"></param>
        public SampleModelEntityTypeConfiguration(UIDWorker worker) : base(worker)
        {
        }

        /// <summary>
        /// 属性配置
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<SampleModel> builder)
        {
            builder.ToTable("db_sample", "table_sample");
            builder.HasQueryFilter(o => o.ZuoFeiBZ == 0);
            base.Configure(builder);
        }
    }
}
