namespace Shutdauwn
{
    partial class ShutdauwnForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShutdauwnForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.vlcStatusLabel = new System.Windows.Forms.Label();
            this.monitorVlcButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.hoursUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.minutesUpDown = new System.Windows.Forms.NumericUpDown();
            this.timerStatusLabel = new System.Windows.Forms.Label();
            this.timerButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.minimizeCheckBox = new System.Windows.Forms.CheckBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.philipsHueIpTextBox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.philipsHueUsernameTextBox = new System.Windows.Forms.TextBox();
            this.turnOffCheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hoursUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minutesUpDown)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(220, 162);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.vlcStatusLabel);
            this.tabPage1.Controls.Add(this.monitorVlcButton);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(212, 136);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "VLC";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // vlcStatusLabel
            // 
            this.vlcStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vlcStatusLabel.Location = new System.Drawing.Point(8, 67);
            this.vlcStatusLabel.Name = "vlcStatusLabel";
            this.vlcStatusLabel.Size = new System.Drawing.Size(196, 13);
            this.vlcStatusLabel.TabIndex = 2;
            this.vlcStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // monitorVlcButton
            // 
            this.monitorVlcButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.monitorVlcButton.Location = new System.Drawing.Point(8, 41);
            this.monitorVlcButton.Name = "monitorVlcButton";
            this.monitorVlcButton.Size = new System.Drawing.Size(196, 23);
            this.monitorVlcButton.TabIndex = 1;
            this.monitorVlcButton.Text = "Start monitoring VLC";
            this.monitorVlcButton.UseVisualStyleBackColor = true;
            this.monitorVlcButton.Click += new System.EventHandler(this.monitorVlcButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Monitor VLC to shut down your pc\r\nwhen your media stops.";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.timerStatusLabel);
            this.tabPage2.Controls.Add(this.timerButton);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(212, 136);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Timer";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.hoursUpDown);
            this.groupBox2.Location = new System.Drawing.Point(109, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(65, 49);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "hours";
            // 
            // hoursUpDown
            // 
            this.hoursUpDown.Location = new System.Drawing.Point(6, 19);
            this.hoursUpDown.Name = "hoursUpDown";
            this.hoursUpDown.Size = new System.Drawing.Size(53, 20);
            this.hoursUpDown.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.minutesUpDown);
            this.groupBox1.Location = new System.Drawing.Point(38, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(65, 49);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "minutes";
            // 
            // minutesUpDown
            // 
            this.minutesUpDown.Location = new System.Drawing.Point(6, 19);
            this.minutesUpDown.Name = "minutesUpDown";
            this.minutesUpDown.Size = new System.Drawing.Size(53, 20);
            this.minutesUpDown.TabIndex = 1;
            // 
            // timerStatusLabel
            // 
            this.timerStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timerStatusLabel.Location = new System.Drawing.Point(8, 109);
            this.timerStatusLabel.Name = "timerStatusLabel";
            this.timerStatusLabel.Size = new System.Drawing.Size(196, 13);
            this.timerStatusLabel.TabIndex = 2;
            this.timerStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerButton
            // 
            this.timerButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timerButton.Location = new System.Drawing.Point(8, 83);
            this.timerButton.Name = "timerButton";
            this.timerButton.Size = new System.Drawing.Size(196, 23);
            this.timerButton.TabIndex = 3;
            this.timerButton.Text = "Start timer";
            this.timerButton.UseVisualStyleBackColor = true;
            this.timerButton.Click += new System.EventHandler(this.timerButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Shutdown in:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.turnOffCheckBox);
            this.tabPage3.Controls.Add(this.minimizeCheckBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(212, 136);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // minimizeCheckBox
            // 
            this.minimizeCheckBox.AutoSize = true;
            this.minimizeCheckBox.Location = new System.Drawing.Point(3, 12);
            this.minimizeCheckBox.Name = "minimizeCheckBox";
            this.minimizeCheckBox.Size = new System.Drawing.Size(98, 17);
            this.minimizeCheckBox.TabIndex = 0;
            this.minimizeCheckBox.Text = "Minimize to tray";
            this.minimizeCheckBox.UseVisualStyleBackColor = true;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Click to open";
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Controls.Add(this.groupBox3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(212, 136);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "Philips Hue";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.philipsHueIpTextBox);
            this.groupBox3.Location = new System.Drawing.Point(3, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(206, 46);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Philips Hue IP";
            // 
            // philipsHueIpTextBox
            // 
            this.philipsHueIpTextBox.Location = new System.Drawing.Point(6, 19);
            this.philipsHueIpTextBox.Name = "philipsHueIpTextBox";
            this.philipsHueIpTextBox.Size = new System.Drawing.Size(194, 20);
            this.philipsHueIpTextBox.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.philipsHueUsernameTextBox);
            this.groupBox4.Location = new System.Drawing.Point(3, 64);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(206, 46);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Philips Hue username";
            // 
            // philipsHueUsernameTextBox
            // 
            this.philipsHueUsernameTextBox.Location = new System.Drawing.Point(6, 19);
            this.philipsHueUsernameTextBox.Name = "philipsHueUsernameTextBox";
            this.philipsHueUsernameTextBox.Size = new System.Drawing.Size(194, 20);
            this.philipsHueUsernameTextBox.TabIndex = 0;
            // 
            // turnOffCheckBox
            // 
            this.turnOffCheckBox.AutoSize = true;
            this.turnOffCheckBox.Location = new System.Drawing.Point(3, 36);
            this.turnOffCheckBox.Name = "turnOffCheckBox";
            this.turnOffCheckBox.Size = new System.Drawing.Size(183, 17);
            this.turnOffCheckBox.TabIndex = 1;
            this.turnOffCheckBox.Text = "Turn off Philips Hue on shutdown";
            this.turnOffCheckBox.UseVisualStyleBackColor = true;
            // 
            // ShutdauwnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 162);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ShutdauwnForm";
            this.Text = "Shutdauwn";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShutdauwnForm_FormClosing);
            this.Resize += new System.EventHandler(this.ShutdauwnForm_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hoursUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.minutesUpDown)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button monitorVlcButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label vlcStatusLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button timerButton;
        private System.Windows.Forms.Label timerStatusLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown hoursUpDown;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown minutesUpDown;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox minimizeCheckBox;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox philipsHueUsernameTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox philipsHueIpTextBox;
        private System.Windows.Forms.CheckBox turnOffCheckBox;

    }
}

