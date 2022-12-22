using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using CATHODE.Commands;

namespace AIPAKDifferentiation {

    public partial class AIPAKDifferentiation : Form {

        private static String pakDirectory = @"C:\Users\Oliver\Desktop\Programming\Alien Isolation\AIPAKDifferentiation\examplePAKs\";
        private static String pakPath1 = pakDirectory + @"COMMANDS_TECH_HUB_vanilla.PAK";
        private static String pakPath2 = pakDirectory + @"COMMANDS_TECH_HUB_modified.PAK";

        public AIPAKDifferentiation() {
            InitializeComponent();
        }

        private void AIPAKDifferentiation_Load(object sender, EventArgs e) {

        }

        private void buttonPakShowDifferences_Click(object sender, EventArgs e) {
            listviewDifferences.Items.Clear();

            List<CompositeDifference> differences = this.loadPakFileDifferences();
            drawDifferencesToListView(differences);
        }

        private List<CompositeDifference> loadPakFileDifferences() {
            PAKFileDifferentiation pakFileDifferentiation = new PAKFileDifferentiation(pakPath1, pakPath2);
            List<CompositeDifference> compositeDifferences = pakFileDifferentiation.loadDifferences();

            return compositeDifferences;
        }

        private void drawDifferencesToListView(List<CompositeDifference> differences) {
            if(null != differences) {
                foreach (CompositeDifference compositeDifference in differences) {
                    ListViewItemEntry compositeEntry = new ListViewItemEntry(
                        CATHODE_TYPE.COMPOSITE, 
                        compositeDifference.composite.shortGUID,
                        "-",
                        compositeDifference.composite.name, 
                        compositeDifference.differenceType.ToString()
                    );

                    listviewDifferences.Items.Add(new ListViewItem(compositeEntry.ToStringArray()));

                    foreach (EntityDifference entityDifference in compositeDifference.entityDifferences) {
                        ListViewItemEntry entityEntry = new ListViewItemEntry(
                            CATHODE_TYPE.ENTITY,
                            entityDifference.entity.shortGUID,
                            compositeDifference.composite.name,
                            entityDifference.entity.variant.ToString(),
                            entityDifference.differenceType.ToString()
                        );

                        listviewDifferences.Items.Add(new ListViewItem(entityEntry.ToStringArray()));

                        foreach (ParameterDifference parameterDifference in entityDifference.parameterDiffereces) {
                            ListViewItemEntry parameterEntry = new ListViewItemEntry(
                                CATHODE_TYPE.PARAMETER,
                                parameterDifference.parameter.shortGUID,
                                compositeDifference.composite.name,
                                parameterDifference.parameter.content.dataType.ToString(),
                                parameterDifference.differenceType.ToString()
                            );

                            listviewDifferences.Items.Add(new ListViewItem(parameterEntry.ToStringArray()));
                        }
                    }
                }
            }
        }
    }
}
