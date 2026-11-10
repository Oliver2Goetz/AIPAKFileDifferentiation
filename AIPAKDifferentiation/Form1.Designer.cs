namespace AIPAKDifferentiation {
    partial class AIPAKDifferentiation {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AIPAKDifferentiation));
            buttonPakShowDifferences = new System.Windows.Forms.Button();
            listviewDifferences = new System.Windows.Forms.ListView();
            columnCathodeType = new System.Windows.Forms.ColumnHeader();
            columnGuid = new System.Windows.Forms.ColumnHeader();
            columnName = new System.Windows.Forms.ColumnHeader();
            columnValueBefore = new System.Windows.Forms.ColumnHeader();
            columnValueAfter = new System.Windows.Forms.ColumnHeader();
            columnDifferenceType = new System.Windows.Forms.ColumnHeader();
            labelDifferences = new System.Windows.Forms.Label();
            buttonBrowsePak1 = new System.Windows.Forms.Button();
            buttonBrowsePak2 = new System.Windows.Forms.Button();
            labelPak1 = new System.Windows.Forms.Label();
            labelPak2 = new System.Windows.Forms.Label();
            fileDialog = new System.Windows.Forms.OpenFileDialog();
            checkboxEntityHideOverrides = new System.Windows.Forms.CheckBox();
            labelFilters = new System.Windows.Forms.Label();
            checkboxHideComposites = new System.Windows.Forms.CheckBox();
            checkboxHideEntities = new System.Windows.Forms.CheckBox();
            checkboxHideParameters = new System.Windows.Forms.CheckBox();
            checkboxHideLinks = new System.Windows.Forms.CheckBox();
            checkboxHideCreated = new System.Windows.Forms.CheckBox();
            checkboxHideModified = new System.Windows.Forms.CheckBox();
            checkboxHideDeleted = new System.Windows.Forms.CheckBox();
            buttonSwitchView = new System.Windows.Forms.Button();
            panelTreeView = new System.Windows.Forms.Panel();
            treeviewDifferences = new System.Windows.Forms.TreeView();
            panelTreeViewDetails = new System.Windows.Forms.Panel();
            buttonExportAsTxt = new System.Windows.Forms.Button();
            checkboxShowWindowOnTop = new System.Windows.Forms.CheckBox();
            checkboxApplyFiltersForExport = new System.Windows.Forms.CheckBox();
            menuStripSettings = new System.Windows.Forms.MenuStrip();
            settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            rememberPakPathsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            settingsFileLocationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            panelTreeView.SuspendLayout();
            menuStripSettings.SuspendLayout();
            SuspendLayout();
            // 
            // buttonPakShowDifferences
            // 
            buttonPakShowDifferences.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            buttonPakShowDifferences.Location = new System.Drawing.Point(14, 117);
            buttonPakShowDifferences.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonPakShowDifferences.Name = "buttonPakShowDifferences";
            buttonPakShowDifferences.Size = new System.Drawing.Size(152, 44);
            buttonPakShowDifferences.TabIndex = 0;
            buttonPakShowDifferences.TabStop = false;
            buttonPakShowDifferences.Text = "Show differences";
            buttonPakShowDifferences.UseVisualStyleBackColor = true;
            buttonPakShowDifferences.Click += buttonPakShowDifferences_Click;
            // 
            // listviewDifferences
            // 
            listviewDifferences.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            listviewDifferences.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnCathodeType, columnGuid, columnName, columnValueBefore, columnValueAfter, columnDifferenceType });
            listviewDifferences.FullRowSelect = true;
            listviewDifferences.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listviewDifferences.Location = new System.Drawing.Point(14, 196);
            listviewDifferences.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listviewDifferences.Name = "listviewDifferences";
            listviewDifferences.RightToLeft = System.Windows.Forms.RightToLeft.No;
            listviewDifferences.ShowItemToolTips = true;
            listviewDifferences.Size = new System.Drawing.Size(1470, 577);
            listviewDifferences.TabIndex = 0;
            listviewDifferences.TabStop = false;
            listviewDifferences.UseCompatibleStateImageBehavior = false;
            listviewDifferences.View = System.Windows.Forms.View.Details;
            // 
            // columnCathodeType
            // 
            columnCathodeType.Text = "Cathode Type";
            columnCathodeType.Width = 85;
            // 
            // columnGuid
            // 
            columnGuid.Text = "Guid";
            columnGuid.Width = 85;
            // 
            // columnName
            // 
            columnName.Text = "Name";
            columnName.Width = 540;
            // 
            // columnValueBefore
            // 
            columnValueBefore.Text = "Initial Value";
            columnValueBefore.Width = 320;
            // 
            // columnValueAfter
            // 
            columnValueAfter.Text = "New value";
            columnValueAfter.Width = 320;
            // 
            // columnDifferenceType
            // 
            columnDifferenceType.Text = "Difference Type";
            columnDifferenceType.Width = 100;
            // 
            // labelDifferences
            // 
            labelDifferences.AutoSize = true;
            labelDifferences.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            labelDifferences.Location = new System.Drawing.Point(14, 173);
            labelDifferences.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelDifferences.Name = "labelDifferences";
            labelDifferences.Size = new System.Drawing.Size(80, 17);
            labelDifferences.TabIndex = 2;
            labelDifferences.Text = "Differences";
            // 
            // buttonBrowsePak1
            // 
            buttonBrowsePak1.Location = new System.Drawing.Point(14, 38);
            buttonBrowsePak1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonBrowsePak1.Name = "buttonBrowsePak1";
            buttonBrowsePak1.Size = new System.Drawing.Size(152, 29);
            buttonBrowsePak1.TabIndex = 3;
            buttonBrowsePak1.TabStop = false;
            buttonBrowsePak1.Text = "Select PAK 1";
            buttonBrowsePak1.UseVisualStyleBackColor = true;
            buttonBrowsePak1.Click += buttonBrowsePak1_Click;
            // 
            // buttonBrowsePak2
            // 
            buttonBrowsePak2.Location = new System.Drawing.Point(14, 70);
            buttonBrowsePak2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonBrowsePak2.Name = "buttonBrowsePak2";
            buttonBrowsePak2.Size = new System.Drawing.Size(152, 29);
            buttonBrowsePak2.TabIndex = 4;
            buttonBrowsePak2.TabStop = false;
            buttonBrowsePak2.Text = "Select PAK 2";
            buttonBrowsePak2.UseVisualStyleBackColor = true;
            buttonBrowsePak2.Click += buttonBrowsePak2_Click;
            // 
            // labelPak1
            // 
            labelPak1.AutoSize = true;
            labelPak1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            labelPak1.Location = new System.Drawing.Point(173, 42);
            labelPak1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelPak1.Name = "labelPak1";
            labelPak1.Size = new System.Drawing.Size(50, 15);
            labelPak1.TabIndex = 5;
            labelPak1.Text = "PAK 1: -";
            // 
            // labelPak2
            // 
            labelPak2.AutoSize = true;
            labelPak2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            labelPak2.Location = new System.Drawing.Point(173, 75);
            labelPak2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelPak2.Name = "labelPak2";
            labelPak2.Size = new System.Drawing.Size(50, 15);
            labelPak2.TabIndex = 6;
            labelPak2.Text = "PAK 2: -";
            // 
            // fileDialog
            // 
            fileDialog.DefaultExt = "pak";
            fileDialog.Filter = "PAK files (*.pak)|*pak";
            fileDialog.InitialDirectory = "C:\\";
            // 
            // checkboxEntityHideOverrides
            // 
            checkboxEntityHideOverrides.AutoSize = true;
            checkboxEntityHideOverrides.Location = new System.Drawing.Point(676, 117);
            checkboxEntityHideOverrides.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkboxEntityHideOverrides.Name = "checkboxEntityHideOverrides";
            checkboxEntityHideOverrides.Size = new System.Drawing.Size(126, 19);
            checkboxEntityHideOverrides.TabIndex = 7;
            checkboxEntityHideOverrides.Text = "Hide ALIAS entities";
            checkboxEntityHideOverrides.UseVisualStyleBackColor = true;
            checkboxEntityHideOverrides.CheckedChanged += checkboxEntityHideOverrides_CheckedChanged;
            // 
            // labelFilters
            // 
            labelFilters.AutoSize = true;
            labelFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            labelFilters.Location = new System.Drawing.Point(173, 116);
            labelFilters.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelFilters.Name = "labelFilters";
            labelFilters.Size = new System.Drawing.Size(43, 15);
            labelFilters.TabIndex = 8;
            labelFilters.Text = "Filters:";
            // 
            // checkboxHideComposites
            // 
            checkboxHideComposites.AutoSize = true;
            checkboxHideComposites.Location = new System.Drawing.Point(230, 117);
            checkboxHideComposites.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkboxHideComposites.Name = "checkboxHideComposites";
            checkboxHideComposites.Size = new System.Drawing.Size(115, 19);
            checkboxHideComposites.TabIndex = 9;
            checkboxHideComposites.Text = "Hide composites";
            checkboxHideComposites.UseVisualStyleBackColor = true;
            checkboxHideComposites.CheckedChanged += checkboxHideComposites_CheckedChanged;
            // 
            // checkboxHideEntities
            // 
            checkboxHideEntities.AutoSize = true;
            checkboxHideEntities.Location = new System.Drawing.Point(358, 117);
            checkboxHideEntities.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkboxHideEntities.Name = "checkboxHideEntities";
            checkboxHideEntities.Size = new System.Drawing.Size(92, 19);
            checkboxHideEntities.TabIndex = 10;
            checkboxHideEntities.Text = "Hide entities";
            checkboxHideEntities.UseVisualStyleBackColor = true;
            checkboxHideEntities.CheckedChanged += checkboxHideEntities_CheckedChanged;
            // 
            // checkboxHideParameters
            // 
            checkboxHideParameters.AutoSize = true;
            checkboxHideParameters.Location = new System.Drawing.Point(463, 117);
            checkboxHideParameters.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkboxHideParameters.Name = "checkboxHideParameters";
            checkboxHideParameters.Size = new System.Drawing.Size(113, 19);
            checkboxHideParameters.TabIndex = 11;
            checkboxHideParameters.Text = "Hide parameters";
            checkboxHideParameters.UseVisualStyleBackColor = true;
            checkboxHideParameters.CheckedChanged += checkboxHideParameters_CheckedChanged;
            // 
            // checkboxHideLinks
            // 
            checkboxHideLinks.AutoSize = true;
            checkboxHideLinks.Location = new System.Drawing.Point(590, 117);
            checkboxHideLinks.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkboxHideLinks.Name = "checkboxHideLinks";
            checkboxHideLinks.Size = new System.Drawing.Size(78, 19);
            checkboxHideLinks.TabIndex = 11;
            checkboxHideLinks.Text = "Hide links";
            checkboxHideLinks.UseVisualStyleBackColor = true;
            checkboxHideLinks.CheckedChanged += checkboxHideLinks_CheckedChanged;
            // 
            // checkboxHideCreated
            // 
            checkboxHideCreated.AutoSize = true;
            checkboxHideCreated.Location = new System.Drawing.Point(230, 142);
            checkboxHideCreated.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkboxHideCreated.Name = "checkboxHideCreated";
            checkboxHideCreated.Size = new System.Drawing.Size(93, 19);
            checkboxHideCreated.TabIndex = 12;
            checkboxHideCreated.Text = "Hide created";
            checkboxHideCreated.UseVisualStyleBackColor = true;
            checkboxHideCreated.CheckedChanged += checkHideCreated_CheckedChanged;
            // 
            // checkboxHideModified
            // 
            checkboxHideModified.AutoSize = true;
            checkboxHideModified.Location = new System.Drawing.Point(358, 142);
            checkboxHideModified.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkboxHideModified.Name = "checkboxHideModified";
            checkboxHideModified.Size = new System.Drawing.Size(102, 19);
            checkboxHideModified.TabIndex = 13;
            checkboxHideModified.Text = "Hide modified";
            checkboxHideModified.UseVisualStyleBackColor = true;
            checkboxHideModified.CheckedChanged += checkboxHideModified_CheckedChanged;
            // 
            // checkboxHideDeleted
            // 
            checkboxHideDeleted.AutoSize = true;
            checkboxHideDeleted.Location = new System.Drawing.Point(463, 142);
            checkboxHideDeleted.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkboxHideDeleted.Name = "checkboxHideDeleted";
            checkboxHideDeleted.Size = new System.Drawing.Size(93, 19);
            checkboxHideDeleted.TabIndex = 14;
            checkboxHideDeleted.Text = "Hide deleted";
            checkboxHideDeleted.UseVisualStyleBackColor = true;
            checkboxHideDeleted.CheckedChanged += checkboxHideDeleted_CheckedChanged;
            // 
            // buttonSwitchView
            // 
            buttonSwitchView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            buttonSwitchView.Location = new System.Drawing.Point(1385, 152);
            buttonSwitchView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonSwitchView.Name = "buttonSwitchView";
            buttonSwitchView.Size = new System.Drawing.Size(99, 30);
            buttonSwitchView.TabIndex = 15;
            buttonSwitchView.TabStop = false;
            buttonSwitchView.Text = "listview";
            buttonSwitchView.UseVisualStyleBackColor = true;
            buttonSwitchView.Click += buttonSwitchView_Click;
            // 
            // panelTreeView
            // 
            panelTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelTreeView.Controls.Add(treeviewDifferences);
            panelTreeView.Controls.Add(panelTreeViewDetails);
            panelTreeView.Location = new System.Drawing.Point(14, 196);
            panelTreeView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelTreeView.Name = "panelTreeView";
            panelTreeView.Size = new System.Drawing.Size(1470, 577);
            panelTreeView.TabIndex = 16;
            panelTreeView.Visible = false;
            // 
            // treeviewDifferences
            // 
            treeviewDifferences.Location = new System.Drawing.Point(5, 4);
            treeviewDifferences.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            treeviewDifferences.Name = "treeviewDifferences";
            treeviewDifferences.Size = new System.Drawing.Size(864, 566);
            treeviewDifferences.TabIndex = 0;
            treeviewDifferences.AfterSelect += treeviewDifferences_AfterSelect;
            // 
            // panelTreeViewDetails
            // 
            panelTreeViewDetails.BackColor = System.Drawing.Color.White;
            panelTreeViewDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelTreeViewDetails.Location = new System.Drawing.Point(875, 5);
            panelTreeViewDetails.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelTreeViewDetails.Name = "panelTreeViewDetails";
            panelTreeViewDetails.Size = new System.Drawing.Size(589, 566);
            panelTreeViewDetails.TabIndex = 1;
            // 
            // buttonExportAsTxt
            // 
            buttonExportAsTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            buttonExportAsTxt.Location = new System.Drawing.Point(1291, 110);
            buttonExportAsTxt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonExportAsTxt.Name = "buttonExportAsTxt";
            buttonExportAsTxt.Size = new System.Drawing.Size(194, 30);
            buttonExportAsTxt.TabIndex = 17;
            buttonExportAsTxt.TabStop = false;
            buttonExportAsTxt.Text = "Export diffrences as txt";
            buttonExportAsTxt.UseVisualStyleBackColor = true;
            buttonExportAsTxt.Click += buttonExportAsTxt_Click;
            // 
            // checkboxShowWindowOnTop
            // 
            checkboxShowWindowOnTop.AutoSize = true;
            checkboxShowWindowOnTop.Location = new System.Drawing.Point(1347, 44);
            checkboxShowWindowOnTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkboxShowWindowOnTop.Name = "checkboxShowWindowOnTop";
            checkboxShowWindowOnTop.Size = new System.Drawing.Size(138, 19);
            checkboxShowWindowOnTop.TabIndex = 18;
            checkboxShowWindowOnTop.Text = "Show window on top";
            checkboxShowWindowOnTop.UseVisualStyleBackColor = true;
            checkboxShowWindowOnTop.CheckedChanged += checkboxShowWindowOnTop_CheckedChanged;
            // 
            // checkboxApplyFiltersForExport
            // 
            checkboxApplyFiltersForExport.AutoSize = true;
            checkboxApplyFiltersForExport.Location = new System.Drawing.Point(1139, 116);
            checkboxApplyFiltersForExport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkboxApplyFiltersForExport.Name = "checkboxApplyFiltersForExport";
            checkboxApplyFiltersForExport.Size = new System.Drawing.Size(144, 19);
            checkboxApplyFiltersForExport.TabIndex = 19;
            checkboxApplyFiltersForExport.Text = "Apply filters for export";
            checkboxApplyFiltersForExport.UseVisualStyleBackColor = true;
            //
            // menuStripSettings
            //
            menuStripSettings.BackColor = System.Drawing.Color.White;
            menuStripSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { settingsToolStripMenuItem });
            menuStripSettings.Location = new System.Drawing.Point(0, 0);
            menuStripSettings.Name = "menuStripSettings";
            menuStripSettings.Size = new System.Drawing.Size(1498, 24);
            menuStripSettings.TabIndex = 20;
            menuStripSettings.Text = "Settings menu";
            //
            // settingsToolStripMenuItem
            //
            settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { rememberPakPathsMenuItem, settingsFileLocationMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            //
            // rememberPakPathsMenuItem
            //
            rememberPakPathsMenuItem.CheckOnClick = true;
            rememberPakPathsMenuItem.Name = "rememberPakPathsMenuItem";
            rememberPakPathsMenuItem.Size = new System.Drawing.Size(416, 22);
            rememberPakPathsMenuItem.Text = "Remember selected COMMANDS.PAK paths";
            rememberPakPathsMenuItem.Click += rememberPakPathsMenuItem_Click;
            //
            // settingsFileLocationMenuItem
            //
            settingsFileLocationMenuItem.Enabled = false;
            settingsFileLocationMenuItem.Name = "settingsFileLocationMenuItem";
            settingsFileLocationMenuItem.Size = new System.Drawing.Size(416, 22);
            this.settingsFileLocationMenuItem.Text = "Settings file: %APPDATA%\\AIPAKDifferentiation\\settings.json";
            // 
            // AIPAKDifferentiation
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1498, 787);
            Controls.Add(menuStripSettings);
            Controls.Add(checkboxApplyFiltersForExport);
            Controls.Add(checkboxShowWindowOnTop);
            Controls.Add(buttonExportAsTxt);
            Controls.Add(panelTreeView);
            Controls.Add(buttonSwitchView);
            Controls.Add(checkboxHideDeleted);
            Controls.Add(checkboxHideModified);
            Controls.Add(checkboxHideCreated);
            Controls.Add(checkboxHideLinks);
            Controls.Add(checkboxHideParameters);
            Controls.Add(checkboxHideEntities);
            Controls.Add(checkboxHideComposites);
            Controls.Add(labelFilters);
            Controls.Add(checkboxEntityHideOverrides);
            Controls.Add(labelPak2);
            Controls.Add(labelPak1);
            Controls.Add(buttonBrowsePak2);
            Controls.Add(buttonBrowsePak1);
            Controls.Add(labelDifferences);
            Controls.Add(listviewDifferences);
            Controls.Add(buttonPakShowDifferences);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStripSettings;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MaximumSize = new System.Drawing.Size(1514, 826);
            MinimumSize = new System.Drawing.Size(1514, 826);
            Name = "AIPAKDifferentiation";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "AIPAKDifferentiation";
            Load += AIPAKDifferentiation_Load;
            panelTreeView.ResumeLayout(false);
            menuStripSettings.ResumeLayout(false);
            menuStripSettings.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPakShowDifferences;
        private System.Windows.Forms.ListView listviewDifferences;
        private System.Windows.Forms.Label labelDifferences;
        private System.Windows.Forms.ColumnHeader columnCathodeType;
        private System.Windows.Forms.ColumnHeader columnValueBefore;
        private System.Windows.Forms.ColumnHeader columnDifferenceType;
        private System.Windows.Forms.ColumnHeader columnGuid;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.Button buttonBrowsePak1;
        private System.Windows.Forms.Button buttonBrowsePak2;
        private System.Windows.Forms.Label labelPak1;
        private System.Windows.Forms.Label labelPak2;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.CheckBox checkboxEntityHideOverrides;
        private System.Windows.Forms.ColumnHeader columnValueAfter;
        private System.Windows.Forms.Label labelFilters;
        private System.Windows.Forms.CheckBox checkboxHideComposites;
        private System.Windows.Forms.CheckBox checkboxHideEntities;
        private System.Windows.Forms.CheckBox checkboxHideParameters;
        private System.Windows.Forms.CheckBox checkboxHideLinks;
        private System.Windows.Forms.CheckBox checkboxHideCreated;
        private System.Windows.Forms.CheckBox checkboxHideModified;
        private System.Windows.Forms.CheckBox checkboxHideDeleted;
        private System.Windows.Forms.Button buttonSwitchView;
        private System.Windows.Forms.Panel panelTreeView;
        private System.Windows.Forms.Panel panelTreeViewDetails;
        private System.Windows.Forms.TreeView treeviewDifferences;
        private System.Windows.Forms.Button buttonExportAsTxt;
        private System.Windows.Forms.CheckBox checkboxShowWindowOnTop;
        private System.Windows.Forms.CheckBox checkboxApplyFiltersForExport;
        private System.Windows.Forms.MenuStrip menuStripSettings;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rememberPakPathsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsFileLocationMenuItem;
    }
}

