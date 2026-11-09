using System.Drawing;
using System.Windows.Forms;

namespace AIPAKDifferentiation.Views {

    class TreeViewDetails {

        public Control buildTreeViewDetails(TreeNodeEntry entry) {
            string content = entry.cathodeType switch {
                CATHODE_TYPE.COMPOSITE => DifferenceDetails.composite((CompositeDifference)entry.difference),
                CATHODE_TYPE.ENTITY => DifferenceDetails.entity((EntityDifference)entry.difference),
                CATHODE_TYPE.PARAMETER => DifferenceDetails.parameter((ParameterDifference)entry.difference),
                CATHODE_TYPE.LINK => DifferenceDetails.link((LinkDifference)entry.difference),
                _ => ""
            };

            return new TextBox {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both,
                WordWrap = true,
                BorderStyle = BorderStyle.None,
                BackColor = SystemColors.Control,
                Font = new Font(FontFamily.GenericSansSerif, 10),
                Text = content
            };
        }
    }
}
