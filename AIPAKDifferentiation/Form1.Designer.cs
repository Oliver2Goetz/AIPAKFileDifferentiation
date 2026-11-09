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
            this.columnCathodeType = new System.Windows.Forms.ColumnHeader();
            this.columnGuid = new System.Windows.Forms.ColumnHeader();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.columnValueBefore = new System.Windows.Forms.ColumnHeader();
            this.columnValueAfter = new System.Windows.Forms.ColumnHeader();
            this.columnDifferenceType = new System.Windows.Forms.ColumnHeader();
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
            this.buttonPakShowDifferences.Location = new System.Drawing.Point(14, 93);
            this.buttonPakShowDifferences.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonPakShowDifferences.Name = "buttonPakShowDifferences";
            this.buttonPakShowDifferences.Size = new System.Drawing.Size(152, 44);
            this.buttonPakShowDifferences.TabIndex = 0;
            this.buttonPakShowDifferences.TabStop = false;
            this.buttonPakShowDifferences.Text = "Show differences";
            this.buttonPakShowDifferences.UseVisualStyleBackColor = true;
            this.buttonPakShowDifferences.Click += this.buttonPakShowDifferences_Click;
            // 
            // listviewDifferences
            // 
            this.listviewDifferences.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listviewDifferences.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.columnCathodeType, this.columnGuid, this.columnName, this.columnValueBefore, this.columnValueAfter, this.columnDifferenceType });
            this.listviewDifferences.FullRowSelect = true;
            this.listviewDifferences.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listviewDifferences.Location = new System.Drawing.Point(14, 172);
            this.listviewDifferences.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listviewDifferences.Name = "listviewDifferences";
            this.listviewDifferences.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listviewDifferences.ShowItemToolTips = true;
            this.listviewDifferences.Size = new System.Drawing.Size(1470, 577);
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
            this.labelDifferences.Location = new System.Drawing.Point(14, 149);
            this.labelDifferences.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDifferences.Name = "labelDifferences";
            this.labelDifferences.Size = new System.Drawing.Size(80, 17);
            this.labelDifferences.TabIndex = 2;
            this.labelDifferences.Text = "Differences";
            // 
            // buttonBrowsePak1
            // 
            this.buttonBrowsePak1.Location = new System.Drawing.Point(14, 14);
            this.buttonBrowsePak1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonBrowsePak1.Name = "buttonBrowsePak1";
            this.buttonBrowsePak1.Size = new System.Drawing.Size(152, 29);
            this.buttonBrowsePak1.TabIndex = 3;
            this.buttonBrowsePak1.TabStop = false;
            this.buttonBrowsePak1.Text = "Select PAK 1";
            this.buttonBrowsePak1.UseVisualStyleBackColor = true;
            this.buttonBrowsePak1.Click += this.buttonBrowsePak1_Click;
            // 
            // buttonBrowsePak2
            // 
            this.buttonBrowsePak2.Location = new System.Drawing.Point(14, 46);
            this.buttonBrowsePak2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonBrowsePak2.Name = "buttonBrowsePak2";
            this.buttonBrowsePak2.Size = new System.Drawing.Size(152, 29);
            this.buttonBrowsePak2.TabIndex = 4;
            this.buttonBrowsePak2.TabStop = false;
            this.buttonBrowsePak2.Text = "Select PAK 2";
            this.buttonBrowsePak2.UseVisualStyleBackColor = true;
            this.buttonBrowsePak2.Click += this.buttonBrowsePak2_Click;
            // 
            // labelPak1
            // 
            this.labelPak1.AutoSize = true;
            this.labelPak1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.labelPak1.Location = new System.Drawing.Point(173, 18);
            this.labelPak1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPak1.Name = "labelPak1";
            this.labelPak1.Size = new System.Drawing.Size(50, 15);
            this.labelPak1.TabIndex = 5;
            this.labelPak1.Text = "PAK 1: -";
            // 
            // labelPak2
            // 
            this.labelPak2.AutoSize = true;
            this.labelPak2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.labelPak2.Location = new System.Drawing.Point(173, 51);
            this.labelPak2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
            this.checkboxEntityHideOverrides.Location = new System.Drawing.Point(712, 93);
            this.checkboxEntityHideOverrides.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkboxEntityHideOverrides.Name = "checkboxEntityHideOverrides";
            this.checkboxEntityHideOverrides.Size = new System.Drawing.Size(126, 19);
            this.checkboxEntityHideOverrides.TabIndex = 7;
            this.checkboxEntityHideOverrides.Text = "Hide ALIAS entities";
            this.checkboxEntityHideOverrides.UseVisualStyleBackColor = true;
            this.checkboxEntityHideOverrides.CheckedChanged += this.checkboxEntityHideOverrides_CheckedChanged;
            // 
            // labelFilters
            // 
            this.labelFilters.AutoSize = true;
            this.labelFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.labelFilters.Location = new System.Drawing.Point(173, 92);
            this.labelFilters.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFilters.Name = "labelFilters";
            this.labelFilters.Size = new System.Drawing.Size(43, 15);
            this.labelFilters.TabIndex = 8;
            this.labelFilters.Text = "Filters:";
            // 
            // checkboxHideComposites
            // 
            this.checkboxHideComposites.AutoSize = true;
            this.checkboxHideComposites.Location = new System.Drawing.Point(230, 93);
            this.checkboxHideComposites.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkboxHideComposites.Name = "checkboxHideComposites";
            this.checkboxHideComposites.Size = new System.Drawing.Size(115, 19);
            this.checkboxHideComposites.TabIndex = 9;
            this.checkboxHideComposites.Text = "Hide composites";
            this.checkboxHideComposites.UseVisualStyleBackColor = true;
            this.checkboxHideComposites.CheckedChanged += this.checkboxHideComposites_CheckedChanged;
            // 
            // checkboxHideEntities
            // 
            this.checkboxHideEntities.AutoSize = true;
            this.checkboxHideEntities.Location = new System.Drawing.Point(358, 93);
            this.checkboxHideEntities.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkboxHideEntities.Name = "checkboxHideEntities";
            this.checkboxHideEntities.Size = new System.Drawing.Size(92, 19);
            this.checkboxHideEntities.TabIndex = 10;
            this.checkboxHideEntities.Text = "Hide entities";
            this.checkboxHideEntities.UseVisualStyleBackColor = true;
            this.checkboxHideEntities.CheckedChanged += this.checkboxHideEntities_CheckedChanged;
            // 
            // checkboxHideParameters
            // 
            this.checkboxHideParameters.AutoSize = true;
            this.checkboxHideParameters.Location = new System.Drawing.Point(463, 93);
            this.checkboxHideParameters.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkboxHideParameters.Name = "checkboxHideParameters";
            this.checkboxHideParameters.Size = new System.Drawing.Size(113, 19);
            this.checkboxHideParameters.TabIndex = 11;
            this.checkboxHideParameters.Text = "Hide parameters";
            this.checkboxHideParameters.UseVisualStyleBackColor = true;
            this.checkboxHideParameters.CheckedChanged += this.checkboxHideParameters_CheckedChanged;
            // 
            // checkboxHideLinks
            // 
            this.checkboxHideLinks.AutoSize = true;
            this.checkboxHideLinks.Location = new System.Drawing.Point(590, 93);
            this.checkboxHideLinks.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkboxHideLinks.Name = "checkboxHideLinks";
            this.checkboxHideLinks.Size = new System.Drawing.Size(78, 19);
            this.checkboxHideLinks.TabIndex = 11;
            this.checkboxHideLinks.Text = "Hide links";
            this.checkboxHideLinks.UseVisualStyleBackColor = true;
            this.checkboxHideLinks.CheckedChanged += this.checkboxHideLinks_CheckedChanged;
            // 
            // checkboxHideCreated
            // 
            this.checkboxHideCreated.AutoSize = true;
            this.checkboxHideCreated.Location = new System.Drawing.Point(230, 118);
            this.checkboxHideCreated.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkboxHideCreated.Name = "checkboxHideCreated";
            this.checkboxHideCreated.Size = new System.Drawing.Size(93, 19);
            this.checkboxHideCreated.TabIndex = 12;
            this.checkboxHideCreated.Text = "Hide created";
            this.checkboxHideCreated.UseVisualStyleBackColor = true;
            this.checkboxHideCreated.CheckedChanged += this.checkHideCreated_CheckedChanged;
            // 
            // checkboxHideModified
            // 
            this.checkboxHideModified.AutoSize = true;
            this.checkboxHideModified.Location = new System.Drawing.Point(358, 118);
            this.checkboxHideModified.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkboxHideModified.Name = "checkboxHideModified";
            this.checkboxHideModified.Size = new System.Drawing.Size(102, 19);
            this.checkboxHideModified.TabIndex = 13;
            this.checkboxHideModified.Text = "Hide modified";
            this.checkboxHideModified.UseVisualStyleBackColor = true;
            this.checkboxHideModified.CheckedChanged += this.checkboxHideModified_CheckedChanged;
            // 
            // checkboxHideDeleted
            // 
            this.checkboxHideDeleted.AutoSize = true;
            this.checkboxHideDeleted.Location = new System.Drawing.Point(463, 118);
            this.checkboxHideDeleted.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkboxHideDeleted.Name = "checkboxHideDeleted";
            this.checkboxHideDeleted.Size = new System.Drawing.Size(93, 19);
            this.checkboxHideDeleted.TabIndex = 14;
            this.checkboxHideDeleted.Text = "Hide deleted";
            this.checkboxHideDeleted.UseVisualStyleBackColor = true;
            this.checkboxHideDeleted.CheckedChanged += this.checkboxHideDeleted_CheckedChanged;
            // 
            // buttonSwitchView
            // 
            this.buttonSwitchView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonSwitchView.Location = new System.Drawing.Point(1385, 118);
            this.buttonSwitchView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSwitchView.Name = "buttonSwitchView";
            this.buttonSwitchView.Size = new System.Drawing.Size(99, 30);
            this.buttonSwitchView.TabIndex = 15;
            this.buttonSwitchView.TabStop = false;
            this.buttonSwitchView.Text = "listview";
            this.buttonSwitchView.UseVisualStyleBackColor = true;
            this.buttonSwitchView.Click += this.buttonSwitchView_Click;
            // 
            // panelTreeView
            // 
            this.panelTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTreeView.Controls.Add(this.treeviewDifferences);
            this.panelTreeView.Controls.Add(this.panelTreeViewDetails);
            this.panelTreeView.Location = new System.Drawing.Point(14, 172);
            this.panelTreeView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelTreeView.Name = "panelTreeView";
            this.panelTreeView.Size = new System.Drawing.Size(1470, 577);
            this.panelTreeView.TabIndex = 16;
            this.panelTreeView.Visible = false;
            // 
            // treeviewDifferences
            // 
            this.treeviewDifferences.Location = new System.Drawing.Point(5, 4);
            this.treeviewDifferences.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.treeviewDifferences.Name = "treeviewDifferences";
            this.treeviewDifferences.Size = new System.Drawing.Size(864, 566);
            this.treeviewDifferences.TabIndex = 0;
            this.treeviewDifferences.AfterSelect += this.treeviewDifferences_AfterSelect;
            // 
            // panelTreeViewDetails
            // 
            this.panelTreeViewDetails.BackColor = System.Drawing.Color.White;
            this.panelTreeViewDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTreeViewDetails.Location = new System.Drawing.Point(875, 5);
            this.panelTreeViewDetails.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelTreeViewDetails.Name = "panelTreeViewDetails";
            this.panelTreeViewDetails.Size = new System.Drawing.Size(589, 566);
            this.panelTreeViewDetails.TabIndex = 1;
            // 
            // buttonExportAsTxt
            // 
            this.buttonExportAsTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonExportAsTxt.Location = new System.Drawing.Point(1290, 80);
            this.buttonExportAsTxt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonExportAsTxt.Name = "buttonExportAsTxt";
            this.buttonExportAsTxt.Size = new System.Drawing.Size(194, 30);
            this.buttonExportAsTxt.TabIndex = 17;
            this.buttonExportAsTxt.TabStop = false;
            this.buttonExportAsTxt.Text = "Export diffrences as txt";
            this.buttonExportAsTxt.UseVisualStyleBackColor = true;
            this.buttonExportAsTxt.Click += this.buttonExportAsTxt_Click;
            // 
            // checkboxShowWindowOnTop
            // 
            this.checkboxShowWindowOnTop.AutoSize = true;
            this.checkboxShowWindowOnTop.Location = new System.Drawing.Point(1138, 87);
            this.checkboxShowWindowOnTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkboxShowWindowOnTop.Name = "checkboxShowWindowOnTop";
            this.checkboxShowWindowOnTop.Size = new System.Drawing.Size(138, 19);
            this.checkboxShowWindowOnTop.TabIndex = 18;
            this.checkboxShowWindowOnTop.Text = "Show window on top";
            this.checkboxShowWindowOnTop.UseVisualStyleBackColor = true;
            this.checkboxShowWindowOnTop.CheckedChanged += this.checkboxShowWindowOnTop_CheckedChanged;
            // 
            // AIPAKDifferentiation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1498, 763);
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
            this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1514, 802);
            this.MinimumSize = new System.Drawing.Size(1514, 802);
            this.Name = "AIPAKDifferentiation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AIPAKDifferentiation";
            this.Load += this.AIPAKDifferentiation_Load;
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

