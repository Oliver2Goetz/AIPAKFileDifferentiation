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

using CATHODE;
using CATHODE.Scripting;

namespace AIPAKDifferentiation {

    public partial class AIPAKDifferentiation : Form {

        private string pakPath1 = "";
        private string pakPath2 = "";
        EntityUtils entityUtilsPak1 = null;
        EntityUtils entityUtilsPak2 = null;

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
            this.entityUtilsPak1 = pakFileDifferentiation.entityUtilsPak1;
            this.entityUtilsPak2 = pakFileDifferentiation.entityUtilsPak2;
            this.compositeDifferences = pakFileDifferentiation.loadDifferences();
        }

        private void drawDifferencesToListView(List<CompositeDifference> differences) {
            if (null != differences) {
                foreach (CompositeDifference compositeDifference in differences) {
                    ListViewItemEntry compositeEntry = new ListViewItemEntry(
                        CATHODE_TYPE.COMPOSITE,
                        compositeDifference.composite.shortGUID.ToString(),
                        compositeDifference.composite.name,
                        "-",
                        compositeDifference.differenceType.ToString()
                    );

                    listviewDifferences.Items.Add(new ListViewItem(compositeEntry.ToStringArray()));

                    foreach (EntityDifference entityDifference in compositeDifference.entityDifferences) {
                        if (this.isValidEntityToShow(entityDifference)) {
                            ListViewItemEntry entityEntry = new ListViewItemEntry(
                                CATHODE_TYPE.ENTITY,
                                entityDifference.entity.shortGUID.ToString(),
                                entityUtilsPak1.GetName(compositeDifference.composite.shortGUID, entityDifference.entity.shortGUID),
                                entityDifference.entity.variant.ToString(),
                                entityDifference.differenceType.ToString()
                            );

                            listviewDifferences.Items.Add(new ListViewItem(entityEntry.ToStringArray()));

                            foreach (ParameterDifference parameterDifference in entityDifference.parameterDiffereces) {
                                ListViewItemEntry parameterEntry = new ListViewItemEntry(
                                    CATHODE_TYPE.PARAMETER,
                                    parameterDifference.parameter.shortGUID.ToString(),
                                    parameterDifference.parameter.variant.ToString(),
                                    parameterDifference.parameter.content.dataType.ToString(),
                                    parameterDifference.differenceType.ToString()
                                );

                                listviewDifferences.Items.Add(new ListViewItem(parameterEntry.ToStringArray()));
                            }

                            foreach (LinkDifference linkDifference in entityDifference.linkDifferences) {
                                ListViewItemEntry linkEntry = new ListViewItemEntry(
                                    CATHODE_TYPE.LINK,
                                    linkDifference.link.connectionID.ToString(),
                                    "childID: " + linkDifference.link.childID,
                                    "parentParamID: " + linkDifference.link.parentParamID + " | childParamID: " + linkDifference.link.childParamID,
                                    linkDifference.differenceType.ToString()
                                );

                                listviewDifferences.Items.Add(new ListViewItem(linkEntry.ToStringArray()));
                            }
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

        /*
         * Checks if an entity is valid -> used for filtering unwanted entities out (eg. override)
         */
        private bool isValidEntityToShow(EntityDifference entityDifference) {
            bool isValid = true;

            if (entityDifference.entity.variant == EntityVariant.OVERRIDE && checkboxEntityHideOverrides.Checked) {
                isValid = false;
            }

            return isValid;
        }

        private void checkboxEntityHideOverrides_CheckedChanged(object sender, EventArgs e) {
            listviewDifferences.Items.Clear();
            this.drawDifferencesToListView(this.compositeDifferences);
        }
    }
}
