namespace WinStudio.SqlCoparer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnCompare = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lbBTableCount = new System.Windows.Forms.Label();
            this.treeB = new System.Windows.Forms.TreeView();
            this.cbDbListB = new System.Windows.Forms.ComboBox();
            this.tbServerB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConnectB = new System.Windows.Forms.Button();
            this.tbPasswordB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbUsernameB = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chbAAll = new System.Windows.Forms.CheckBox();
            this.lbATableCount = new System.Windows.Forms.Label();
            this.treeA = new System.Windows.Forms.TreeView();
            this.cbDbListA = new System.Windows.Forms.ComboBox();
            this.tbServerA = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConnectA = new System.Windows.Forms.Button();
            this.tbPasswordA = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUsernameA = new System.Windows.Forms.TextBox();
            this.btnSynAtoB = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(716, 113);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 23);
            this.btnCompare.TabIndex = 2;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.lbBTableCount);
            this.groupBox2.Controls.Add(this.treeB);
            this.groupBox2.Controls.Add(this.cbDbListB);
            this.groupBox2.Controls.Add(this.tbServerB);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnConnectB);
            this.groupBox2.Controls.Add(this.tbPasswordB);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tbUsernameB);
            this.groupBox2.Location = new System.Drawing.Point(372, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(324, 470);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database B";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(70, 78);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "DBs";
            // 
            // lbBTableCount
            // 
            this.lbBTableCount.AutoSize = true;
            this.lbBTableCount.Location = new System.Drawing.Point(251, 78);
            this.lbBTableCount.Name = "lbBTableCount";
            this.lbBTableCount.Size = new System.Drawing.Size(0, 13);
            this.lbBTableCount.TabIndex = 21;
            // 
            // treeB
            // 
            this.treeB.Location = new System.Drawing.Point(19, 101);
            this.treeB.Name = "treeB";
            this.treeB.Size = new System.Drawing.Size(296, 363);
            this.treeB.TabIndex = 5;
            // 
            // cbDbListB
            // 
            this.cbDbListB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDbListB.FormattingEnabled = true;
            this.cbDbListB.Location = new System.Drawing.Point(99, 74);
            this.cbDbListB.Name = "cbDbListB";
            this.cbDbListB.Size = new System.Drawing.Size(146, 21);
            this.cbDbListB.TabIndex = 4;
            this.cbDbListB.SelectedIndexChanged += new System.EventHandler(this.cbDbListB_SelectedIndexChanged);
            // 
            // tbServerB
            // 
            this.tbServerB.Location = new System.Drawing.Point(19, 44);
            this.tbServerB.Name = "tbServerB";
            this.tbServerB.Size = new System.Drawing.Size(100, 20);
            this.tbServerB.TabIndex = 0;
            this.tbServerB.Text = ".";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Password";
            // 
            // btnConnectB
            // 
            this.btnConnectB.Location = new System.Drawing.Point(274, 43);
            this.btnConnectB.Name = "btnConnectB";
            this.btnConnectB.Size = new System.Drawing.Size(41, 23);
            this.btnConnectB.TabIndex = 3;
            this.btnConnectB.Text = "Open";
            this.btnConnectB.UseVisualStyleBackColor = true;
            this.btnConnectB.Click += new System.EventHandler(this.btnConnectB_Click);
            // 
            // tbPasswordB
            // 
            this.tbPasswordB.Location = new System.Drawing.Point(188, 44);
            this.tbPasswordB.Name = "tbPasswordB";
            this.tbPasswordB.PasswordChar = '*';
            this.tbPasswordB.Size = new System.Drawing.Size(57, 20);
            this.tbPasswordB.TabIndex = 2;
            this.tbPasswordB.Text = "sa";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Server";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(122, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Username";
            // 
            // tbUsernameB
            // 
            this.tbUsernameB.Location = new System.Drawing.Point(125, 44);
            this.tbUsernameB.Name = "tbUsernameB";
            this.tbUsernameB.Size = new System.Drawing.Size(57, 20);
            this.tbUsernameB.TabIndex = 1;
            this.tbUsernameB.Text = "sa";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.chbAAll);
            this.groupBox1.Controls.Add(this.lbATableCount);
            this.groupBox1.Controls.Add(this.treeA);
            this.groupBox1.Controls.Add(this.cbDbListA);
            this.groupBox1.Controls.Add(this.tbServerA);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnConnectA);
            this.groupBox1.Controls.Add(this.tbPasswordA);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbUsernameA);
            this.groupBox1.Location = new System.Drawing.Point(26, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 470);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database A";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(76, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "DBs";
            // 
            // chbAAll
            // 
            this.chbAAll.AutoSize = true;
            this.chbAAll.Location = new System.Drawing.Point(25, 77);
            this.chbAAll.Name = "chbAAll";
            this.chbAAll.Size = new System.Drawing.Size(37, 17);
            this.chbAAll.TabIndex = 5;
            this.chbAAll.Text = "All";
            this.chbAAll.UseVisualStyleBackColor = true;
            this.chbAAll.CheckedChanged += new System.EventHandler(this.chbAAll_CheckedChanged);
            // 
            // lbATableCount
            // 
            this.lbATableCount.AutoSize = true;
            this.lbATableCount.Location = new System.Drawing.Point(258, 78);
            this.lbATableCount.Name = "lbATableCount";
            this.lbATableCount.Size = new System.Drawing.Size(0, 13);
            this.lbATableCount.TabIndex = 9;
            // 
            // treeA
            // 
            this.treeA.CheckBoxes = true;
            this.treeA.Location = new System.Drawing.Point(22, 101);
            this.treeA.Name = "treeA";
            this.treeA.Size = new System.Drawing.Size(296, 363);
            this.treeA.TabIndex = 6;
            // 
            // cbDbListA
            // 
            this.cbDbListA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDbListA.FormattingEnabled = true;
            this.cbDbListA.Location = new System.Drawing.Point(105, 74);
            this.cbDbListA.Name = "cbDbListA";
            this.cbDbListA.Size = new System.Drawing.Size(147, 21);
            this.cbDbListA.TabIndex = 4;
            this.cbDbListA.SelectedIndexChanged += new System.EventHandler(this.cbDbListA_SelectedIndexChanged);
            // 
            // tbServerA
            // 
            this.tbServerA.Location = new System.Drawing.Point(22, 44);
            this.tbServerA.Name = "tbServerA";
            this.tbServerA.Size = new System.Drawing.Size(100, 20);
            this.tbServerA.TabIndex = 0;
            this.tbServerA.Text = ".";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password";
            // 
            // btnConnectA
            // 
            this.btnConnectA.Location = new System.Drawing.Point(277, 42);
            this.btnConnectA.Name = "btnConnectA";
            this.btnConnectA.Size = new System.Drawing.Size(41, 23);
            this.btnConnectA.TabIndex = 3;
            this.btnConnectA.Text = "Open";
            this.btnConnectA.UseVisualStyleBackColor = true;
            this.btnConnectA.Click += new System.EventHandler(this.btnConnectA_Click);
            // 
            // tbPasswordA
            // 
            this.tbPasswordA.Location = new System.Drawing.Point(195, 44);
            this.tbPasswordA.Name = "tbPasswordA";
            this.tbPasswordA.PasswordChar = '*';
            this.tbPasswordA.Size = new System.Drawing.Size(57, 20);
            this.tbPasswordA.TabIndex = 2;
            this.tbPasswordA.Text = "sa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Username";
            // 
            // tbUsernameA
            // 
            this.tbUsernameA.Location = new System.Drawing.Point(132, 44);
            this.tbUsernameA.Name = "tbUsernameA";
            this.tbUsernameA.Size = new System.Drawing.Size(57, 20);
            this.tbUsernameA.TabIndex = 1;
            this.tbUsernameA.Text = "sa";
            // 
            // btnSynAtoB
            // 
            this.btnSynAtoB.Enabled = false;
            this.btnSynAtoB.Location = new System.Drawing.Point(716, 142);
            this.btnSynAtoB.Name = "btnSynAtoB";
            this.btnSynAtoB.Size = new System.Drawing.Size(75, 23);
            this.btnSynAtoB.TabIndex = 3;
            this.btnSynAtoB.Text = "A -> B";
            this.btnSynAtoB.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(290, 488);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Different  In  Both";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(399, 488);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "A Has, B not";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(487, 488);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "B Has, A not";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(574, 488);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(155, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "[type] [lengh] [nullable] [identity]";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Red;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(156, 488);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "Different table  In  Both";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.GreenYellow;
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(27, 488);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 13);
            this.label14.TabIndex = 17;
            this.label14.Text = "Dont has in another";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 510);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSynAtoB);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sql Server 2008 Structure Comparer";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbBTableCount;
        private System.Windows.Forms.TreeView treeB;
        private System.Windows.Forms.ComboBox cbDbListB;
        private System.Windows.Forms.TextBox tbServerB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConnectB;
        private System.Windows.Forms.TextBox tbPasswordB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbUsernameB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbAAll;
        private System.Windows.Forms.Label lbATableCount;
        private System.Windows.Forms.TreeView treeA;
        private System.Windows.Forms.ComboBox cbDbListA;
        private System.Windows.Forms.TextBox tbServerA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConnectA;
        private System.Windows.Forms.TextBox tbPasswordA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUsernameA;
        private System.Windows.Forms.Button btnSynAtoB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;

    }
}

