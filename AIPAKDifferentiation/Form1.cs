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
        private static String pakPath1 = pakDirectory + @"COMMANDS_HAB_CORPORATEPENT_vanilla.PAK";
        private static String pakPath2 = pakDirectory + @"COMMANDS_HAB_CORPORATEPENT_invisible_alien.PAK";

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
                    string[] compositeEntry = new string[3];
                    compositeEntry[0] = DIFFERENCE_TYPE.COMPOSITE.ToString();
                    compositeEntry[1] = compositeDifference.composite.name;
                    compositeEntry[2] = compositeDifference.differenceType.ToString();

                    listviewDifferences.Items.Add(new ListViewItem(compositeEntry));

                    foreach (EntityDifference entityDifference in compositeDifference.entityDifferences) {
                        string[] entityEntry = new string[3];
                        entityEntry[0] = DIFFERENCE_TYPE.ENTITY.ToString();
                        entityEntry[1] = "Variant: " + entityDifference.entity.variant.ToString();
                        entityEntry[2] = entityDifference.differenceType.ToString();
                        listviewDifferences.Items.Add(new ListViewItem(entityEntry));

                        foreach (ParameterDifference parameterDifference in entityDifference.parameterDiffereces) {
                            string[] parameterEntry = new string[3];
                            parameterEntry[0] = DIFFERENCE_TYPE.PARAMETER.ToString();
                            parameterEntry[1] = "DataType: " + parameterDifference.parameter.dataType.ToString();
                            parameterEntry[2] = parameterDifference.differenceType.ToString();
                            listviewDifferences.Items.Add(new ListViewItem(parameterEntry));
                        }
                    }
                }
            }
        }
    }
}
