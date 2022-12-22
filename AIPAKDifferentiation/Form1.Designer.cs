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
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDifferenceType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelDifferences = new System.Windows.Forms.Label();
            this.columnGuid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCompositeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // buttonPakShowDifferences
            // 
            this.buttonPakShowDifferences.Location = new System.Drawing.Point(12, 12);
            this.buttonPakShowDifferences.Name = "buttonPakShowDifferences";
            this.buttonPakShowDifferences.Size = new System.Drawing.Size(158, 40);
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
            this.listviewDifferences.Location = new System.Drawing.Point(12, 103);
            this.listviewDifferences.Name = "listviewDifferences";
            this.listviewDifferences.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listviewDifferences.ShowItemToolTips = true;
            this.listviewDifferences.Size = new System.Drawing.Size(960, 446);
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
            this.labelDifferences.Location = new System.Drawing.Point(12, 79);
            this.labelDifferences.Name = "labelDifferences";
            this.labelDifferences.Size = new System.Drawing.Size(80, 17);
            this.labelDifferences.TabIndex = 2;
            this.labelDifferences.Text = "Differences";
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
            // AIPAKDifferentiation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.labelDifferences);
            this.Controls.Add(this.listviewDifferences);
            this.Controls.Add(this.buttonPakShowDifferences);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 600);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "AIPAKDifferentiation";
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
    }
}

