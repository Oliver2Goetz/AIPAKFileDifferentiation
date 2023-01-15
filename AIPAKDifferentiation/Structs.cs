using System.Collections.Generic;

using CATHODE;
using CATHODE.Scripting;
using CATHODE.Scripting.Internal;

namespace AIPAKDifferentiation {

    abstract class AbstractDifference {
        public DIFFERENCE_TYPE differenceType;
    }

    class CompositeDifference : AbstractDifference {

        public Composite composite;
        public List<EntityDifference> entityDifferences = new List<EntityDifference>();
        public Composite composite2; // in case of CREATED = null, MODIFIED = pak2Composite, DELETED = null

        public CompositeDifference(Composite composite, DIFFERENCE_TYPE differenceType, Composite composite2) {
            this.composite = composite;
            this.differenceType = differenceType;
            this.composite2 = composite2;
        }
    }

    class EntityDifference : AbstractDifference {

        public Entity entity;
        public List<ParameterDifference> parameterDiffereces = new List<ParameterDifference>();
        public List<LinkDifference> linkDifferences = new List<LinkDifference>();
        public Entity entity2;

        public EntityDifference(Entity entity, DIFFERENCE_TYPE differenceType, Entity entity2) {
            this.entity = entity;
            this.differenceType = differenceType;
            this.entity2 = entity2;
        }
    }

    class ParameterDifference : AbstractDifference {

        public Parameter parameter;
        public string valueBefore = "-";
        public string valueAfter = "-";
        public Parameter parameter2;

        public ParameterDifference(Parameter parameter, DIFFERENCE_TYPE differenceType, Parameter parameter2) {
            this.parameter = parameter;
            this.differenceType = differenceType;
            this.parameter2 = parameter2;
        }
    }

    class LinkDifference : AbstractDifference {

        public EntityLink link;
        public EntityLink? link2;
        public Composite composite;
        public Entity entity;

        public LinkDifference(EntityLink link, DIFFERENCE_TYPE differenceType, EntityLink? link2, Composite composite, Entity entity) {
            this.link = link;
            this.differenceType = differenceType;
            this.link2 = link2;
            this.composite = composite;
            this.entity = entity;
        }
    }

    class ListViewItemEntry {

        private CATHODE_TYPE cathodeType;
        private string identifier; // in case of composite and entity this should be the shortGuid. Parameter doesn't have a shortGuid as of now
        private string name;
        private string valueBefore;
        private string valueAfter;
        private string differenceType;

        public ListViewItemEntry(CATHODE_TYPE cathodeType, string identifier, string name, string valueBefore, string valueAfter, string differenceType) {
            this.cathodeType = cathodeType;
            this.identifier = identifier;
            this.name = name;
            this.valueBefore = valueBefore;
            this.valueAfter = valueAfter;
            this.differenceType = differenceType;
        }

        public string[] ToStringArray() {
            string[] arr = new string[6];

            arr[0] = this.cathodeType.ToString();
            arr[1] = this.identifier;
            arr[2] = this.name;
            arr[3] = this.valueBefore;
            arr[4] = this.valueAfter;
            arr[5] = this.differenceType;

            return arr;
        }
    }

    class TreeNodeEntry {

        public CATHODE_TYPE cathodeType;
        public DIFFERENCE_TYPE differenceType;
        public AbstractDifference difference;
        public string identifier;
        public string name;
        public string valueBefore;
        public string valueAfter;

        public TreeNodeEntry(CATHODE_TYPE cathodeType, string identifier, string name, string valueBefore, string valueAfter, DIFFERENCE_TYPE differenceType, AbstractDifference difference) {
            this.cathodeType = cathodeType;
            this.differenceType = differenceType;
            this.difference = difference;
            this.identifier = identifier;
            this.name = name;
            this.valueBefore = valueBefore;
            this.valueAfter = valueAfter;
        }
    }

    enum CATHODE_TYPE {
        COMPOSITE,
        ENTITY,
        PARAMETER,
        LINK
    }

    enum DIFFERENCE_TYPE {
        // means that in PAK2 this composite was created
        // means that in PAK2 this entity was created
        // means that in PAK2 this parameter was created
        // means that in PAK2 this link was created
        CREATED,
        // means that in PAK2 this composite was modified (on it's entities)
        // means that in PAK2 this entity was modified (on it's parameter)
        // means that in PAK2 this parameter was modified (on it's parameter settings)
        MODIFIED,
        // means that in PAK2 this composite was deleted
        // means that in PAK2 this entity was deleted
        // means that in PAK2 this parameter was deleted
        // means that in PAK2 this link was deleted
        DELETED
    }

    enum DISPLAY_MODE {
        LISTVIEW, // show differences in a list view
        TREEVIEW  // show differences in a tree view
    }
}
