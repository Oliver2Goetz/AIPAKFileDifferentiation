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
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AIPAKDifferentiation));
            this.buttonPakShowDifferences = new System.Windows.Forms.Button();
            this.listviewDifferences = new System.Windows.Forms.ListView();
            this.columnCathodeType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnGuid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnValueBefore = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnValueAfter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDifferenceType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelDifferences = new System.Windows.Forms.Label();
            this.buttonBrowsePak1 = new System.Windows.Forms.Button();
            this.buttonBrowsePak2 = new System.Windows.Forms.Button();
            this.labelPak1 = new System.Windows.Forms.Label();
            this.labelPak2 = new System.Windows.Forms.Label();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.checkboxEntityHideOverrides = new System.Windows.Forms.CheckBox();
            this.labelFilters = new System.Windows.Forms.Label();
            this.checkboxHideComposites = new System.Windows.Forms.CheckBox();
            this.checkboxHideEntities = new System.Windows.Forms.CheckBox();
            this.checkboxHideParameters = new System.Windows.Forms.CheckBox();
            this.checkboxHideLinks = new System.Windows.Forms.CheckBox();
            this.checkboxHideCreated = new System.Windows.Forms.CheckBox();
            this.checkboxHideModified = new System.Windows.Forms.CheckBox();
            this.checkboxHideDeleted = new System.Windows.Forms.CheckBox();
            this.buttonSwitchView = new System.Windows.Forms.Button();
            this.panelTreeView = new System.Windows.Forms.Panel();
            this.treeviewDifferences = new System.Windows.Forms.TreeView();
            this.panelTreeViewDetails = new System.Windows.Forms.Panel();
            this.buttonExportAsTxt = new System.Windows.Forms.Button();
            this.checkboxShowWindowOnTop = new System.Windows.Forms.CheckBox();
            this.panelTreeView.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPakShowDifferences
            // 
            this.buttonPakShowDifferences.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonPakShowDifferences.Location = new System.Drawing.Point(12, 81);
            this.buttonPakShowDifferences.Name = "buttonPakShowDifferences";
            this.buttonPakShowDifferences.Size = new System.Drawing.Size(130, 38);
            this.buttonPakShowDifferences.TabIndex = 0;
            this.buttonPakShowDifferences.TabStop = false;
            this.buttonPakShowDifferences.Text = "Show differences";
            this.buttonPakShowDifferences.UseVisualStyleBackColor = true;
            this.buttonPakShowDifferences.Click += new System.EventHandler(this.buttonPakShowDifferences_Click);
            // 
            // listviewDifferences
            // 
            this.listviewDifferences.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listviewDifferences.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnCathodeType,
            this.columnGuid,
            this.columnName,
            this.columnValueBefore,
            this.columnValueAfter,
            this.columnDifferenceType});
            this.listviewDifferences.FullRowSelect = true;
            this.listviewDifferences.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listviewDifferences.HideSelection = false;
            this.listviewDifferences.Location = new System.Drawing.Point(12, 149);
            this.listviewDifferences.Name = "listviewDifferences";
            this.listviewDifferences.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listviewDifferences.ShowItemToolTips = true;
            this.listviewDifferences.Size = new System.Drawing.Size(1260, 500);
            this.listviewDifferences.TabIndex = 0;
            this.listviewDifferences.TabStop = false;
            this.listviewDifferences.UseCompatibleStateImageBehavior = false;
            this.listviewDifferences.View = System.Windows.Forms.View.Details;
            // 
            // columnCathodeType
            // 
            this.columnCathodeType.Text = "Cathode Type";
            this.columnCathodeType.Width = 85;
            // 
            // columnGuid
            // 
            this.columnGuid.Text = "Guid";
            this.columnGuid.Width = 75;
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 510;
            // 
            // columnValueBefore
            // 
            this.columnValueBefore.Text = "Initial Value";
            this.columnValueBefore.Width = 240;
            // 
            // columnValueAfter
            // 
            this.columnValueAfter.Text = "New value";
            this.columnValueAfter.Width = 240;
            // 
            // columnDifferenceType
            // 
            this.columnDifferenceType.Text = "Difference Type";
            this.columnDifferenceType.Width = 90;
            // 
            // labelDifferences
            // 
            this.labelDifferences.AutoSize = true;
            this.labelDifferences.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelDifferences.Location = new System.Drawing.Point(12, 129);
            this.labelDifferences.Name = "labelDifferences";
            this.labelDifferences.Size = new System.Drawing.Size(80, 17);
            this.labelDifferences.TabIndex = 2;
            this.labelDifferences.Text = "Differences";
            // 
            // buttonBrowsePak1
            // 
            this.buttonBrowsePak1.Location = new System.Drawing.Point(12, 12);
            this.buttonBrowsePak1.Name = "buttonBrowsePak1";
            this.buttonBrowsePak1.Size = new System.Drawing.Size(130, 25);
            this.buttonBrowsePak1.TabIndex = 3;
            this.buttonBrowsePak1.TabStop = false;
            this.buttonBrowsePak1.Text = "Select PAK 1";
            this.buttonBrowsePak1.UseVisualStyleBackColor = true;
            this.buttonBrowsePak1.Click += new System.EventHandler(this.buttonBrowsePak1_Click);
            // 
            // buttonBrowsePak2
            // 
            this.buttonBrowsePak2.Location = new System.Drawing.Point(12, 40);
            this.buttonBrowsePak2.Name = "buttonBrowsePak2";
            this.buttonBrowsePak2.Size = new System.Drawing.Size(130, 25);
            this.buttonBrowsePak2.TabIndex = 4;
            this.buttonBrowsePak2.TabStop = false;
            this.buttonBrowsePak2.Text = "Select PAK 2";
            this.buttonBrowsePak2.UseVisualStyleBackColor = true;
            this.buttonBrowsePak2.Click += new System.EventHandler(this.buttonBrowsePak2_Click);
            // 
            // labelPak1
            // 
            this.labelPak1.AutoSize = true;
            this.labelPak1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.labelPak1.Location = new System.Drawing.Point(148, 16);
            this.labelPak1.Name = "labelPak1";
            this.labelPak1.Size = new System.Drawing.Size(50, 15);
            this.labelPak1.TabIndex = 5;
            this.labelPak1.Text = "PAK 1: -";
            // 
            // labelPak2
            // 
            this.labelPak2.AutoSize = true;
            this.labelPak2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.labelPak2.Location = new System.Drawing.Point(148, 44);
            this.labelPak2.Name = "labelPak2";
            this.labelPak2.Size = new System.Drawing.Size(50, 15);
            this.labelPak2.TabIndex = 6;
            this.labelPak2.Text = "PAK 2: -";
            // 
            // fileDialog
            // 
            this.fileDialog.DefaultExt = "pak";
            this.fileDialog.Filter = "PAK files (*.pak)|*pak";
            this.fileDialog.InitialDirectory = "C:\\";
            // 
            // checkboxEntityHideOverrides
            // 
            this.checkboxEntityHideOverrides.AutoSize = true;
            this.checkboxEntityHideOverrides.Location = new System.Drawing.Point(610, 81);
            this.checkboxEntityHideOverrides.Name = "checkboxEntityHideOverrides";
            this.checkboxEntityHideOverrides.Size = new System.Drawing.Size(143, 17);
            this.checkboxEntityHideOverrides.TabIndex = 7;
            this.checkboxEntityHideOverrides.Text = "Hide OVERRIDE entities";
            this.checkboxEntityHideOverrides.UseVisualStyleBackColor = true;
            this.checkboxEntityHideOverrides.CheckedChanged += new System.EventHandler(this.checkboxEntityHideOverrides_CheckedChanged);
            // 
            // labelFilters
            // 
            this.labelFilters.AutoSize = true;
            this.labelFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.labelFilters.Location = new System.Drawing.Point(148, 80);
            this.labelFilters.Name = "labelFilters";
            this.labelFilters.Size = new System.Drawing.Size(43, 15);
            this.labelFilters.TabIndex = 8;
            this.labelFilters.Text = "Filters:";
            // 
            // checkboxHideComposites
            // 
            this.checkboxHideComposites.AutoSize = true;
            this.checkboxHideComposites.Location = new System.Drawing.Point(197, 81);
            this.checkboxHideComposites.Name = "checkboxHideComposites";
            this.checkboxHideComposites.Size = new System.Drawing.Size(104, 17);
            this.checkboxHideComposites.TabIndex = 9;
            this.checkboxHideComposites.Text = "Hide composites";
            this.checkboxHideComposites.UseVisualStyleBackColor = true;
            this.checkboxHideComposites.CheckedChanged += new System.EventHandler(this.checkboxHideComposites_CheckedChanged);
            // 
            // checkboxHideEntities
            // 
            this.checkboxHideEntities.AutoSize = true;
            this.checkboxHideEntities.Location = new System.Drawing.Point(307, 81);
            this.checkboxHideEntities.Name = "checkboxHideEntities";
            this.checkboxHideEntities.Size = new System.Drawing.Size(84, 17);
            this.checkboxHideEntities.TabIndex = 10;
            this.checkboxHideEntities.Text = "Hide entities";
            this.checkboxHideEntities.UseVisualStyleBackColor = true;
            this.checkboxHideEntities.CheckedChanged += new System.EventHandler(this.checkboxHideEntities_CheckedChanged);
            // 
            // checkboxHideParameters
            // 
            this.checkboxHideParameters.AutoSize = true;
            this.checkboxHideParameters.Location = new System.Drawing.Point(397, 81);
            this.checkboxHideParameters.Name = "checkboxHideParameters";
            this.checkboxHideParameters.Size = new System.Drawing.Size(103, 17);
            this.checkboxHideParameters.TabIndex = 11;
            this.checkboxHideParameters.Text = "Hide parameters";
            this.checkboxHideParameters.UseVisualStyleBackColor = true;
            this.checkboxHideParameters.CheckedChanged += new System.EventHandler(this.checkboxHideParameters_CheckedChanged);
            // 
            // checkboxHideLinks
            // 
            this.checkboxHideLinks.AutoSize = true;
            this.checkboxHideLinks.Location = new System.Drawing.Point(506, 81);
            this.checkboxHideLinks.Name = "checkboxHideLinks";
            this.checkboxHideLinks.Size = new System.Drawing.Size(72, 17);
            this.checkboxHideLinks.TabIndex = 11;
            this.checkboxHideLinks.Text = "Hide links";
            this.checkboxHideLinks.UseVisualStyleBackColor = true;
            this.checkboxHideLinks.CheckedChanged += new System.EventHandler(this.checkboxHideLinks_CheckedChanged);
            // 
            // checkboxHideCreated
            // 
            this.checkboxHideCreated.AutoSize = true;
            this.checkboxHideCreated.Location = new System.Drawing.Point(197, 102);
            this.checkboxHideCreated.Name = "checkboxHideCreated";
            this.checkboxHideCreated.Size = new System.Drawing.Size(87, 17);
            this.checkboxHideCreated.TabIndex = 12;
            this.checkboxHideCreated.Text = "Hide created";
            this.checkboxHideCreated.UseVisualStyleBackColor = true;
            this.checkboxHideCreated.CheckedChanged += new System.EventHandler(this.checkHideCreated_CheckedChanged);
            // 
            // checkboxHideModified
            // 
            this.checkboxHideModified.AutoSize = true;
            this.checkboxHideModified.Location = new System.Drawing.Point(307, 102);
            this.checkboxHideModified.Name = "checkboxHideModified";
            this.checkboxHideModified.Size = new System.Drawing.Size(90, 17);
            this.checkboxHideModified.TabIndex = 13;
            this.checkboxHideModified.Text = "Hide modified";
            this.checkboxHideModified.UseVisualStyleBackColor = true;
            this.checkboxHideModified.CheckedChanged += new System.EventHandler(this.checkboxHideModified_CheckedChanged);
            // 
            // checkboxHideDeleted
            // 
            this.checkboxHideDeleted.AutoSize = true;
            this.checkboxHideDeleted.Location = new System.Drawing.Point(397, 102);
            this.checkboxHideDeleted.Name = "checkboxHideDeleted";
            this.checkboxHideDeleted.Size = new System.Drawing.Size(86, 17);
            this.checkboxHideDeleted.TabIndex = 14;
            this.checkboxHideDeleted.Text = "Hide deleted";
            this.checkboxHideDeleted.UseVisualStyleBackColor = true;
            this.checkboxHideDeleted.CheckedChanged += new System.EventHandler(this.checkboxHideDeleted_CheckedChanged);
            // 
            // buttonSwitchView
            // 
            this.buttonSwitchView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonSwitchView.Location = new System.Drawing.Point(1187, 102);
            this.buttonSwitchView.Name = "buttonSwitchView";
            this.buttonSwitchView.Size = new System.Drawing.Size(85, 26);
            this.buttonSwitchView.TabIndex = 15;
            this.buttonSwitchView.TabStop = false;
            this.buttonSwitchView.Text = "listview";
            this.buttonSwitchView.UseVisualStyleBackColor = true;
            this.buttonSwitchView.Click += new System.EventHandler(this.buttonSwitchView_Click);
            // 
            // panelTreeView
            // 
            this.panelTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTreeView.Controls.Add(this.treeviewDifferences);
            this.panelTreeView.Controls.Add(this.panelTreeViewDetails);
            this.panelTreeView.Location = new System.Drawing.Point(12, 149);
            this.panelTreeView.Name = "panelTreeView";
            this.panelTreeView.Size = new System.Drawing.Size(1260, 500);
            this.panelTreeView.TabIndex = 16;
            this.panelTreeView.Visible = false;
            // 
            // treeviewDifferences
            // 
            this.treeviewDifferences.Location = new System.Drawing.Point(4, 4);
            this.treeviewDifferences.Name = "treeviewDifferences";
            this.treeviewDifferences.Size = new System.Drawing.Size(741, 491);
            this.treeviewDifferences.TabIndex = 0;
            this.treeviewDifferences.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeviewDifferences_AfterSelect);
            // 
            // panelTreeViewDetails
            // 
            this.panelTreeViewDetails.BackColor = System.Drawing.Color.White;
            this.panelTreeViewDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTreeViewDetails.Location = new System.Drawing.Point(750, 4);
            this.panelTreeViewDetails.Name = "panelTreeViewDetails";
            this.panelTreeViewDetails.Size = new System.Drawing.Size(505, 491);
            this.panelTreeViewDetails.TabIndex = 1;
            // 
            // buttonExportAsTxt
            // 
            this.buttonExportAsTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonExportAsTxt.Location = new System.Drawing.Point(1106, 69);
            this.buttonExportAsTxt.Name = "buttonExportAsTxt";
            this.buttonExportAsTxt.Size = new System.Drawing.Size(166, 26);
            this.buttonExportAsTxt.TabIndex = 17;
            this.buttonExportAsTxt.TabStop = false;
            this.buttonExportAsTxt.Text = "Export diffrences as txt";
            this.buttonExportAsTxt.UseVisualStyleBackColor = true;
            this.buttonExportAsTxt.Click += new System.EventHandler(this.buttonExportAsTxt_Click);
            // 
            // checkboxShowWindowOnTop
            // 
            this.checkboxShowWindowOnTop.AutoSize = true;
            this.checkboxShowWindowOnTop.Location = new System.Drawing.Point(975, 75);
            this.checkboxShowWindowOnTop.Name = "checkboxShowWindowOnTop";
            this.checkboxShowWindowOnTop.Size = new System.Drawing.Size(125, 17);
            this.checkboxShowWindowOnTop.TabIndex = 18;
            this.checkboxShowWindowOnTop.Text = "Show window on top";
            this.checkboxShowWindowOnTop.UseVisualStyleBackColor = true;
            this.checkboxShowWindowOnTop.CheckedChanged += new System.EventHandler(this.checkboxShowWindowOnTop_CheckedChanged);
            // 
            // AIPAKDifferentiation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 661);
            this.Controls.Add(this.checkboxShowWindowOnTop);
            this.Controls.Add(this.buttonExportAsTxt);
            this.Controls.Add(this.panelTreeView);
            this.Controls.Add(this.buttonSwitchView);
            this.Controls.Add(this.checkboxHideDeleted);
            this.Controls.Add(this.checkboxHideModified);
            this.Controls.Add(this.checkboxHideCreated);
            this.Controls.Add(this.checkboxHideLinks);
            this.Controls.Add(this.checkboxHideParameters);
            this.Controls.Add(this.checkboxHideEntities);
            this.Controls.Add(this.checkboxHideComposites);
            this.Controls.Add(this.labelFilters);
            this.Controls.Add(this.checkboxEntityHideOverrides);
            this.Controls.Add(this.labelPak2);
            this.Controls.Add(this.labelPak1);
            this.Controls.Add(this.buttonBrowsePak2);
            this.Controls.Add(this.buttonBrowsePak1);
            this.Controls.Add(this.labelDifferences);
            this.Controls.Add(this.listviewDifferences);
            this.Controls.Add(this.buttonPakShowDifferences);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1300, 700);
            this.MinimumSize = new System.Drawing.Size(1300, 700);
            this.Name = "AIPAKDifferentiation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AIPAKDifferentiation";
            this.Load += new System.EventHandler(this.AIPAKDifferentiation_Load);
            this.panelTreeView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}

