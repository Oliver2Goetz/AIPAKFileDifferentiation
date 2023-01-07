using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CATHODE;
using CATHODE.Scripting;
using CATHODE.Scripting.Internal;

namespace AIPAKDifferentiation {

    class CompositeDifference {

        public Composite composite;
        public DIFFERENCE_TYPE differenceType;
        public List<EntityDifference> entityDifferences = new List<EntityDifference>();

        public CompositeDifference(Composite composite, DIFFERENCE_TYPE differenceType) {
            this.composite = composite;
            this.differenceType = differenceType;
        }
    }

    class EntityDifference {

        public Entity entity;
        public DIFFERENCE_TYPE differenceType;
        public List<ParameterDifference> parameterDiffereces = new List<ParameterDifference>();
        public List<LinkDifference> linkDifferences = new List<LinkDifference>();

        public EntityDifference(Entity entity, DIFFERENCE_TYPE differenceType) {
            this.entity = entity;
            this.differenceType = differenceType;
        }
    }

    class ParameterDifference {

        public Parameter parameter;
        public DIFFERENCE_TYPE differenceType;
        public string valueBefore = "-";
        public string valueAfter = "-";

        public ParameterDifference(Parameter parameter, DIFFERENCE_TYPE differenceType) {
            this.parameter = parameter;
            this.differenceType = differenceType;
        }
    }

    class LinkDifference {

        public EntityLink link;
        public DIFFERENCE_TYPE differenceType;

        public LinkDifference(EntityLink link, DIFFERENCE_TYPE differenceType) {
            this.link = link;
            this.differenceType = differenceType;
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

    enum CATHODE_TYPE {
        COMPOSITE,
        ENTITY,
        PARAMETER,
        LINK
    }

    enum DIFFERENCE_TYPE {
        // means that in PAK2 this composite was created// means that in PAK2 this entity was created
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
}
