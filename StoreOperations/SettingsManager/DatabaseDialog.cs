using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StoreOperations.SettingsManager
{
    public partial class DatabaseDialog : Form
    {
        private string _strConError = "";
        
        public DatabaseDialog()
        {
            InitializeComponent();
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            var bConnectSuccess = TestConnect();
            if (bConnectSuccess)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(@"Connect Failed: " + _strConError, @"Fail!", MessageBoxButtons.OK);
            }
        }
        
        private bool TestConnect()
        {
            if (!string.IsNullOrEmpty(tbServerName.Text) & !string.IsNullOrEmpty(tbUser.Text) & !string.IsNullOrEmpty(tbPasswd.Text) & !string.IsNullOrEmpty(tbDatabase.Text))
            {
                const int intTimeout = 10;
                var strPw = tbPasswd.Text;
                var testConn = new System.Data.SqlClient.SqlConnection("Data Source=" + tbServerName.Text + ";User ID=" + tbUser.Text + ";Password=" + strPw + ";Initial Catalog=" + tbDatabase.Text + ";Connection Timeout=" + intTimeout);
                try
                {
                    testConn.Open();
                    testConn.Close();
                    return true;
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    _strConError = sqlEx.Message;
                    return false;
                }
            }
            _strConError = "ServerName, User, Password and Database Name are all required";
            return false;
        }

        /// <summary>
        /// Split the connection string into it;s separate parts
        /// </summary>
        /// <param name="strToSplit"></param>
        public void SplitConString(string strToSplit)
        {
            var arConString = strToSplit.Split(';');
            var diConSettings = new Dictionary<string, string>();
            foreach (var strSetting in arConString)
            {
                string[] arSeps = { "=" };
                var arSetting = strSetting.Split(arSeps, 2, StringSplitOptions.None);
                if (arSetting.Length > 1)
                {
                    if (diConSettings.ContainsKey(arSetting[0]))
                    {
                        diConSettings[arSetting[0]] = arSetting[1];
                    }
                    else
                    {
                        diConSettings.Add(arSetting[0], arSetting[1]);
                    }
                }
            }

            tbServerName.Text = diConSettings.TryGetValue("Data Source", out var dataSource) ? dataSource : "";
            tbUser.Text = diConSettings.TryGetValue("User ID", out var usrId) ? usrId : "";
            tbPasswd.Text = diConSettings.TryGetValue("Password", out var usrPw) ? usrPw : "";
            tbDatabase.Text = diConSettings.TryGetValue("Initial Catalog", out var initCatalog) ? initCatalog : "";
            tbTimeout.Text = diConSettings.TryGetValue("Connection Timeout", out var conTimeout) ? conTimeout : "20";
        }

        /// <summary>
        /// Combine the separate fields to a complete connection string
        /// </summary>
        /// <returns></returns>
        public string CombineConString()
        {
            var strResult = "Data Source=" + tbServerName.Text + ";User ID=" + tbUser.Text + 
                            ";Password=" + tbPasswd.Text + ";Initial Catalog=" + tbDatabase.Text;
            if (int.TryParse(tbTimeout.Text, out var intTimeout))
            {
                strResult += ";Connection Timeout=" + intTimeout;
            }

            return strResult;
        }
    }
}