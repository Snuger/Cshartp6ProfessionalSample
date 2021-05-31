namespace Sampke.Console.Productor.Models
{
    public class CollegeStudents : People
    {
        /// <summary>
        /// 班级名称
        /// </summary>      
        public string ClassName { get; set; }

        /// <summary>
        /// 辅导员
        /// </summary>       
        public string Counselor { get; set; }


        public override string ToString()
        {
            return $"{{className:{this.ClassName},counselor:{this.Counselor},name:{this.Name},age:{this.Age},address:{this.Address}}}";
        }
    }
}