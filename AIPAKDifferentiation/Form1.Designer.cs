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
            this.labelDifferences = new System.Windows.Forms.Label();
            this.columnCathodeType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnName2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDifferenceType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // buttonPakShowDifferences
            // 
            this.buttonPakShowDifferences.Location = new System.Drawing.Point(12, 12);
            this.buttonPakShowDifferences.Name = "buttonPakShowDifferences";
            this.buttonPakShowDifferences.Size = new System.Drawing.Size(158, 40);
            this.buttonPakShowDifferences.TabIndex = 0;
            this.buttonPakShowDifferences.Text = "show differences";
            this.buttonPakShowDifferences.UseVisualStyleBackColor = true;
            this.buttonPakShowDifferences.Click += new System.EventHandler(this.buttonPakShowDifferences_Click);
            // 
            // listviewDifferences
            // 
            this.listviewDifferences.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnCathodeType,
            this.columnName2,
            this.columnDifferenceType});
            this.listviewDifferences.HideSelection = false;
            this.listviewDifferences.Location = new System.Drawing.Point(12, 103);
            this.listviewDifferences.Name = "listviewDifferences";
            this.listviewDifferences.Size = new System.Drawing.Size(560, 346);
            this.listviewDifferences.TabIndex = 1;
            this.listviewDifferences.UseCompatibleStateImageBehavior = false;
            this.listviewDifferences.View = System.Windows.Forms.View.Details;
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
            // columnCathodeType
            // 
            this.columnCathodeType.Text = "cathode type";
            this.columnCathodeType.Width = 150;
            // 
            // columnName2
            // 
            this.columnName2.Text = "Name";
            this.columnName2.Width = 300;
            // 
            // columnDifferenceType
            // 
            this.columnDifferenceType.Text = "Difference Type";
            this.columnDifferenceType.Width = 100;
            // 
            // AIPAKDifferentiation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.labelDifferences);
            this.Controls.Add(this.listviewDifferences);
            this.Controls.Add(this.buttonPakShowDifferences);
            this.Name = "AIPAKDifferentiation";
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
        private System.Windows.Forms.ColumnHeader columnName2;
        private System.Windows.Forms.ColumnHeader columnDifferenceType;
    }
}

