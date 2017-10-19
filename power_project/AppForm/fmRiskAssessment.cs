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

        private static string[][][] contentStr = new string[11][][];
        private static bool[][][] selectedStr = new bool[11][][];
        private static int nowX;
        private static int nowY;
        private static int[][] selectedLoc = new int[1000][];
        private static string[][] diyStr = new string[11][];
        private static int[] diyLoc = new int[11];

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
            //倒闸操作风险管控（危险点）分析
            string[] str = { "开关部分注意事项", "刀闸部分注意事项", "线路部分注意事项", "母线部分注意事项", "变压器部分注意事项", "继电保护部分注意事项", "操作二次设备部分注意事项", "安全工器具部分注意事项", "防误闭锁装置部分注意事项", "装设接地线（合接地刀闸）部分注意事项", "操作中的其它安全注意事项（如操作人员精神状态、气候环境）" };

            for (int i = 0,len = str.Length; i < len; i++)
            {
                TreeNode rootNode = new TreeNode("任务描述", IconIndexes.picon, IconIndexes.picon); // 创建一个TreeNode节点,加入图标
                rootNode.Tag = i;
                rootNode.Text = (i + 1).ToString() + " " + str[i];
                this.treeView2.Nodes.Add(rootNode);  // 将所创建的节点加入TreeView控件
                
                OleDbDataReader tmpDR = MyDataAccess.Read_Cmd("select * from tb_user_privilege where level1_belong = " + (i + 1) + " order by level2_id" );
                int j = 0;
                while (tmpDR.Read())
                {
                    string content = tmpDR["level2_des"].ToString();
                    TreeNode cNode = new TreeNode();
                    cNode.Tag = (i + 1).ToString() + ";" + tmpDR["level2_id"];
                    cNode.Text = tmpDR["level2_id"] + " " + content;
                    rootNode.Nodes.Add(cNode);
                    j++;
                }
                if (j == 0)
                    j = 1;
                contentStr[i] = new string[j][];
                selectedStr[i] = new bool[j][];
                if (i == 0)
                    rootNode.Expand();  // 节点自动展开
            }

            treeView2.AfterSelect += new TreeViewEventHandler(treeView2_AfterSelect);
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //当点击第一层时，更改展开的第二层列表
            if(treeView2.SelectedNode.Level == 0)
            {
                TreeNodeCollection ts = ((TreeView)sender).Nodes;

                foreach (TreeNode node in ts)
                {
                    if (node != e.Node)
                    {
                        node.Collapse();
                    }
                }
                e.Node.Expand();
                nowX = int.Parse(e.Node.Tag.ToString());
                nowY = 0;
                formList2();
            }
            else                //当点击第二层时，显示该节点的风险标识列表
            {
                string tag =e.Node.Tag.ToString();
                string[] tArr = tag.Split(';');
                nowX = int.Parse(tArr[0]) - 1;
                nowY = int.Parse(tArr[1]) - 1;
            }
            string lab3 = "选中标识" + (nowX + 1);
            label3.Text = lab3;
            checkedListBox1.Items.Clear();      // 在点击前保证所有节点为空
            if (contentStr[nowX][nowY] == null)
            {
                OleDbDataReader contentDR = MyDataAccess.Read_Cmd("select * from tb_user_privilege where level1_belong = " + (nowX + 1).ToString() + " AND level2_id = " + (nowY + 1).ToString());
                while (contentDR.Read())
                {
                    string des = contentDR["level3_des"].ToString();
                    des = des.TrimEnd(';');
                    string[] desArr = Regex.Split(des, ";;");  // 分割字符串
                    contentStr[nowX][nowY] = new string[desArr.Length];
                    selectedStr[nowX][nowY] = new bool[desArr.Length];
                    for (int i = 0; i < desArr.Length; i++)
                    {
                        checkedListBox1.Items.Add((i + 1) + " " + desArr[i]);
                        contentStr[nowX][nowY][i] = desArr[i];
                        selectedStr[nowX][nowY][i] = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < contentStr[nowX][nowY].Length; i++)
                {
                    if (selectedStr[nowX][nowY][i])
                        checkedListBox1.Items.Add((i + 1) + " " + contentStr[nowX][nowY][i], true);
                    else
                        checkedListBox1.Items.Add((i + 1) + " " + contentStr[nowX][nowY][i], false);
                }
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //获取当前选中的条目
            for(int i = 0, len = selectedStr[nowX][nowY].Length; i < len; i++)
            {
                selectedStr[nowX][nowY][i] = checkedListBox1.GetItemChecked(i);
            }

            formList2();
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int loc = 0;
            while (selectedLoc[loc] != null)
            {
                if( ! checkedListBox2.GetItemChecked(loc) )
                {
                    MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                    DialogResult dr;
                    //若为用户填写的信息，执行以下操作
                    if (selectedLoc[loc][0] == -1)
                    {
                        dr = MessageBox.Show("确定要删除该项？\n      " + diyStr[nowX][selectedLoc[loc][1]], "删除", messButton);
                        if (dr == DialogResult.OK)//如果点击“确定”按钮
                        {
                        }
                        else//如果点击“取消”按钮
                        {
                            break;
                        }
                        for(int i = selectedLoc[loc][1]; i < diyLoc[nowX]; i++)
                        {
                            diyStr[nowX][i] = diyStr[nowX][i + 1];
                        }
                        diyLoc[nowX]--;
                        break;
                    }
                    dr = MessageBox.Show("确定要删除该项？\n      " + contentStr[selectedLoc[loc][0]][selectedLoc[loc][1]][selectedLoc[loc][2]], "删除", messButton);
                    if (dr == DialogResult.OK)//如果点击“确定”按钮
                    {
                    }
                    else//如果点击“取消”按钮
                    {
                        break;
                    }
                    selectedStr[selectedLoc[loc][0]][selectedLoc[loc][1]][selectedLoc[loc][2]] = false;
                    if(selectedLoc[loc][0] == nowX && selectedLoc[loc][1] == nowY)
                    {
                        checkedListBox1.SetItemChecked(selectedLoc[loc][2], false);
                        break;
                    }
                }
                loc++;
            }

            formList2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            if(str != "")
            {
                if(diyStr[nowX] == null)
                {
                    diyStr[nowX] = new string[100];
                    diyLoc[nowX] = 0;
                }
                diyStr[nowX][diyLoc[nowX]++] = textBox1.Text;
                formList2();
                textBox1.Text = "";
            }
        }

        //生成所有选中的条目并赋入selectedLoc
        protected void formList2()
        {
            //清空列表2
            checkedListBox2.Items.Clear();
            //初始化selectedLoc
            for (int i = 0; i < 1000; i++)
            {
                selectedLoc[i] = null;
            }
            int loc = 0;
            for (int j = 0, len_j = selectedStr[nowX].Length; j < len_j; j++)
            {
                if (selectedStr[nowX][j] == null)
                {
                    continue;
                }
                for (int k = 0, len_k = selectedStr[nowX][j].Length; k < len_k; k++)
                {
                    if (selectedStr[nowX][j][k])
                    {
                        selectedLoc[loc] = new int[3];
                        selectedLoc[loc][0] = nowX;
                        selectedLoc[loc][1] = j;
                        selectedLoc[loc++][2] = k;
                        checkedListBox2.Items.Add(loc+ " " + contentStr[nowX][j][k], true);
                    }
                }
            }
            for (int i = 0; i < diyLoc[nowX]; i++)
            {
                selectedLoc[loc] = new int[2];
                selectedLoc[loc][0] = -1;
                selectedLoc[loc++][1] = i;
                checkedListBox2.Items.Add(loc + " " + diyStr[nowX][i], true);
            }
        }

        private void btPost_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("请填写所有内容");
                return;
            }

            string[][] str = new string[12][];

            for (int i = 0; i < 11; i++)
            {
                str[i] = new string[100];
                int loc = 0;
                if (selectedStr[i] == null)
                {
                    continue;
                }
                for (int j = 0, len_j = selectedStr[i].Length; j < len_j; j++)
                {
                    if (selectedStr[i][j] == null)
                    {
                        continue;
                    }
                    for (int k = 0, len_k = selectedStr[i][j].Length; k < len_k; k++)
                    {
                        if (selectedStr[i][j][k])
                        {
                            str[i][loc] = (++loc) + " " + contentStr[i][j][k];
                        }
                    }
                }
                for (int j = 0; j < diyLoc[i]; j++)
                {
                    str[i][loc] = + (++loc) + " " + diyStr[i][j];
                }
                str[i][loc] = "";
            }
            str[11] = new string[1];
            str[11][0] = "操作票票号：" + textBox3.Text + "—" + textBox5.Text + "号";
            AppForm.show sh = new AppForm.show(str);
            sh.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("请填写所有内容");
                return;
            }
            Add add = new Add();
            string folderAdd = add.folder();
            string str = Application.StartupPath.Substring(0, Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("\\")).LastIndexOf("\\"));
            Report re = new Report(str + "\\template.doc", folderAdd + "\\result.doc");
            re.SetBookMark("num1", textBox3.Text);
            re.SetBookMark("num2", textBox5.Text);
            for (int i = 0; i < 11; i++)
            {
                string res = "";
                int loc = 0;
                if (selectedStr[i] == null)
                {
                    continue;
                }
                for (int j = 0, len_j = selectedStr[i].Length; j < len_j; j++)
                {
                    if (selectedStr[i][j] == null)
                    {
                        continue;
                    }
                    for (int k = 0, len_k = selectedStr[i][j].Length; k < len_k; k++)
                    {
                        if (selectedStr[i][j][k])
                        {
                            res = res + (++loc) + " " + contentStr[i][j][k] + "\n";
                        }
                    }
                }
                for (int j = 0; j < diyLoc[i]; j++)
                {
                    res = res + (++loc) + " " + diyStr[i][j] + "\n";
                }
                res = res.TrimEnd('\n');
                re.SetBookMark("l" + (i + 1), res);
            }
            if (!re.SaveAsWord())
            {
                MessageBox.Show("请关闭当前路径下的同名文件：\n      " + folderAdd + "\\result.doc");
            }
            else
            {
                System.Diagnostics.Process.Start(folderAdd + "\\result.doc");
            }
        }
    }

    public class IconIndexes
    {
        public const int picon = 0;
    }
}
