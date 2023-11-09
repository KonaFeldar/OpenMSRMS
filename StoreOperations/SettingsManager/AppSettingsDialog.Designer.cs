using System.ComponentModel;

namespace StoreOperations.SettingsManager
{
    partial class AppSettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.btnLoad = new System.Windows.Forms.Button();
            this.gbImageInfo = new System.Windows.Forms.GroupBox();
            this.lblImgInfo = new System.Windows.Forms.Label();
            this.tbImageEnding = new System.Windows.Forms.TextBox();
            this.cbEnding = new System.Windows.Forms.CheckBox();
            this.cbSubFolders = new System.Windows.Forms.CheckBox();
            this.tbWebPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbFolderPath = new System.Windows.Forms.TextBox();
            this.rbFromWeb = new System.Windows.Forms.RadioButton();
            this.rbFromFolder = new System.Windows.Forms.RadioButton();
            this.gbConnections = new System.Windows.Forms.GroupBox();
            this.lblMSCon = new System.Windows.Forms.Label();
            this.tbMSCon = new System.Windows.Forms.TextBox();
            this.btnMSCon = new System.Windows.Forms.Button();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.OK_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.gbImageInfo.SuspendLayout();
            this.gbConnections.SuspendLayout();
            this.TableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 277);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "Load";
            this.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoad.UseVisualStyleBackColor = true;
            // 
            // gbImageInfo
            // 
            this.gbImageInfo.Controls.Add(this.lblImgInfo);
            this.gbImageInfo.Controls.Add(this.tbImageEnding);
            this.gbImageInfo.Controls.Add(this.cbEnding);
            this.gbImageInfo.Controls.Add(this.cbSubFolders);
            this.gbImageInfo.Controls.Add(this.tbWebPath);
            this.gbImageInfo.Controls.Add(this.btnBrowse);
            this.gbImageInfo.Controls.Add(this.tbFolderPath);
            this.gbImageInfo.Controls.Add(this.rbFromWeb);
            this.gbImageInfo.Controls.Add(this.rbFromFolder);
            this.gbImageInfo.Location = new System.Drawing.Point(12, 124);
            this.gbImageInfo.Name = "gbImageInfo";
            this.gbImageInfo.Size = new System.Drawing.Size(411, 144);
            this.gbImageInfo.TabIndex = 6;
            this.gbImageInfo.TabStop = false;
            this.gbImageInfo.Text = "Product Image Storage";
            // 
            // lblImgInfo
            // 
            this.lblImgInfo.AutoSize = true;
            this.lblImgInfo.Location = new System.Drawing.Point(125, 119);
            this.lblImgInfo.Name = "lblImgInfo";
            this.lblImgInfo.Size = new System.Drawing.Size(147, 13);
            this.lblImgInfo.TabIndex = 17;
            this.lblImgInfo.Text = "1st Ltr^  ^2nd Ltr      ^ Ending";
            // 
            // tbImageEnding
            // 
            this.tbImageEnding.Location = new System.Drawing.Point(150, 73);
            this.tbImageEnding.Name = "tbImageEnding";
            this.tbImageEnding.Size = new System.Drawing.Size(97, 20);
            this.tbImageEnding.TabIndex = 16;
            this.tbImageEnding.Text = "-450.jpg";
            // 
            // cbEnding
            // 
            this.cbEnding.AutoSize = true;
            this.cbEnding.Checked = true;
            this.cbEnding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEnding.Location = new System.Drawing.Point(6, 75);
            this.cbEnding.Name = "cbEnding";
            this.cbEnding.Size = new System.Drawing.Size(138, 17);
            this.cbEnding.TabIndex = 15;
            this.cbEnding.Text = "Use specific file ending:";
            this.cbEnding.UseVisualStyleBackColor = true;
            // 
            // cbSubFolders
            // 
            this.cbSubFolders.AutoSize = true;
            this.cbSubFolders.Checked = true;
            this.cbSubFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSubFolders.Location = new System.Drawing.Point(6, 99);
            this.cbSubFolders.Name = "cbSubFolders";
            this.cbSubFolders.Size = new System.Drawing.Size(258, 17);
            this.cbSubFolders.TabIndex = 14;
            this.cbSubFolders.Text = "Use Magento Subfolders. (/M/A/MA232-450.jpg)";
            this.cbSubFolders.UseVisualStyleBackColor = true;
            // 
            // tbWebPath
            // 
            this.tbWebPath.Location = new System.Drawing.Point(98, 47);
            this.tbWebPath.Name = "tbWebPath";
            this.tbWebPath.Size = new System.Drawing.Size(150, 20);
            this.tbWebPath.TabIndex = 13;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(254, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 12;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbFolderPath
            // 
            this.tbFolderPath.Location = new System.Drawing.Point(98, 21);
            this.tbFolderPath.Name = "tbFolderPath";
            this.tbFolderPath.Size = new System.Drawing.Size(150, 20);
            this.tbFolderPath.TabIndex = 11;
            // 
            // rbFromWeb
            // 
            this.rbFromWeb.AutoSize = true;
            this.rbFromWeb.Location = new System.Drawing.Point(6, 48);
            this.rbFromWeb.Name = "rbFromWeb";
            this.rbFromWeb.Size = new System.Drawing.Size(67, 17);
            this.rbFromWeb.TabIndex = 10;
            this.rbFromWeb.Text = "Website:";
            this.rbFromWeb.UseVisualStyleBackColor = true;
            // 
            // rbFromFolder
            // 
            this.rbFromFolder.AutoSize = true;
            this.rbFromFolder.Checked = true;
            this.rbFromFolder.Location = new System.Drawing.Point(6, 22);
            this.rbFromFolder.Name = "rbFromFolder";
            this.rbFromFolder.Size = new System.Drawing.Size(86, 17);
            this.rbFromFolder.TabIndex = 9;
            this.rbFromFolder.TabStop = true;
            this.rbFromFolder.Text = "Local Folder:";
            this.rbFromFolder.UseVisualStyleBackColor = true;
            // 
            // gbConnections
            // 
            this.gbConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.gbConnections.Controls.Add(this.lblMSCon);
            this.gbConnections.Controls.Add(this.tbMSCon);
            this.gbConnections.Controls.Add(this.btnMSCon);
            this.gbConnections.Location = new System.Drawing.Point(12, 12);
            this.gbConnections.Name = "gbConnections";
            this.gbConnections.Size = new System.Drawing.Size(411, 106);
            this.gbConnections.TabIndex = 5;
            this.gbConnections.TabStop = false;
            this.gbConnections.Text = "Database Connections";
            // 
            // lblMSCon
            // 
            this.lblMSCon.AutoSize = true;
            this.lblMSCon.Location = new System.Drawing.Point(6, 22);
            this.lblMSCon.Name = "lblMSCon";
            this.lblMSCon.Size = new System.Drawing.Size(113, 13);
            this.lblMSCon.TabIndex = 7;
            this.lblMSCon.Text = "MS Connection String:";
            // 
            // tbMSCon
            // 
            this.tbMSCon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMSCon.Location = new System.Drawing.Point(125, 19);
            this.tbMSCon.Name = "tbMSCon";
            this.tbMSCon.Size = new System.Drawing.Size(199, 20);
            this.tbMSCon.TabIndex = 5;
            // 
            // btnMSCon
            // 
            this.btnMSCon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMSCon.Location = new System.Drawing.Point(330, 17);
            this.btnMSCon.Name = "btnMSCon";
            this.btnMSCon.Size = new System.Drawing.Size(75, 23);
            this.btnMSCon.TabIndex = 3;
            this.btnMSCon.Text = "Build";
            this.btnMSCon.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMSCon.UseVisualStyleBackColor = true;
            this.btnMSCon.Click += new System.EventHandler(this.btnMSCon_Click);
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel1.ColumnCount = 2;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Controls.Add(this.OK_Button, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.Cancel_Button, 1, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(277, 274);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 1;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(146, 29);
            this.TableLayoutPanel1.TabIndex = 4;
            // 
            // OK_Button
            // 
            this.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OK_Button.Location = new System.Drawing.Point(3, 3);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(67, 23);
            this.OK_Button.TabIndex = 0;
            this.OK_Button.Text = "OK";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Location = new System.Drawing.Point(76, 3);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(67, 23);
            this.Cancel_Button.TabIndex = 1;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // AppSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 315);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.gbImageInfo);
            this.Controls.Add(this.gbConnections);
            this.Controls.Add(this.TableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AppSettingsDialog";
            this.Text = "Application Settings";
            this.gbImageInfo.ResumeLayout(false);
            this.gbImageInfo.PerformLayout();
            this.gbConnections.ResumeLayout(false);
            this.gbConnections.PerformLayout();
            this.TableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        internal System.Windows.Forms.Button btnLoad;
        internal System.Windows.Forms.GroupBox gbImageInfo;
        internal System.Windows.Forms.Label lblImgInfo;
        internal System.Windows.Forms.TextBox tbImageEnding;
        internal System.Windows.Forms.CheckBox cbEnding;
        internal System.Windows.Forms.CheckBox cbSubFolders;
        internal System.Windows.Forms.TextBox tbWebPath;
        internal System.Windows.Forms.Button btnBrowse;
        internal System.Windows.Forms.TextBox tbFolderPath;
        internal System.Windows.Forms.RadioButton rbFromWeb;
        internal System.Windows.Forms.RadioButton rbFromFolder;
        internal System.Windows.Forms.GroupBox gbConnections;
        internal System.Windows.Forms.Label lblMSCon;
        internal System.Windows.Forms.TextBox tbMSCon;
        internal System.Windows.Forms.Button btnMSCon;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.Button OK_Button;
        internal System.Windows.Forms.Button Cancel_Button;

        #endregion
    }
}