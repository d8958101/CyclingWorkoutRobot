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
            ((System.ComponentModel.ISupportInitialize)(this.gridWorkout)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFTP
            // 
            this.txtFTP.Location = new System.Drawing.Point(212, 6);
            this.txtFTP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFTP.Name = "txtFTP";
            this.txtFTP.Size = new System.Drawing.Size(148, 29);
            this.txtFTP.TabIndex = 8;
            this.txtFTP.TextChanged += new System.EventHandler(this.txtFTP_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "FTP:";
            // 
            // chkRememberFTP
            // 
            this.chkRememberFTP.AutoSize = true;
            this.chkRememberFTP.Location = new System.Drawing.Point(381, 10);
            this.chkRememberFTP.Name = "chkRememberFTP";
            this.chkRememberFTP.Size = new System.Drawing.Size(137, 22);
            this.chkRememberFTP.TabIndex = 9;
            this.chkRememberFTP.Text = "remember FTP";
            this.chkRememberFTP.UseVisualStyleBackColor = true;
            this.chkRememberFTP.CheckedChanged += new System.EventHandler(this.chkRememberFTP_CheckedChanged);
            // 
            // cboLanguage
            // 
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point(1110, 40);
            this.cboLanguage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(180, 118);
            this.cboLanguage.TabIndex = 10;
            this.cboLanguage.SelectedValueChanged += new System.EventHandler(this.cboLanguage_SelectedValueChanged);
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(1108, 16);
            this.lblLanguage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(79, 18);
            this.lblLanguage.TabIndex = 11;
            this.lblLanguage.Text = "Language:\r\n";
            this.lblLanguage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(14, 40);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(190, 60);
            this.btnOpenFile.TabIndex = 12;
            this.btnOpenFile.Text = "select workout file";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(212, 40);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFileName.Multiline = true;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFileName.Size = new System.Drawing.Size(378, 103);
            this.txtFileName.TabIndex = 13;
            // 
            // gridWorkout
            // 
            this.gridWorkout.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridWorkout.Location = new System.Drawing.Point(14, 262);
            this.gridWorkout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridWorkout.Name = "gridWorkout";
            this.gridWorkout.RowHeadersWidth = 62;
            this.gridWorkout.RowTemplate.Height = 24;
            this.gridWorkout.Size = new System.Drawing.Size(1286, 518);
            this.gridWorkout.TabIndex = 14;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(602, 40);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(128, 60);
            this.btnUpload.TabIndex = 15;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // chkShowAdv
            // 
            this.chkShowAdv.AutoSize = true;
            this.chkShowAdv.Location = new System.Drawing.Point(213, 162);
            this.chkShowAdv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkShowAdv.Name = "chkShowAdv";
            this.chkShowAdv.Size = new System.Drawing.Size(202, 22);
            this.chkShowAdv.TabIndex = 16;
            this.chkShowAdv.Text = "Show Advanced Options";
            this.chkShowAdv.UseVisualStyleBackColor = true;
            this.chkShowAdv.CheckedChanged += new System.EventHandler(this.chkShowAdv_CheckedChanged);
            // 
            // chkHideBrowser
            // 
            this.chkHideBrowser.Location = new System.Drawing.Point(495, 154);
            this.chkHideBrowser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkHideBrowser.Name = "chkHideBrowser";
            this.chkHideBrowser.Size = new System.Drawing.Size(220, 64);
            this.chkHideBrowser.TabIndex = 17;
            this.chkHideBrowser.Text = "Hide Web Browser and remember id/password";
            this.chkHideBrowser.UseVisualStyleBackColor = true;
            this.chkHideBrowser.CheckedChanged += new System.EventHandler(this.chkHideBrowser_CheckedChanged);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(746, 16);
            this.lblMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(50, 18);
            this.lblMsg.TabIndex = 18;
            this.lblMsg.Text = "label2";
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(749, 40);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(288, 94);
            this.txtOutput.TabIndex = 19;
            // 
            // txtLoginId
            // 
            this.txtLoginId.Location = new System.Drawing.Point(842, 166);
            this.txtLoginId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLoginId.Name = "txtLoginId";
            this.txtLoginId.Size = new System.Drawing.Size(122, 29);
            this.txtLoginId.TabIndex = 20;
            // 
            // lblIdPwd
            // 
            this.lblIdPwd.AutoSize = true;
            this.lblIdPwd.Location = new System.Drawing.Point(746, 174);
            this.lblIdPwd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIdPwd.Name = "lblIdPwd";
            this.lblIdPwd.Size = new System.Drawing.Size(97, 18);
            this.lblIdPwd.TabIndex = 21;
            this.lblIdPwd.Text = "Id/Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(994, 166);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(122, 29);
            this.txtPassword.TabIndex = 22;
            // 
            // lblSlash
            // 
            this.lblSlash.AutoSize = true;
            this.lblSlash.Location = new System.Drawing.Point(974, 171);
            this.lblSlash.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSlash.Name = "lblSlash";
            this.lblSlash.Size = new System.Drawing.Size(13, 18);
            this.lblSlash.TabIndex = 23;
            this.lblSlash.Text = "/";
            // 
            // btnSaveIdPwd
            // 
            this.btnSaveIdPwd.Location = new System.Drawing.Point(1136, 165);
            this.btnSaveIdPwd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSaveIdPwd.Name = "btnSaveIdPwd";
            this.btnSaveIdPwd.Size = new System.Drawing.Size(112, 34);
            this.btnSaveIdPwd.TabIndex = 24;
            this.btnSaveIdPwd.Text = "save";
            this.btnSaveIdPwd.UseVisualStyleBackColor = true;
            this.btnSaveIdPwd.Click += new System.EventHandler(this.btnSaveIdPwd_Click);
            // 
            // chkRememberIdPwd
            // 
            this.chkRememberIdPwd.AutoSize = true;
            this.chkRememberIdPwd.Location = new System.Drawing.Point(495, 228);
            this.chkRememberIdPwd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkRememberIdPwd.Name = "chkRememberIdPwd";
            this.chkRememberIdPwd.Size = new System.Drawing.Size(231, 22);
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
            this.btnExampleWorkoutFileDownload.Location = new System.Drawing.Point(14, 111);
            this.btnExampleWorkoutFileDownload.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExampleWorkoutFileDownload.Name = "btnExampleWorkoutFileDownload";
            this.btnExampleWorkoutFileDownload.Size = new System.Drawing.Size(190, 34);
            this.btnExampleWorkoutFileDownload.TabIndex = 26;
            this.btnExampleWorkoutFileDownload.Text = "example workout file";
            this.btnExampleWorkoutFileDownload.UseVisualStyleBackColor = true;
            this.btnExampleWorkoutFileDownload.Click += new System.EventHandler(this.btnExampleWorkoutFileDownload_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1413, 807);
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
    }
}

