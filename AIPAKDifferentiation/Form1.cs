using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;

using CATHODE;
using CATHODE.Scripting;

using AIPAKDifferentiation.Views;

namespace AIPAKDifferentiation {

    public partial class AIPAKDifferentiation : Form {

        private string pakPath1 = "";
        private string pakPath2 = "";
        EntityUtils entityUtilsPak1 = null;
        EntityUtils entityUtilsPak2 = null;

        private List<CompositeDifference> compositeDifferences = new List<CompositeDifference>();
        private List<ListViewItem> preparedDifferenceItemList = new List<ListViewItem>();
        private List<TreeNode> preparedDifferenceNodeList = new List<TreeNode>();

        private DISPLAY_MODE displayMode = DISPLAY_MODE.LISTVIEW;
        private TreeViewDetails treeViewDetails;

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

            bool success = this.loadPakFileDifferences();
            this.treeViewDetails = new TreeViewDetails(this.entityUtilsPak1, this.entityUtilsPak2);

            if (success) {
                this.buildActiveView();
            } else {
                listviewDifferences.Items.Clear();
            }
        }

        private bool loadPakFileDifferences() {
            PAKFileDifferentiation pakFileDifferentiation = new PAKFileDifferentiation(this.pakPath1, this.pakPath2);
            if (pakFileDifferentiation.pak1.EntryPoints[0].name != pakFileDifferentiation.pak2.EntryPoints[0].name) {
                string namePak1 = pakFileDifferentiation.pak1.EntryPoints[0].name.Split('\\').Last();
                string namePak2 = pakFileDifferentiation.pak2.EntryPoints[0].name.Split('\\').Last();
                string message = "Both PAKs have to be from the same level!\nPAK 1: " + namePak1 + "\nPAK 2: " + namePak2;
                MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            this.entityUtilsPak1 = pakFileDifferentiation.entityUtilsPak1;
            this.entityUtilsPak2 = pakFileDifferentiation.entityUtilsPak2;
            this.compositeDifferences = pakFileDifferentiation.loadDifferences();

            return true;
        }

        private List<ListViewItem> getDifferencesAsListViewItemList(List<CompositeDifference> differences) {
            List<ListViewItem> preparedDifferencesList = new List<ListViewItem>();

            if (null != differences) {
                foreach (CompositeDifference compositeDifference in differences) {
                    if (this.isShowComposites() && this.showDifferenceTypeByDifferenceType(compositeDifference.differenceType)) {
                        ListViewItemEntry compositeEntry = new ListViewItemEntry(
                            CATHODE_TYPE.COMPOSITE,
                            compositeDifference.composite.shortGUID.ToString(),
                            compositeDifference.composite.name,
                            "-",
                            "-",
                            compositeDifference.differenceType.ToString()
                        );

                        preparedDifferencesList.Add(new ListViewItem(compositeEntry.ToStringArray(), 0, Color.Black, Color.FromArgb(150, 150, 150), new Font("Microsoft Sans Serif", 8.25f)));
                    }

                    foreach (EntityDifference entityDifference in compositeDifference.entityDifferences) {
                        if (this.isValidEntityToShow(entityDifference)) {
                            if (this.isShowEntities() && this.showDifferenceTypeByDifferenceType(entityDifference.differenceType)) {
                                ListViewItemEntry entityEntry = new ListViewItemEntry(
                                    CATHODE_TYPE.ENTITY,
                                    entityDifference.entity.shortGUID.ToString(),
                                    entityUtilsPak1.GetName(compositeDifference.composite.shortGUID, entityDifference.entity.shortGUID),
                                    entityDifference.entity.variant.ToString(),
                                    entityDifference.entity.variant.ToString(),
                                    entityDifference.differenceType.ToString()
                                );

                                preparedDifferencesList.Add(new ListViewItem(entityEntry.ToStringArray(), 0, Color.Black, Color.LightGray, new Font("Microsoft Sans Serif", 8.25f)));
                            }

                            foreach (ParameterDifference parameterDifference in entityDifference.parameterDiffereces) {
                                if (this.isShowParameters() && this.showDifferenceTypeByDifferenceType(parameterDifference.differenceType)) {
                                    ListViewItemEntry parameterEntry = new ListViewItemEntry(
                                        CATHODE_TYPE.PARAMETER,
                                        parameterDifference.parameter.shortGUID.ToByteString(),
                                        parameterDifference.parameter.shortGUID.ToString(),
                                        parameterDifference.valueBefore,
                                        parameterDifference.valueAfter,
                                        parameterDifference.differenceType.ToString()
                                    );

                                    preparedDifferencesList.Add(new ListViewItem(parameterEntry.ToStringArray()));
                                }
                            }

                            foreach (LinkDifference linkDifference in entityDifference.linkDifferences) {
                                if (this.isShowLinks() && this.showDifferenceTypeByDifferenceType(linkDifference.differenceType)) {
                                    string valueBefore = "[" + linkDifference.link.parentParamID.ToString() + "] => [" + linkDifference.link.childParamID + "]";
                                    string valueAfter = "-";
                                    if (linkDifference.differenceType == DIFFERENCE_TYPE.CREATED) {
                                        valueAfter = valueBefore;
                                        valueBefore = "-";
                                    }

                                    ListViewItemEntry linkEntry = new ListViewItemEntry(
                                        CATHODE_TYPE.LINK,
                                        linkDifference.link.connectionID.ToString(),
                                        "entityID: " + entityDifference.entity.shortGUID.ToString() + " childID: " + linkDifference.link.childID,
                                        valueBefore,
                                        valueAfter,
                                        linkDifference.differenceType.ToString()
                                    );

                                    preparedDifferencesList.Add(new ListViewItem(linkEntry.ToStringArray()));
                                }
                            }
                        }
                    }
                }
            }

            return preparedDifferencesList;
        }

        private List<TreeNode> getDifferencesAsTreeNodeList(List<CompositeDifference> differences) {
            List<TreeNode> preparedDifferenceNodes = new List<TreeNode>();

            if (null == differences) {
                return preparedDifferenceNodes;
            }

            foreach (CompositeDifference compositeDifference in differences) {
                TreeNodeEntry compositeEntry = new TreeNodeEntry(
                    CATHODE_TYPE.COMPOSITE,
                    compositeDifference.composite.shortGUID.ToString(),
                    compositeDifference.composite.name,
                    "-",
                    "-",
                    compositeDifference.differenceType,
                    compositeDifference
                );

                TreeNode treeNodeComposite = new TreeNode(compositeEntry.name);
                treeNodeComposite.Tag = compositeEntry;
                preparedDifferenceNodes.Add(treeNodeComposite);

                foreach (EntityDifference entityDifference in compositeDifference.entityDifferences) {
                    TreeNodeEntry entityEntry = new TreeNodeEntry(
                        CATHODE_TYPE.ENTITY,
                        entityDifference.entity.shortGUID.ToString(),
                        entityUtilsPak1.GetName(compositeDifference.composite.shortGUID, entityDifference.entity.shortGUID),
                        entityDifference.entity.variant.ToString(),
                        entityDifference.entity.variant.ToString(),
                        entityDifference.differenceType,
                        entityDifference
                    );

                    TreeNode treeNodeEntity = new TreeNode(entityEntry.name);
                    treeNodeEntity.Tag = entityEntry;
                    treeNodeComposite.Nodes.Add(treeNodeEntity);

                    TreeNode treeNodeEntityParameters = new TreeNode("parameters");
                    TreeNode treeNodeEntityLinks = new TreeNode("links");
                    treeNodeEntity.Nodes.Add(treeNodeEntityParameters);
                    treeNodeEntity.Nodes.Add(treeNodeEntityLinks);

                    foreach (ParameterDifference parameterDifference in entityDifference.parameterDiffereces) {
                        TreeNodeEntry parameterEntry = new TreeNodeEntry(
                            CATHODE_TYPE.PARAMETER,
                            parameterDifference.parameter.shortGUID.ToByteString(),
                            parameterDifference.parameter.shortGUID.ToString(),
                            parameterDifference.valueBefore,
                            parameterDifference.valueAfter,
                            parameterDifference.differenceType,
                            parameterDifference
                        );

                        TreeNode treeNodeParameter = new TreeNode(parameterEntry.name);
                        treeNodeParameter.Tag = parameterEntry;
                        treeNodeEntityParameters.Nodes.Add(treeNodeParameter);
                    }

                    foreach (LinkDifference linkDifference in entityDifference.linkDifferences) {
                        string valueBefore = "[" + linkDifference.link.parentParamID.ToString() + "] => [" + linkDifference.link.childParamID + "]";
                        string valueAfter = "-";
                        if (linkDifference.differenceType == DIFFERENCE_TYPE.CREATED) {
                            valueAfter = valueBefore;
                            valueBefore = "-";
                        }

                        TreeNodeEntry linkEntry = new TreeNodeEntry(
                            CATHODE_TYPE.LINK,
                            linkDifference.link.connectionID.ToString(),
                            "entityID: " + entityDifference.entity.shortGUID.ToString() + " childID: " + linkDifference.link.childID,
                            valueBefore,
                            valueAfter,
                            linkDifference.differenceType,
                            linkDifference
                        );

                        TreeNode treeNodeLink = new TreeNode(linkEntry.name);
                        treeNodeLink.Tag = linkEntry;
                        treeNodeEntityLinks.Nodes.Add(treeNodeLink);
                    }
                }
            }

            return preparedDifferenceNodes;
        }

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

        private string getFileDialogResult(string title, string initialDirectory = "") {
            string result = "";

            fileDialog.Title = title;
            fileDialog.InitialDirectory = initialDirectory;

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

        private bool isShowComposites() {
            return !checkboxHideComposites.Checked;
        }

        private bool isShowEntities() {
            return !checkboxHideEntities.Checked;
        }

        private bool isShowParameters() {
            return !checkboxHideParameters.Checked;
        }

        private bool isShowLinks() {
            return !checkboxHideLinks.Checked;
        }

        /*
         * Validates if the given differenceType of the given differenceType is allowed to be shown
         * The param differenceType is given by the corresponding composite, entity, parameter or link
         */
        private bool showDifferenceTypeByDifferenceType(DIFFERENCE_TYPE differenceType) {
            bool show = true;

            switch (differenceType) {
                case DIFFERENCE_TYPE.CREATED:
                    if (this.checkboxHideCreated.Checked) {
                        show = false;
                    }
                    break;
                case DIFFERENCE_TYPE.MODIFIED:
                    if (this.checkboxHideModified.Checked) {
                        show = false;
                    }
                    break;
                case DIFFERENCE_TYPE.DELETED:
                    if (this.checkboxHideDeleted.Checked) {
                        show = false;
                    }
                    break;
                default:
                    break;
            }

            return show;
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
            this.preparedDifferenceItemList = getDifferencesAsListViewItemList(this.compositeDifferences);
            listviewDifferences.Items.AddRange(this.preparedDifferenceItemList.ToArray());
        }

        /*
         * Display the treeview with the differences
         */
        private void buildTreeView() {
            treeviewDifferences.Nodes.Clear();
            this.preparedDifferenceNodeList = this.getDifferencesAsTreeNodeList(this.compositeDifferences);
            treeviewDifferences.Nodes.AddRange(this.preparedDifferenceNodeList.ToArray());
        }

        private void buttonSwitchView_Click(object sender, EventArgs e) {
            if (displayMode == DISPLAY_MODE.LISTVIEW) {
                listviewDifferences.Hide();
                panelTreeView.Show();
                displayMode = DISPLAY_MODE.TREEVIEW;
            } else {
                panelTreeView.Hide();
                listviewDifferences.Show();
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

    }
}
