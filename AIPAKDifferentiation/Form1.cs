using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

using CATHODE.Scripting;

using AIPAKDifferentiation.Views;

namespace AIPAKDifferentiation {

    public partial class AIPAKDifferentiation : Form {

        private string pakPath1 = "";
        private string pakPath2 = "";

        private string pak1Name = "";
        private string pak2Name = "";

        private List<CompositeDifference> compositeDifferences = new List<CompositeDifference>();
        private List<ListViewItem> preparedDifferenceItemList = new List<ListViewItem>();
        private List<TreeNode> preparedDifferenceNodeList = new List<TreeNode>();

        private DifferenceListViewItemList differencesAsListViewItemList = null;
        private DifferenceTreeNodeList differencesAsTreeNodeList = null;

        private DISPLAY_MODE displayMode = DISPLAY_MODE.LISTVIEW;
        private TreeViewDetails treeViewDetails;

        public AIPAKDifferentiation() {
            InitializeComponent();
        }

        private void AIPAKDifferentiation_Load(object sender, EventArgs e) {
            // for testing
            this.pakPath1 = @"C:\Users\Oliver\Desktop\Programming\Alien Isolation\AIPAKDifferentiation\examplePAKs\COMMANDS_TECH_HUB_vanilla.PAK";
            this.pakPath2 = @"C:\Users\Oliver\Desktop\Programming\Alien Isolation\AIPAKDifferentiation\examplePAKs\COMMANDS_TECH_HUB_modified.PAK";

            this.differencesAsListViewItemList = new DifferenceListViewItemList(this);
            this.differencesAsTreeNodeList = new DifferenceTreeNodeList();
        }

        /*
         * button click - show all the differences from the selected PAK files
         * Shows a MessagBox on error
         */
        private void buttonPakShowDifferences_Click(object sender, EventArgs e) {
            if (0 >= this.pakPath1.Length || 0 >= this.pakPath2.Length || !File.Exists(this.pakPath1) || !File.Exists(this.pakPath2)) {
                MessageBox.Show("Select 2 PAK files to compare.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            bool success = this.loadPakFileDifferences();

            if (success) {
                this.treeViewDetails = new TreeViewDetails();
                this.buildActiveView();
            } else {
                listviewDifferences.Items.Clear();
                treeviewDifferences.Nodes.Clear();
            }
        }

        /*
         * Reads both given PAK files and loads all the differences
         * Shows a MessageBox on error
         */
        private bool loadPakFileDifferences() {
            PAKFileDifferentiation pakFileDifferentiation = new PAKFileDifferentiation(this.pakPath1, this.pakPath2);

            string splittedPak1Name = pakFileDifferentiation.pak1.EntryPoints[0].name.Split('\\').Last();
            string splittedPak2Name = pakFileDifferentiation.pak1.EntryPoints[0].name.Split('\\').Last();

            if (splittedPak1Name != splittedPak2Name) {
                string namePak1 = pakFileDifferentiation.pak1.EntryPoints[0].name.Split('\\').Last();
                string namePak2 = pakFileDifferentiation.pak2.EntryPoints[0].name.Split('\\').Last();
                string message = "Both PAKs have to be from the same level!\nPAK 1: " + namePak1 + "\nPAK 2: " + namePak2;
                MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            this.pak1Name = splittedPak1Name;
            this.pak2Name = splittedPak2Name;

            this.compositeDifferences = pakFileDifferentiation.loadDifferences();
            this.differencesAsListViewItemList = new DifferenceListViewItemList(this);
            this.differencesAsTreeNodeList = new DifferenceTreeNodeList();

            return true;
        }

        /*
         * button click - opens file dialog for PAK1
         */
        private void buttonBrowsePak1_Click(object sender, EventArgs e) {
            string initialDirectory = "";
            if (0 < this.pakPath1.Length) {
                string directoryName = Path.GetDirectoryName(this.pakPath1);
                if (Directory.Exists(directoryName)) {
                    initialDirectory = directoryName;
                }
            }

            this.pakPath1 = this.getFileDialogResult("Select PAK 1", initialDirectory);
            labelPak1.Text = "PAK 1: " + this.pakPath1;
            this.setToolTip(labelPak1, this.pakPath1);
        }

        /*
         * button click - opens file dialog for PAK2
         */
        private void buttonBrowsePak2_Click(object sender, EventArgs e) {
            string initialDirectory = "";
            if (0 < this.pakPath2.Length) {
                string directoryName = Path.GetDirectoryName(this.pakPath2);
                if (Directory.Exists(directoryName)) {
                    initialDirectory = directoryName;
                }
            }

            this.pakPath2 = this.getFileDialogResult("Select PAK 2", initialDirectory);
            labelPak2.Text = "PAK 2: " + this.pakPath2;
            this.setToolTip(labelPak2, this.pakPath2);
        }

        /*
         * Opens a file dialog and returns the result
         */
        private string getFileDialogResult(string title, string initialDirectory = "") {
            string result = "";

            fileDialog.Title = title;
            fileDialog.InitialDirectory = initialDirectory;

            if (fileDialog.ShowDialog() == DialogResult.OK) {
                result = fileDialog.FileName;
            }

            return result;
        }

        /*
         * Sets a tooltip onto the given Control
         */
        private void setToolTip(Control control, string text) {
            ToolTip toolTip = new ToolTip();
            toolTip.ToolTipIcon = ToolTipIcon.None;
            toolTip.IsBalloon = false;
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(control, text);
        }

        #region view mode

        /*
         * Build the currently active view
         */
        private void buildActiveView() {
            if (displayMode == DISPLAY_MODE.LISTVIEW) {
                this.buildListView();
            } else {
                this.buildTreeView();
            }
        }

        /*
         * Display the listview with the differences
         */
        private void buildListView() {
            listviewDifferences.Items.Clear();
            this.preparedDifferenceItemList = this.differencesAsListViewItemList.getDifferencesAsListViewItemList(this.compositeDifferences);
            listviewDifferences.Items.AddRange(this.preparedDifferenceItemList.ToArray());
        }

        /*
         * Display the treeview with the differences
         */
        private void buildTreeView() {
            treeviewDifferences.Nodes.Clear();
            this.preparedDifferenceNodeList = this.differencesAsTreeNodeList.getDifferencesAsTreeNodeList(this.compositeDifferences);
            treeviewDifferences.Nodes.AddRange(this.preparedDifferenceNodeList.ToArray());
        }

        /*
         * Button click - switches the view
         */
        private void buttonSwitchView_Click(object sender, EventArgs e) {
            if (displayMode == DISPLAY_MODE.LISTVIEW) {
                listviewDifferences.Hide();
                panelTreeView.Show();
                hideFilters();
                displayMode = DISPLAY_MODE.TREEVIEW;
            } else {
                panelTreeView.Hide();
                listviewDifferences.Show();
                showFilters();
                displayMode = DISPLAY_MODE.LISTVIEW;
            }

            buttonSwitchView.Text = displayMode.ToString().ToLower();
        }

        /*
         * Gets executed once a tree view node has been clicked
         */
        private void treeviewDifferences_AfterSelect(object sender, TreeViewEventArgs e) {
            if (treeviewDifferences.SelectedNode != null) {
                TreeNodeEntry entry = (TreeNodeEntry)treeviewDifferences.SelectedNode.Tag;
                buildTreeViewDetails(entry);
            }
        }

        /*
         * Build the treeview details of hte current selected TreeNode
         */
        private void buildTreeViewDetails(TreeNodeEntry entry) {
            if (null != entry) {
                panelTreeViewDetails.Controls.Clear();
                Label label = this.treeViewDetails.buildTreeViewDetails(entry);
                panelTreeViewDetails.Controls.Add(label);
            }
        }

        #endregion

        #region filters

        /*
         * Shows all the filters
         */
        private void showFilters() {
            labelFilters.Show();
            checkboxHideComposites.Show();
            checkboxHideEntities.Show();
            checkboxHideParameters.Show();
            checkboxHideLinks.Show();
            checkboxEntityHideOverrides.Show();
            checkboxHideCreated.Show();
            checkboxHideModified.Show();
            checkboxHideDeleted.Show();
        }

        /*
         * Hides all the filters
         */
        private void hideFilters() {
            labelFilters.Hide();
            checkboxHideComposites.Hide();
            checkboxHideEntities.Hide();
            checkboxHideParameters.Hide();
            checkboxHideLinks.Hide();
            checkboxEntityHideOverrides.Hide();
            checkboxHideCreated.Hide();
            checkboxHideModified.Hide();
            checkboxHideDeleted.Hide();
        }

        private void checkboxEntityHideOverrides_CheckedChanged(object sender, EventArgs e) {
            this.buildActiveView();
        }

        private void checkboxHideComposites_CheckedChanged(object sender, EventArgs e) {
            this.buildActiveView();
        }

        private void checkboxHideEntities_CheckedChanged(object sender, EventArgs e) {
            this.buildActiveView();
        }

        private void checkboxHideParameters_CheckedChanged(object sender, EventArgs e) {
            this.buildActiveView();
        }

        private void checkboxHideLinks_CheckedChanged(object sender, EventArgs e) {
            this.buildActiveView();
        }

        private void checkHideCreated_CheckedChanged(object sender, EventArgs e) {
            this.buildActiveView();
        }

        private void checkboxHideModified_CheckedChanged(object sender, EventArgs e) {
            this.buildActiveView();
        }

        private void checkboxHideDeleted_CheckedChanged(object sender, EventArgs e) {
            this.buildActiveView();
        }

        #endregion

        /*
         * Exports the file
         */
        private void buttonExportAsTxt_Click(object sender, EventArgs e) {
            if (0 == this.compositeDifferences.Count) {
                MessageBox.Show("Please make sure there are differences existing to use the export.", "Export error");
            } else {

                ExportDifferences export = new ExportDifferences();
                List<string> result = export.export(this.compositeDifferences, this.pak1Name);

                if ("exception" == result[0]) {
                    MessageBox.Show(result[1], "Export Error");
                } else {
                    string message = result[1] + "\n\n" + "Do you want to open the export file?";
                    DialogResult dialogResult = MessageBox.Show(message, "Export Result", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes) {
                        if (File.Exists(result[2])) {
                            Process.Start(result[2]);
                        } else {
                            MessageBox.Show("The file does not exist: " + result[2]);
                        }
                    }
                }
            }
        }
    }
}
