using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace power_project.AppForm
{
   
    public partial class fmCreatTask : Form
    { static int count1 = 3;

        public fmCreatTask()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btPost_Click(object sender, EventArgs e)
        {
            this.Close();
            fmRiskAssessment frmResk = new fmRiskAssessment();
            frmResk.ShowDialog();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fmCreatTask_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {  if (count1 == 8)
            {
                MessageBox.Show("已达到最大添加上限");
                return;
            }
            count1++;          //此时是待加入行
       
            TableLayoutPanelCellPosition Cellpos=new TableLayoutPanelCellPosition(0,count1+1);   
            layout_buttom.SetCellPosition(button_add,Cellpos);
            Cellpos.Column++;
            layout_buttom.SetCellPosition(button_remove, Cellpos);

            //加入右边的框框
            Cellpos.Row--;
            TextBox tb2 = new TextBox();
            tb2.Height = 21;
            tb2.Width = 841;
            int helpid = 2 * count1+2;
            tb2.Name = "textBox" + helpid;
            layout_buttom.Controls.Add(tb2);
            layout_buttom.SetCellPosition(tb2, Cellpos);
            //加入左边的框框
            Cellpos.Column--;
            TextBox tb = new TextBox();
            tb.Height = 21;
            tb.Width = 124;
             helpid = 2 * count1 +1;
            tb.Name = "textBox" + helpid;
            layout_buttom.Controls.Add(tb);
            
            layout_buttom.SetCellPosition(tb, Cellpos);
            
         //最终的count是当前两个末尾textbox的行数
        }

        private void button_remove_Click(object sender, EventArgs e)
        {  
            if(count1==0)
            {
                MessageBox.Show("无可删除行数");
                return;
            }
            int helpnum = count1 * 2+1;
            //删除左边textbox
            String BoxTmpName = "textBox" + helpnum;
            Control TmpButton = Controls.Find(BoxTmpName, true)[0];
            layout_buttom.Controls.Remove(TmpButton);
            //删除右边textbox
            helpnum = count1 * 2 + 2;
            BoxTmpName = "textBox" + helpnum;
             TmpButton = Controls.Find(BoxTmpName, true)[0];
            layout_buttom.Controls.Remove(TmpButton);

            count1--;           //count1成为当前标准行
            TableLayoutPanelCellPosition Cellpos = new TableLayoutPanelCellPosition(0, count1 + 1);
            layout_buttom.SetCellPosition(button_add, Cellpos);
            Cellpos.Column++;
            layout_buttom.SetCellPosition(button_remove, Cellpos);



        }
    }
}
