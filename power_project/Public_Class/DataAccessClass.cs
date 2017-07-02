using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;


namespace power_project.Public_Class
{
    class DataAccessClass
    {

        #region  全局变量

        public static OleDbConnection Conn = null;

        //创建连接数据库的字符串
        //public static string ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + PublicCommonClass.DataPath + @"\AppData\db_test.mdb";
        public static string ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + Public_Class.PublicCommonClass.DataPath + @"\AppData\db_test.mdb";
        public static string AllSql = "Select * from tb_";
        
        #endregion

        #region  建立数据库连接

        public static OleDbConnection Open_Conn()
        {

            Conn = new OleDbConnection(ConStr);   //用SqlConnection对象与指定的数据库相连接
            Conn.Open();  //打开数据库连接
            return Conn;  //返回Connection对象的信息
        }
        #endregion



        #region  关闭数据库连接
        public void Close_Conn()
        {
            if (Conn.State == ConnectionState.Open)   //判断是否打开与数据库的连接
            {
                Conn.Close();   //关闭数据库的连接
                Conn.Dispose();   //释放My_con变量的所有空间
            }
        }
        #endregion

        #region  读取指定表中的信息

        public OleDbDataReader Read_Cmd(string SQLstr)
        {
            Open_Conn();   //打开与数据库的连接
            OleDbCommand My_cmd = new OleDbCommand();
            My_cmd.Connection = Conn;
            My_cmd.CommandText = SQLstr;    
            My_cmd.CommandType = CommandType.Text;
            OleDbDataReader My_Dr = My_cmd.ExecuteReader(); 
            return My_Dr;
        }
        #endregion

        #region 执行SqlCommand命令
  
        public void Post_Cmd(string SQLstr)
        {
            Open_Conn();   //打开与数据库的连接
            OleDbCommand SQLcom = new OleDbCommand();
            SQLcom.Connection = Conn;
            SQLcom.CommandText = SQLstr;
            SQLcom.CommandType = CommandType.Text;
            SQLcom.ExecuteNonQuery();   //执行SQL语句
            SQLcom.Dispose();   //释放所有空间
            Close_Conn();    //调用con_close()方法，关闭与数据库的连接
        }
        #endregion

        #region  创建DataSet对象
 
        public DataSet Get_DataSet(string SQLstr, string tableName)
        {
            Open_Conn();   
            OleDbDataAdapter SQLda = new OleDbDataAdapter(SQLstr, Conn);  
            DataSet My_DataSet = new DataSet(); 
            SQLda.Fill(My_DataSet, tableName);  
            Close_Conn();    
            return My_DataSet;  


        }
        #endregion
    }
}
