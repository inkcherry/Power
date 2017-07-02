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
    public partial class DBManager : Form
    {
        public String TmpLabName ="1";   
        public String TmpLv2Name = "1";
        public String TmpLv3Name = "1";   //当前选中项
        //String Level2Color = "0";     //记录颜色变化
        //String Level3Color = "0";
        power_project.Public_Class.DataAccessClass MyDataAccess = new Public_Class.DataAccessClass();  // 创建数据库对象
        public DBManager()
        {
            InitializeComponent();
        }
        

        private void DBManager_Load(object sender, EventArgs e)
        {

        }


     // 模拟hover特效
        private void Level1_Enter(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.ForeColor = Color.Red;
        }

        private void Level1_Leave(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.ForeColor = Color.MediumBlue;
        }
       
        // endhover
        private void Level1_Click(object sender, EventArgs e)
        {
            level2_panel.Controls.Clear();
            int Level2_Rows = 0;
            Label label = (Label)sender;
            TmpLabName = label.Name.Substring(5);
            //MessageBox.Show(TmpLabName);
            OleDbDataReader tmpDR = MyDataAccess.Read_Cmd("select * from tb_user_privilege where (level1_belong =" + TmpLabName+") order by id ASC");
            while(tmpDR.Read())
            {   Level2_Rows++;
                Label lb = new Label();
                lb.Name = "Level2Lable" + Level2_Rows;
                lb.Text = TmpLabName+"-"+Level2_Rows+":"+tmpDR["level2_des"].ToString();
                
                lb.Width = 400;
                lb.Height = 35;
                lb.Click += new EventHandler(this.Level2_Click);
                lb.BackColor = Color.White;
                Point TP = new Point(0, 40 * (Level2_Rows - 1));
                lb.Location = TP;     
                level2_panel.Controls.Add(lb);
            }
        }
       public void Level2_Click(object sender, EventArgs e)
        {
            TmpLv3Name = "1";    //初始化三 防止越界
            level3_panel.Controls.Clear();         //清空布局
            Label label = (Label)sender;
            Control TmpLabForWhite = Controls.Find("Level2Lable" + TmpLv2Name, true)[0];      //清除先前设置的红色
            TmpLabForWhite.BackColor = Color.White;
            TmpLv2Name = label.Name.Substring(11);
            TmpLabForWhite = Controls.Find("Level2Lable" + TmpLv2Name, true)[0];      //清除先前设置的红色
            TmpLabForWhite.BackColor = Color.DodgerBlue ;
            OleDbDataReader tmpDR = MyDataAccess.Read_Cmd("select * from tb_user_privilege where level1_belong =" + TmpLabName + "and level2_id = " + TmpLv2Name);
            if(tmpDR.Read())               //给leve3创建label
            {   
                string content = tmpDR["level3_des"].ToString();
                string[] sArr = Regex.Split(content, ";;");
                    for(int i=0;i<sArr.Length-1;i++)
                {
                    //MessageBox.Show(sArr[i]);
                    Label lb = new Label();
                    lb.Name = "Level3Lable" + (i+1).ToString();
                    lb.Text = TmpLabName + "-" + TmpLv2Name + "-" + (i+1).ToString() + ":" + sArr[i].ToString();
                    lb.Width = 400;
                    lb.Height = 35;
                    lb.BackColor = Color.White;
                    lb.Click += new EventHandler(this.Level3_Click);
                    Point TP = new Point(0, 40 * i);
                    lb.Location = TP;
                    level3_panel.Controls.Add(lb);
                }
            }
        }
        private void Level3_Click(object sender,EventArgs e)
        {
            Control TmpLabForWhite = Controls.Find("Level3Lable" + TmpLv3Name, true)[0]; 
            TmpLabForWhite.BackColor = Color.White;
            Label lb = (Label)sender;
            lb.BackColor = Color.DodgerBlue;
            TmpLv3Name = lb.Name.Substring(11);
        }

        private void Lb_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }



        private void Level2_Add(object sender, EventArgs e)
        {
                AppForm.LevelAllAdd laa = new AppForm.LevelAllAdd(TmpLabName,TmpLv2Name,"level2add");
                laa.ShowDialog();
                Label TMPC = new Label();
                TMPC.Name= "label" + TmpLabName.ToString();  
                Level1_Click(TMPC,null);
        }

        private void Level2_Update(object sender, EventArgs e)
        {
            AppForm.LevelAllAdd laa = new AppForm.LevelAllAdd(TmpLabName, TmpLv2Name, "level2update");
            laa.ShowDialog();
            Label TMPC = new Label();
            TMPC.Name = "label" + TmpLabName.ToString();
            Level1_Click(TMPC, null);
        }

        private void Level2_De(object sender, EventArgs e)
        {
            if(MessageBox.Show("确认删除？","此删除不可恢复",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {

                OleDbDataReader tmpDR = MyDataAccess.Read_Cmd("delete from tb_user_privilege where level1_belong=" + TmpLabName + "and level2_id=" + TmpLv2Name);
                OleDbDataReader tmpDRLevel2Rows = MyDataAccess.Read_Cmd("select count(*) as level2rows from tb_user_privilege where level1_belong=" + TmpLabName);
                tmpDRLevel2Rows.Read();
               
                int st = Convert.ToInt32(TmpLv2Name) ;                       //循环启始
                int en = Convert.ToInt32(tmpDRLevel2Rows["level2rows"]);        //循环末尾
             
                for (int i=Convert.ToInt32(TmpLv2Name);i<=en;i++)   //更新以后的id   i是新id
                {
                    OleDbDataReader tmpDR2 = MyDataAccess.Read_Cmd("update tb_user_privilege set level2_id =" + i.ToString() + " where level2_id=" + (i + 1).ToString());
                    //MessageBox.Show("update tb_user_privilege set level2_id = " + i.ToString()+ " where level2_id = " + (i+1).ToString());
                }
                MessageBox.Show("成功删除:)");
                Label TMPC = new Label();
                TMPC.Name = "label" + TmpLabName.ToString();
                Level1_Click(TMPC, null);
            }
        }
    
        private void Level3_Add(object sender, EventArgs e)
        {
            AppForm.LevelAllAdd laa = new AppForm.LevelAllAdd(TmpLabName, TmpLv2Name, TmpLv3Name,"level3add");
            laa.ShowDialog();
            Label TMPC = new Label();
            TMPC.Name = "Level2Lable" + TmpLv2Name.ToString();
            Level2_Click(TMPC, null);
        }

        private void Level3_Update(object sender, EventArgs e)
        {
            AppForm.LevelAllAdd laa = new AppForm.LevelAllAdd(TmpLabName, TmpLv2Name, TmpLv3Name, "level3update");
            laa.ShowDialog();
            Label TMPC = new Label();
            TMPC.Name = "Level2Lable" + TmpLv2Name.ToString();
            Level2_Click(TMPC, null);
        }

        private void Level3_Delete(object sender, EventArgs e)
        {
            if(MessageBox.Show("确认删除","次删除不可恢复",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                    LevelAllAdd laa = new LevelAllAdd();
                    OleDbDataReader tmpDR = MyDataAccess.Read_Cmd("select *from tb_user_privilege where level1_belong= " + TmpLabName + " and level2_id=" + TmpLv2Name);
                    tmpDR.Read();
                String TmpLevel3Delete = tmpDR["level3_des"].ToString();
                    int TmpIndexst = 0;               //相应子串起始位置
                    int TmpIndexen = 0;                 //相应字串终止位置
                    //MessageBox.Show(TmpLevel3Delete);
                    //MessageBox.Show("leve3更新操作");
                    for (int i = 0; i < Convert.ToInt32(TmpLv3Name); i++)     //进行搜索相应下标
                    {
                        TmpIndexst = TmpIndexen;                      //改起始等于上一次终止
                        TmpIndexen = laa.SplitForSemiconlon(TmpLevel3Delete.ToString(), TmpIndexst) + 2;   //返回标准的带;;的  完整st~en 下标
                       
                    }
                    String TmpFinalDelete = TmpLevel3Delete.Remove(TmpIndexst, TmpIndexen - TmpIndexst);
                //MessageBox.Show("update tb_user_privilege set level3_des ='" + TmpFinalDelete + " '  where level1_belong= " + TmpLabName + " and level2_id= " + TmpLv2Name);
                    OleDbDataReader tmpDR2 = MyDataAccess.Read_Cmd("update tb_user_privilege set level3_des ='" + TmpFinalDelete + " '  where level1_belong= " + TmpLabName + " and level2_id= " + TmpLv2Name);
                MessageBox.Show("成功删除");
                Label TMPC = new Label();
                TMPC.Name = "Level2Lable" + TmpLv2Name.ToString();
                Level2_Click(TMPC, null);

            }
        }
    }
}
