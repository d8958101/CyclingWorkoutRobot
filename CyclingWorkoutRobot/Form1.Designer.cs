namespace CyclingWorkoutRobot
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtFTP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkRememberFTP = new System.Windows.Forms.CheckBox();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.gridWorkout = new System.Windows.Forms.DataGridView();
            this.btnUpload = new System.Windows.Forms.Button();
            this.chkShowAdv = new System.Windows.Forms.CheckBox();
            this.chkHideBrowser = new System.Windows.Forms.CheckBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtLoginId = new System.Windows.Forms.TextBox();
            this.lblIdPwd = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblSlash = new System.Windows.Forms.Label();
            this.btnSaveIdPwd = new System.Windows.Forms.Button();
            this.chkRememberIdPwd = new System.Windows.Forms.CheckBox();
            this.bwUpload = new System.ComponentModel.BackgroundWorker();
            this.timerOutput = new System.Windows.Forms.Timer(this.components);
            this.btnExampleWorkoutFileDownload = new System.Windows.Forms.Button();
            this.lblDownloadFile = new System.Windows.Forms.Label();
            this.cboDownloadFiles = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridWorkout)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFTP
            // 
            this.txtFTP.Location = new System.Drawing.Point(195, 6);
            this.txtFTP.Name = "txtFTP";
            this.txtFTP.Size = new System.Drawing.Size(100, 22);
            this.txtFTP.TabIndex = 8;
            this.txtFTP.TextChanged += new System.EventHandler(this.txtFTP_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "FTP:";
            // 
            // chkRememberFTP
            // 
            this.chkRememberFTP.AutoSize = true;
            this.chkRememberFTP.Location = new System.Drawing.Point(300, 9);
            this.chkRememberFTP.Margin = new System.Windows.Forms.Padding(2);
            this.chkRememberFTP.Name = "chkRememberFTP";
            this.chkRememberFTP.Size = new System.Drawing.Size(93, 16);
            this.chkRememberFTP.TabIndex = 9;
            this.chkRememberFTP.Text = "remember FTP";
            this.chkRememberFTP.UseVisualStyleBackColor = true;
            this.chkRememberFTP.CheckedChanged += new System.EventHandler(this.chkRememberFTP_CheckedChanged);
            // 
            // cboLanguage
            // 
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point(752, 29);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(121, 80);
            this.cboLanguage.TabIndex = 10;
            this.cboLanguage.SelectedValueChanged += new System.EventHandler(this.cboLanguage_SelectedValueChanged);
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(751, 13);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(54, 12);
            this.lblLanguage.TabIndex = 11;
            this.lblLanguage.Text = "Language:\r\n";
            this.lblLanguage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(21, 29);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(168, 40);
            this.btnOpenFile.TabIndex = 12;
            this.btnOpenFile.Text = "select workout file";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(195, 29);
            this.txtFileName.Multiline = true;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFileName.Size = new System.Drawing.Size(211, 70);
            this.txtFileName.TabIndex = 13;
            // 
            // gridWorkout
            // 
            this.gridWorkout.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridWorkout.Location = new System.Drawing.Point(9, 175);
            this.gridWorkout.Name = "gridWorkout";
            this.gridWorkout.RowHeadersWidth = 62;
            this.gridWorkout.RowTemplate.Height = 24;
            this.gridWorkout.Size = new System.Drawing.Size(857, 345);
            this.gridWorkout.TabIndex = 14;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(413, 29);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(85, 40);
            this.btnUpload.TabIndex = 15;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // chkShowAdv
            // 
            this.chkShowAdv.AutoSize = true;
            this.chkShowAdv.Location = new System.Drawing.Point(198, 122);
            this.chkShowAdv.Name = "chkShowAdv";
            this.chkShowAdv.Size = new System.Drawing.Size(139, 16);
            this.chkShowAdv.TabIndex = 16;
            this.chkShowAdv.Text = "Show Advanced Options";
            this.chkShowAdv.UseVisualStyleBackColor = true;
            this.chkShowAdv.CheckedChanged += new System.EventHandler(this.chkShowAdv_CheckedChanged);
            // 
            // chkHideBrowser
            // 
            this.chkHideBrowser.Location = new System.Drawing.Point(345, 108);
            this.chkHideBrowser.Name = "chkHideBrowser";
            this.chkHideBrowser.Size = new System.Drawing.Size(147, 43);
            this.chkHideBrowser.TabIndex = 17;
            this.chkHideBrowser.Text = "Hide Web Browser and remember id/password";
            this.chkHideBrowser.UseVisualStyleBackColor = true;
            this.chkHideBrowser.CheckedChanged += new System.EventHandler(this.chkHideBrowser_CheckedChanged);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(509, 13);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(33, 12);
            this.lblMsg.TabIndex = 18;
            this.lblMsg.Text = "label2";
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(511, 29);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(193, 64);
            this.txtOutput.TabIndex = 19;
            // 
            // txtLoginId
            // 
            this.txtLoginId.Location = new System.Drawing.Point(576, 116);
            this.txtLoginId.Name = "txtLoginId";
            this.txtLoginId.Size = new System.Drawing.Size(83, 22);
            this.txtLoginId.TabIndex = 20;
            // 
            // lblIdPwd
            // 
            this.lblIdPwd.AutoSize = true;
            this.lblIdPwd.Location = new System.Drawing.Point(512, 121);
            this.lblIdPwd.Name = "lblIdPwd";
            this.lblIdPwd.Size = new System.Drawing.Size(64, 12);
            this.lblIdPwd.TabIndex = 21;
            this.lblIdPwd.Text = "Id/Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(678, 116);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(83, 22);
            this.txtPassword.TabIndex = 22;
            // 
            // lblSlash
            // 
            this.lblSlash.AutoSize = true;
            this.lblSlash.Location = new System.Drawing.Point(664, 119);
            this.lblSlash.Name = "lblSlash";
            this.lblSlash.Size = new System.Drawing.Size(8, 12);
            this.lblSlash.TabIndex = 23;
            this.lblSlash.Text = "/";
            // 
            // btnSaveIdPwd
            // 
            this.btnSaveIdPwd.Location = new System.Drawing.Point(772, 115);
            this.btnSaveIdPwd.Name = "btnSaveIdPwd";
            this.btnSaveIdPwd.Size = new System.Drawing.Size(75, 23);
            this.btnSaveIdPwd.TabIndex = 24;
            this.btnSaveIdPwd.Text = "save";
            this.btnSaveIdPwd.UseVisualStyleBackColor = true;
            this.btnSaveIdPwd.Click += new System.EventHandler(this.btnSaveIdPwd_Click);
            // 
            // chkRememberIdPwd
            // 
            this.chkRememberIdPwd.AutoSize = true;
            this.chkRememberIdPwd.Location = new System.Drawing.Point(345, 157);
            this.chkRememberIdPwd.Name = "chkRememberIdPwd";
            this.chkRememberIdPwd.Size = new System.Drawing.Size(156, 16);
            this.chkRememberIdPwd.TabIndex = 25;
            this.chkRememberIdPwd.Text = "Only remember Id/Password";
            this.chkRememberIdPwd.UseVisualStyleBackColor = true;
            this.chkRememberIdPwd.CheckedChanged += new System.EventHandler(this.chkRememberIdPwd_CheckedChanged);
            // 
            // bwUpload
            // 
            this.bwUpload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwUpload_DoWork);
            // 
            // timerOutput
            // 
            this.timerOutput.Tick += new System.EventHandler(this.timerOutput_Tick);
            // 
            // btnExampleWorkoutFileDownload
            // 
            this.btnExampleWorkoutFileDownload.Location = new System.Drawing.Point(68, 129);
            this.btnExampleWorkoutFileDownload.Name = "btnExampleWorkoutFileDownload";
            this.btnExampleWorkoutFileDownload.Size = new System.Drawing.Size(121, 23);
            this.btnExampleWorkoutFileDownload.TabIndex = 26;
            this.btnExampleWorkoutFileDownload.Text = "Download File";
            this.btnExampleWorkoutFileDownload.UseVisualStyleBackColor = true;
            this.btnExampleWorkoutFileDownload.Click += new System.EventHandler(this.btnExampleWorkoutFileDownload_Click);
            // 
            // lblDownloadFile
            // 
            this.lblDownloadFile.Location = new System.Drawing.Point(-3, 81);
            this.lblDownloadFile.Name = "lblDownloadFile";
            this.lblDownloadFile.Size = new System.Drawing.Size(192, 20);
            this.lblDownloadFile.TabIndex = 27;
            this.lblDownloadFile.Text = "Download Example Workout File:";
            this.lblDownloadFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDownloadFiles
            // 
            this.cboDownloadFiles.FormattingEnabled = true;
            this.cboDownloadFiles.Items.AddRange(new object[] {
            "example workout",
            "202108 22 Minutes"});
            this.cboDownloadFiles.Location = new System.Drawing.Point(68, 103);
            this.cboDownloadFiles.Name = "cboDownloadFiles";
            this.cboDownloadFiles.Size = new System.Drawing.Size(121, 20);
            this.cboDownloadFiles.TabIndex = 28;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(913, 499);
            this.Controls.Add(this.cboDownloadFiles);
            this.Controls.Add(this.lblDownloadFile);
            this.Controls.Add(this.btnExampleWorkoutFileDownload);
            this.Controls.Add(this.chkRememberIdPwd);
            this.Controls.Add(this.btnSaveIdPwd);
            this.Controls.Add(this.lblSlash);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblIdPwd);
            this.Controls.Add(this.txtLoginId);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.chkHideBrowser);
            this.Controls.Add(this.chkShowAdv);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.gridWorkout);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.cboLanguage);
            this.Controls.Add(this.chkRememberFTP);
            this.Controls.Add(this.txtFTP);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "CyclingWorkoutRobot(For garmin connect)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridWorkout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFTP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkRememberFTP;
        private System.Windows.Forms.ComboBox cboLanguage;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.DataGridView gridWorkout;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.CheckBox chkShowAdv;
        private System.Windows.Forms.CheckBox chkHideBrowser;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtLoginId;
        private System.Windows.Forms.Label lblIdPwd;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblSlash;
        private System.Windows.Forms.Button btnSaveIdPwd;
        private System.Windows.Forms.CheckBox chkRememberIdPwd;
        private System.ComponentModel.BackgroundWorker bwUpload;
        private System.Windows.Forms.Timer timerOutput;
        private System.Windows.Forms.Button btnExampleWorkoutFileDownload;
        private System.Windows.Forms.Label lblDownloadFile;
        private System.Windows.Forms.ComboBox cboDownloadFiles;
    }
}

