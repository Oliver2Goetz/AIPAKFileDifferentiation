﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CATHODE.Commands;

namespace AIPAKDifferentiation {

    class CompositeDifference {

        public CathodeComposite composite;
        public COMPOSITE_DIFFERENCE_TYPE differenceType;
        public List<EntityDifference> entityDifferences = new List<EntityDifference>();

        public CompositeDifference(CathodeComposite composite, COMPOSITE_DIFFERENCE_TYPE differenceType) {
            this.composite = composite;
            this.differenceType = differenceType;
        }
    }

    class EntityDifference {

        public CathodeEntity entity;
        public ENTITIY_DIFFERENCE_TYPE differenceType;
        public List<ParameterDifference> parameterDiffereces = new List<ParameterDifference>();

        public EntityDifference(CathodeEntity entity, ENTITIY_DIFFERENCE_TYPE differenceType) {
            this.entity = entity;
            this.differenceType = differenceType;
        }
    }

    class ParameterDifference {

        public CathodeLoadedParameter parameter;
        public PARAMETER_DIFFERENCE_TYPE differenceType;

        public ParameterDifference(CathodeLoadedParameter parameter, PARAMETER_DIFFERENCE_TYPE differenceType) {
            this.parameter = parameter;
            this.differenceType = differenceType;
        }
    }

    class ListViewItemEntry {

        private CATHODE_TYPE cathodeType;
        private ShortGuid shortGuid;
        private string compositeName;
        private string name;
        private string differenceType;

        public ListViewItemEntry(CATHODE_TYPE cathodeType, ShortGuid shortGuid, string compositeName, string name, string differenceType) {
            this.cathodeType = cathodeType;
            this.shortGuid = shortGuid;
            this.compositeName = compositeName;
            this.name = name;
            this.differenceType = differenceType;
        }

        public string[] ToStringArray() {
            string[] arr = new string[5];

            arr[0] = this.cathodeType.ToString();
            arr[1] = this.shortGuid.ToString();
            arr[2] = this.compositeName;
            arr[3] = this.name;
            arr[4] = this.differenceType;

            return arr;
        }
    }

    enum CATHODE_TYPE {
        COMPOSITE,
        ENTITY,
        PARAMETER
    }

    enum COMPOSITE_DIFFERENCE_TYPE {
        CREATED,   // means that in PAK2 this composite was created
        MODIFIED,  // means that in PAK2 this composite was modified (on it's entities)
        DELETED    // means that in PAK2 this composite was deleted
    }

    enum ENTITIY_DIFFERENCE_TYPE {
        CREATED,   // means that in PAK2 this entity was created
        MODIFIED,  // means that in PAK2 this entity was modified (on it's parameter)
        DELETED    // means that in PAK2 this entity was deleted
    }

    enum PARAMETER_DIFFERENCE_TYPE {
        CREATED,   // means that in PAK2 this parameter was created
        MODIFIED,  // means that in PAK2 this parameter was modified (on it's parameter settings)
        DELETED    // means that in PAK2 this parameter was deleted
    }
}