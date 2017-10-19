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
    public partial class show : Form
    {
        public show(string[][] str)
        {
            InitializeComponent();
            label1.Text = ope(str[0]);
            label2.Text = ope(str[1]);
            label3.Text = ope(str[2]);
            label4.Text = ope(str[3]);
            label5.Text = ope(str[4]);
            label6.Text = ope(str[5]);
            label7.Text = ope(str[6]);
            label8.Text = ope(str[7]);
            label9.Text = ope(str[8]);
            label10.Text = ope(str[9]);
            label11.Text = ope(str[10]);
            label1.Height = str[0].Length * 20;
            label2.Height = str[1].Length * 20;
            label3.Height = str[2].Length * 20;
            label4.Height = str[3].Length * 20;
            label5.Height = str[4].Length * 20;
            label6.Height = str[5].Length * 20;
            label7.Height = str[6].Length * 20;
            label8.Height = str[7].Length * 20;
            label9.Height = str[8].Length * 20;
            label10.Height = str[9].Length * 20;
            label11.Height = str[10].Length * 20;
            num1.Text = str[11][0];
        }

        private void show_Load(object sender, EventArgs e)
        {

        }

        private string ope(string[] str)
        {
            string res = "";
            int flag = 0;
            for(int i = 0; str[i] != ""; i++)
            {
                res = res + str[i] + "\n";
                if (flag > 0)
                {
                    flag++;
                }
                if (str[i].Length > 74)
                    flag = 1;
            }
            if(flag > 1)
            {
                res = res + "-防止显示不全而占位，请无视-";
            }
            return res;
        }
    }
}
