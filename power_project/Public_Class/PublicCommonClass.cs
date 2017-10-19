using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace power_project.Public_Class
{
     class PublicCommonClass
     {
         #region
         public static string Login_ID = ""; //定义全局变量，记录当前登录的用户编号
         public static string Login_Name = "";  //定义全局变量，记录当前登录的用户名
         public static int Login_n = 0;  //用户登录与重新登录的标识  
         static string path = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("\\"));
        //运行时配置
        //public static string DataPath = path + "\\" + getDer(path);
        //开发时配置
        public static string DataPath = Application.StartupPath.Substring(0, Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("\\")).LastIndexOf("\\"));


        #endregion

        public static string getDer(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            string str = "";
            foreach (DirectoryInfo dChild in dir.GetDirectories("*"))
            {
                if(dChild.Name.Length > 9 && dChild.Name.Substring(0, 10) == "powe...exe")
                {
                    str = dChild.Name;
                    break;
                }
            }
            return str;
        }

     }
}
