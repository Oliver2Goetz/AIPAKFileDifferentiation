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
        private string activeSearch = "";

        public AIPAKDifferentiation() {
            InitializeComponent();
        }

        private void AIPAKDifferentiation_Load(object sender, EventArgs e) {
            this.settingsFileLocationMenuItem.Text = "Settings file: " + PakPathSettings.FilePath;
            try {
                PakPathSettings settings = PakPathSettings.load();
                this.rememberPakPathsMenuItem.Checked = settings.RememberPaths;
                if (settings.RememberPaths) {
                    this.pakPath1 = settings.Pak1Path ?? "";
                    this.pakPath2 = settings.Pak2Path ?? "";
                }
            } catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is System.Text.Json.JsonException) {
                MessageBox.Show("The saved PAK paths could not be read from:\n" + PakPathSettings.FilePath + "\n\n" + ex.Message,
                    "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.updatePakPathLabels();

            this.differencesAsListViewItemList = new DifferenceListViewItemList(this);
            this.differencesAsTreeNodeList = new DifferenceTreeNodeList();
        }

        private void rememberPakPathsMenuItem_Click(object sender, EventArgs e) {
            try {
                this.savePakPathSettings();
            } catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException) {
                this.rememberPakPathsMenuItem.Checked = !this.rememberPakPathsMenuItem.Checked;
                MessageBox.Show("The PAK paths could not be saved to:\n" + PakPathSettings.FilePath + "\n\n" + ex.Message,
                    "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void savePakPathSettings() {
            PakPathSettings settings = new PakPathSettings {
                RememberPaths = this.rememberPakPathsMenuItem.Checked,
                Pak1Path = this.rememberPakPathsMenuItem.Checked ? this.pakPath1 : "",
                Pak2Path = this.rememberPakPathsMenuItem.Checked ? this.pakPath2 : ""
            };
            settings.save();
        }

        private void updatePakPathLabels() {
            this.labelPak1.Text = "PAK 1: " + (this.pakPath1.Length == 0 ? "-" : this.pakPath1);
            this.labelPak2.Text = "PAK 2: " + (this.pakPath2.Length == 0 ? "-" : this.pakPath2);
            this.setToolTip(this.labelPak1, this.pakPath1);
            this.setToolTip(this.labelPak2, this.pakPath2);
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
                this.updateSummary();
                this.buildActiveView();
            } else {
                this.compositeDifferences.Clear();
                this.listviewDifferences.Items.Clear();
                this.treeviewDifferences.Nodes.Clear();
                this.panelTreeViewDetails.Controls.Clear();
                this.updateSummary();
            }
        }

        /*
         * Reads both given PAK files and loads all the differences
         * Shows a MessageBox on error
         */
        private bool loadPakFileDifferences() {
            PAKFileDifferentiation pakFileDifferentiation = new PAKFileDifferentiation(this.pakPath1, this.pakPath2);

            if (!pakFileDifferentiation.pak1.Loaded || !pakFileDifferentiation.pak2.Loaded ||
                pakFileDifferentiation.pak1.EntryPoints?.FirstOrDefault() == null ||
                pakFileDifferentiation.pak2.EntryPoints?.FirstOrDefault() == null) {
                MessageBox.Show("One or both PAK files could not be loaded or have no level entry point.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string splittedPak1Name = pakFileDifferentiation.pak1.EntryPoints[0].name.Split('\\').Last();
            string splittedPak2Name = pakFileDifferentiation.pak2.EntryPoints[0].name.Split('\\').Last();

            if (!string.Equals(splittedPak1Name, splittedPak2Name, StringComparison.OrdinalIgnoreCase)) {
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
            string selectedPath = this.getFileDialogResult("Select PAK 1", this.getInitialPakDirectory(this.pakPath1));
            if (selectedPath.Length == 0) {
                return;
            }

            this.pakPath1 = selectedPath;
            this.updatePakPathLabels();
            if (this.rememberPakPathsMenuItem.Checked) {
                this.trySavePakPathSettings();
            }
        }

        /*
         * button click - opens file dialog for PAK2
         */
        private void buttonBrowsePak2_Click(object sender, EventArgs e) {
            string selectedPath = this.getFileDialogResult("Select PAK 2", this.getInitialPakDirectory(this.pakPath2));
            if (selectedPath.Length == 0) {
                return;
            }

            this.pakPath2 = selectedPath;
            this.updatePakPathLabels();
            if (this.rememberPakPathsMenuItem.Checked) {
                this.trySavePakPathSettings();
            }
        }

        private void trySavePakPathSettings() {
            try {
                this.savePakPathSettings();
            } catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException) {
                MessageBox.Show("The PAK path was selected but could not be saved to:\n" + PakPathSettings.FilePath + "\n\n" + ex.Message,
                    "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string getInitialPakDirectory(string pakPath) {
            if (!string.IsNullOrEmpty(pakPath)) {
                try {
                    string directory = Path.GetDirectoryName(pakPath);
                    if (Directory.Exists(directory)) {
                        return directory;
                    }
                } catch (ArgumentException) {
                    // A stale path should not prevent the user from choosing a new PAK.
                }
            }

            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        /*
         * Opens a file dialog and returns the result
         */
        private string getFileDialogResult(string title, string initialDirectory = "") {
            string result = "";

            fileDialog.Title = title;
            fileDialog.InitialDirectory = initialDirectory;
            fileDialog.FileName = "";

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
            this.listviewDifferences.Items.Clear();
            this.preparedDifferenceItemList = this.differencesAsListViewItemList.getDifferencesAsListViewItemList(
                DifferenceSearch.filter(this.compositeDifferences, this.activeSearch));
            this.listviewDifferences.Items.AddRange(this.preparedDifferenceItemList.ToArray());
        }

        /*
         * Display the treeview with the differences
         */
        private void buildTreeView() {
            this.treeviewDifferences.Nodes.Clear();
            this.panelTreeViewDetails.Controls.Clear();
            this.preparedDifferenceNodeList = this.differencesAsTreeNodeList.getDifferencesAsTreeNodeList(
                DifferenceSearch.filter(this.compositeDifferences, this.activeSearch));
            this.treeviewDifferences.Nodes.AddRange(this.preparedDifferenceNodeList.ToArray());
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                this.activeSearch = this.textBoxSearch.Text.Trim();
                this.buildActiveView();
            }
        }

        private void updateSummary() {
            List<AbstractDifference> composites = this.compositeDifferences.Cast<AbstractDifference>().ToList();
            List<AbstractDifference> entities = this.compositeDifferences.SelectMany(composite => composite.entityDifferences).Cast<AbstractDifference>().ToList();
            List<AbstractDifference> parameters = this.compositeDifferences.SelectMany(composite => composite.entityDifferences)
                .SelectMany(entity => entity.parameterDiffereces).Cast<AbstractDifference>().ToList();
            List<AbstractDifference> links = this.compositeDifferences.SelectMany(composite => composite.entityDifferences)
                .SelectMany(entity => entity.linkDifferences).Cast<AbstractDifference>().ToList();

            this.labelSummary.Text = "Composites: " + this.summaryCounts(composites) +
                "   Entities: " + this.summaryCounts(entities) +
                "   Parameters: " + this.summaryCounts(parameters) +
                "   Links: " + this.summaryCounts(links);
        }

        private string summaryCounts(List<AbstractDifference> differences) {
            return differences.Count(difference => difference.differenceType == DIFFERENCE_TYPE.CREATED) + ", " +
                differences.Count(difference => difference.differenceType == DIFFERENCE_TYPE.MODIFIED) + ", " +
                differences.Count(difference => difference.differenceType == DIFFERENCE_TYPE.DELETED);
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
            this.buildActiveView();
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
            this.panelTreeViewDetails.Controls.Clear();
            if (null != entry) {
                Control details = this.treeViewDetails.buildTreeViewDetails(entry);
                this.panelTreeViewDetails.Controls.Add(details);
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
                ExportFilterOptions exportFilters = null;
                if (this.checkboxApplyFiltersForExport.Checked) {
                    exportFilters = new ExportFilterOptions {
                        HideComposites = this.checkboxHideComposites.Checked,
                        HideEntities = this.checkboxHideEntities.Checked,
                        HideParameters = this.checkboxHideParameters.Checked,
                        HideLinks = this.checkboxHideLinks.Checked,
                        HideAliasEntities = this.checkboxEntityHideOverrides.Checked,
                        HideCreated = this.checkboxHideCreated.Checked,
                        HideModified = this.checkboxHideModified.Checked,
                        HideDeleted = this.checkboxHideDeleted.Checked
                    };
                }
                List<CompositeDifference> exportDifferences = this.checkboxApplyFiltersForExport.Checked
                    ? DifferenceSearch.filter(this.compositeDifferences, this.activeSearch)
                    : this.compositeDifferences;
                List<string> result = export.export(exportDifferences, this.pak1Name, exportFilters);

                if ("exception" == result[0]) {
                    MessageBox.Show(result[1], "Export Error");
                } else {
                    string message = result[1] + "\n\n" + "Do you want to open the export file?";
                    DialogResult dialogResult = MessageBox.Show(message, "Export Result", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes) {
                        if (File.Exists(result[2])) {
                            try {
                                Process.Start(new ProcessStartInfo {
                                    FileName = result[2],
                                    UseShellExecute = true
                                });
                            } catch (Exception ex) when (ex is System.ComponentModel.Win32Exception || ex is InvalidOperationException) {
                                MessageBox.Show("The export was saved, but could not be opened.\n\n" + result[2] + "\n\n" + ex.Message,
                                    "Open Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        } else {
                            MessageBox.Show("The file does not exist: " + result[2]);
                        }
                    }
                }
            }
        }

        /*
         * Always shows the window on top, if checked
         */
        private void checkboxShowWindowOnTop_CheckedChanged(object sender, EventArgs e) {
            if (checkboxShowWindowOnTop.Checked) {
                AIPAKDifferentiation.ActiveForm.TopMost = true;
            } else {
                AIPAKDifferentiation.ActiveForm.TopMost = false;
            }
        }
    }
}
