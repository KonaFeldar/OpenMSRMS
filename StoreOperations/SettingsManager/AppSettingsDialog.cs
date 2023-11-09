using System;
using System.Windows.Forms;

namespace StoreOperations.SettingsManager
{
    public partial class AppSettingsDialog : Form
    {
        public AppSettingsDialog()
        {
            InitializeComponent();
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        
        private void btnMSCon_Click(object sender, EventArgs e)
        {
            // Get MS Database Location
            using (var dgDbPrompt = new DatabaseDialog())
            {
                dgDbPrompt.Text = @"MS Database Configuration";
                dgDbPrompt.tbDesc.Text = @"MSConnection";

                if (!string.IsNullOrEmpty(tbMSCon.Text))
                {
                    dgDbPrompt.SplitConString(tbMSCon.Text);
                }
                if (dgDbPrompt.ShowDialog() == DialogResult.OK)
                {
                    tbMSCon.Text = dgDbPrompt.CombineConString();
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dgFolders = new FolderBrowserDialog())
            {
                dgFolders.RootFolder = Environment.SpecialFolder.MyComputer;

                if (dgFolders.ShowDialog() == DialogResult.OK)
                {
                    tbFolderPath.Text = dgFolders.SelectedPath;
                }
            }
        }
    }
}