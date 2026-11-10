using System;
using System.Collections.Generic;
using System.Linq;

using CATHODE.Scripting;
using CATHODE.Scripting.Internal;

namespace AIPAKDifferentiation {

    internal static class DifferenceSearch {

        internal static List<CompositeDifference> filter(List<CompositeDifference> differences, string query) {
            if (String.IsNullOrWhiteSpace(query)) {
                return differences;
            }

            string search = query.Trim();
            List<CompositeDifference> results = new List<CompositeDifference>();

            foreach (CompositeDifference compositeDifference in differences) {
                Composite composite = compositeDifference.composite ?? compositeDifference.compositePak2;
                if (matches(composite.name, search) || matches(composite.shortGUID.ToByteString(), search)) {
                    results.Add(compositeDifference);
                    continue;
                }

                CompositeDifference matchingComposite = new CompositeDifference(
                    compositeDifference.composite,
                    compositeDifference.differenceType,
                    compositeDifference.compositePak2
                );

                foreach (EntityDifference entityDifference in compositeDifference.entityDifferences) {
                    Entity entity = entityDifference.entity ?? entityDifference.entityPak2;
                    string initialName = entityDifference.entity == null ? "" : entityDifference.utils.GetEntityName(entityDifference.composite, entityDifference.entity);
                    string newName = entityDifference.entityPak2 == null ? "" : entityDifference.utilsPak2.GetEntityName(entityDifference.compositePak2, entityDifference.entityPak2);

                    if (
                        matches(initialName, search) ||
                        matches(newName, search) ||
                        matches(entity.shortGUID.ToByteString(), search) ||
                        entityDifference.storedDataChanges.Any(change => matches(change, search))
                    ) {
                        matchingComposite.entityDifferences.Add(entityDifference);
                        continue;
                    }

                    List<ParameterDifference> parameters = entityDifference.parameterDiffereces
                        .Where(parameter => matches((parameter.parameter ?? parameter.parameterPak2).name.ToString(), search) ||
                            matches((parameter.parameter ?? parameter.parameterPak2).name.ToByteString(), search) ||
                            matches(parameter.valueBefore, search) || matches(parameter.valueAfter, search)
                        ).ToList();

                    List<LinkDifference> links = entityDifference.linkDifferences
                        .Where(link => matches(DifferenceDetails.link(link), search)).ToList();

                    if (parameters.Count > 0 || links.Count > 0) {
                        EntityDifference matchingEntity = new EntityDifference(
                            entityDifference.entity,
                            entityDifference.differenceType,
                            entityDifference.entityPak2,
                            entityDifference.composite,
                            entityDifference.compositePak2,
                            entityDifference.utils,
                            entityDifference.utilsPak2,
                            entityDifference.commands,
                            entityDifference.commandsPak2
                        );

                        matchingEntity.parameterDiffereces.AddRange(parameters);
                        matchingEntity.linkDifferences.AddRange(links);
                        matchingEntity.storedDataChanges.AddRange(entityDifference.storedDataChanges);
                        matchingComposite.entityDifferences.Add(matchingEntity);
                    }
                }

                if (matchingComposite.entityDifferences.Count > 0) {
                    results.Add(matchingComposite);
                }
            }

            return results;
        }

        private static bool matches(string value, string search) {
            return value?.Contains(search, StringComparison.OrdinalIgnoreCase) == true;
        }
    }
}
