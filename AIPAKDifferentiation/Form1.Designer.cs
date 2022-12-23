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
            this.buttonPakShowDifferences = new System.Windows.Forms.Button();
            this.listviewDifferences = new System.Windows.Forms.ListView();
            this.columnCathodeType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnGuid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCompositeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDifferenceType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelDifferences = new System.Windows.Forms.Label();
            this.buttonBrowsePak1 = new System.Windows.Forms.Button();
            this.buttonBrowsePak2 = new System.Windows.Forms.Button();
            this.labelPak1 = new System.Windows.Forms.Label();
            this.labelPak2 = new System.Windows.Forms.Label();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
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
            this.columnCompositeName,
            this.columnName,
            this.columnDifferenceType});
            this.listviewDifferences.FullRowSelect = true;
            this.listviewDifferences.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listviewDifferences.HideSelection = false;
            this.listviewDifferences.HoverSelection = true;
            this.listviewDifferences.Location = new System.Drawing.Point(12, 149);
            this.listviewDifferences.Name = "listviewDifferences";
            this.listviewDifferences.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listviewDifferences.ShowItemToolTips = true;
            this.listviewDifferences.Size = new System.Drawing.Size(960, 400);
            this.listviewDifferences.TabIndex = 0;
            this.listviewDifferences.TabStop = false;
            this.listviewDifferences.UseCompatibleStateImageBehavior = false;
            this.listviewDifferences.View = System.Windows.Forms.View.Details;
            // 
            // columnCathodeType
            // 
            this.columnCathodeType.Text = "Cathode Type";
            this.columnCathodeType.Width = 100;
            // 
            // columnGuid
            // 
            this.columnGuid.Text = "Guid";
            this.columnGuid.Width = 90;
            // 
            // columnCompositeName
            // 
            this.columnCompositeName.Text = "composite name";
            this.columnCompositeName.Width = 330;
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 330;
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
            // AIPAKDifferentiation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.labelPak2);
            this.Controls.Add(this.labelPak1);
            this.Controls.Add(this.buttonBrowsePak2);
            this.Controls.Add(this.buttonBrowsePak1);
            this.Controls.Add(this.labelDifferences);
            this.Controls.Add(this.listviewDifferences);
            this.Controls.Add(this.buttonPakShowDifferences);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 600);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "AIPAKDifferentiation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AIPAKDifferentiation";
            this.Load += new System.EventHandler(this.AIPAKDifferentiation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPakShowDifferences;
        private System.Windows.Forms.ListView listviewDifferences;
        private System.Windows.Forms.Label labelDifferences;
        private System.Windows.Forms.ColumnHeader columnCathodeType;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnDifferenceType;
        private System.Windows.Forms.ColumnHeader columnGuid;
        private System.Windows.Forms.ColumnHeader columnCompositeName;
        private System.Windows.Forms.Button buttonBrowsePak1;
        private System.Windows.Forms.Button buttonBrowsePak2;
        private System.Windows.Forms.Label labelPak1;
        private System.Windows.Forms.Label labelPak2;
        private System.Windows.Forms.OpenFileDialog fileDialog;
    }
}

