using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace power_project
{
    public partial class fm_Main : Form
    {
        public fm_Main()
        {
            InitializeComponent();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fm_Main_Load(object sender, EventArgs e)
        {
            Form fmLog = new power_project.AppForm.fmLogin();
            fmLog.ShowDialog();  // 将窗体显示为活动对话框
            fmLog.Dispose();     // 运行完毕会自动释放资源
            this.statusStrip1.Items[2].Text = Public_Class.PublicCommonClass.Login_Name+"  ||";


        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fm_Main_Activated(object sender, EventArgs e)
        {
            
        }

        private void 建工作票ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppForm.fmCreatTask frm = new AppForm.fmCreatTask();
            frm.ShowDialog();
        }

        private void 数据管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppForm.DBManager dbm = new AppForm.DBManager();
            dbm.ShowDialog();
        }

        private void 系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
