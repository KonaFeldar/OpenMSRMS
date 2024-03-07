using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreOperations
{
    public partial class Form1 : Qios.DevSuite.Components.Ribbon.QRibbonForm
    {
        private static System.Drawing.Text.PrivateFontCollection _pFont;
        private static readonly RegistriedAppSettings.BaseAppSettings MyAppSettings = new RegistriedAppSettings.BaseAppSettings();
        private static readonly AddonManager MyAddonMgr = new AddonManager();
        public Form1()
        {
            InitializeComponent();
            // Initialize the font collection
            _pFont = new System.Drawing.Text.PrivateFontCollection();
            
            // Load the font into the font collection from file
            _pFont.AddFontFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "BCW_Code128B_NoLetters.ttf"));

            // Load the AppSettings from registry
            var bSettingsLoaded = MyAppSettings.Load();

            if (bSettingsLoaded == false)
            {
                MessageBox.Show(@"Settings aren't loaded, please update config.", @"Settings not loaded", MessageBoxButtons.OK);
            }
            else
            {
                // Load Addons
                MyAddonMgr.LoadAllAddons(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Addons"));
                // Fill menus and task pad with buttons
            }
        }
        
        public static FontFamily BarcodeFont => _pFont.Families[0];
    }
}