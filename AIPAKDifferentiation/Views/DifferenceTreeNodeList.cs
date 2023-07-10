using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using CATHODE.Scripting;
using CATHODE.Scripting.Internal;

namespace AIPAKDifferentiation.Views {

    class DifferenceTreeNodeList {

        public DifferenceTreeNodeList() {
        }

        /*
         * Gets all the differences as a list of TreeNodes
         */
        public List<TreeNode> getDifferencesAsTreeNodeList(List<CompositeDifference> differences) {
            List<TreeNode> preparedDifferenceNodes = new List<TreeNode>();

            if (null == differences) {
                return preparedDifferenceNodes;
            }

            foreach (CompositeDifference compositeDifference in differences) {

                string compositeIdentifier = "";
                string compositeName = "";

                switch (compositeDifference.differenceType) {
                    case DIFFERENCE_TYPE.CREATED:
                        compositeIdentifier = compositeDifference.compositePak2.shortGUID.ToString();
                        compositeName = compositeDifference.compositePak2.name;
                        break;
                    case DIFFERENCE_TYPE.MODIFIED:
                    case DIFFERENCE_TYPE.DELETED:
                        compositeIdentifier = compositeDifference.composite.shortGUID.ToString();
                        compositeName = compositeDifference.composite.name;
                        break;
                    default:
                        break;
                }

                TreeNodeEntry compositeEntry = new TreeNodeEntry(
                    CATHODE_TYPE.COMPOSITE,
                    compositeIdentifier,
                    compositeName,
                    "-",
                    "-",
                    compositeDifference.differenceType,
                    compositeDifference
                );

                TreeNode treeNodeComposite = new TreeNode(compositeEntry.name);
                treeNodeComposite.Tag = compositeEntry;
                preparedDifferenceNodes.Add(treeNodeComposite);

                foreach (EntityDifference entityDifference in compositeDifference.entityDifferences) {
                
                    string entityIdentifier = "";
                    string entityName = "";
                    string entityVariant = "";

                    switch (entityDifference.differenceType) {
                        case DIFFERENCE_TYPE.CREATED:
                            entityIdentifier = entityDifference.entityPak2.shortGUID.ToString();
                            entityName = EntityUtils.GetName(entityDifference.compositePak2.shortGUID, entityDifference.entityPak2.shortGUID);
                            entityVariant = entityDifference.entityPak2.variant.ToString();
                            break;
                        case DIFFERENCE_TYPE.MODIFIED:
                        case DIFFERENCE_TYPE.DELETED:
                            entityIdentifier = entityDifference.entity.shortGUID.ToString();
                            entityName = EntityUtils.GetName(entityDifference.composite.shortGUID, entityDifference.entity.shortGUID);
                            entityVariant = entityDifference.entity.variant.ToString();
                            break;
                        default:
                            break;
                    }

                    TreeNodeEntry entityEntry = new TreeNodeEntry(
                        CATHODE_TYPE.ENTITY,
                        entityIdentifier,
                        entityName,
                        entityVariant,
                        entityVariant,
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

                        string parameterIdentifier = "";
                        string parameterName = "";

                        switch (parameterDifference.differenceType) {
                            case DIFFERENCE_TYPE.CREATED:
                                parameterIdentifier = parameterDifference.parameterPak2.name.ToByteString();
                                parameterName = parameterDifference.parameterPak2.name.ToString();
                                break;
                            case DIFFERENCE_TYPE.MODIFIED:
                            case DIFFERENCE_TYPE.DELETED:
                                parameterIdentifier = parameterDifference.parameter.name.ToByteString();
                                parameterName = parameterDifference.parameter.name.ToString();
                                break;
                            default:
                                break;
                        }

                        TreeNodeEntry parameterEntry = new TreeNodeEntry(
                            CATHODE_TYPE.PARAMETER,
                            parameterIdentifier,
                            parameterName,
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

                        string connectionIDstr = "";
                        string name = "";
                        string valueBefore = "";
                        string valueAfter = "";

                        switch (linkDifference.differenceType) {
                            case DIFFERENCE_TYPE.CREATED:
                                connectionIDstr = linkDifference.linkPak2.connectionID.ToString();
                                name = "entityID: " + entityDifference.entityPak2.shortGUID.ToString() + " childID: " + linkDifference.linkPak2.childID;
                                valueBefore = "-";
                                valueAfter = "[" + linkDifference.linkPak2.parentParamID.ToString() + "] => [" + linkDifference.linkPak2.childParamID + "]";
                                break;
                            case DIFFERENCE_TYPE.MODIFIED:
                                connectionIDstr = linkDifference.link.connectionID.ToString();
                                name = "entityID: " + linkDifference.entity.shortGUID.ToString() + " childID: " + linkDifference.link.childID;
                                valueBefore = "[" + linkDifference.link.parentParamID.ToString() + "] => [" + linkDifference.link.childParamID + "]";
                                valueAfter = "[" + linkDifference.linkPak2.parentParamID.ToString() + "] => [" + linkDifference.linkPak2.childParamID + "]";
                                break;
                            case DIFFERENCE_TYPE.DELETED:
                                connectionIDstr = linkDifference.link.connectionID.ToString();
                                name = "entityID: " + linkDifference.entity.shortGUID.ToString() + " childID: " + linkDifference.link.childID;
                                valueBefore = "[" + linkDifference.link.parentParamID.ToString() + "] => [" + linkDifference.link.childParamID + "]";
                                valueAfter = "-";
                                break;
                            default:
                                break;
                        }

                        TreeNodeEntry linkEntry = new TreeNodeEntry(
                            CATHODE_TYPE.LINK,
                            connectionIDstr,
                            name,
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
    }
}
