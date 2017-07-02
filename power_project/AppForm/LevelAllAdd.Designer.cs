namespace power_project.AppForm
{
    partial class LevelAllAdd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AddTextBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.LevelAdd = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddTextBox
            // 
            this.AddTextBox.Location = new System.Drawing.Point(12, 56);
            this.AddTextBox.MinimumSize = new System.Drawing.Size(231, 80);
            this.AddTextBox.Name = "AddTextBox";
            this.AddTextBox.Size = new System.Drawing.Size(231, 21);
            this.AddTextBox.TabIndex = 0;
            this.AddTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // LevelAdd
            // 
            this.LevelAdd.Location = new System.Drawing.Point(33, 109);
            this.LevelAdd.Name = "LevelAdd";
            this.LevelAdd.Size = new System.Drawing.Size(75, 23);
            this.LevelAdd.TabIndex = 2;
            this.LevelAdd.Text = "确定";
            this.LevelAdd.UseVisualStyleBackColor = true;
            this.LevelAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(151, 109);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // LevelAllAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 187);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.LevelAdd);
            this.Controls.Add(this.AddTextBox);
            this.Name = "LevelAllAdd";
            this.Text = "LevelAllAdd";
            this.Load += new System.EventHandler(this.LevelAllAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AddTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button LevelAdd;
        private System.Windows.Forms.Button button2;
    }
}