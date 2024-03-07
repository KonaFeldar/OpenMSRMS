namespace StoreOperations
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.qrcTop = new Qios.DevSuite.Components.Ribbon.QRibbonCaption();
            this.qMainMenu1 = new Qios.DevSuite.Components.QMainMenu();
            this.qFileMenu = new Qios.DevSuite.Components.QMenuItem(this.components);
            this.qDatabaseMenu = new Qios.DevSuite.Components.QMenuItem(this.components);
            this.qInventoryMenu = new Qios.DevSuite.Components.QMenuItem(this.components);
            this.qJournalMenu = new Qios.DevSuite.Components.QMenuItem(this.components);
            this.qWizardsMenu = new Qios.DevSuite.Components.QMenuItem(this.components);
            this.qReportsMenu = new Qios.DevSuite.Components.QMenuItem(this.components);
            this.qUtilitiesMenu = new Qios.DevSuite.Components.QMenuItem(this.components);
            this.qAddonsMenu = new Qios.DevSuite.Components.QMenuItem(this.components);
            this.qWindowMenu = new Qios.DevSuite.Components.QMenuItem(this.components);
            this.qHelpMenu = new Qios.DevSuite.Components.QMenuItem(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.qrcTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qMainMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // qrcTop
            // 
            this.qrcTop.Location = new System.Drawing.Point(0, 0);
            this.qrcTop.Name = "qrcTop";
            this.qrcTop.Size = new System.Drawing.Size(800, 28);
            this.qrcTop.TabIndex = 1;
            this.qrcTop.Text = "Open MSRMS";
            // 
            // qMainMenu1
            // 
            this.qMainMenu1.Appearance.ShowBorders = false;
            this.qMainMenu1.Location = new System.Drawing.Point(0, 28);
            this.qMainMenu1.MenuItems.AddRange(new Qios.DevSuite.Components.QMenuItem[] {
            this.qFileMenu,
            this.qDatabaseMenu,
            this.qInventoryMenu,
            this.qJournalMenu,
            this.qWizardsMenu,
            this.qReportsMenu,
            this.qUtilitiesMenu,
            this.qAddonsMenu,
            this.qWindowMenu,
            this.qHelpMenu});
            this.qMainMenu1.Name = "qMainMenu1";
            this.qMainMenu1.PersistGuid = new System.Guid("20a9d83b-b359-4b8f-b637-dae6ff72d0bd");
            this.qMainMenu1.Size = new System.Drawing.Size(800, 30);
            this.qMainMenu1.TabIndex = 3;
            this.qMainMenu1.Text = "Main Menu";
            // 
            // qFileMenu
            // 
            this.qFileMenu.ItemName = "File";
            this.qFileMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.qFileMenu.Title = "&File";
            this.qFileMenu.ToolTip = "File";
            // 
            // qDatabaseMenu
            // 
            this.qDatabaseMenu.ItemName = "Database";
            this.qDatabaseMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlD;
            this.qDatabaseMenu.Title = "&Database";
            this.qDatabaseMenu.ToolTip = "Database";
            // 
            // qInventoryMenu
            // 
            this.qInventoryMenu.ItemName = "Inventory";
            this.qInventoryMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.qInventoryMenu.Title = "&Inventory";
            this.qInventoryMenu.ToolTip = "Inventory";
            // 
            // qJournalMenu
            // 
            this.qJournalMenu.ItemName = "Journal";
            this.qJournalMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlJ;
            this.qJournalMenu.Title = "&Journal";
            this.qJournalMenu.ToolTip = "Journal";
            // 
            // qWizardsMenu
            // 
            this.qWizardsMenu.ItemName = "Wizards";
            this.qWizardsMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
            this.qWizardsMenu.Title = "&Wizards";
            this.qWizardsMenu.ToolTip = "Wizards";
            // 
            // qReportsMenu
            // 
            this.qReportsMenu.ItemName = "Reports";
            this.qReportsMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
            this.qReportsMenu.Title = "&Reports";
            this.qReportsMenu.ToolTip = "Reports";
            // 
            // qUtilitiesMenu
            // 
            this.qUtilitiesMenu.ItemName = "Utilities";
            this.qUtilitiesMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlU;
            this.qUtilitiesMenu.Title = "&Utilities";
            this.qUtilitiesMenu.ToolTip = "Utilities";
            // 
            // qAddonsMenu
            // 
            this.qAddonsMenu.ItemName = "Addons";
            this.qAddonsMenu.Title = "&Addons";
            this.qAddonsMenu.ToolTip = "Addons";
            // 
            // qWindowMenu
            // 
            this.qWindowMenu.ItemName = "Window";
            this.qWindowMenu.Title = "Window";
            this.qWindowMenu.ToolTip = "Window";
            // 
            // qHelpMenu
            // 
            this.qHelpMenu.ItemName = "Help";
            this.qHelpMenu.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.qHelpMenu.Title = "&Help";
            this.qHelpMenu.ToolTip = "Help";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.qMainMenu1);
            this.Controls.Add(this.qrcTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "Form1";
            this.Text = "Open MSRMS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.qrcTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qMainMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Qios.DevSuite.Components.Ribbon.QRibbonCaption qrcTop;
        private Qios.DevSuite.Components.QMainMenu qMainMenu1;
        private Qios.DevSuite.Components.QMenuItem qFileMenu;
        private Qios.DevSuite.Components.QMenuItem qDatabaseMenu;
        private Qios.DevSuite.Components.QMenuItem qInventoryMenu;
        private Qios.DevSuite.Components.QMenuItem qJournalMenu;
        private Qios.DevSuite.Components.QMenuItem qWizardsMenu;
        private Qios.DevSuite.Components.QMenuItem qReportsMenu;
        private Qios.DevSuite.Components.QMenuItem qUtilitiesMenu;
        private Qios.DevSuite.Components.QMenuItem qAddonsMenu;
        private Qios.DevSuite.Components.QMenuItem qWindowMenu;
        private Qios.DevSuite.Components.QMenuItem qHelpMenu;
    }
}