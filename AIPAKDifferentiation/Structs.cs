using System;
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

        public CathodeParameter parameter;
        public PARAMETER_DIFFERENCE_TYPE differenceType;

        public ParameterDifference(CathodeParameter parameter, PARAMETER_DIFFERENCE_TYPE differenceType) {
            this.parameter = parameter;
            this.differenceType = differenceType;
        }
    }

    enum DIFFERENCE_TYPE {
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
