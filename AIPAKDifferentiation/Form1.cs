using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using CATHODE.Commands;

namespace AIPAKDifferentiation {

    public partial class AIPAKDifferentiation : Form {

        private string pakPath1 = "";
        private string pakPath2 = "";

        private List<CompositeDifference> compositeDifferences = new List<CompositeDifference>();

        public AIPAKDifferentiation() {
            InitializeComponent();
        }

        private void AIPAKDifferentiation_Load(object sender, EventArgs e) {
            // for testing
            this.pakPath1 = @"C:\Users\Oliver\Desktop\Programming\Alien Isolation\AIPAKDifferentiation\examplePAKs\COMMANDS_TECH_HUB_vanilla.PAK";
            this.pakPath2 = @"C:\Users\Oliver\Desktop\Programming\Alien Isolation\AIPAKDifferentiation\examplePAKs\COMMANDS_TECH_HUB_modified.PAK";
        }

        private void buttonPakShowDifferences_Click(object sender, EventArgs e) {
            if (0 >= this.pakPath1.Length || 0 >= this.pakPath2.Length || !File.Exists(this.pakPath1) || !File.Exists(this.pakPath2)) {
                MessageBox.Show("Select 2 PAK files to compare.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            listviewDifferences.Items.Clear();

            this.loadPakFileDifferences();
            drawDifferencesToListView(this.compositeDifferences);
        }

        private void loadPakFileDifferences() {
            PAKFileDifferentiation pakFileDifferentiation = new PAKFileDifferentiation(this.pakPath1, this.pakPath2);
            this.compositeDifferences = pakFileDifferentiation.loadDifferences();
        }

        private void drawDifferencesToListView(List<CompositeDifference> differences) {
            if (null != differences) {
                foreach (CompositeDifference compositeDifference in differences) {
                    ListViewItemEntry compositeEntry = new ListViewItemEntry(
                        CATHODE_TYPE.COMPOSITE,
                        compositeDifference.composite.shortGUID,
                        "-",
                        compositeDifference.composite.name,
                        compositeDifference.differenceType.ToString()
                    );

                    listviewDifferences.Items.Add(new ListViewItem(compositeEntry.ToStringArray()));

                    foreach (EntityDifference entityDifference in compositeDifference.entityDifferences) {
                        ListViewItemEntry entityEntry = new ListViewItemEntry(
                            CATHODE_TYPE.ENTITY,
                            entityDifference.entity.shortGUID,
                            compositeDifference.composite.name,
                            entityDifference.entity.variant.ToString(),
                            entityDifference.differenceType.ToString()
                        );

                        listviewDifferences.Items.Add(new ListViewItem(entityEntry.ToStringArray()));

                        foreach (ParameterDifference parameterDifference in entityDifference.parameterDiffereces) {
                            ListViewItemEntry parameterEntry = new ListViewItemEntry(
                                CATHODE_TYPE.PARAMETER,
                                parameterDifference.parameter.shortGUID,
                                compositeDifference.composite.name,
                                parameterDifference.parameter.content.dataType.ToString(),
                                parameterDifference.differenceType.ToString()
                            );

                            listviewDifferences.Items.Add(new ListViewItem(parameterEntry.ToStringArray()));
                        }
                    }
                }
            }
        }

        private void buttonBrowsePak1_Click(object sender, EventArgs e) {
            this.pakPath1 = this.getFileDialogResult("Select PAK 1");
            labelPak1.Text = "PAK 1: " + this.pakPath1;
            this.setToolTip(labelPak1, this.pakPath1);
            
        }

        private void buttonBrowsePak2_Click(object sender, EventArgs e) {
            this.pakPath2 = this.getFileDialogResult("Select PAK 2");
            labelPak2.Text = "PAK 2: " + this.pakPath2;
            this.setToolTip(labelPak2, this.pakPath2);
        }

        private string getFileDialogResult(string title) {
            string result = "";

            fileDialog.Title = title;

            if (fileDialog.ShowDialog() == DialogResult.OK) {
                result = fileDialog.FileName;
            }

            return result;
        }

        private void setToolTip(Control control, string text) {
            ToolTip toolTip = new ToolTip();
            toolTip.ToolTipIcon = ToolTipIcon.None;
            toolTip.IsBalloon = false;
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(control, text);
        }
    }
}
