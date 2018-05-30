namespace PDBundleModPatcher
{
    partial class MainForm
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
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("PDMod Mods", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Override Folder Mods", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("BLT Mods", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup8 = new System.Windows.Forms.ListViewGroup("Downloadable Mods", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabs = new System.Windows.Forms.TabControl();
            this.ApplyTab = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ExtraOptions1 = new System.Windows.Forms.Button();
            this.availiableModsSearch_textbox = new System.Windows.Forms.TextBox();
            this.filecontrol_button = new System.Windows.Forms.Button();
            this.availiableMods_listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AvailableModsLabel = new System.Windows.Forms.Label();
            this.BundleProgress = new System.Windows.Forms.ProgressBar();
            this.OpenModButton = new System.Windows.Forms.Button();
            this.TotalProgress = new System.Windows.Forms.ProgressBar();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.TotalProgressLabel = new System.Windows.Forms.Label();
            this.patchingTimeDetails = new System.Windows.Forms.Label();
            this.viewModConfig = new System.Windows.Forms.Button();
            this.moreModDetails_button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DescriptionText = new System.Windows.Forms.TextBox();
            this.ModNameText = new System.Windows.Forms.TextBox();
            this.AuthorText = new System.Windows.Forms.TextBox();
            this.CreateTab = new System.Windows.Forms.TabPage();
            this.selectedPackage = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.editModOptions_button = new System.Windows.Forms.Button();
            this.modVariables_button = new System.Windows.Forms.Button();
            this.fileReplacementType_ComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BundleFileName = new System.Windows.Forms.TextBox();
            this.JPModStyleName = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ReplacementFileName = new System.Windows.Forms.TextBox();
            this.BrowseForReplacement = new System.Windows.Forms.Button();
            this.AddReplacementButton = new System.Windows.Forms.Button();
            this.UndoLast = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SpecificVersion = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.AddedFilesView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bundleRewriteItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.ModNameEdit = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ModAuthorEdit = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ModDescriptionEdit = new System.Windows.Forms.TextBox();
            this.CreateModButton = new System.Windows.Forms.Button();
            this.savePDMod = new System.Windows.Forms.Button();
            this.createModLoad = new System.Windows.Forms.Button();
            this.ConfigureReplacementButton = new System.Windows.Forms.Button();
            this.OptionsTab = new System.Windows.Forms.TabPage();
            this.OptionsFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.GameAssetFolderLabel = new System.Windows.Forms.Label();
            this.AssetFolderEdit = new System.Windows.Forms.TextBox();
            this.AssetFolderButton = new System.Windows.Forms.Button();
            this.backupMethodLabel = new System.Windows.Forms.Label();
            this.backupMethodComboBox = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.patchingBufferSize = new System.Windows.Forms.ComboBox();
            this.useOverrideFolderCheckbox = new System.Windows.Forms.CheckBox();
            this.createOverrideFolderDummies = new System.Windows.Forms.CheckBox();
            this.useSharedPDMODTOOLfolder = new System.Windows.Forms.CheckBox();
            this.ThemeSelectionLabel = new System.Windows.Forms.Label();
            this.ThemeSelection = new System.Windows.Forms.ComboBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.bundleFilePathAutotompletion_checkbox = new System.Windows.Forms.CheckBox();
            this.WriteConsole = new System.Windows.Forms.CheckBox();
            this.playsoundoncompletion_checkbox = new System.Windows.Forms.CheckBox();
            this.checkBoxOverrideShow = new System.Windows.Forms.CheckBox();
            this.rungameoncompletion_checkbox = new System.Windows.Forms.CheckBox();
            this.checkupdatesonstartup_checkbox = new System.Windows.Forms.CheckBox();
            this.emptylabeltofixflowlayout = new System.Windows.Forms.Label();
            this.checkForUpdates_Button = new System.Windows.Forms.Button();
            this.programIcon = new System.Windows.Forms.PictureBox();
            this.AboutLabel = new System.Windows.Forms.LinkLabel();
            this.UtilitiesTab = new System.Windows.Forms.TabPage();
            this.corruptedBundlesGroup = new System.Windows.Forms.GroupBox();
            this.attemptRepair_button = new System.Windows.Forms.Button();
            this.corruptedShowOnlyCorrupted_checkbox = new System.Windows.Forms.CheckBox();
            this.corruptedBundlesReport_richTextBox = new System.Windows.Forms.RichTextBox();
            this.progressTextCorruptedBundlesCheck = new System.Windows.Forms.Label();
            this.progressCorruptedBundlesCheck = new System.Windows.Forms.ProgressBar();
            this.verifyCorruptedBundlesCheckCheckBox = new System.Windows.Forms.CheckBox();
            this.runCorruptedCheckButton = new System.Windows.Forms.Button();
            this.hashConverterGroup = new System.Windows.Forms.GroupBox();
            this.hashSwapHexEndiannessCheckbox = new System.Windows.Forms.CheckBox();
            this.hashUseHexcheckBox = new System.Windows.Forms.CheckBox();
            this.unHashButton = new System.Windows.Forms.Button();
            this.hashHashBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.hashButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.hashTextBox = new System.Windows.Forms.TextBox();
            this.ReportsTab = new System.Windows.Forms.TabPage();
            this.reports_FormattingComboBox = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.reports_LuaBLTIncludeLog = new System.Windows.Forms.CheckBox();
            this.reports_GenerateReportButton = new System.Windows.Forms.Button();
            this.reports_IncludePdmodModsList = new System.Windows.Forms.CheckBox();
            this.reports_IncludeLuaModsList = new System.Windows.Forms.CheckBox();
            this.reports_IncludeLuaInformationCheckBox = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.reports_ReportRichTextBox = new System.Windows.Forms.RichTextBox();
            this.tabExtractGameFiles = new System.Windows.Forms.TabPage();
            this.lblGeneralExtractOptions = new System.Windows.Forms.Label();
            this.prgExtractSubProgress = new System.Windows.Forms.ProgressBar();
            this.flpGeneralExtractOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSingleBundle = new System.Windows.Forms.Label();
            this.txtSingleBundle = new System.Windows.Forms.TextBox();
            this.chkExtractAll = new System.Windows.Forms.CheckBox();
            this.tabExtractOptions = new System.Windows.Forms.TabControl();
            this.tabExtract = new System.Windows.Forms.TabPage();
            this.flpExtract = new System.Windows.Forms.FlowLayoutPanel();
            this.lblExtractFolder = new System.Windows.Forms.Label();
            this.txtExtractFolder = new System.Windows.Forms.TextBox();
            this.btnExtractFolder = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteExtMod = new System.Windows.Forms.Button();
            this.btnApplyExtensionChange = new System.Windows.Forms.Button();
            this.txtExtensionReplacement = new System.Windows.Forms.TextBox();
            this.txtExtensionMod = new System.Windows.Forms.TextBox();
            this.lblReplacement = new System.Windows.Forms.Label();
            this.lblExtension = new System.Windows.Forms.Label();
            this.lstChangeExtension = new System.Windows.Forms.ListView();
            this.clmOriginalExtension = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmNewExtension = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkIgnoreExisting = new System.Windows.Forms.CheckBox();
            this.tabList = new System.Windows.Forms.TabPage();
            this.flpList = new System.Windows.Forms.FlowLayoutPanel();
            this.lblFormat = new System.Windows.Forms.Label();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.lblListFile = new System.Windows.Forms.Label();
            this.txtListFile = new System.Windows.Forms.TextBox();
            this.btnListFileBrowse = new System.Windows.Forms.Button();
            this.lblSelectInformation = new System.Windows.Forms.Label();
            this.clstSelectInformation = new System.Windows.Forms.CheckedListBox();
            this.lblExtractLog = new System.Windows.Forms.Label();
            this.txtExtractLog = new System.Windows.Forms.TextBox();
            this.lblExtractProgress = new System.Windows.Forms.Label();
            this.prgExtractMain = new System.Windows.Forms.ProgressBar();
            this.btnStartExtracting = new System.Windows.Forms.Button();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.modContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.markForInstallationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markForReinstallationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markForUninstallationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.showPDModFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInModoverrideFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInBLTModsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bundleModBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabs.SuspendLayout();
            this.ApplyTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.CreateTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddedFilesView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bundleRewriteItemBindingSource)).BeginInit();
            this.OptionsTab.SuspendLayout();
            this.OptionsFlowLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.programIcon)).BeginInit();
            this.UtilitiesTab.SuspendLayout();
            this.corruptedBundlesGroup.SuspendLayout();
            this.hashConverterGroup.SuspendLayout();
            this.ReportsTab.SuspendLayout();
            this.tabExtractGameFiles.SuspendLayout();
            this.flpGeneralExtractOptions.SuspendLayout();
            this.tabExtractOptions.SuspendLayout();
            this.tabExtract.SuspendLayout();
            this.flpExtract.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabList.SuspendLayout();
            this.flpList.SuspendLayout();
            this.modContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bundleModBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.ApplyTab);
            this.tabs.Controls.Add(this.CreateTab);
            this.tabs.Controls.Add(this.OptionsTab);
            this.tabs.Controls.Add(this.UtilitiesTab);
            this.tabs.Controls.Add(this.ReportsTab);
            this.tabs.Controls.Add(this.tabExtractGameFiles);
            this.tabs.Location = new System.Drawing.Point(0, 1);
            this.tabs.MinimumSize = new System.Drawing.Size(690, 320);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(800, 497);
            this.tabs.TabIndex = 0;
            // 
            // ApplyTab
            // 
            this.ApplyTab.Controls.Add(this.splitContainer1);
            this.ApplyTab.Location = new System.Drawing.Point(4, 22);
            this.ApplyTab.Name = "ApplyTab";
            this.ApplyTab.Padding = new System.Windows.Forms.Padding(3);
            this.ApplyTab.Size = new System.Drawing.Size(792, 471);
            this.ApplyTab.TabIndex = 0;
            this.ApplyTab.Text = "Mods";
            this.ApplyTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ExtraOptions1);
            this.splitContainer1.Panel1.Controls.Add(this.availiableModsSearch_textbox);
            this.splitContainer1.Panel1.Controls.Add(this.filecontrol_button);
            this.splitContainer1.Panel1.Controls.Add(this.availiableMods_listView);
            this.splitContainer1.Panel1.Controls.Add(this.AvailableModsLabel);
            this.splitContainer1.Panel1.Controls.Add(this.BundleProgress);
            this.splitContainer1.Panel1.Controls.Add(this.OpenModButton);
            this.splitContainer1.Panel1.Controls.Add(this.TotalProgress);
            this.splitContainer1.Panel1.Controls.Add(this.ApplyButton);
            this.splitContainer1.Panel1.Controls.Add(this.refreshButton);
            this.splitContainer1.Panel1.Controls.Add(this.TotalProgressLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.patchingTimeDetails);
            this.splitContainer1.Panel2.Controls.Add(this.viewModConfig);
            this.splitContainer1.Panel2.Controls.Add(this.moreModDetails_button);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.DescriptionText);
            this.splitContainer1.Panel2.Controls.Add(this.ModNameText);
            this.splitContainer1.Panel2.Controls.Add(this.AuthorText);
            this.splitContainer1.Size = new System.Drawing.Size(786, 475);
            this.splitContainer1.SplitterDistance = 415;
            this.splitContainer1.TabIndex = 4;
            // 
            // ExtraOptions1
            // 
            this.ExtraOptions1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExtraOptions1.Location = new System.Drawing.Point(242, 377);
            this.ExtraOptions1.Name = "ExtraOptions1";
            this.ExtraOptions1.Size = new System.Drawing.Size(65, 25);
            this.ExtraOptions1.TabIndex = 16;
            this.ExtraOptions1.Text = "Extra Opt.";
            this.ExtraOptions1.UseVisualStyleBackColor = true;
            this.ExtraOptions1.Click += new System.EventHandler(this.ExtraOptions_OnClick);
            // 
            // availiableModsSearch_textbox
            // 
            this.availiableModsSearch_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.availiableModsSearch_textbox.Location = new System.Drawing.Point(6, 16);
            this.availiableModsSearch_textbox.Name = "availiableModsSearch_textbox";
            this.availiableModsSearch_textbox.Size = new System.Drawing.Size(406, 20);
            this.availiableModsSearch_textbox.TabIndex = 15;
            this.toolTips.SetToolTip(this.availiableModsSearch_textbox, "Search availiable mods");
            this.availiableModsSearch_textbox.TextChanged += new System.EventHandler(this.availiableModsSearch_textbox_TextChanged);
            // 
            // filecontrol_button
            // 
            this.filecontrol_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.filecontrol_button.Enabled = false;
            this.filecontrol_button.Location = new System.Drawing.Point(88, 377);
            this.filecontrol_button.Name = "filecontrol_button";
            this.filecontrol_button.Size = new System.Drawing.Size(90, 25);
            this.filecontrol_button.TabIndex = 14;
            this.filecontrol_button.Text = "File Ctrl";
            this.filecontrol_button.UseVisualStyleBackColor = true;
            this.filecontrol_button.Click += new System.EventHandler(this.filecontrol_button_Click);
            // 
            // availiableMods_listView
            // 
            this.availiableMods_listView.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.availiableMods_listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.availiableMods_listView.CheckBoxes = true;
            this.availiableMods_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            listViewGroup5.Header = "PDMod Mods";
            listViewGroup5.Name = "listViewLocalModsGroup";
            listViewGroup5.Tag = "LocalModsGroup";
            listViewGroup6.Header = "Override Folder Mods";
            listViewGroup6.Name = "listViewOverrideFolderGroup";
            listViewGroup7.Header = "BLT Mods";
            listViewGroup7.Name = "listViewBLTModsGroup";
            listViewGroup8.Header = "Downloadable Mods";
            listViewGroup8.Name = "listViewDownloadableModsGroup";
            listViewGroup8.Tag = "DownloadableModsGroup";
            this.availiableMods_listView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup5,
            listViewGroup6,
            listViewGroup7,
            listViewGroup8});
            this.availiableMods_listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.availiableMods_listView.HideSelection = false;
            this.availiableMods_listView.Location = new System.Drawing.Point(6, 42);
            this.availiableMods_listView.MultiSelect = false;
            this.availiableMods_listView.Name = "availiableMods_listView";
            this.availiableMods_listView.ShowItemToolTips = true;
            this.availiableMods_listView.Size = new System.Drawing.Size(407, 329);
            this.availiableMods_listView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.availiableMods_listView.TabIndex = 13;
            this.availiableMods_listView.UseCompatibleStateImageBehavior = false;
            this.availiableMods_listView.View = System.Windows.Forms.View.Details;
            this.availiableMods_listView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.availableMods_listView_ItemChecked);
            this.availiableMods_listView.SelectedIndexChanged += new System.EventHandler(this.availableMods_listView_SelectedIndexChanged);
            this.availiableMods_listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.availiableMods_listView_MouseClick);
            this.availiableMods_listView.Resize += new System.EventHandler(this.availiableMods_listView_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Mods";
            this.columnHeader1.Width = 364;
            // 
            // AvailableModsLabel
            // 
            this.AvailableModsLabel.AutoSize = true;
            this.AvailableModsLabel.Location = new System.Drawing.Point(3, 1);
            this.AvailableModsLabel.Name = "AvailableModsLabel";
            this.AvailableModsLabel.Size = new System.Drawing.Size(79, 13);
            this.AvailableModsLabel.TabIndex = 0;
            this.AvailableModsLabel.Text = "Available Mods";
            // 
            // BundleProgress
            // 
            this.BundleProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BundleProgress.Location = new System.Drawing.Point(3, 428);
            this.BundleProgress.Name = "BundleProgress";
            this.BundleProgress.Size = new System.Drawing.Size(410, 15);
            this.BundleProgress.Step = 1;
            this.BundleProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BundleProgress.TabIndex = 1;
            // 
            // OpenModButton
            // 
            this.OpenModButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OpenModButton.Enabled = false;
            this.OpenModButton.Location = new System.Drawing.Point(6, 377);
            this.OpenModButton.Name = "OpenModButton";
            this.OpenModButton.Size = new System.Drawing.Size(76, 25);
            this.OpenModButton.TabIndex = 5;
            this.OpenModButton.Text = "Add Mod(s)";
            this.OpenModButton.UseVisualStyleBackColor = true;
            this.OpenModButton.Click += new System.EventHandler(this.AddMod_Click);
            // 
            // TotalProgress
            // 
            this.TotalProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TotalProgress.Location = new System.Drawing.Point(3, 445);
            this.TotalProgress.Name = "TotalProgress";
            this.TotalProgress.Size = new System.Drawing.Size(410, 15);
            this.TotalProgress.Step = 1;
            this.TotalProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.TotalProgress.TabIndex = 2;
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyButton.Enabled = false;
            this.ApplyButton.Location = new System.Drawing.Point(313, 377);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(100, 25);
            this.ApplyButton.TabIndex = 0;
            this.ApplyButton.Text = "Apply Changes";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButtonClick);
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Location = new System.Drawing.Point(184, 377);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(52, 25);
            this.refreshButton.TabIndex = 12;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // TotalProgressLabel
            // 
            this.TotalProgressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TotalProgressLabel.Location = new System.Drawing.Point(3, 410);
            this.TotalProgressLabel.Name = "TotalProgressLabel";
            this.TotalProgressLabel.Size = new System.Drawing.Size(410, 15);
            this.TotalProgressLabel.TabIndex = 4;
            this.TotalProgressLabel.Text = "Total Progress";
            this.TotalProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // patchingTimeDetails
            // 
            this.patchingTimeDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.patchingTimeDetails.Location = new System.Drawing.Point(3, 410);
            this.patchingTimeDetails.Name = "patchingTimeDetails";
            this.patchingTimeDetails.Size = new System.Drawing.Size(361, 50);
            this.patchingTimeDetails.TabIndex = 15;
            this.patchingTimeDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // viewModConfig
            // 
            this.viewModConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.viewModConfig.Enabled = false;
            this.viewModConfig.Location = new System.Drawing.Point(186, 377);
            this.viewModConfig.Name = "viewModConfig";
            this.viewModConfig.Size = new System.Drawing.Size(92, 25);
            this.viewModConfig.TabIndex = 14;
            this.viewModConfig.Text = "Mod Options";
            this.viewModConfig.UseVisualStyleBackColor = true;
            this.viewModConfig.Click += new System.EventHandler(this.viewModConfig_Click);
            // 
            // moreModDetails_button
            // 
            this.moreModDetails_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.moreModDetails_button.Enabled = false;
            this.moreModDetails_button.Location = new System.Drawing.Point(284, 377);
            this.moreModDetails_button.Name = "moreModDetails_button";
            this.moreModDetails_button.Size = new System.Drawing.Size(80, 25);
            this.moreModDetails_button.TabIndex = 13;
            this.moreModDetails_button.Text = "Mod Details";
            this.moreModDetails_button.UseVisualStyleBackColor = true;
            this.moreModDetails_button.Click += new System.EventHandler(this.moreModDetails_button_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(3, -1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(361, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Mod Name";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(0, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(361, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "Description";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(3, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(361, 19);
            this.label4.TabIndex = 10;
            this.label4.Text = "Author";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DescriptionText
            // 
            this.DescriptionText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionText.Location = new System.Drawing.Point(3, 106);
            this.DescriptionText.Multiline = true;
            this.DescriptionText.Name = "DescriptionText";
            this.DescriptionText.ReadOnly = true;
            this.DescriptionText.Size = new System.Drawing.Size(361, 265);
            this.DescriptionText.TabIndex = 8;
            // 
            // ModNameText
            // 
            this.ModNameText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ModNameText.Location = new System.Drawing.Point(3, 16);
            this.ModNameText.Name = "ModNameText";
            this.ModNameText.ReadOnly = true;
            this.ModNameText.Size = new System.Drawing.Size(361, 20);
            this.ModNameText.TabIndex = 6;
            // 
            // AuthorText
            // 
            this.AuthorText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AuthorText.Location = new System.Drawing.Point(3, 61);
            this.AuthorText.Name = "AuthorText";
            this.AuthorText.ReadOnly = true;
            this.AuthorText.Size = new System.Drawing.Size(361, 20);
            this.AuthorText.TabIndex = 7;
            // 
            // CreateTab
            // 
            this.CreateTab.Controls.Add(this.selectedPackage);
            this.CreateTab.Controls.Add(this.label12);
            this.CreateTab.Controls.Add(this.editModOptions_button);
            this.CreateTab.Controls.Add(this.modVariables_button);
            this.CreateTab.Controls.Add(this.fileReplacementType_ComboBox);
            this.CreateTab.Controls.Add(this.label1);
            this.CreateTab.Controls.Add(this.label6);
            this.CreateTab.Controls.Add(this.BundleFileName);
            this.CreateTab.Controls.Add(this.JPModStyleName);
            this.CreateTab.Controls.Add(this.label7);
            this.CreateTab.Controls.Add(this.ReplacementFileName);
            this.CreateTab.Controls.Add(this.BrowseForReplacement);
            this.CreateTab.Controls.Add(this.AddReplacementButton);
            this.CreateTab.Controls.Add(this.UndoLast);
            this.CreateTab.Controls.Add(this.label2);
            this.CreateTab.Controls.Add(this.SpecificVersion);
            this.CreateTab.Controls.Add(this.label8);
            this.CreateTab.Controls.Add(this.AddedFilesView);
            this.CreateTab.Controls.Add(this.label9);
            this.CreateTab.Controls.Add(this.ModNameEdit);
            this.CreateTab.Controls.Add(this.label10);
            this.CreateTab.Controls.Add(this.ModAuthorEdit);
            this.CreateTab.Controls.Add(this.label11);
            this.CreateTab.Controls.Add(this.ModDescriptionEdit);
            this.CreateTab.Controls.Add(this.CreateModButton);
            this.CreateTab.Controls.Add(this.savePDMod);
            this.CreateTab.Controls.Add(this.createModLoad);
            this.CreateTab.Controls.Add(this.ConfigureReplacementButton);
            this.CreateTab.Location = new System.Drawing.Point(4, 22);
            this.CreateTab.Name = "CreateTab";
            this.CreateTab.Padding = new System.Windows.Forms.Padding(3);
            this.CreateTab.Size = new System.Drawing.Size(792, 471);
            this.CreateTab.TabIndex = 1;
            this.CreateTab.Text = "Create Mod";
            this.CreateTab.UseVisualStyleBackColor = true;
            // 
            // selectedPackage
            // 
            this.selectedPackage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedPackage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.selectedPackage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.selectedPackage.FormattingEnabled = true;
            this.selectedPackage.Location = new System.Drawing.Point(409, 6);
            this.selectedPackage.Name = "selectedPackage";
            this.selectedPackage.Size = new System.Drawing.Size(369, 21);
            this.selectedPackage.TabIndex = 51;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(345, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 50;
            this.label12.Text = "Packages:";
            // 
            // editModOptions_button
            // 
            this.editModOptions_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editModOptions_button.Location = new System.Drawing.Point(369, 439);
            this.editModOptions_button.Name = "editModOptions_button";
            this.editModOptions_button.Size = new System.Drawing.Size(110, 23);
            this.editModOptions_button.TabIndex = 49;
            this.editModOptions_button.Text = "Edit Mod Options";
            this.editModOptions_button.UseVisualStyleBackColor = true;
            this.editModOptions_button.Click += new System.EventHandler(this.modConfiguration_button_Click);
            // 
            // modVariables_button
            // 
            this.modVariables_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.modVariables_button.Location = new System.Drawing.Point(250, 439);
            this.modVariables_button.Name = "modVariables_button";
            this.modVariables_button.Size = new System.Drawing.Size(114, 23);
            this.modVariables_button.TabIndex = 48;
            this.modVariables_button.Text = "Mod Variables";
            this.modVariables_button.UseVisualStyleBackColor = true;
            this.modVariables_button.Click += new System.EventHandler(this.modVariables_button_Click);
            // 
            // fileReplacementType_ComboBox
            // 
            this.fileReplacementType_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fileReplacementType_ComboBox.FormattingEnabled = true;
            this.fileReplacementType_ComboBox.Items.AddRange(new object[] {
            "File Replacement"});
            this.fileReplacementType_ComboBox.Location = new System.Drawing.Point(101, 6);
            this.fileReplacementType_ComboBox.Name = "fileReplacementType_ComboBox";
            this.fileReplacementType_ComboBox.Size = new System.Drawing.Size(231, 21);
            this.fileReplacementType_ComboBox.TabIndex = 47;
            this.fileReplacementType_ComboBox.SelectedIndexChanged += new System.EventHandler(this.fileReplacementType_ComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Replacement Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 34);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Bundle File Name";
            // 
            // BundleFileName
            // 
            this.BundleFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BundleFileName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.BundleFileName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.BundleFileName.Location = new System.Drawing.Point(101, 31);
            this.BundleFileName.Name = "BundleFileName";
            this.BundleFileName.Size = new System.Drawing.Size(565, 20);
            this.BundleFileName.TabIndex = 24;
            this.BundleFileName.TextChanged += new System.EventHandler(this.BundleFileName_TextChanged);
            this.BundleFileName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BundleFileName_KeyPress);
            // 
            // JPModStyleName
            // 
            this.JPModStyleName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JPModStyleName.AutoSize = true;
            this.JPModStyleName.Location = new System.Drawing.Point(671, 33);
            this.JPModStyleName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.JPModStyleName.Name = "JPModStyleName";
            this.JPModStyleName.Size = new System.Drawing.Size(116, 17);
            this.JPModStyleName.TabIndex = 25;
            this.JPModStyleName.Text = "JPMod Style Name";
            this.JPModStyleName.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 60);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Replacement File";
            // 
            // ReplacementFileName
            // 
            this.ReplacementFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplacementFileName.Location = new System.Drawing.Point(101, 57);
            this.ReplacementFileName.Name = "ReplacementFileName";
            this.ReplacementFileName.ReadOnly = true;
            this.ReplacementFileName.Size = new System.Drawing.Size(564, 20);
            this.ReplacementFileName.TabIndex = 27;
            this.ReplacementFileName.TextChanged += new System.EventHandler(this.ReplacementFileName_TextChanged);
            // 
            // BrowseForReplacement
            // 
            this.BrowseForReplacement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseForReplacement.AutoSize = true;
            this.BrowseForReplacement.Location = new System.Drawing.Point(670, 55);
            this.BrowseForReplacement.Name = "BrowseForReplacement";
            this.BrowseForReplacement.Size = new System.Drawing.Size(72, 23);
            this.BrowseForReplacement.TabIndex = 28;
            this.BrowseForReplacement.Text = "Browse...";
            this.BrowseForReplacement.UseVisualStyleBackColor = true;
            this.BrowseForReplacement.Click += new System.EventHandler(this.BrowseForReplacementClick);
            // 
            // AddReplacementButton
            // 
            this.AddReplacementButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddReplacementButton.AutoSize = true;
            this.AddReplacementButton.Enabled = false;
            this.AddReplacementButton.Location = new System.Drawing.Point(429, 83);
            this.AddReplacementButton.Name = "AddReplacementButton";
            this.AddReplacementButton.Size = new System.Drawing.Size(153, 23);
            this.AddReplacementButton.TabIndex = 30;
            this.AddReplacementButton.Text = "Add Replacement To Mod";
            this.AddReplacementButton.UseVisualStyleBackColor = true;
            this.AddReplacementButton.Click += new System.EventHandler(this.AddReplacementButtonClick);
            // 
            // UndoLast
            // 
            this.UndoLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UndoLast.AutoSize = true;
            this.UndoLast.Location = new System.Drawing.Point(587, 83);
            this.UndoLast.Name = "UndoLast";
            this.UndoLast.Size = new System.Drawing.Size(190, 23);
            this.UndoLast.TabIndex = 40;
            this.UndoLast.Text = "Remove Selected Replacement(s)";
            this.UndoLast.UseVisualStyleBackColor = true;
            this.UndoLast.Click += new System.EventHandler(this.UndoLastClick);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(98, 444);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "For Version";
            // 
            // SpecificVersion
            // 
            this.SpecificVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SpecificVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SpecificVersion.FormattingEnabled = true;
            this.SpecificVersion.Items.AddRange(new object[] {
            "1.15",
            "1.16"});
            this.SpecificVersion.Location = new System.Drawing.Point(164, 440);
            this.SpecificVersion.Name = "SpecificVersion";
            this.SpecificVersion.Size = new System.Drawing.Size(71, 21);
            this.SpecificVersion.TabIndex = 44;
            this.SpecificVersion.Tag = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 93);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Replacement Files";
            // 
            // AddedFilesView
            // 
            this.AddedFilesView.AllowUserToAddRows = false;
            this.AddedFilesView.AllowUserToDeleteRows = false;
            this.AddedFilesView.AllowUserToResizeRows = false;
            this.AddedFilesView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddedFilesView.AutoGenerateColumns = false;
            this.AddedFilesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AddedFilesView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewCheckBoxColumn1});
            this.AddedFilesView.DataSource = this.bundleRewriteItemBindingSource;
            this.AddedFilesView.Location = new System.Drawing.Point(6, 112);
            this.AddedFilesView.MinimumSize = new System.Drawing.Size(0, 85);
            this.AddedFilesView.Name = "AddedFilesView";
            this.AddedFilesView.ReadOnly = true;
            this.AddedFilesView.RowHeadersVisible = false;
            this.AddedFilesView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.AddedFilesView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AddedFilesView.Size = new System.Drawing.Size(772, 210);
            this.AddedFilesView.TabIndex = 32;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "SourceFile";
            this.dataGridViewTextBoxColumn1.HeaderText = "Source File";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 340;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ReplacementFile";
            this.dataGridViewTextBoxColumn2.HeaderText = "Replacement File";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 340;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "hasVariables";
            this.dataGridViewCheckBoxColumn1.HeaderText = "has Variables";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxColumn1.Width = 88;
            // 
            // bundleRewriteItemBindingSource
            // 
            this.bundleRewriteItemBindingSource.DataSource = typeof(PDBundleModPatcher.BundleRewriteItem);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 332);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "Mod Name";
            // 
            // ModNameEdit
            // 
            this.ModNameEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ModNameEdit.Location = new System.Drawing.Point(72, 328);
            this.ModNameEdit.Name = "ModNameEdit";
            this.ModNameEdit.Size = new System.Drawing.Size(316, 20);
            this.ModNameEdit.TabIndex = 34;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(393, 331);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 35;
            this.label10.Text = "Mod Author";
            // 
            // ModAuthorEdit
            // 
            this.ModAuthorEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModAuthorEdit.Location = new System.Drawing.Point(461, 328);
            this.ModAuthorEdit.Name = "ModAuthorEdit";
            this.ModAuthorEdit.Size = new System.Drawing.Size(317, 20);
            this.ModAuthorEdit.TabIndex = 36;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 353);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 0, 3, 40);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label11.Size = new System.Drawing.Size(60, 16);
            this.label11.TabIndex = 37;
            this.label11.Text = "Description";
            // 
            // ModDescriptionEdit
            // 
            this.ModDescriptionEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModDescriptionEdit.Location = new System.Drawing.Point(72, 353);
            this.ModDescriptionEdit.Multiline = true;
            this.ModDescriptionEdit.Name = "ModDescriptionEdit";
            this.ModDescriptionEdit.Size = new System.Drawing.Size(706, 80);
            this.ModDescriptionEdit.TabIndex = 38;
            // 
            // CreateModButton
            // 
            this.CreateModButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CreateModButton.AutoSize = true;
            this.CreateModButton.Enabled = false;
            this.CreateModButton.Location = new System.Drawing.Point(6, 439);
            this.CreateModButton.Name = "CreateModButton";
            this.CreateModButton.Size = new System.Drawing.Size(83, 23);
            this.CreateModButton.TabIndex = 39;
            this.CreateModButton.Text = "Create Mod";
            this.CreateModButton.UseVisualStyleBackColor = true;
            this.CreateModButton.Click += new System.EventHandler(this.CreateModButtonClick);
            // 
            // savePDMod
            // 
            this.savePDMod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.savePDMod.AutoSize = true;
            this.savePDMod.Location = new System.Drawing.Point(517, 439);
            this.savePDMod.Name = "savePDMod";
            this.savePDMod.Size = new System.Drawing.Size(128, 23);
            this.savePDMod.TabIndex = 42;
            this.savePDMod.Text = "Save PDMod Project";
            this.savePDMod.UseVisualStyleBackColor = true;
            this.savePDMod.Click += new System.EventHandler(this.savePDMod_Click);
            // 
            // createModLoad
            // 
            this.createModLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createModLoad.AutoSize = true;
            this.createModLoad.Location = new System.Drawing.Point(650, 439);
            this.createModLoad.Name = "createModLoad";
            this.createModLoad.Size = new System.Drawing.Size(128, 23);
            this.createModLoad.TabIndex = 41;
            this.createModLoad.Text = "Load PDMod Project";
            this.createModLoad.UseVisualStyleBackColor = true;
            this.createModLoad.Click += new System.EventHandler(this.createModLoad_Click);
            // 
            // ConfigureReplacementButton
            // 
            this.ConfigureReplacementButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigureReplacementButton.Location = new System.Drawing.Point(671, 55);
            this.ConfigureReplacementButton.Name = "ConfigureReplacementButton";
            this.ConfigureReplacementButton.Size = new System.Drawing.Size(86, 23);
            this.ConfigureReplacementButton.TabIndex = 52;
            this.ConfigureReplacementButton.Text = "Configure";
            this.ConfigureReplacementButton.UseVisualStyleBackColor = true;
            this.ConfigureReplacementButton.Visible = false;
            this.ConfigureReplacementButton.Click += new System.EventHandler(this.ConfigureReplacementButton_Click);
            // 
            // OptionsTab
            // 
            this.OptionsTab.Controls.Add(this.OptionsFlowLayout);
            this.OptionsTab.Location = new System.Drawing.Point(4, 22);
            this.OptionsTab.Name = "OptionsTab";
            this.OptionsTab.Size = new System.Drawing.Size(792, 471);
            this.OptionsTab.TabIndex = 2;
            this.OptionsTab.Text = "Options/About";
            this.OptionsTab.UseVisualStyleBackColor = true;
            // 
            // OptionsFlowLayout
            // 
            this.OptionsFlowLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OptionsFlowLayout.Controls.Add(this.GameAssetFolderLabel);
            this.OptionsFlowLayout.Controls.Add(this.AssetFolderEdit);
            this.OptionsFlowLayout.Controls.Add(this.AssetFolderButton);
            this.OptionsFlowLayout.Controls.Add(this.backupMethodLabel);
            this.OptionsFlowLayout.Controls.Add(this.backupMethodComboBox);
            this.OptionsFlowLayout.Controls.Add(this.label13);
            this.OptionsFlowLayout.Controls.Add(this.patchingBufferSize);
            this.OptionsFlowLayout.Controls.Add(this.useOverrideFolderCheckbox);
            this.OptionsFlowLayout.Controls.Add(this.createOverrideFolderDummies);
            this.OptionsFlowLayout.Controls.Add(this.useSharedPDMODTOOLfolder);
            this.OptionsFlowLayout.Controls.Add(this.ThemeSelectionLabel);
            this.OptionsFlowLayout.Controls.Add(this.ThemeSelection);
            this.OptionsFlowLayout.Controls.Add(this.checkBox4);
            this.OptionsFlowLayout.Controls.Add(this.bundleFilePathAutotompletion_checkbox);
            this.OptionsFlowLayout.Controls.Add(this.WriteConsole);
            this.OptionsFlowLayout.Controls.Add(this.playsoundoncompletion_checkbox);
            this.OptionsFlowLayout.Controls.Add(this.checkBoxOverrideShow);
            this.OptionsFlowLayout.Controls.Add(this.rungameoncompletion_checkbox);
            this.OptionsFlowLayout.Controls.Add(this.checkupdatesonstartup_checkbox);
            this.OptionsFlowLayout.Controls.Add(this.emptylabeltofixflowlayout);
            this.OptionsFlowLayout.Controls.Add(this.checkForUpdates_Button);
            this.OptionsFlowLayout.Controls.Add(this.programIcon);
            this.OptionsFlowLayout.Controls.Add(this.AboutLabel);
            this.OptionsFlowLayout.Location = new System.Drawing.Point(0, 3);
            this.OptionsFlowLayout.Name = "OptionsFlowLayout";
            this.OptionsFlowLayout.Size = new System.Drawing.Size(789, 465);
            this.OptionsFlowLayout.TabIndex = 0;
            // 
            // GameAssetFolderLabel
            // 
            this.GameAssetFolderLabel.AutoSize = true;
            this.GameAssetFolderLabel.Location = new System.Drawing.Point(10, 10);
            this.GameAssetFolderLabel.Margin = new System.Windows.Forms.Padding(10, 10, 5, 0);
            this.GameAssetFolderLabel.Name = "GameAssetFolderLabel";
            this.GameAssetFolderLabel.Size = new System.Drawing.Size(96, 13);
            this.GameAssetFolderLabel.TabIndex = 0;
            this.GameAssetFolderLabel.Text = "Game Asset Folder";
            this.GameAssetFolderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AssetFolderEdit
            // 
            this.AssetFolderEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AssetFolderEdit.Location = new System.Drawing.Point(111, 6);
            this.AssetFolderEdit.Margin = new System.Windows.Forms.Padding(0, 6, 3, 3);
            this.AssetFolderEdit.Name = "AssetFolderEdit";
            this.AssetFolderEdit.ReadOnly = true;
            this.AssetFolderEdit.Size = new System.Drawing.Size(570, 20);
            this.AssetFolderEdit.TabIndex = 1;
            this.AssetFolderEdit.WordWrap = false;
            // 
            // AssetFolderButton
            // 
            this.AssetFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OptionsFlowLayout.SetFlowBreak(this.AssetFolderButton, true);
            this.AssetFolderButton.Location = new System.Drawing.Point(684, 6);
            this.AssetFolderButton.Margin = new System.Windows.Forms.Padding(0, 6, 3, 3);
            this.AssetFolderButton.Name = "AssetFolderButton";
            this.AssetFolderButton.Size = new System.Drawing.Size(70, 23);
            this.AssetFolderButton.TabIndex = 2;
            this.AssetFolderButton.Text = "Browse...";
            this.AssetFolderButton.UseVisualStyleBackColor = true;
            this.AssetFolderButton.Click += new System.EventHandler(this.AssetFolderButtonClick);
            // 
            // backupMethodLabel
            // 
            this.backupMethodLabel.AutoSize = true;
            this.backupMethodLabel.Location = new System.Drawing.Point(10, 37);
            this.backupMethodLabel.Margin = new System.Windows.Forms.Padding(10, 5, 5, 0);
            this.backupMethodLabel.Name = "backupMethodLabel";
            this.backupMethodLabel.Size = new System.Drawing.Size(83, 13);
            this.backupMethodLabel.TabIndex = 4;
            this.backupMethodLabel.Text = "Backup Method";
            // 
            // backupMethodComboBox
            // 
            this.backupMethodComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.backupMethodComboBox.Items.AddRange(new object[] {
            "Bundles",
            "Bundle Entries"});
            this.backupMethodComboBox.Location = new System.Drawing.Point(98, 32);
            this.backupMethodComboBox.Margin = new System.Windows.Forms.Padding(0);
            this.backupMethodComboBox.Name = "backupMethodComboBox";
            this.backupMethodComboBox.Size = new System.Drawing.Size(121, 21);
            this.backupMethodComboBox.TabIndex = 0;
            this.backupMethodComboBox.SelectedIndexChanged += new System.EventHandler(this.backupMethodComboBox_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(229, 37);
            this.label13.Margin = new System.Windows.Forms.Padding(10, 5, 5, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "Buffer Size";
            this.toolTips.SetToolTip(this.label13, "Affects memory usage and patching speed");
            // 
            // patchingBufferSize
            // 
            this.patchingBufferSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OptionsFlowLayout.SetFlowBreak(this.patchingBufferSize, true);
            this.patchingBufferSize.Items.AddRange(new object[] {
            "16384",
            "8192",
            "4096",
            "2048",
            "1024",
            "512",
            "256"});
            this.patchingBufferSize.Location = new System.Drawing.Point(292, 32);
            this.patchingBufferSize.Margin = new System.Windows.Forms.Padding(0);
            this.patchingBufferSize.Name = "patchingBufferSize";
            this.patchingBufferSize.Size = new System.Drawing.Size(121, 21);
            this.patchingBufferSize.TabIndex = 12;
            this.patchingBufferSize.SelectedIndexChanged += new System.EventHandler(this.patchingBufferSize_SelectedIndexChanged);
            // 
            // useOverrideFolderCheckbox
            // 
            this.useOverrideFolderCheckbox.AutoSize = true;
            this.useOverrideFolderCheckbox.Checked = true;
            this.useOverrideFolderCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useOverrideFolderCheckbox.Location = new System.Drawing.Point(10, 58);
            this.useOverrideFolderCheckbox.Margin = new System.Windows.Forms.Padding(10, 5, 5, 0);
            this.useOverrideFolderCheckbox.Name = "useOverrideFolderCheckbox";
            this.useOverrideFolderCheckbox.Size = new System.Drawing.Size(115, 17);
            this.useOverrideFolderCheckbox.TabIndex = 7;
            this.useOverrideFolderCheckbox.Text = "Use override folder";
            this.useOverrideFolderCheckbox.UseVisualStyleBackColor = true;
            this.useOverrideFolderCheckbox.CheckedChanged += new System.EventHandler(this.useOverrideFolderCheckbox_CheckedChanged);
            // 
            // createOverrideFolderDummies
            // 
            this.createOverrideFolderDummies.AutoSize = true;
            this.createOverrideFolderDummies.Checked = true;
            this.createOverrideFolderDummies.CheckState = System.Windows.Forms.CheckState.Checked;
            this.createOverrideFolderDummies.Location = new System.Drawing.Point(140, 58);
            this.createOverrideFolderDummies.Margin = new System.Windows.Forms.Padding(10, 5, 5, 0);
            this.createOverrideFolderDummies.Name = "createOverrideFolderDummies";
            this.createOverrideFolderDummies.Size = new System.Drawing.Size(282, 17);
            this.createOverrideFolderDummies.TabIndex = 8;
            this.createOverrideFolderDummies.Text = "Create override folder dummies (for incompatible mods)";
            this.createOverrideFolderDummies.UseVisualStyleBackColor = true;
            this.createOverrideFolderDummies.CheckedChanged += new System.EventHandler(this.createOverrideFolderDummies_CheckedChanged);
            // 
            // useSharedPDMODTOOLfolder
            // 
            this.useSharedPDMODTOOLfolder.AutoSize = true;
            this.useSharedPDMODTOOLfolder.Checked = true;
            this.useSharedPDMODTOOLfolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OptionsFlowLayout.SetFlowBreak(this.useSharedPDMODTOOLfolder, true);
            this.useSharedPDMODTOOLfolder.Location = new System.Drawing.Point(437, 58);
            this.useSharedPDMODTOOLfolder.Margin = new System.Windows.Forms.Padding(10, 5, 5, 0);
            this.useSharedPDMODTOOLfolder.Name = "useSharedPDMODTOOLfolder";
            this.useSharedPDMODTOOLfolder.Size = new System.Drawing.Size(261, 17);
            this.useSharedPDMODTOOLfolder.TabIndex = 9;
            this.useSharedPDMODTOOLfolder.Text = "Use shared pdmodtool folder (for conflicting mods)";
            this.useSharedPDMODTOOLfolder.UseVisualStyleBackColor = true;
            this.useSharedPDMODTOOLfolder.CheckedChanged += new System.EventHandler(this.useSharedPDMODTOOLfolder_CheckedChanged);
            // 
            // ThemeSelectionLabel
            // 
            this.ThemeSelectionLabel.AutoSize = true;
            this.ThemeSelectionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ThemeSelectionLabel.Location = new System.Drawing.Point(3, 75);
            this.ThemeSelectionLabel.Name = "ThemeSelectionLabel";
            this.ThemeSelectionLabel.Size = new System.Drawing.Size(40, 27);
            this.ThemeSelectionLabel.TabIndex = 17;
            this.ThemeSelectionLabel.Text = "Theme";
            this.ThemeSelectionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ThemeSelectionLabel.Visible = false;
            // 
            // ThemeSelection
            // 
            this.ThemeSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ThemeSelection.FormattingEnabled = true;
            this.ThemeSelection.Items.AddRange(new object[] {
            "Default",
            "Dark",
            "LastBullet"});
            this.ThemeSelection.Location = new System.Drawing.Point(49, 78);
            this.ThemeSelection.Name = "ThemeSelection";
            this.ThemeSelection.Size = new System.Drawing.Size(121, 21);
            this.ThemeSelection.TabIndex = 16;
            this.ThemeSelection.Visible = false;
            this.ThemeSelection.SelectedIndexChanged += new System.EventHandler(this.ThemSelectionChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.OptionsFlowLayout.SetFlowBreak(this.checkBox4, true);
            this.checkBox4.Location = new System.Drawing.Point(183, 80);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(10, 5, 5, 0);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(275, 17);
            this.checkBox4.TabIndex = 5;
            this.checkBox4.Text = "Check bundles for corruption before each mod patch";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.Visible = false;
            // 
            // bundleFilePathAutotompletion_checkbox
            // 
            this.bundleFilePathAutotompletion_checkbox.AutoSize = true;
            this.bundleFilePathAutotompletion_checkbox.Checked = true;
            this.bundleFilePathAutotompletion_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bundleFilePathAutotompletion_checkbox.Location = new System.Drawing.Point(10, 107);
            this.bundleFilePathAutotompletion_checkbox.Margin = new System.Windows.Forms.Padding(10, 5, 5, 0);
            this.bundleFilePathAutotompletion_checkbox.Name = "bundleFilePathAutotompletion_checkbox";
            this.bundleFilePathAutotompletion_checkbox.Size = new System.Drawing.Size(134, 17);
            this.bundleFilePathAutotompletion_checkbox.TabIndex = 6;
            this.bundleFilePathAutotompletion_checkbox.Text = "Enable autocompletion";
            this.bundleFilePathAutotompletion_checkbox.UseVisualStyleBackColor = true;
            this.bundleFilePathAutotompletion_checkbox.CheckedChanged += new System.EventHandler(this.bundleFilePathAutotompletion_checkbox_CheckedChanged);
            // 
            // WriteConsole
            // 
            this.WriteConsole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WriteConsole.AutoSize = true;
            this.WriteConsole.BackColor = System.Drawing.Color.Transparent;
            this.WriteConsole.Location = new System.Drawing.Point(152, 105);
            this.WriteConsole.Name = "WriteConsole";
            this.WriteConsole.Size = new System.Drawing.Size(123, 17);
            this.WriteConsole.TabIndex = 47;
            this.WriteConsole.Text = "Write Console to File";
            this.WriteConsole.UseVisualStyleBackColor = true;
            this.WriteConsole.CheckedChanged += new System.EventHandler(this.WriteConsoleCheckChanged);
            // 
            // playsoundoncompletion_checkbox
            // 
            this.playsoundoncompletion_checkbox.AutoSize = true;
            this.playsoundoncompletion_checkbox.Checked = true;
            this.playsoundoncompletion_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.playsoundoncompletion_checkbox.Location = new System.Drawing.Point(288, 107);
            this.playsoundoncompletion_checkbox.Margin = new System.Windows.Forms.Padding(10, 5, 5, 0);
            this.playsoundoncompletion_checkbox.Name = "playsoundoncompletion_checkbox";
            this.playsoundoncompletion_checkbox.Size = new System.Drawing.Size(221, 17);
            this.playsoundoncompletion_checkbox.TabIndex = 10;
            this.playsoundoncompletion_checkbox.Text = "Play a sound upon mod patch completion";
            this.playsoundoncompletion_checkbox.UseVisualStyleBackColor = true;
            this.playsoundoncompletion_checkbox.CheckedChanged += new System.EventHandler(this.playsoundoncompletion_checkbox_CheckedChanged);
            // 
            // checkBoxOverrideShow
            // 
            this.checkBoxOverrideShow.AutoSize = true;
            this.checkBoxOverrideShow.Checked = true;
            this.checkBoxOverrideShow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOverrideShow.Location = new System.Drawing.Point(524, 107);
            this.checkBoxOverrideShow.Margin = new System.Windows.Forms.Padding(10, 5, 5, 0);
            this.checkBoxOverrideShow.Name = "checkBoxOverrideShow";
            this.checkBoxOverrideShow.Size = new System.Drawing.Size(122, 17);
            this.checkBoxOverrideShow.TabIndex = 15;
            this.checkBoxOverrideShow.Text = "Show override mods";
            this.checkBoxOverrideShow.UseVisualStyleBackColor = true;
            this.checkBoxOverrideShow.CheckedChanged += new System.EventHandler(this.ShowOverrideMods_CheckedChanged);
            // 
            // rungameoncompletion_checkbox
            // 
            this.rungameoncompletion_checkbox.AutoSize = true;
            this.rungameoncompletion_checkbox.Location = new System.Drawing.Point(10, 130);
            this.rungameoncompletion_checkbox.Margin = new System.Windows.Forms.Padding(10, 5, 5, 0);
            this.rungameoncompletion_checkbox.Name = "rungameoncompletion_checkbox";
            this.rungameoncompletion_checkbox.Size = new System.Drawing.Size(209, 17);
            this.rungameoncompletion_checkbox.TabIndex = 11;
            this.rungameoncompletion_checkbox.Text = "Run game upon mod patch completion";
            this.rungameoncompletion_checkbox.UseVisualStyleBackColor = true;
            this.rungameoncompletion_checkbox.CheckedChanged += new System.EventHandler(this.rungameoncompletion_checkbox_CheckedChanged);
            // 
            // checkupdatesonstartup_checkbox
            // 
            this.checkupdatesonstartup_checkbox.AutoSize = true;
            this.checkupdatesonstartup_checkbox.Location = new System.Drawing.Point(234, 130);
            this.checkupdatesonstartup_checkbox.Margin = new System.Windows.Forms.Padding(10, 5, 5, 0);
            this.checkupdatesonstartup_checkbox.Name = "checkupdatesonstartup_checkbox";
            this.checkupdatesonstartup_checkbox.Size = new System.Drawing.Size(163, 17);
            this.checkupdatesonstartup_checkbox.TabIndex = 15;
            this.checkupdatesonstartup_checkbox.Text = "Check for updates on startup";
            this.checkupdatesonstartup_checkbox.UseVisualStyleBackColor = true;
            this.checkupdatesonstartup_checkbox.CheckedChanged += new System.EventHandler(this.checkupdatesonstartup_checkbox_CheckedChanged);
            // 
            // emptylabeltofixflowlayout
            // 
            this.emptylabeltofixflowlayout.AutoSize = true;
            this.emptylabeltofixflowlayout.Location = new System.Drawing.Point(405, 125);
            this.emptylabeltofixflowlayout.Name = "emptylabeltofixflowlayout";
            this.emptylabeltofixflowlayout.Size = new System.Drawing.Size(0, 13);
            this.emptylabeltofixflowlayout.TabIndex = 16;
            // 
            // checkForUpdates_Button
            // 
            this.checkForUpdates_Button.AutoSize = true;
            this.OptionsFlowLayout.SetFlowBreak(this.checkForUpdates_Button, true);
            this.checkForUpdates_Button.Location = new System.Drawing.Point(408, 128);
            this.checkForUpdates_Button.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.checkForUpdates_Button.Name = "checkForUpdates_Button";
            this.checkForUpdates_Button.Size = new System.Drawing.Size(106, 23);
            this.checkForUpdates_Button.TabIndex = 17;
            this.checkForUpdates_Button.Text = "Check for Updates";
            this.checkForUpdates_Button.UseVisualStyleBackColor = true;
            this.checkForUpdates_Button.Click += new System.EventHandler(this.checkForUpdates_Button_Click);
            // 
            // programIcon
            // 
            this.programIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.programIcon.Image = global::PDBundleModPatcher.Properties.Resources.icon_256x256;
            this.programIcon.InitialImage = global::PDBundleModPatcher.Properties.Resources.icon_256x256;
            this.programIcon.Location = new System.Drawing.Point(3, 157);
            this.programIcon.Name = "programIcon";
            this.programIcon.Size = new System.Drawing.Size(256, 256);
            this.programIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.programIcon.TabIndex = 14;
            this.programIcon.TabStop = false;
            // 
            // AboutLabel
            // 
            this.AboutLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AboutLabel.LinkArea = new System.Windows.Forms.LinkArea(102, 32);
            this.AboutLabel.Location = new System.Drawing.Point(265, 154);
            this.AboutLabel.MinimumSize = new System.Drawing.Size(0, 200);
            this.AboutLabel.Name = "AboutLabel";
            this.AboutLabel.Size = new System.Drawing.Size(511, 262);
            this.AboutLabel.TabIndex = 3;
            this.AboutLabel.TabStop = true;
            this.AboutLabel.Text = "------\r\nPAYDAY 2 Bundle Modder {0}\r\nBy I am not a spy..., Zwagoth and GREAT BIG B" +
    "USHY BEARD\r\n------\r\nhttp://www.bitbucket.org/zabb65/\r\n------";
            this.AboutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AboutLabel.UseCompatibleTextRendering = true;
            this.AboutLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AboutLabelLinkClicked);
            // 
            // UtilitiesTab
            // 
            this.UtilitiesTab.Controls.Add(this.corruptedBundlesGroup);
            this.UtilitiesTab.Controls.Add(this.hashConverterGroup);
            this.UtilitiesTab.Location = new System.Drawing.Point(4, 22);
            this.UtilitiesTab.Name = "UtilitiesTab";
            this.UtilitiesTab.Padding = new System.Windows.Forms.Padding(3);
            this.UtilitiesTab.Size = new System.Drawing.Size(792, 471);
            this.UtilitiesTab.TabIndex = 5;
            this.UtilitiesTab.Text = "Utilities";
            this.UtilitiesTab.UseVisualStyleBackColor = true;
            // 
            // corruptedBundlesGroup
            // 
            this.corruptedBundlesGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.corruptedBundlesGroup.Controls.Add(this.attemptRepair_button);
            this.corruptedBundlesGroup.Controls.Add(this.corruptedShowOnlyCorrupted_checkbox);
            this.corruptedBundlesGroup.Controls.Add(this.corruptedBundlesReport_richTextBox);
            this.corruptedBundlesGroup.Controls.Add(this.progressTextCorruptedBundlesCheck);
            this.corruptedBundlesGroup.Controls.Add(this.progressCorruptedBundlesCheck);
            this.corruptedBundlesGroup.Controls.Add(this.verifyCorruptedBundlesCheckCheckBox);
            this.corruptedBundlesGroup.Controls.Add(this.runCorruptedCheckButton);
            this.corruptedBundlesGroup.Location = new System.Drawing.Point(6, 84);
            this.corruptedBundlesGroup.Name = "corruptedBundlesGroup";
            this.corruptedBundlesGroup.Size = new System.Drawing.Size(780, 381);
            this.corruptedBundlesGroup.TabIndex = 1;
            this.corruptedBundlesGroup.TabStop = false;
            this.corruptedBundlesGroup.Text = "Corrupted Bundles Check";
            // 
            // attemptRepair_button
            // 
            this.attemptRepair_button.Location = new System.Drawing.Point(587, 42);
            this.attemptRepair_button.Name = "attemptRepair_button";
            this.attemptRepair_button.Size = new System.Drawing.Size(187, 23);
            this.attemptRepair_button.TabIndex = 8;
            this.attemptRepair_button.Text = "Attempt Repairs [Not-Implemented]";
            this.attemptRepair_button.UseVisualStyleBackColor = true;
            this.attemptRepair_button.Visible = false;
            // 
            // corruptedShowOnlyCorrupted_checkbox
            // 
            this.corruptedShowOnlyCorrupted_checkbox.AutoSize = true;
            this.corruptedShowOnlyCorrupted_checkbox.Checked = true;
            this.corruptedShowOnlyCorrupted_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.corruptedShowOnlyCorrupted_checkbox.Location = new System.Drawing.Point(182, 19);
            this.corruptedShowOnlyCorrupted_checkbox.Name = "corruptedShowOnlyCorrupted_checkbox";
            this.corruptedShowOnlyCorrupted_checkbox.Size = new System.Drawing.Size(163, 17);
            this.corruptedShowOnlyCorrupted_checkbox.TabIndex = 7;
            this.corruptedShowOnlyCorrupted_checkbox.Text = "Only show corrupted bundles";
            this.corruptedShowOnlyCorrupted_checkbox.UseVisualStyleBackColor = true;
            this.corruptedShowOnlyCorrupted_checkbox.CheckedChanged += new System.EventHandler(this.corruptedShowOnlyCorrupted_checkbox_CheckedChanged);
            // 
            // corruptedBundlesReport_richTextBox
            // 
            this.corruptedBundlesReport_richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.corruptedBundlesReport_richTextBox.Location = new System.Drawing.Point(6, 71);
            this.corruptedBundlesReport_richTextBox.Name = "corruptedBundlesReport_richTextBox";
            this.corruptedBundlesReport_richTextBox.ReadOnly = true;
            this.corruptedBundlesReport_richTextBox.Size = new System.Drawing.Size(766, 304);
            this.corruptedBundlesReport_richTextBox.TabIndex = 6;
            this.corruptedBundlesReport_richTextBox.Text = "";
            // 
            // progressTextCorruptedBundlesCheck
            // 
            this.progressTextCorruptedBundlesCheck.AutoSize = true;
            this.progressTextCorruptedBundlesCheck.Location = new System.Drawing.Point(298, 47);
            this.progressTextCorruptedBundlesCheck.Name = "progressTextCorruptedBundlesCheck";
            this.progressTextCorruptedBundlesCheck.Size = new System.Drawing.Size(169, 13);
            this.progressTextCorruptedBundlesCheck.TabIndex = 4;
            this.progressTextCorruptedBundlesCheck.Text = "Current Progress: 0/0 Corrupted: 0";
            // 
            // progressCorruptedBundlesCheck
            // 
            this.progressCorruptedBundlesCheck.Location = new System.Drawing.Point(69, 42);
            this.progressCorruptedBundlesCheck.Name = "progressCorruptedBundlesCheck";
            this.progressCorruptedBundlesCheck.Size = new System.Drawing.Size(225, 23);
            this.progressCorruptedBundlesCheck.TabIndex = 3;
            // 
            // verifyCorruptedBundlesCheckCheckBox
            // 
            this.verifyCorruptedBundlesCheckCheckBox.AutoSize = true;
            this.verifyCorruptedBundlesCheckCheckBox.Location = new System.Drawing.Point(9, 19);
            this.verifyCorruptedBundlesCheckCheckBox.Name = "verifyCorruptedBundlesCheckCheckBox";
            this.verifyCorruptedBundlesCheckCheckBox.Size = new System.Drawing.Size(167, 17);
            this.verifyCorruptedBundlesCheckCheckBox.TabIndex = 2;
            this.verifyCorruptedBundlesCheckCheckBox.Text = "Verify game cache after repair";
            this.verifyCorruptedBundlesCheckCheckBox.UseVisualStyleBackColor = true;
            // 
            // runCorruptedCheckButton
            // 
            this.runCorruptedCheckButton.Location = new System.Drawing.Point(6, 42);
            this.runCorruptedCheckButton.Name = "runCorruptedCheckButton";
            this.runCorruptedCheckButton.Size = new System.Drawing.Size(57, 23);
            this.runCorruptedCheckButton.TabIndex = 0;
            this.runCorruptedCheckButton.Text = "Run";
            this.runCorruptedCheckButton.UseVisualStyleBackColor = true;
            this.runCorruptedCheckButton.Click += new System.EventHandler(this.runCorruptedCheckButton_Click);
            // 
            // hashConverterGroup
            // 
            this.hashConverterGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hashConverterGroup.Controls.Add(this.hashSwapHexEndiannessCheckbox);
            this.hashConverterGroup.Controls.Add(this.hashUseHexcheckBox);
            this.hashConverterGroup.Controls.Add(this.unHashButton);
            this.hashConverterGroup.Controls.Add(this.hashHashBox);
            this.hashConverterGroup.Controls.Add(this.label15);
            this.hashConverterGroup.Controls.Add(this.hashButton);
            this.hashConverterGroup.Controls.Add(this.label14);
            this.hashConverterGroup.Controls.Add(this.hashTextBox);
            this.hashConverterGroup.Location = new System.Drawing.Point(6, 6);
            this.hashConverterGroup.Name = "hashConverterGroup";
            this.hashConverterGroup.Size = new System.Drawing.Size(780, 72);
            this.hashConverterGroup.TabIndex = 0;
            this.hashConverterGroup.TabStop = false;
            this.hashConverterGroup.Text = "Hash Converter";
            // 
            // hashSwapHexEndiannessCheckbox
            // 
            this.hashSwapHexEndiannessCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hashSwapHexEndiannessCheckbox.AutoSize = true;
            this.hashSwapHexEndiannessCheckbox.Location = new System.Drawing.Point(538, 49);
            this.hashSwapHexEndiannessCheckbox.Name = "hashSwapHexEndiannessCheckbox";
            this.hashSwapHexEndiannessCheckbox.Size = new System.Drawing.Size(110, 17);
            this.hashSwapHexEndiannessCheckbox.TabIndex = 7;
            this.hashSwapHexEndiannessCheckbox.Text = "Swap endianness";
            this.hashSwapHexEndiannessCheckbox.UseVisualStyleBackColor = true;
            // 
            // hashUseHexcheckBox
            // 
            this.hashUseHexcheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hashUseHexcheckBox.AutoSize = true;
            this.hashUseHexcheckBox.Location = new System.Drawing.Point(467, 49);
            this.hashUseHexcheckBox.Name = "hashUseHexcheckBox";
            this.hashUseHexcheckBox.Size = new System.Drawing.Size(65, 17);
            this.hashUseHexcheckBox.TabIndex = 6;
            this.hashUseHexcheckBox.Text = "Use hex";
            this.hashUseHexcheckBox.UseVisualStyleBackColor = true;
            // 
            // unHashButton
            // 
            this.unHashButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.unHashButton.Location = new System.Drawing.Point(332, 40);
            this.unHashButton.Name = "unHashButton";
            this.unHashButton.Size = new System.Drawing.Size(95, 23);
            this.unHashButton.TabIndex = 5;
            this.unHashButton.Text = "<- Unhash";
            this.unHashButton.UseVisualStyleBackColor = true;
            this.unHashButton.Click += new System.EventHandler(this.unHashButton_Click);
            // 
            // hashHashBox
            // 
            this.hashHashBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hashHashBox.Location = new System.Drawing.Point(467, 27);
            this.hashHashBox.Name = "hashHashBox";
            this.hashHashBox.Size = new System.Drawing.Size(305, 20);
            this.hashHashBox.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(433, 30);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Hash:";
            // 
            // hashButton
            // 
            this.hashButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hashButton.Location = new System.Drawing.Point(332, 14);
            this.hashButton.Name = "hashButton";
            this.hashButton.Size = new System.Drawing.Size(95, 23);
            this.hashButton.TabIndex = 2;
            this.hashButton.Text = "Hash ->";
            this.hashButton.UseVisualStyleBackColor = true;
            this.hashButton.Click += new System.EventHandler(this.hashButton_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Text:";
            // 
            // hashTextBox
            // 
            this.hashTextBox.Location = new System.Drawing.Point(43, 27);
            this.hashTextBox.Name = "hashTextBox";
            this.hashTextBox.Size = new System.Drawing.Size(280, 20);
            this.hashTextBox.TabIndex = 0;
            // 
            // ReportsTab
            // 
            this.ReportsTab.Controls.Add(this.reports_FormattingComboBox);
            this.ReportsTab.Controls.Add(this.label18);
            this.ReportsTab.Controls.Add(this.reports_LuaBLTIncludeLog);
            this.ReportsTab.Controls.Add(this.reports_GenerateReportButton);
            this.ReportsTab.Controls.Add(this.reports_IncludePdmodModsList);
            this.ReportsTab.Controls.Add(this.reports_IncludeLuaModsList);
            this.ReportsTab.Controls.Add(this.reports_IncludeLuaInformationCheckBox);
            this.ReportsTab.Controls.Add(this.label17);
            this.ReportsTab.Controls.Add(this.label16);
            this.ReportsTab.Controls.Add(this.reports_ReportRichTextBox);
            this.ReportsTab.Location = new System.Drawing.Point(4, 22);
            this.ReportsTab.Name = "ReportsTab";
            this.ReportsTab.Size = new System.Drawing.Size(792, 471);
            this.ReportsTab.TabIndex = 6;
            this.ReportsTab.Text = "Crash Reporter";
            this.ReportsTab.UseVisualStyleBackColor = true;
            // 
            // reports_FormattingComboBox
            // 
            this.reports_FormattingComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reports_FormattingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.reports_FormattingComboBox.FormattingEnabled = true;
            this.reports_FormattingComboBox.Items.AddRange(new object[] {
            "Regular Text",
            "Steam Community",
            "Reddit"});
            this.reports_FormattingComboBox.Location = new System.Drawing.Point(534, 82);
            this.reports_FormattingComboBox.Name = "reports_FormattingComboBox";
            this.reports_FormattingComboBox.Size = new System.Drawing.Size(242, 21);
            this.reports_FormattingComboBox.TabIndex = 11;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(469, 85);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 13);
            this.label18.TabIndex = 10;
            this.label18.Text = "Formatting:";
            // 
            // reports_LuaBLTIncludeLog
            // 
            this.reports_LuaBLTIncludeLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reports_LuaBLTIncludeLog.Enabled = false;
            this.reports_LuaBLTIncludeLog.Location = new System.Drawing.Point(469, 132);
            this.reports_LuaBLTIncludeLog.Name = "reports_LuaBLTIncludeLog";
            this.reports_LuaBLTIncludeLog.Size = new System.Drawing.Size(299, 17);
            this.reports_LuaBLTIncludeLog.TabIndex = 8;
            this.reports_LuaBLTIncludeLog.Text = "Include BLT Log at around the time of crash (if applicable)";
            this.reports_LuaBLTIncludeLog.UseVisualStyleBackColor = true;
            // 
            // reports_GenerateReportButton
            // 
            this.reports_GenerateReportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reports_GenerateReportButton.Location = new System.Drawing.Point(701, 430);
            this.reports_GenerateReportButton.Name = "reports_GenerateReportButton";
            this.reports_GenerateReportButton.Size = new System.Drawing.Size(75, 23);
            this.reports_GenerateReportButton.TabIndex = 7;
            this.reports_GenerateReportButton.Text = "Generate";
            this.reports_GenerateReportButton.UseVisualStyleBackColor = true;
            this.reports_GenerateReportButton.Click += new System.EventHandler(this.reports_GenerateReportButton_Click);
            // 
            // reports_IncludePdmodModsList
            // 
            this.reports_IncludePdmodModsList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reports_IncludePdmodModsList.Location = new System.Drawing.Point(469, 178);
            this.reports_IncludePdmodModsList.Name = "reports_IncludePdmodModsList";
            this.reports_IncludePdmodModsList.Size = new System.Drawing.Size(188, 17);
            this.reports_IncludePdmodModsList.TabIndex = 6;
            this.reports_IncludePdmodModsList.Text = "Include PDMod/Override mods list";
            this.reports_IncludePdmodModsList.UseVisualStyleBackColor = true;
            // 
            // reports_IncludeLuaModsList
            // 
            this.reports_IncludeLuaModsList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reports_IncludeLuaModsList.Enabled = false;
            this.reports_IncludeLuaModsList.Location = new System.Drawing.Point(469, 155);
            this.reports_IncludeLuaModsList.Name = "reports_IncludeLuaModsList";
            this.reports_IncludeLuaModsList.Size = new System.Drawing.Size(213, 17);
            this.reports_IncludeLuaModsList.TabIndex = 5;
            this.reports_IncludeLuaModsList.Text = "Include BLT Lua mods list (if applicable)";
            this.reports_IncludeLuaModsList.UseVisualStyleBackColor = true;
            // 
            // reports_IncludeLuaInformationCheckBox
            // 
            this.reports_IncludeLuaInformationCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reports_IncludeLuaInformationCheckBox.Location = new System.Drawing.Point(469, 109);
            this.reports_IncludeLuaInformationCheckBox.Name = "reports_IncludeLuaInformationCheckBox";
            this.reports_IncludeLuaInformationCheckBox.Size = new System.Drawing.Size(136, 17);
            this.reports_IncludeLuaInformationCheckBox.TabIndex = 3;
            this.reports_IncludeLuaInformationCheckBox.Text = "Include Lua information";
            this.reports_IncludeLuaInformationCheckBox.UseVisualStyleBackColor = true;
            this.reports_IncludeLuaInformationCheckBox.CheckedChanged += new System.EventHandler(this.reports_IncludeLuaInformationCheckBox_CheckedChanged);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.Location = new System.Drawing.Point(469, 19);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(320, 66);
            this.label17.TabIndex = 2;
            this.label17.Text = resources.GetString("label17.Text");
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(469, 6);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(198, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "PAYDAY 2 Crash report generator";
            // 
            // reports_ReportRichTextBox
            // 
            this.reports_ReportRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reports_ReportRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.reports_ReportRichTextBox.Name = "reports_ReportRichTextBox";
            this.reports_ReportRichTextBox.Size = new System.Drawing.Size(460, 465);
            this.reports_ReportRichTextBox.TabIndex = 0;
            this.reports_ReportRichTextBox.Text = "";
            // 
            // tabExtractGameFiles
            // 
            this.tabExtractGameFiles.Controls.Add(this.lblGeneralExtractOptions);
            this.tabExtractGameFiles.Controls.Add(this.prgExtractSubProgress);
            this.tabExtractGameFiles.Controls.Add(this.flpGeneralExtractOptions);
            this.tabExtractGameFiles.Controls.Add(this.tabExtractOptions);
            this.tabExtractGameFiles.Controls.Add(this.lblExtractLog);
            this.tabExtractGameFiles.Controls.Add(this.txtExtractLog);
            this.tabExtractGameFiles.Controls.Add(this.lblExtractProgress);
            this.tabExtractGameFiles.Controls.Add(this.prgExtractMain);
            this.tabExtractGameFiles.Controls.Add(this.btnStartExtracting);
            this.tabExtractGameFiles.Location = new System.Drawing.Point(4, 22);
            this.tabExtractGameFiles.Name = "tabExtractGameFiles";
            this.tabExtractGameFiles.Size = new System.Drawing.Size(792, 471);
            this.tabExtractGameFiles.TabIndex = 7;
            this.tabExtractGameFiles.Text = "Game File Extraction";
            this.tabExtractGameFiles.UseVisualStyleBackColor = true;
            // 
            // lblGeneralExtractOptions
            // 
            this.lblGeneralExtractOptions.AutoSize = true;
            this.lblGeneralExtractOptions.Location = new System.Drawing.Point(8, 205);
            this.lblGeneralExtractOptions.Name = "lblGeneralExtractOptions";
            this.lblGeneralExtractOptions.Size = new System.Drawing.Size(47, 13);
            this.lblGeneralExtractOptions.TabIndex = 57;
            this.lblGeneralExtractOptions.Text = "General:";
            // 
            // prgExtractSubProgress
            // 
            this.prgExtractSubProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgExtractSubProgress.Location = new System.Drawing.Point(8, 426);
            this.prgExtractSubProgress.Name = "prgExtractSubProgress";
            this.prgExtractSubProgress.Size = new System.Drawing.Size(678, 13);
            this.prgExtractSubProgress.Step = 1;
            this.prgExtractSubProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgExtractSubProgress.TabIndex = 56;
            // 
            // flpGeneralExtractOptions
            // 
            this.flpGeneralExtractOptions.Controls.Add(this.lblSingleBundle);
            this.flpGeneralExtractOptions.Controls.Add(this.txtSingleBundle);
            this.flpGeneralExtractOptions.Controls.Add(this.chkExtractAll);
            this.flpGeneralExtractOptions.Location = new System.Drawing.Point(8, 221);
            this.flpGeneralExtractOptions.Name = "flpGeneralExtractOptions";
            this.flpGeneralExtractOptions.Size = new System.Drawing.Size(406, 181);
            this.flpGeneralExtractOptions.TabIndex = 0;
            // 
            // lblSingleBundle
            // 
            this.lblSingleBundle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSingleBundle.AutoSize = true;
            this.lblSingleBundle.Location = new System.Drawing.Point(3, 6);
            this.lblSingleBundle.Name = "lblSingleBundle";
            this.lblSingleBundle.Size = new System.Drawing.Size(75, 13);
            this.lblSingleBundle.TabIndex = 37;
            this.lblSingleBundle.Text = "Single Bundle:";
            // 
            // txtSingleBundle
            // 
            this.txtSingleBundle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSingleBundle.Enabled = false;
            this.txtSingleBundle.Location = new System.Drawing.Point(84, 3);
            this.txtSingleBundle.Name = "txtSingleBundle";
            this.txtSingleBundle.Size = new System.Drawing.Size(197, 20);
            this.txtSingleBundle.TabIndex = 38;
            // 
            // chkExtractAll
            // 
            this.chkExtractAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkExtractAll.AutoSize = true;
            this.chkExtractAll.Checked = true;
            this.chkExtractAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExtractAll.Location = new System.Drawing.Point(287, 4);
            this.chkExtractAll.Name = "chkExtractAll";
            this.chkExtractAll.Size = new System.Drawing.Size(109, 17);
            this.chkExtractAll.TabIndex = 39;
            this.chkExtractAll.Text = "Use every bundle";
            this.toolTips.SetToolTip(this.chkExtractAll, "Extract or List every bundle that exists in the extract directory");
            this.chkExtractAll.UseVisualStyleBackColor = true;
            this.chkExtractAll.CheckedChanged += new System.EventHandler(this.chkExtractAll_CheckedChanged);
            // 
            // tabExtractOptions
            // 
            this.tabExtractOptions.Controls.Add(this.tabExtract);
            this.tabExtractOptions.Controls.Add(this.tabList);
            this.tabExtractOptions.Location = new System.Drawing.Point(8, 3);
            this.tabExtractOptions.Name = "tabExtractOptions";
            this.tabExtractOptions.SelectedIndex = 0;
            this.tabExtractOptions.Size = new System.Drawing.Size(770, 198);
            this.tabExtractOptions.TabIndex = 54;
            // 
            // tabExtract
            // 
            this.tabExtract.Controls.Add(this.flpExtract);
            this.tabExtract.Location = new System.Drawing.Point(4, 22);
            this.tabExtract.Name = "tabExtract";
            this.tabExtract.Padding = new System.Windows.Forms.Padding(3);
            this.tabExtract.Size = new System.Drawing.Size(762, 172);
            this.tabExtract.TabIndex = 1;
            this.tabExtract.Text = "Extract";
            this.tabExtract.UseVisualStyleBackColor = true;
            // 
            // flpExtract
            // 
            this.flpExtract.Controls.Add(this.lblExtractFolder);
            this.flpExtract.Controls.Add(this.txtExtractFolder);
            this.flpExtract.Controls.Add(this.btnExtractFolder);
            this.flpExtract.Controls.Add(this.groupBox1);
            this.flpExtract.Controls.Add(this.chkIgnoreExisting);
            this.flpExtract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpExtract.Location = new System.Drawing.Point(3, 3);
            this.flpExtract.Name = "flpExtract";
            this.flpExtract.Size = new System.Drawing.Size(756, 166);
            this.flpExtract.TabIndex = 54;
            // 
            // lblExtractFolder
            // 
            this.lblExtractFolder.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblExtractFolder.AutoSize = true;
            this.lblExtractFolder.BackColor = System.Drawing.Color.Transparent;
            this.lblExtractFolder.Location = new System.Drawing.Point(3, 8);
            this.lblExtractFolder.Name = "lblExtractFolder";
            this.lblExtractFolder.Size = new System.Drawing.Size(110, 13);
            this.lblExtractFolder.TabIndex = 41;
            this.lblExtractFolder.Text = "Custom Extract Folder";
            // 
            // txtExtractFolder
            // 
            this.txtExtractFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExtractFolder.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtExtractFolder.Location = new System.Drawing.Point(119, 4);
            this.txtExtractFolder.Name = "txtExtractFolder";
            this.txtExtractFolder.ReadOnly = true;
            this.txtExtractFolder.Size = new System.Drawing.Size(552, 20);
            this.txtExtractFolder.TabIndex = 40;
            // 
            // btnExtractFolder
            // 
            this.btnExtractFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExtractFolder.Location = new System.Drawing.Point(677, 3);
            this.btnExtractFolder.Name = "btnExtractFolder";
            this.btnExtractFolder.Size = new System.Drawing.Size(75, 23);
            this.btnExtractFolder.TabIndex = 42;
            this.btnExtractFolder.Text = "Browse...";
            this.btnExtractFolder.UseVisualStyleBackColor = true;
            this.btnExtractFolder.Click += new System.EventHandler(this.CustomExtractClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeleteExtMod);
            this.groupBox1.Controls.Add(this.btnApplyExtensionChange);
            this.groupBox1.Controls.Add(this.txtExtensionReplacement);
            this.groupBox1.Controls.Add(this.txtExtensionMod);
            this.groupBox1.Controls.Add(this.lblReplacement);
            this.groupBox1.Controls.Add(this.lblExtension);
            this.groupBox1.Controls.Add(this.lstChangeExtension);
            this.groupBox1.Location = new System.Drawing.Point(3, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 131);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Extension Replacement";
            // 
            // btnDeleteExtMod
            // 
            this.btnDeleteExtMod.Location = new System.Drawing.Point(184, 59);
            this.btnDeleteExtMod.Name = "btnDeleteExtMod";
            this.btnDeleteExtMod.Size = new System.Drawing.Size(100, 22);
            this.btnDeleteExtMod.TabIndex = 54;
            this.btnDeleteExtMod.Text = "Remove Selected";
            this.btnDeleteExtMod.UseVisualStyleBackColor = true;
            this.btnDeleteExtMod.Click += new System.EventHandler(this.btnDeleteExtMod_Click);
            // 
            // btnApplyExtensionChange
            // 
            this.btnApplyExtensionChange.Location = new System.Drawing.Point(290, 59);
            this.btnApplyExtensionChange.Name = "btnApplyExtensionChange";
            this.btnApplyExtensionChange.Size = new System.Drawing.Size(101, 22);
            this.btnApplyExtensionChange.TabIndex = 53;
            this.btnApplyExtensionChange.Text = "Add/Update";
            this.btnApplyExtensionChange.UseVisualStyleBackColor = true;
            this.btnApplyExtensionChange.Click += new System.EventHandler(this.btnApplyExtensionChange_Click);
            // 
            // txtExtensionReplacement
            // 
            this.txtExtensionReplacement.Location = new System.Drawing.Point(290, 36);
            this.txtExtensionReplacement.Name = "txtExtensionReplacement";
            this.txtExtensionReplacement.Size = new System.Drawing.Size(100, 20);
            this.txtExtensionReplacement.TabIndex = 52;
            // 
            // txtExtensionMod
            // 
            this.txtExtensionMod.Location = new System.Drawing.Point(184, 36);
            this.txtExtensionMod.Name = "txtExtensionMod";
            this.txtExtensionMod.Size = new System.Drawing.Size(100, 20);
            this.txtExtensionMod.TabIndex = 51;
            // 
            // lblReplacement
            // 
            this.lblReplacement.AutoSize = true;
            this.lblReplacement.Location = new System.Drawing.Point(288, 19);
            this.lblReplacement.Name = "lblReplacement";
            this.lblReplacement.Size = new System.Drawing.Size(73, 13);
            this.lblReplacement.TabIndex = 50;
            this.lblReplacement.Text = "Replacement:";
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.Location = new System.Drawing.Point(181, 19);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(56, 13);
            this.lblExtension.TabIndex = 49;
            this.lblExtension.Text = "Extension:";
            // 
            // lstChangeExtension
            // 
            this.lstChangeExtension.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmOriginalExtension,
            this.clmNewExtension});
            this.lstChangeExtension.FullRowSelect = true;
            this.lstChangeExtension.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstChangeExtension.HideSelection = false;
            this.lstChangeExtension.Location = new System.Drawing.Point(6, 19);
            this.lstChangeExtension.MultiSelect = false;
            this.lstChangeExtension.Name = "lstChangeExtension";
            this.lstChangeExtension.Size = new System.Drawing.Size(170, 106);
            this.lstChangeExtension.TabIndex = 48;
            this.lstChangeExtension.UseCompatibleStateImageBehavior = false;
            this.lstChangeExtension.View = System.Windows.Forms.View.Details;
            this.lstChangeExtension.SelectedIndexChanged += new System.EventHandler(this.lstChangeExtension_SelectedIndexChanged);
            // 
            // clmOriginalExtension
            // 
            this.clmOriginalExtension.Text = "Extension";
            this.clmOriginalExtension.Width = 80;
            // 
            // clmNewExtension
            // 
            this.clmNewExtension.Text = "Replacement";
            this.clmNewExtension.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmNewExtension.Width = 80;
            // 
            // chkIgnoreExisting
            // 
            this.chkIgnoreExisting.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkIgnoreExisting.AutoSize = true;
            this.chkIgnoreExisting.Location = new System.Drawing.Point(405, 32);
            this.chkIgnoreExisting.Name = "chkIgnoreExisting";
            this.chkIgnoreExisting.Size = new System.Drawing.Size(115, 17);
            this.chkIgnoreExisting.TabIndex = 47;
            this.chkIgnoreExisting.Text = "Ignore existing files";
            this.toolTips.SetToolTip(this.chkIgnoreExisting, "Only extract files that do not already exist in the extract directory");
            this.chkIgnoreExisting.UseVisualStyleBackColor = true;
            this.chkIgnoreExisting.CheckedChanged += new System.EventHandler(this.chkIgnoreExisting_CheckedChanged);
            // 
            // tabList
            // 
            this.tabList.Controls.Add(this.flpList);
            this.tabList.Location = new System.Drawing.Point(4, 22);
            this.tabList.Name = "tabList";
            this.tabList.Size = new System.Drawing.Size(762, 172);
            this.tabList.TabIndex = 2;
            this.tabList.Text = "List";
            this.tabList.UseVisualStyleBackColor = true;
            // 
            // flpList
            // 
            this.flpList.Controls.Add(this.lblFormat);
            this.flpList.Controls.Add(this.cmbFormat);
            this.flpList.Controls.Add(this.lblListFile);
            this.flpList.Controls.Add(this.txtListFile);
            this.flpList.Controls.Add(this.btnListFileBrowse);
            this.flpList.Controls.Add(this.lblSelectInformation);
            this.flpList.Controls.Add(this.clstSelectInformation);
            this.flpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpList.Location = new System.Drawing.Point(0, 0);
            this.flpList.Name = "flpList";
            this.flpList.Size = new System.Drawing.Size(762, 172);
            this.flpList.TabIndex = 52;
            // 
            // lblFormat
            // 
            this.lblFormat.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFormat.AutoSize = true;
            this.lblFormat.Location = new System.Drawing.Point(3, 8);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(42, 13);
            this.lblFormat.TabIndex = 52;
            this.lblFormat.Text = "Format:";
            this.lblFormat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbFormat
            // 
            this.cmbFormat.BackColor = System.Drawing.SystemColors.Window;
            this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Items.AddRange(new object[] {
            "Default",
            "CSV"});
            this.cmbFormat.Location = new System.Drawing.Point(51, 3);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(156, 21);
            this.cmbFormat.TabIndex = 53;
            // 
            // lblListFile
            // 
            this.lblListFile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblListFile.AutoSize = true;
            this.lblListFile.Location = new System.Drawing.Point(213, 8);
            this.lblListFile.Name = "lblListFile";
            this.lblListFile.Size = new System.Drawing.Size(80, 13);
            this.lblListFile.TabIndex = 49;
            this.lblListFile.Text = "List Output File:";
            this.lblListFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtListFile
            // 
            this.txtListFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtListFile.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtListFile.Location = new System.Drawing.Point(299, 4);
            this.txtListFile.Name = "txtListFile";
            this.txtListFile.ReadOnly = true;
            this.txtListFile.Size = new System.Drawing.Size(375, 20);
            this.txtListFile.TabIndex = 50;
            this.toolTips.SetToolTip(this.txtListFile, "Defaults to a local file called \"listlog.log\"");
            // 
            // btnListFileBrowse
            // 
            this.btnListFileBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnListFileBrowse.Location = new System.Drawing.Point(680, 3);
            this.btnListFileBrowse.Name = "btnListFileBrowse";
            this.btnListFileBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnListFileBrowse.TabIndex = 51;
            this.btnListFileBrowse.Text = "Browse...";
            this.btnListFileBrowse.UseVisualStyleBackColor = true;
            this.btnListFileBrowse.Click += new System.EventHandler(this.BrowseForLogClick);
            // 
            // lblSelectInformation
            // 
            this.lblSelectInformation.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSelectInformation.AutoSize = true;
            this.lblSelectInformation.Location = new System.Drawing.Point(3, 29);
            this.lblSelectInformation.Name = "lblSelectInformation";
            this.lblSelectInformation.Size = new System.Drawing.Size(106, 13);
            this.lblSelectInformation.TabIndex = 55;
            this.lblSelectInformation.Text = "Included Information:";
            this.lblSelectInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // clstSelectInformation
            // 
            this.clstSelectInformation.FormattingEnabled = true;
            this.clstSelectInformation.Location = new System.Drawing.Point(115, 32);
            this.clstSelectInformation.Name = "clstSelectInformation";
            this.clstSelectInformation.Size = new System.Drawing.Size(204, 139);
            this.clstSelectInformation.TabIndex = 54;
            // 
            // lblExtractLog
            // 
            this.lblExtractLog.Location = new System.Drawing.Point(420, 205);
            this.lblExtractLog.Name = "lblExtractLog";
            this.lblExtractLog.Size = new System.Drawing.Size(358, 13);
            this.lblExtractLog.TabIndex = 51;
            this.lblExtractLog.Text = "Log:";
            this.lblExtractLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtExtractLog
            // 
            this.txtExtractLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExtractLog.Location = new System.Drawing.Point(420, 221);
            this.txtExtractLog.MaxLength = 99999999;
            this.txtExtractLog.Multiline = true;
            this.txtExtractLog.Name = "txtExtractLog";
            this.txtExtractLog.ReadOnly = true;
            this.txtExtractLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExtractLog.Size = new System.Drawing.Size(358, 181);
            this.txtExtractLog.TabIndex = 50;
            // 
            // lblExtractProgress
            // 
            this.lblExtractProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExtractProgress.Location = new System.Drawing.Point(8, 410);
            this.lblExtractProgress.Name = "lblExtractProgress";
            this.lblExtractProgress.Size = new System.Drawing.Size(770, 13);
            this.lblExtractProgress.TabIndex = 49;
            this.lblExtractProgress.Text = "- Entries: 0/0, Bundle: 0/0";
            this.lblExtractProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // prgExtractMain
            // 
            this.prgExtractMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgExtractMain.Location = new System.Drawing.Point(8, 445);
            this.prgExtractMain.Name = "prgExtractMain";
            this.prgExtractMain.Size = new System.Drawing.Size(678, 13);
            this.prgExtractMain.Step = 1;
            this.prgExtractMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgExtractMain.TabIndex = 1;
            // 
            // btnStartExtracting
            // 
            this.btnStartExtracting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartExtracting.Location = new System.Drawing.Point(692, 426);
            this.btnStartExtracting.Name = "btnStartExtracting";
            this.btnStartExtracting.Size = new System.Drawing.Size(86, 32);
            this.btnStartExtracting.TabIndex = 36;
            this.btnStartExtracting.Text = "Start";
            this.btnStartExtracting.UseVisualStyleBackColor = true;
            this.btnStartExtracting.Click += new System.EventHandler(this.StartExtractingClick);
            // 
            // modContextMenuStrip
            // 
            this.modContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markForInstallationToolStripMenuItem,
            this.markForReinstallationToolStripMenuItem,
            this.markForUninstallationToolStripMenuItem,
            this.removeFromListToolStripMenuItem,
            this.toolStripSeparator1,
            this.showPDModFileToolStripMenuItem,
            this.showInModoverrideFolderToolStripMenuItem,
            this.showInBLTModsFolderToolStripMenuItem});
            this.modContextMenuStrip.Name = "contextMenuStrip1";
            this.modContextMenuStrip.Size = new System.Drawing.Size(227, 164);
            this.modContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // markForInstallationToolStripMenuItem
            // 
            this.markForInstallationToolStripMenuItem.Name = "markForInstallationToolStripMenuItem";
            this.markForInstallationToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.markForInstallationToolStripMenuItem.Text = "Toggle Installation";
            this.markForInstallationToolStripMenuItem.Click += new System.EventHandler(this.markForInstallationToolStripMenuItem_Click);
            // 
            // markForReinstallationToolStripMenuItem
            // 
            this.markForReinstallationToolStripMenuItem.Name = "markForReinstallationToolStripMenuItem";
            this.markForReinstallationToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.markForReinstallationToolStripMenuItem.Text = "Toggle Reinstallation";
            this.markForReinstallationToolStripMenuItem.Click += new System.EventHandler(this.markForReinstallationToolStripMenuItem_Click);
            // 
            // markForUninstallationToolStripMenuItem
            // 
            this.markForUninstallationToolStripMenuItem.Name = "markForUninstallationToolStripMenuItem";
            this.markForUninstallationToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.markForUninstallationToolStripMenuItem.Text = "Toggle Uninstallation";
            this.markForUninstallationToolStripMenuItem.Click += new System.EventHandler(this.markForUninstallationToolStripMenuItem_Click);
            // 
            // removeFromListToolStripMenuItem
            // 
            this.removeFromListToolStripMenuItem.Name = "removeFromListToolStripMenuItem";
            this.removeFromListToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.removeFromListToolStripMenuItem.Text = "Remove From List";
            this.removeFromListToolStripMenuItem.Click += new System.EventHandler(this.removeFromListToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(223, 6);
            // 
            // showPDModFileToolStripMenuItem
            // 
            this.showPDModFileToolStripMenuItem.Name = "showPDModFileToolStripMenuItem";
            this.showPDModFileToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.showPDModFileToolStripMenuItem.Tag = "";
            this.showPDModFileToolStripMenuItem.Text = "Show PDMod file";
            this.showPDModFileToolStripMenuItem.Click += new System.EventHandler(this.showPDModFileToolStripMenuItem_Click);
            // 
            // showInModoverrideFolderToolStripMenuItem
            // 
            this.showInModoverrideFolderToolStripMenuItem.Name = "showInModoverrideFolderToolStripMenuItem";
            this.showInModoverrideFolderToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.showInModoverrideFolderToolStripMenuItem.Text = "Show in mod_override folder";
            this.showInModoverrideFolderToolStripMenuItem.Click += new System.EventHandler(this.showInModoverrideFolderToolStripMenuItem_Click);
            // 
            // showInBLTModsFolderToolStripMenuItem
            // 
            this.showInBLTModsFolderToolStripMenuItem.Name = "showInBLTModsFolderToolStripMenuItem";
            this.showInBLTModsFolderToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.showInBLTModsFolderToolStripMenuItem.Text = "Show in BLT mods folder";
            this.showInBLTModsFolderToolStripMenuItem.Click += new System.EventHandler(this.showInBLTModsFolderToolStripMenuItem_Click);
            // 
            // bundleModBindingSource
            // 
            this.bundleModBindingSource.DataSource = typeof(PDBundleModPatcher.BundleMod);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 493);
            this.Controls.Add(this.tabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 1000);
            this.MinimumSize = new System.Drawing.Size(810, 531);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PAYDAY Bundle Modder";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.tabs.ResumeLayout(false);
            this.ApplyTab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.CreateTab.ResumeLayout(false);
            this.CreateTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddedFilesView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bundleRewriteItemBindingSource)).EndInit();
            this.OptionsTab.ResumeLayout(false);
            this.OptionsFlowLayout.ResumeLayout(false);
            this.OptionsFlowLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.programIcon)).EndInit();
            this.UtilitiesTab.ResumeLayout(false);
            this.corruptedBundlesGroup.ResumeLayout(false);
            this.corruptedBundlesGroup.PerformLayout();
            this.hashConverterGroup.ResumeLayout(false);
            this.hashConverterGroup.PerformLayout();
            this.ReportsTab.ResumeLayout(false);
            this.ReportsTab.PerformLayout();
            this.tabExtractGameFiles.ResumeLayout(false);
            this.tabExtractGameFiles.PerformLayout();
            this.flpGeneralExtractOptions.ResumeLayout(false);
            this.flpGeneralExtractOptions.PerformLayout();
            this.tabExtractOptions.ResumeLayout(false);
            this.tabExtract.ResumeLayout(false);
            this.flpExtract.ResumeLayout(false);
            this.flpExtract.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabList.ResumeLayout(false);
            this.flpList.ResumeLayout(false);
            this.flpList.PerformLayout();
            this.modContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bundleModBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage ApplyTab;
        private System.Windows.Forms.TabPage CreateTab;
        private System.Windows.Forms.TabPage OptionsTab;
        private System.Windows.Forms.FlowLayoutPanel OptionsFlowLayout;
        private System.Windows.Forms.Label GameAssetFolderLabel;
        private System.Windows.Forms.TextBox AssetFolderEdit;
        private System.Windows.Forms.Button AssetFolderButton;
        private System.Windows.Forms.Label TotalProgressLabel;
        private System.Windows.Forms.ProgressBar TotalProgress;
        private System.Windows.Forms.ProgressBar BundleProgress;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DescriptionText;
        private System.Windows.Forms.TextBox AuthorText;
        private System.Windows.Forms.TextBox ModNameText;
        private System.Windows.Forms.Button OpenModButton;
        private System.Windows.Forms.LinkLabel AboutLabel;
        private System.Windows.Forms.BindingSource bundleModBindingSource;
        private System.Windows.Forms.Label backupMethodLabel;
        private System.Windows.Forms.ComboBox backupMethodComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox BundleFileName;
        private System.Windows.Forms.CheckBox JPModStyleName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ReplacementFileName;
        private System.Windows.Forms.Button BrowseForReplacement;
        private System.Windows.Forms.Button AddReplacementButton;
        private System.Windows.Forms.Button UndoLast;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox SpecificVersion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView AddedFilesView;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox ModNameEdit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ModAuthorEdit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox ModDescriptionEdit;
        private System.Windows.Forms.Button CreateModButton;
        private System.Windows.Forms.Button savePDMod;
        private System.Windows.Forms.Button createModLoad;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.TabPage UtilitiesTab;
        private System.Windows.Forms.GroupBox hashConverterGroup;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox hashTextBox;
        private System.Windows.Forms.GroupBox corruptedBundlesGroup;
        private System.Windows.Forms.Button runCorruptedCheckButton;
        private System.Windows.Forms.Button unHashButton;
        private System.Windows.Forms.TextBox hashHashBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button hashButton;
        private System.Windows.Forms.Label progressTextCorruptedBundlesCheck;
        private System.Windows.Forms.ProgressBar progressCorruptedBundlesCheck;
        private System.Windows.Forms.CheckBox verifyCorruptedBundlesCheckCheckBox;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox bundleFilePathAutotompletion_checkbox;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Label AvailableModsLabel;
        private System.Windows.Forms.ListView availiableMods_listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button moreModDetails_button;
        private System.Windows.Forms.ContextMenuStrip modContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem markForReinstallationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFromListToolStripMenuItem;
        private System.Windows.Forms.Button filecontrol_button;
        private System.Windows.Forms.CheckBox useOverrideFolderCheckbox;
        private System.Windows.Forms.CheckBox createOverrideFolderDummies;
        private System.Windows.Forms.CheckBox useSharedPDMODTOOLfolder;
        private System.Windows.Forms.CheckBox hashUseHexcheckBox;
        private System.Windows.Forms.ToolStripMenuItem markForInstallationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markForUninstallationToolStripMenuItem;
        private System.Windows.Forms.TabPage ReportsTab;
        private System.Windows.Forms.TextBox availiableModsSearch_textbox;
        private System.Windows.Forms.ComboBox fileReplacementType_ComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox playsoundoncompletion_checkbox;
        private System.Windows.Forms.Button viewModConfig;
        private System.Windows.Forms.Button editModOptions_button;
        private System.Windows.Forms.Button modVariables_button;
        private System.Windows.Forms.CheckBox corruptedShowOnlyCorrupted_checkbox;
        private System.Windows.Forms.RichTextBox corruptedBundlesReport_richTextBox;
        private System.Windows.Forms.CheckBox rungameoncompletion_checkbox;
        private System.Windows.Forms.Button attemptRepair_button;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem showInModoverrideFolderToolStripMenuItem;
        private System.Windows.Forms.CheckBox hashSwapHexEndiannessCheckbox;
        private System.Windows.Forms.ComboBox selectedPackage;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button ConfigureReplacementButton;
        private System.Windows.Forms.Label patchingTimeDetails;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox patchingBufferSize;
        private System.Windows.Forms.Button reports_GenerateReportButton;
        private System.Windows.Forms.CheckBox reports_IncludePdmodModsList;
        private System.Windows.Forms.CheckBox reports_IncludeLuaModsList;
        private System.Windows.Forms.CheckBox reports_IncludeLuaInformationCheckBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RichTextBox reports_ReportRichTextBox;
        private System.Windows.Forms.CheckBox reports_LuaBLTIncludeLog;
        private System.Windows.Forms.ComboBox reports_FormattingComboBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.BindingSource bundleRewriteItemBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceFileDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn replacementFileDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn hasVariablesDataGridViewCheckBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem showInBLTModsFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPDModFileToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxOverrideShow;
        private System.Windows.Forms.Button ExtraOptions1;
        private System.Windows.Forms.ComboBox ThemeSelection;
        private System.Windows.Forms.Label ThemeSelectionLabel;
        private System.Windows.Forms.CheckBox checkupdatesonstartup_checkbox;
        private System.Windows.Forms.Label emptylabeltofixflowlayout;
        private System.Windows.Forms.PictureBox programIcon;
        private System.Windows.Forms.Button checkForUpdates_Button;
        private System.Windows.Forms.TabPage tabExtractGameFiles;
        private System.Windows.Forms.ProgressBar prgExtractMain;
        private System.Windows.Forms.Button btnExtractFolder;
        private System.Windows.Forms.Label lblExtractFolder;
        private System.Windows.Forms.TextBox txtExtractFolder;
        private System.Windows.Forms.Button btnStartExtracting;
        private System.Windows.Forms.Label lblExtractProgress;
        private System.Windows.Forms.TextBox txtExtractLog;
        private System.Windows.Forms.Label lblExtractLog;
        private System.Windows.Forms.TextBox txtListFile;
        private System.Windows.Forms.Label lblListFile;
        private System.Windows.Forms.Button btnListFileBrowse;
		private System.Windows.Forms.CheckBox WriteConsole;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.FlowLayoutPanel flpList;
        private System.Windows.Forms.FlowLayoutPanel flpExtract;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.CheckedListBox clstSelectInformation;
        private System.Windows.Forms.TabControl tabExtractOptions;
        private System.Windows.Forms.TabPage tabExtract;
        private System.Windows.Forms.TabPage tabList;
        private System.Windows.Forms.FlowLayoutPanel flpGeneralExtractOptions;
        private System.Windows.Forms.Label lblSingleBundle;
        private System.Windows.Forms.TextBox txtSingleBundle;
        private System.Windows.Forms.CheckBox chkIgnoreExisting;
        private System.Windows.Forms.Label lblSelectInformation;
        private System.Windows.Forms.CheckBox chkExtractAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnApplyExtensionChange;
        private System.Windows.Forms.TextBox txtExtensionReplacement;
        private System.Windows.Forms.TextBox txtExtensionMod;
        private System.Windows.Forms.Label lblReplacement;
        private System.Windows.Forms.Label lblExtension;
        private System.Windows.Forms.ListView lstChangeExtension;
        private System.Windows.Forms.ColumnHeader clmOriginalExtension;
        private System.Windows.Forms.ColumnHeader clmNewExtension;
        private System.Windows.Forms.Button btnDeleteExtMod;
        private System.Windows.Forms.ProgressBar prgExtractSubProgress;
        private System.Windows.Forms.Label lblGeneralExtractOptions;
    }
}

