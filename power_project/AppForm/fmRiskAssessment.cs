using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace power_project.AppForm
{
    
    public partial class fmRiskAssessment : Form
    {
        power_project.Public_Class.DataAccessClass MyDataAccess = new Public_Class.DataAccessClass();  // 创建数据库对象

        public fmRiskAssessment()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }




        // 主页面载入方法实现
        private void fmRiskAssessment_Load(object sender, EventArgs e)
        {
            int rows = 0;
            OleDbDataReader tmpDR = MyDataAccess.Read_Cmd("select * from tb_oplist where description='10kV友协线04由运行转检修'");

            TreeNode rootNode = new TreeNode("任务描述", IconIndexes.picon, IconIndexes.picon); // 创建一个TreeNode节点,加入图标
            rootNode.Tag = "工作票";
            rootNode.Text = "工作票内容";
            this.treeView2.Nodes.Add(rootNode);  // 将所创建的节点加入TreeView控件

            while (tmpDR.Read())
            {

                string content = tmpDR["content"].ToString();
                char[] charsToTrim = { ';' };   // 去尾部分割符
                content = content.TrimEnd(charsToTrim);
                string[] sArr = Regex.Split(content,";;");  // 分割字符串
                for(int i = 0; i < sArr.Length; i++)
                {
                    TreeNode cNode = new TreeNode();
                    cNode.Tag = i.ToString()+";"+tmpDR["id"].ToString();
                    cNode.Text = sArr[i];
                    rootNode.Nodes.Add(cNode);
                }
            }

            rootNode.Expand();  // 节点自动展开
            treeView2.AfterSelect += new TreeViewEventHandler(treeView2_AfterSelect);
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView2.SelectedNode.Level == 1)  // 仅当点击下层节点时触发事件
            {
                checkedListBox1.Items.Clear();      // 在点击前保证所有节点为空
                checkedListBox2.Items.Clear();

                string tag = e.Node.Tag.ToString();
                string[] tArr = tag.Split(';');
                string sn = tArr[0];
                string id = tArr[1];
                //OleDbDataReader tmpDR = MyDataAccess.Read_Cmd("select * from tb_oplist");
                OleDbDataReader contentDR = MyDataAccess.Read_Cmd("select * from tb_opcontent where id ='"+id.ToString()+"'");
                while (contentDR.Read())
                {
                    string des = contentDR["风险标识"].ToString();
                    char[] charsToTrim1 = { '#' };
                    char[] charsToTrim2 = { ';' };
                    des = des.TrimEnd(charsToTrim1);
                    string[] desArr = Regex.Split(des, "##");  // 分割字符串
                    for (int i = 0; i < desArr.Length; i++)
                    {
                        string cItem;
                        if (i == int.Parse(sn))
                        {
                            cItem = desArr[i];
                            cItem = cItem.TrimEnd(charsToTrim2);
                            string[] chArr = Regex.Split(cItem, ";;");
                            foreach (string item in chArr)
                            {
                                checkedListBox1.Items.Add(item);
                            }
                        }
                    }


                    string des1 = contentDR["控制措施"].ToString();
                    des1 = des1.TrimEnd(charsToTrim1);
                    string[] desArr1 = Regex.Split(des1, "##");  // 分割字符串
                    for (int i = 0; i < desArr1.Length; i++)
                    {
                        string cItem;
                        if (i == int.Parse(sn))
                        {
                            cItem = desArr1[i];
                            cItem = cItem.TrimEnd(charsToTrim2);
                            string[] chArr = Regex.Split(cItem, ";;");
                            foreach (string item in chArr)
                            {
                                checkedListBox2.Items.Add(item);
                            }
                        }
                    }
                }
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void treeView2_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
           
        }
    }



    public class IconIndexes
    {
        public const int picon = 0;
    }
}
