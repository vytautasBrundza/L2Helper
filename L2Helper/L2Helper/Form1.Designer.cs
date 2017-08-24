namespace L2Helper
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
            this.MainControl = new System.Windows.Forms.TabControl();
            this.Tab1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.classDropdown = new System.Windows.Forms.ComboBox();
            this.mainCheckBox = new System.Windows.Forms.CheckBox();
            this.THPBar = new System.Windows.Forms.ProgressBar();
            this.HPBar = new System.Windows.Forms.ProgressBar();
            this.THPlabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MPBar = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.HPlabel = new System.Windows.Forms.Label();
            this.MPlabel = new System.Windows.Forms.Label();
            this.statCheckBox = new System.Windows.Forms.CheckBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.AItextBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.MainControl.SuspendLayout();
            this.Tab1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainControl
            // 
            this.MainControl.Controls.Add(this.Tab1);
            this.MainControl.Location = new System.Drawing.Point(-2, 1);
            this.MainControl.Name = "MainControl";
            this.MainControl.SelectedIndex = 0;
            this.MainControl.Size = new System.Drawing.Size(269, 468);
            this.MainControl.TabIndex = 0;
            // 
            // Tab1
            // 
            this.Tab1.Controls.Add(this.groupBox1);
            this.Tab1.Controls.Add(this.statCheckBox);
            this.Tab1.Controls.Add(this.button6);
            this.Tab1.Controls.Add(this.button7);
            this.Tab1.Controls.Add(this.button5);
            this.Tab1.Controls.Add(this.button1);
            this.Tab1.Controls.Add(this.AItextBox);
            this.Tab1.Controls.Add(this.button2);
            this.Tab1.Controls.Add(this.listBox1);
            this.Tab1.Controls.Add(this.button4);
            this.Tab1.Controls.Add(this.button3);
            this.Tab1.Location = new System.Drawing.Point(4, 22);
            this.Tab1.Name = "Tab1";
            this.Tab1.Padding = new System.Windows.Forms.Padding(3);
            this.Tab1.Size = new System.Drawing.Size(261, 442);
            this.Tab1.TabIndex = 0;
            this.Tab1.Text = "Main";
            this.Tab1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.classDropdown);
            this.groupBox1.Controls.Add(this.mainCheckBox);
            this.groupBox1.Controls.Add(this.THPBar);
            this.groupBox1.Controls.Add(this.HPBar);
            this.groupBox1.Controls.Add(this.THPlabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.MPBar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.HPlabel);
            this.groupBox1.Controls.Add(this.MPlabel);
            this.groupBox1.Location = new System.Drawing.Point(0, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 165);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Character";
            // 
            // classDropdown
            // 
            this.classDropdown.Enabled = false;
            this.classDropdown.FormattingEnabled = true;
            this.classDropdown.Location = new System.Drawing.Point(7, 93);
            this.classDropdown.Name = "classDropdown";
            this.classDropdown.Size = new System.Drawing.Size(141, 21);
            this.classDropdown.TabIndex = 19;
            this.classDropdown.SelectedIndexChanged += new System.EventHandler(this.classSelected);
            // 
            // mainCheckBox
            // 
            this.mainCheckBox.AutoSize = true;
            this.mainCheckBox.Location = new System.Drawing.Point(154, 93);
            this.mainCheckBox.Name = "mainCheckBox";
            this.mainCheckBox.Size = new System.Drawing.Size(49, 17);
            this.mainCheckBox.TabIndex = 18;
            this.mainCheckBox.Text = "Main";
            this.mainCheckBox.UseVisualStyleBackColor = true;
            this.mainCheckBox.CheckedChanged += new System.EventHandler(this.CharMainToggle);
            // 
            // THPBar
            // 
            this.THPBar.BackColor = System.Drawing.Color.DarkOrange;
            this.THPBar.ForeColor = System.Drawing.Color.OrangeRed;
            this.THPBar.Location = new System.Drawing.Point(42, 19);
            this.THPBar.Name = "THPBar";
            this.THPBar.Size = new System.Drawing.Size(109, 15);
            this.THPBar.TabIndex = 12;
            // 
            // HPBar
            // 
            this.HPBar.BackColor = System.Drawing.Color.Red;
            this.HPBar.ForeColor = System.Drawing.Color.Red;
            this.HPBar.Location = new System.Drawing.Point(33, 40);
            this.HPBar.Name = "HPBar";
            this.HPBar.Size = new System.Drawing.Size(118, 14);
            this.HPBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.HPBar.TabIndex = 0;
            // 
            // THPlabel
            // 
            this.THPlabel.AutoSize = true;
            this.THPlabel.Location = new System.Drawing.Point(157, 19);
            this.THPlabel.Name = "THPlabel";
            this.THPlabel.Size = new System.Drawing.Size(51, 13);
            this.THPlabel.TabIndex = 14;
            this.THPlabel.Text = "HP/MHP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "HP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "T HP";
            // 
            // MPBar
            // 
            this.MPBar.BackColor = System.Drawing.Color.RoyalBlue;
            this.MPBar.ForeColor = System.Drawing.Color.RoyalBlue;
            this.MPBar.Location = new System.Drawing.Point(33, 60);
            this.MPBar.Name = "MPBar";
            this.MPBar.Size = new System.Drawing.Size(118, 14);
            this.MPBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.MPBar.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "MP";
            // 
            // HPlabel
            // 
            this.HPlabel.AutoSize = true;
            this.HPlabel.Location = new System.Drawing.Point(157, 40);
            this.HPlabel.Name = "HPlabel";
            this.HPlabel.Size = new System.Drawing.Size(64, 13);
            this.HPlabel.TabIndex = 4;
            this.HPlabel.Text = "HP/MHP/%";
            // 
            // MPlabel
            // 
            this.MPlabel.AutoSize = true;
            this.MPlabel.Location = new System.Drawing.Point(157, 61);
            this.MPlabel.Name = "MPlabel";
            this.MPlabel.Size = new System.Drawing.Size(66, 13);
            this.MPlabel.TabIndex = 5;
            this.MPlabel.Text = "MP/MMP/%";
            // 
            // statCheckBox
            // 
            this.statCheckBox.AutoSize = true;
            this.statCheckBox.Location = new System.Drawing.Point(157, 6);
            this.statCheckBox.Name = "statCheckBox";
            this.statCheckBox.Size = new System.Drawing.Size(96, 17);
            this.statCheckBox.TabIndex = 19;
            this.statCheckBox.Text = "Do Stat Check";
            this.statCheckBox.UseVisualStyleBackColor = true;
            this.statCheckBox.CheckStateChanged += new System.EventHandler(this.DoStatCheckToggle);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button6.Location = new System.Drawing.Point(170, 55);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(77, 24);
            this.button6.TabIndex = 17;
            this.button6.Text = "AI stop All";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.YellowGreen;
            this.button7.Location = new System.Drawing.Point(82, 55);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(77, 24);
            this.button7.TabIndex = 16;
            this.button7.Text = "AI start All";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(82, 29);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(121, 20);
            this.button5.TabIndex = 15;
            this.button5.Text = "RunOnMain";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(209, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 20);
            this.button1.TabIndex = 11;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AItextBox
            // 
            this.AItextBox.Location = new System.Drawing.Point(13, 286);
            this.AItextBox.Multiline = true;
            this.AItextBox.Name = "AItextBox";
            this.AItextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AItextBox.Size = new System.Drawing.Size(241, 127);
            this.AItextBox.TabIndex = 10;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(82, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 20);
            this.button2.TabIndex = 2;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ClickRefresh);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(9, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(67, 108);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.LightPink;
            this.button4.Location = new System.Drawing.Point(170, 85);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(77, 24);
            this.button4.TabIndex = 9;
            this.button4.Text = "AI stop";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LightGreen;
            this.button3.Location = new System.Drawing.Point(82, 85);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 24);
            this.button3.TabIndex = 8;
            this.button3.Text = "AI start";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 470);
            this.Controls.Add(this.MainControl);
            this.Name = "Form1";
            this.Text = "L2 Helper";
            this.MainControl.ResumeLayout(false);
            this.Tab1.ResumeLayout(false);
            this.Tab1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage Tab1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar HPBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar MPBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label MPlabel;
        private System.Windows.Forms.Label HPlabel;
        private System.Windows.Forms.TabControl MainControl;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.TextBox AItextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar THPBar;
        private System.Windows.Forms.Label THPlabel;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.CheckBox mainCheckBox;
        private System.Windows.Forms.CheckBox statCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox classDropdown;
    }
}

