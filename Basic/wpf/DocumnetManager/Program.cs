using DocumnetManager.Constructor;
using DocumnetManager;
using DocumnetManager.Phone;
using System;
using System.Text;
using System.Collections;

namespace DocumnetManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Console.WriteLine("以下是泛型接口约束");

           DocumentManager <Document> manager = new DocumentManager<Document>();
            manager.Add(new Document() { Content = "Sample a", Title = "A.txt" });
            manager.Add(new Document() { Content = "Sample b", Title = "b.txt" });
            manager.Add(new Document() { Content = "Sample c", Title = "c.txt" });
            manager.Add(new Document() { Content = "Sample d", Title = "d.txt" });

            manager.DisplayAllDocumnet();
            if (manager.IsEmpty) {
                Document doc= manager.GetDocument();
                Console.WriteLine(doc.Content);
            }

            SampleDemo<string>.count = 4;
            SampleDemo<int>.count = 99;

            Console.WriteLine(SampleDemo<string>.count);



            SampleDemoDefault.count = 933;
            SampleDemoDefault.count = 2232;
            Console.WriteLine(SampleDemoDefault.count);

            Console.ReadLine();


           
            DocumnetMnagerPro<Document> managerpro = new DocumnetMnagerPro<Document>();
            managerpro.Add(new Document() { Content = "Sample A", Title = "AA.txt" });
            managerpro.Add(new Document() { Content = "Sample B", Title = "BB.txt" });
            managerpro.Add(new Document() { Content = "Sample C", Title = "CC.txt" });
            managerpro.Add(new Document() { Content = "Sample D", Title = "DD.txt" });

            managerpro.DisplayAllDocumnet();

            if (managerpro.isDocumnetAble())
            {
                Document doc = manager.GetDocument();
                Console.WriteLine(doc.Content);
            }

            Console.WriteLine("以下是泛型基类约束");
            Console.ReadLine();
            

            TelplhoneBookes<Supplier> supplierBooks = new TelplhoneBookes<Supplier>();
            supplierBooks.Add(new Supplier("0571-88994508", "联众智慧科技股份有限公司"));
            supplierBooks.Add(new Supplier("0571-88994508", "浙江联众卫生信息技术有限公司"));
            supplierBooks.Add(new Supplier("0571-88994508", "创业软件"));
            supplierBooks.Add(new Supplier("0571-88994508", "银江集团"));
            supplierBooks.Add(new Supplier("027-828994508", "成达集团"));


            supplierBooks.DisplayAllPhone();

            Console.WriteLine("以下演示电话号码查找！");
            Console.ReadLine();

            Supplier supplier= supplierBooks.GetPhoneByName("联众智慧科技股份有限公司");

            if (supplier == null)
            {
                Console.WriteLine($"未找到相应供应商电话号码！");

            }
            else {
                Console.WriteLine($"找到供应商电话 :{supplier.Number}");
            }



            Console.ReadLine();

            Fund<PostalFund> fund = new Fund<PostalFund>();


            GenericList<Employee> genericList = new GenericList<Employee>();
            genericList.AddNode(new Employee("张三", 1));
            genericList.AddNode(new Employee("李四", 2));
            genericList.AddNode(new Employee("王五", 3));
            genericList.AddNode(new Employee("陈六", 4));
            genericList.AddNode(new Employee("离七", 5));


            var  employee = genericList.GetEnumerator();
            while (employee.MoveNext()) {
                Employee current =(Employee) employee.Current;
                Console.WriteLine($"{current.Id},{current.Name}");

            }


     


            // Console.WriteLine($"{employee.Id},{employee.Name}");
            Console.ReadLine();


        
        }
    }
}
