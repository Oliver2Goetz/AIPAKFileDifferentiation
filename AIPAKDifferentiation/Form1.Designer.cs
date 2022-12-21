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
            this.textboxStatus = new System.Windows.Forms.TextBox();
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
            // textboxStatus
            // 
            this.textboxStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textboxStatus.Location = new System.Drawing.Point(192, 12);
            this.textboxStatus.Multiline = true;
            this.textboxStatus.Name = "textboxStatus";
            this.textboxStatus.ReadOnly = true;
            this.textboxStatus.Size = new System.Drawing.Size(130, 40);
            this.textboxStatus.TabIndex = 0;
            this.textboxStatus.TabStop = false;
            // 
            // AIPAKDifferentiation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textboxStatus);
            this.Controls.Add(this.buttonPakShowDifferences);
            this.Name = "AIPAKDifferentiation";
            this.Text = "AIPAKDifferentiation";
            this.Load += new System.EventHandler(this.AIPAKDifferentiation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPakShowDifferences;
        private System.Windows.Forms.TextBox textboxStatus;
    }
}

