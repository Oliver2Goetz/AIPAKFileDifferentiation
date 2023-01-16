using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using CATHODE.Scripting;
using CATHODE.Scripting.Internal;

namespace AIPAKDifferentiation.Views {

    class DifferenceListViewItemList {

        AIPAKDifferentiation form1;

        EntityUtils entityUtilsPak1;
        EntityUtils entityUtilsPak2;

        // checkbox filters
        CheckBox checkboxHideComposites;
        CheckBox checkboxHideEntities;
        CheckBox checkboxHideParameters;
        CheckBox checkboxHideLinks;
        CheckBox checkboxHideCreated;
        CheckBox checkboxHideModified;
        CheckBox checkboxHideDeleted;
        CheckBox checkboxEntityHideOverrides;

        /*
         * @param form            : used for getting controls for filter logic
         * @param entityUtilsPak1 : EntityUtils for PAK1
         * @param entityUtilsPak2 : EntityUtils for PAK2
         */
        public DifferenceListViewItemList(AIPAKDifferentiation form, EntityUtils entityUtilsPak1, EntityUtils entityUtilsPak2) {
            this.form1 = form;
            this.entityUtilsPak1 = entityUtilsPak1;
            this.entityUtilsPak2 = entityUtilsPak2;

            this.initializeControls();
        }

        /*
         * Gets all the differences as a list of ListViewItems
         */
        public List<ListViewItem> getDifferencesAsListViewItemList(List<CompositeDifference> differences) {
            List<ListViewItem> preparedDifferencesList = new List<ListViewItem>();

            if (null != differences) {
                foreach (CompositeDifference compositeDifference in differences) {
                    if (this.isShowComposites() && this.showDifferenceTypeByDifferenceType(compositeDifference.differenceType)) {

                        string identifier = "";
                        string name = "";

                        switch (compositeDifference.differenceType) {
                            case DIFFERENCE_TYPE.CREATED:
                                identifier = compositeDifference.compositePak2.shortGUID.ToString();
                                name = compositeDifference.compositePak2.name;
                                break;
                            case DIFFERENCE_TYPE.MODIFIED:
                            case DIFFERENCE_TYPE.DELETED:
                                identifier = compositeDifference.composite.shortGUID.ToString();
                                name = compositeDifference.composite.name;
                                break;
                            default:
                                break;
                        }

                        ListViewItemEntry compositeEntry = new ListViewItemEntry(
                            CATHODE_TYPE.COMPOSITE,
                            identifier,
                            name,
                            "-",
                            "-",
                            compositeDifference.differenceType.ToString()
                        );

                        preparedDifferencesList.Add(new ListViewItem(compositeEntry.ToStringArray(), 0, Color.Black, Color.FromArgb(150, 150, 150), new Font("Microsoft Sans Serif", 8.25f)));
                    }

                    foreach (EntityDifference entityDifference in compositeDifference.entityDifferences) {
                        if (this.isValidEntityToShow(entityDifference)) {
                            if (this.isShowEntities() && this.showDifferenceTypeByDifferenceType(entityDifference.differenceType)) {

                                string identifier = "";
                                string name = "";
                                string variant = "";

                                switch (entityDifference.differenceType) {
                                    case DIFFERENCE_TYPE.CREATED:
                                        identifier = entityDifference.entityPak2.shortGUID.ToString();
                                        name = entityUtilsPak2.GetName(entityDifference.compositePak2.shortGUID, entityDifference.entityPak2.shortGUID);
                                        variant = entityDifference.entityPak2.variant.ToString();
                                        break;
                                    case DIFFERENCE_TYPE.MODIFIED:
                                    case DIFFERENCE_TYPE.DELETED:
                                        identifier = entityDifference.entity.shortGUID.ToString();
                                        name = entityUtilsPak1.GetName(entityDifference.composite.shortGUID, entityDifference.entity.shortGUID);
                                        variant = entityDifference.entity.variant.ToString();
                                        break;
                                    default:
                                        break;
                                }

                                ListViewItemEntry entityEntry = new ListViewItemEntry(
                                    CATHODE_TYPE.ENTITY,
                                    identifier,
                                    name,
                                    variant,
                                    variant,
                                    entityDifference.differenceType.ToString()
                                );

                                preparedDifferencesList.Add(new ListViewItem(entityEntry.ToStringArray(), 0, Color.Black, Color.LightGray, new Font("Microsoft Sans Serif", 8.25f)));
                            }

                            foreach (ParameterDifference parameterDifference in entityDifference.parameterDiffereces) {
                                if (this.isShowParameters() && this.showDifferenceTypeByDifferenceType(parameterDifference.differenceType)) {

                                    string identifier = "";
                                    string name = "";

                                    switch (parameterDifference.differenceType) {
                                        case DIFFERENCE_TYPE.CREATED:
                                            identifier = parameterDifference.parameterPak2.shortGUID.ToByteString();
                                            name = parameterDifference.parameterPak2.shortGUID.ToString();
                                            break;
                                        case DIFFERENCE_TYPE.MODIFIED:
                                        case DIFFERENCE_TYPE.DELETED:
                                            identifier = parameterDifference.parameter.shortGUID.ToByteString();
                                            name = parameterDifference.parameter.shortGUID.ToString();
                                            break;
                                        default:
                                            break;
                                    }

                                    ListViewItemEntry parameterEntry = new ListViewItemEntry(
                                        CATHODE_TYPE.PARAMETER,
                                        identifier,
                                        name,
                                        parameterDifference.valueBefore,
                                        parameterDifference.valueAfter,
                                        parameterDifference.differenceType.ToString()
                                    );

                                    preparedDifferencesList.Add(new ListViewItem(parameterEntry.ToStringArray()));
                                }
                            }

                            foreach (LinkDifference linkDifference in entityDifference.linkDifferences) {
                                if (this.isShowLinks() && this.showDifferenceTypeByDifferenceType(linkDifference.differenceType)) {

                                    string identifier = "";
                                    string name = "";
                                    string valueBefore = "";
                                    string valueAfter = "";

                                    switch (linkDifference.differenceType) {
                                        case DIFFERENCE_TYPE.CREATED:
                                            identifier = linkDifference.linkPak2.connectionID.ToString();
                                            name = "entityID: " + entityDifference.entityPak2.shortGUID.ToString() + " childID: " + linkDifference.linkPak2.childID;
                                            valueBefore = "-";
                                            valueAfter = "[" + linkDifference.linkPak2.parentParamID.ToString() + "] => [" + linkDifference.linkPak2.childParamID + "]";
                                            break;
                                        case DIFFERENCE_TYPE.MODIFIED:
                                        case DIFFERENCE_TYPE.DELETED:
                                            identifier = linkDifference.link.connectionID.ToString();
                                            name = "entityID: " + entityDifference.entity.shortGUID.ToString() + " childID: " + linkDifference.link.childID;
                                            valueBefore = "[" + linkDifference.link.parentParamID.ToString() + "] => [" + linkDifference.link.childParamID + "]";
                                            valueAfter = "-";
                                            break;
                                        default:
                                            break;
                                    }

                                    ListViewItemEntry linkEntry = new ListViewItemEntry(
                                        CATHODE_TYPE.LINK,
                                        identifier,
                                        name,
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

        private void initializeControls() {
            this.checkboxHideComposites = (CheckBox)this.getControlByName("checkboxHideComposites");
            this.checkboxHideEntities= (CheckBox)this.getControlByName("checkboxHideEntities");
            this.checkboxHideParameters = (CheckBox)this.getControlByName("checkboxHideParameters");
            this.checkboxHideLinks = (CheckBox)this.getControlByName("checkboxHideLinks");
            this.checkboxHideCreated = (CheckBox)this.getControlByName("checkboxHideCreated");
            this.checkboxHideModified = (CheckBox)this.getControlByName("checkboxHideModified");
            this.checkboxHideDeleted = (CheckBox)this.getControlByName("checkboxHideDeleted");
            this.checkboxEntityHideOverrides = (CheckBox)this.getControlByName("checkboxEntityHideOverrides");
        }

        /*
         * Checks if an entity is valid -> used for filtering unwanted entities out (eg. override)
         */
        private bool isValidEntityToShow(EntityDifference entityDifference) {
            bool isValid = true;

            Entity entity = entityDifference.entity;
            if (entityDifference.differenceType == DIFFERENCE_TYPE.CREATED) {
                entity = entityDifference.entityPak2;
            }

            if (entity.variant == EntityVariant.OVERRIDE && checkboxEntityHideOverrides.Checked) {
                isValid = false;
            }

            return isValid;
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
         * Returns the control by its name
         */
        private Control getControlByName(string controlName, Form form = null) {
            Control result = null;

            if (null == form) {
                form = this.form1;
            }

            Control[] controls = form.Controls.Find(controlName, true);
            foreach (Control control in controls) {
                if (control.Name.Equals(controlName)) {
                    result = control;
                    break;
                }
            }

            return result;
        }
    }
}
