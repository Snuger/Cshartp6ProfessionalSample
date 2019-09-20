using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmSamples.Contract;
using MvvmSamples.Model;

namespace MvvmSamples.Design
{
    public class MenuService : IMenuService
    {
        public void GetMenu(Action<FunctionMenu, Exception> callback)
        {
            FunctionMenu menu = new FunctionMenu()
            {
                MenuRootCode = "root",
                MenuRootName = "数据上传管理",
                Modules = new List<FunctionalModule>() {
                      new FunctionalModule(){
                           ModuleCode="1",
                           ModuleName="系统管理",
                            FunctionPoints=new List<FunctionPoint>(){
                                 new FunctionPoint(){ FunctionCode="11" ,FunctionName="用户管理"},
                                 new FunctionPoint(){ FunctionCode="12" ,FunctionName="功能管理"},
                                 new FunctionPoint(){ FunctionCode="13" ,FunctionName="权限管理"},
                                 new FunctionPoint(){ FunctionCode="14" ,FunctionName="角色管理"}
                            }
                      }
                }
            };
            callback(menu, null);             
        }
    }
}
