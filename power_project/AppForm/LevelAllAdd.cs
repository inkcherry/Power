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
   
    public partial class LevelAllAdd : Form
    {
        power_project.Public_Class.DataAccessClass MyDataAccess = new Public_Class.DataAccessClass();
        String level1="0";
        String level2="0";
        String level3="0";
        String level3operate = "";
        public LevelAllAdd()
        {
            InitializeComponent();
        }
        public LevelAllAdd(String level1,String level2,String level3)
        {
            this.level1 = level1;
            this.level2 = level2;
            this.level3 = level3;
            InitializeComponent();
        }
        public LevelAllAdd(String level1, String level2, String level3,String level3operate)
        {
            this.level1 = level1;
            this.level2 = level2;
            this.level3 = level3;
            this.level3operate = level3operate;
            InitializeComponent();
        }
        public int SplitForSemiconlon(String str, int IndexSemiconlonOld)
        {
          
           int IndexSemiconlonNew = str.IndexOf(";;", IndexSemiconlonOld );   //寻找;;的下标位置
            return IndexSemiconlonNew;
        }
        private void LevelAllAdd_Load(object sender, EventArgs e)
        { if (level3.Equals("level2update"))
            {
                OleDbDataReader tmpDRForShow = MyDataAccess.Read_Cmd("select *from tb_user_privilege where level1_belong= " + level1 + " and level2_id=" + level2);
                tmpDRForShow.Read();
                AddTextBox.Text = tmpDRForShow["level2_des"].ToString();
            }
          else if(level3operate.Equals("level3update"))
            {
                OleDbDataReader tmpDRForShow = MyDataAccess.Read_Cmd("select *from tb_user_privilege where level1_belong= " + level1 + " and level2_id=" + level2);
                tmpDRForShow.Read();
                string content = tmpDRForShow["level3_des"].ToString();
                string[] sArr = Regex.Split(content, ";;");
                AddTextBox.Text = sArr[Convert.ToInt32(level3)-1].ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        { if(level3.Equals("level2add"))             //添加level2条
            {
                OleDbDataReader tmpDRForCount = MyDataAccess.Read_Cmd("select count(level2_id) as level2row from tb_user_privilege where level1_belong=" + level1);   //统计条数 编号level2_id
                tmpDRForCount.Read();
                String Level2Rows = tmpDRForCount["level2row"].ToString();
                int ConveyNum = Convert.ToInt32(Level2Rows)+1;   //将string类型转换为int类型
                Level2Rows = ConveyNum.ToString();
                //MessageBox.Show(Level2Rows);
                //MessageBox.Show(level1);
                //MessageBox.Show(AddTextBox.Text);
                OleDbDataReader tmpDR = MyDataAccess.Read_Cmd("insert into tb_user_privilege(level1_belong,level2_des,level2_id) values (" + level1 + ",'" + AddTextBox.Text + "',"+Level2Rows+")");

                MessageBox.Show("添加成功:)");
                this.Close();
            }
          else if(level3.Equals("level2update"))    //更新level2条
            {
                //MessageBox.Show(level1);
                //MessageBox.Show(level2);
                //MessageBox.Show(AddTextBox.Text);
                //MessageBox.Show("update tb_user_privilege set level2_des='" + AddTextBox.Text + "' where (level1_belong = " + level1 + " and level2_id = " + level2 + ")");
              

                OleDbDataReader tmpDR = MyDataAccess.Read_Cmd("update tb_user_privilege set level2_des='"+AddTextBox.Text+"' where (level1_belong = "+level1+" and level2_id = "+level2+")");
                MessageBox.Show("信息更新成功");
                this.Close();
            }
          else                                      //level3操作
            {
                //MessageBox.Show(level3operate);          //先读取level3des
                OleDbDataReader tmpDR = MyDataAccess.Read_Cmd("select *from tb_user_privilege where level1_belong= "+level1+" and level2_id="+level2);
                tmpDR.Read();
                  if (level3operate.Equals("level3add"))   //level3增加操作
                  {
                    
    
                    String TmpLevel3DesForAdd = tmpDR["level3_des"].ToString()+AddTextBox.Text+";;";
                    //MessageBox.Show("修改后的信息" + TmpLevel3DesForAdd);
                    OleDbDataReader tmpDR2 = MyDataAccess.Read_Cmd("update tb_user_privilege set level3_des ='"+TmpLevel3DesForAdd+" '  where level1_belong= "+level1+" and level2_id= "+level2);
                    MessageBox.Show("信息添加成功");
                    this.Close();
                   }
                  else  if (level3operate.Equals("level3update"))        //level3更新操作
                {
                    
                    String TmpLevel3Update = tmpDR["level3_des"].ToString();
                    int TmpIndexst = 0;               //相应子串起始位置
                    int TmpIndexen = 0;                 //相应字串终止位置
                    //MessageBox.Show(TmpLevel3Update);
                    //MessageBox.Show("leve3更新操作"); 
                    for(int i=0;i<Convert.ToInt32(level3);i++)     //进行搜索相应下标
                    {
                        TmpIndexst = TmpIndexen;                   //改起始等于上一次终止
                        TmpIndexen = SplitForSemiconlon(TmpLevel3Update.ToString(),TmpIndexst)+2;   //返回标准的带;;的  完整st~en 下标
                        //MessageBox.Show(TmpIndexst.ToString() + " " + TmpIndexen.ToString());
                        //MessageBox.Show(tmpDR["level3_des"].ToString().Substring(TmpIndexst,TmpIndexen-TmpIndexst));
                    }
                    String TmpForReplace = TmpLevel3Update.Substring(TmpIndexst, TmpIndexen - TmpIndexst);
                    //MessageBox.Show(TmpForReplace);
                    String TmpFinalUpdate = TmpLevel3Update.Replace(TmpForReplace, AddTextBox.Text+";;");
                    //MessageBox.Show("Final" + TmpFinalUpdate);
                    OleDbDataReader tmpDR2 = MyDataAccess.Read_Cmd("update tb_user_privilege set level3_des ='" + TmpFinalUpdate + " '  where level1_belong= " + level1 + " and level2_id= " + level2);
                    //TmpLevel3Update.Remove(TmpIndexst, TmpIndexen - TmpIndexst);
                    MessageBox.Show("信息更新成功");
                    this.Close();
                }
           
            }
            return;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
