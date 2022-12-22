using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using CATHODE.Commands;

namespace AIPAKDifferentiation {

    class PAKFileDifferentiation {

        private String pakPath1 = "";
        private String pakPath2 = "";

        CommandsPAK pak1 = null;
        CommandsPAK pak2 = null;

        List<CompositeDifference> compositeDifferences = new List<CompositeDifference>();

        /**
         * This class compares two PAK files with each other and shows the differences in composites, entities and parameters
         * pakPath1 is handeled like the original PAK file, while pakPath2 is considered modified
         */
        public PAKFileDifferentiation(String pakPath1, String pakPath2) {
            this.pakPath1 = pakPath1;
            this.pakPath2 = pakPath2;

            // Load PAKs
            this.pak1 = new CommandsPAK(pakPath1);
            this.pak2 = new CommandsPAK(pakPath2);
        }

        public List<CompositeDifference> loadDifferences() {
            if (pak1.Loaded && pak2.Loaded) {
                loadDifferencesComposites();
                return this.compositeDifferences;
            }

            return null;
        }

        private void loadDifferencesComposites() {
            foreach (CathodeComposite composite in pak1.Composites) {
                // first we check for composite deletion
                if (!pak2.GetCompositeNames().ToList().Contains(composite.name)) {
                    CompositeDifference compositeDifference = new CompositeDifference(composite, COMPOSITE_DIFFERENCE_TYPE.DELETED);
                    this.compositeDifferences.Add(compositeDifference);
                } else {
                    // this part handles modifications (responsible for checking entities) - we do not want to check for modified entities if a composite got deleted
                    this.loadDifferencesEntities(composite, pak2.GetComposite(composite.shortGUID));
                }
            }
            // afterwards we check if any composite has been created - this does not mean we want all the entities and parameter inside the newly created composite. We only want the composite in this case
            foreach (CathodeComposite pak2composite in pak2.Composites) {
                if (!pak1.GetCompositeNames().ToList().Contains(pak2composite.name)) {
                    CompositeDifference compositeDifference = new CompositeDifference(pak2composite, COMPOSITE_DIFFERENCE_TYPE.CREATED);
                    this.compositeDifferences.Add(compositeDifference);
                }
            }
        }

        private void loadDifferencesEntities(CathodeComposite composite, CathodeComposite pak2Composite) {
            CompositeDifference compositeDifference = new CompositeDifference(composite, COMPOSITE_DIFFERENCE_TYPE.MODIFIED);
            
            foreach(CathodeEntity entity in composite.GetEntities()) {
                // first we check for entity deletion
                CathodeEntity pak2Entity = pak2Composite.GetEntityByID(entity.shortGUID);
                if (pak2Entity == null) {
                    EntityDifference entityDifference = new EntityDifference(entity, ENTITIY_DIFFERENCE_TYPE.DELETED);
                    compositeDifference.entityDifferences.Add(entityDifference);
                } else {
                    // now we handle modifications with parameters of this entity
                    this.loadDifferencesParameters(entity, pak2Composite.GetEntityByID(entity.shortGUID), compositeDifference);
                }
            }
            // afterwards we check if any entity has been created - we dont want any parameters here, just the newly created entities
            foreach (CathodeEntity pak2Entity in pak2Composite.GetEntities()) {
                if (null == composite.GetEntityByID(pak2Entity.shortGUID)) { // null means the entity with guid from composite2 wasn't found in composite1
                    EntityDifference entityDifference = new EntityDifference(pak2Entity, ENTITIY_DIFFERENCE_TYPE.CREATED);
                    compositeDifference.entityDifferences.Add(entityDifference);
                }
            }

            // if we found any changed entities -> add the composite where the entity lays to the compositeDifferences list
            if (0 < compositeDifference.entityDifferences.Count) {
                this.compositeDifferences.Add(compositeDifference);
            }
        }

        private void loadDifferencesParameters(CathodeEntity entity, CathodeEntity pak2Entity, CompositeDifference compositeDifference) {
            EntityDifference entityDifference = new EntityDifference(entity, ENTITIY_DIFFERENCE_TYPE.MODIFIED);

            foreach (CathodeLoadedParameter parameter in entity.parameters) {
                // first we check for parameter deletion
                CathodeParameter content = parameter.content;
                CathodeLoadedParameter pak2Parameter = pak2Entity.parameters.Find(x => x.shortGUID == parameter.shortGUID);
                if(null == pak2Parameter) {
                    ParameterDifference parameterDifference = new ParameterDifference(parameter, PARAMETER_DIFFERENCE_TYPE.DELETED);
                    entityDifference.parameterDiffereces.Add(parameterDifference);
                } else {
                    // now we handle modification of parameter values  of this parameter
                    this.loadParameterValues(parameter, pak2Parameter, entityDifference); // TODO has to be implemented
                }
            }
            //afterwards we check if any parameter has been created - we dont want any parameter values here, just newly created parameters
            foreach(CathodeLoadedParameter pak2Parameter in pak2Entity.parameters) {
                CathodeLoadedParameter foundPak1Parameter = entity.parameters.Find(x => x.shortGUID == pak2Parameter.shortGUID);
                if(null == foundPak1Parameter) {
                    ParameterDifference parameterDifference = new ParameterDifference(pak2Parameter, PARAMETER_DIFFERENCE_TYPE.CREATED);
                    entityDifference.parameterDiffereces.Add(parameterDifference);
                }
            }

            if (0 < entityDifference.parameterDiffereces.Count) {
                compositeDifference.entityDifferences.Add(entityDifference);
            }
        }

        private void loadParameterValues(CathodeLoadedParameter parameter, CathodeLoadedParameter pak2Parameter, EntityDifference entityDifference) {

        }
    }
}
