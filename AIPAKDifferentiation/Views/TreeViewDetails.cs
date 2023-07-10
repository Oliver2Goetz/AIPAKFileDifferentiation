using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using CATHODE.Scripting;

namespace AIPAKDifferentiation.Views {

    class TreeViewDetails {

        public TreeViewDetails() {
        }

        /*
         * Builds the details of the selected/given TreeNodeEntry
         */
        public Label buildTreeViewDetails(TreeNodeEntry entry) {
            Label label = new Label();
            label.Location = new Point(5, 5);
            label.Size = new Size(500, 486);
            label.Font = new Font(FontFamily.GenericSansSerif, 10);

            string content = "";
            switch (entry.cathodeType) {
                case CATHODE_TYPE.COMPOSITE:
                    content = getCompositeDetails(entry);
                    break;
                case CATHODE_TYPE.ENTITY:
                    content = getEntityDetails(entry);
                    break;
                case CATHODE_TYPE.PARAMETER:
                    content = getParameterDetails(entry);
                    break;
                case CATHODE_TYPE.LINK:
                    content = getLinkDetails(entry);
                    break;
                default:
                    break;
            }

            label.Text = content;

            return label;
        }

        private string getCompositeDetails(TreeNodeEntry entry) {
            CompositeDifference compositeDifference = (CompositeDifference)entry.difference;

            string content = "Composite differences - " + entry.differenceType.ToString() + "\n\n";

            switch (entry.differenceType) {
                case DIFFERENCE_TYPE.CREATED:
                    content += "entities: " + compositeDifference.composite.functions.Count() + "\n";
                    content += "entity differences: " + compositeDifference.entityDifferences.Count();
                    break;
                case DIFFERENCE_TYPE.MODIFIED:
                    content += "entities: " + compositeDifference.composite.functions.Count() + "\n";
                    content += "entitiy differences: " + compositeDifference.entityDifferences.Count();
                    break;
                case DIFFERENCE_TYPE.DELETED:
                    content += "entities: " + compositeDifference.composite.functions.Count() + "\n";
                    content += "entitiy differences: " + compositeDifference.entityDifferences.Count();
                    break;
                default:
                    break;
            }

            return content;
        }

        private string getEntityDetails(TreeNodeEntry entry) {
            EntityDifference entityDifference = (EntityDifference)entry.difference;

            string content = "Entity difference - " + entry.differenceType.ToString() + "\n\n";
            content += "entity type: [coming soon] \n\n";

            // parameters
            int parameterCount = 0;
            if (entityDifference.differenceType == DIFFERENCE_TYPE.CREATED) {
                parameterCount = entityDifference.entityPak2.parameters.Count();
            } else {
                parameterCount = entityDifference.entity.parameters.Count();
            }
            content += "parameters: " + parameterCount + "\n";
            content += "parameter differences: " + entityDifference.parameterDiffereces.Count() + "\n\n";

            // links
            int childLinkCount = 0;
            if (entityDifference.differenceType == DIFFERENCE_TYPE.CREATED) {
                childLinkCount = entityDifference.entityPak2.childLinks.Count();
            } else {
                childLinkCount = entityDifference.entity.childLinks.Count();
            }
            content += "child links: " + childLinkCount + "\n";
            content += "child link differences: " + entityDifference.linkDifferences.Count();

            return content;
        }

        private string getParameterDetails(TreeNodeEntry entry) {
            ParameterDifference parameterDifference = (ParameterDifference)entry.difference;

            string content = "Parameter difference - " + entry.differenceType.ToString() + "\n\n";

            content += "Initial value:\n";
            content += parameterDifference.valueBefore + "\n\n";
            content += "New value:\n";
            content += parameterDifference.valueAfter + "\n\n";

            return content;
        }

        private string getLinkDetails(TreeNodeEntry entry) {
            LinkDifference linkDifference = (LinkDifference)entry.difference;
            
            string content = "Link difference - " + entry.differenceType.ToString() + "\n\n";

            switch (entry.differenceType) {
                case DIFFERENCE_TYPE.CREATED:
                    // todo
                    break;
                case DIFFERENCE_TYPE.MODIFIED:
                    string entityName = EntityUtils.GetName(linkDifference.composite.shortGUID, linkDifference.entity.shortGUID);
                    string childEntityName = EntityUtils.GetName(linkDifference.composite.shortGUID, linkDifference.link.childID);
                    content += "Initial value:\n";
                    content += entityName + " [" + linkDifference.link.parentParamID.ToString() + "] => " +
                        linkDifference.link.childID.ToString() + " [" + linkDifference.link.childParamID.ToString() + "]" + "\n\n";

                    //todo
                    content += "New value:\n";
                    content += "todo" + " [" + linkDifference.link.parentParamID.ToString() + "]";//todo
                    break;
                case DIFFERENCE_TYPE.DELETED:
                    break;
                default:
                    break;
            }

            return content;
        } 
    }
}
