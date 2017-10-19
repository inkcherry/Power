using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace power_project.AppForm
{
    public partial class fmLogin : Form
    {
        power_project.Public_Class.DataAccessClass MyDataAccess = new Public_Class.DataAccessClass();

        public fmLogin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            Application.Exit();

        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                //OleDbDataReader all_data = MyDataAccess.Read_Cmd("select * from tb_privilege_list where privilege='daf'");
                //OleDbDataReader tmpDR =MyDataAccess.Read_Cmd("select * from tb_Login where us_name='" + textBox1.Text.Trim() + "' and us_pwd='" + textBox2.Text.Trim() + "'");
                OleDbDataReader tmpDR = MyDataAccess.Read_Cmd("select * from tb_oplist");


                bool ret = tmpDR.Read();    // 根据存在条件判断是否存在这种用户
               if (ret)
               {
                    Public_Class.PublicCommonClass.Login_ID = tmpDR.GetString(0);
                    Public_Class.PublicCommonClass.Login_Name = textBox1.Text.Trim();
                    Public_Class.DataAccessClass.Conn.Close();
                    Public_Class.DataAccessClass.Conn.Dispose();
                    this.Close();
                    //MessageBox.Show("登录成功");
                    //string tt = all_data.GetString(0);
                    //MessageBox.Show(tmpDR.GetString());

                }
                else
               {
                   MessageBox.Show("用户名或密码错误！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                   textBox1.Text = "";
                   textBox2.Text = "";
               }
               
            }
            else
            {
                MessageBox.Show("将信息填充完整！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }

        private void fmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                Public_Class.DataAccessClass.Open_Conn();    // 建立连接
                Public_Class.DataAccessClass.Conn.Close();   // 连接关闭
                textBox1.Text = "";
                textBox2.Text = "";
            }
            catch
            {
                MessageBox.Show("数据库连接失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
    }
}
