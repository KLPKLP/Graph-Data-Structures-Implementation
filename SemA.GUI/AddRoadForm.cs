using System.Globalization;

namespace SemA.GUI
{
    public partial class AddRoadForm : Form
    {
        public string FromTownKey
            => comboBoxFrom.SelectedItem?.ToString() ?? string.Empty;

        public string ToTownKey
            => comboBoxTo.SelectedItem?.ToString() ?? string.Empty;

        public double RoadTime
            => double.Parse(textBoxTime.Text.Trim(), CultureInfo.InvariantCulture);

        public bool IsProblematic
            => checkBoxIsProblematic.Checked;

        public AddRoadForm(IEnumerable<string> townKeys)
        {
            InitializeComponent();

            foreach (string townKey in townKeys)
            {
                comboBoxFrom.Items.Add(townKey);
                comboBoxTo.Items.Add(townKey);
            }

            if (comboBoxFrom.Items.Count > 0)
            {
                comboBoxFrom.SelectedIndex = 0;
            }

            if (comboBoxTo.Items.Count > 1)
            {
                comboBoxTo.SelectedIndex = 1;
            }
            else if (comboBoxTo.Items.Count > 0)
            {
                comboBoxTo.SelectedIndex = 0;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (comboBoxFrom.SelectedItem is null)
            {
                MessageBox.Show(
                    "Vyber počáteční město.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                comboBoxFrom.Focus();
                return;
            }

            if (comboBoxTo.SelectedItem is null)
            {
                MessageBox.Show(
                    "Vyber cílové město.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                comboBoxTo.Focus();
                return;
            }

            if (FromTownKey == ToTownKey)
            {
                MessageBox.Show(
                    "Silnice nesmí vést z města do stejného města.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                comboBoxTo.Focus();
                return;
            }

            bool timeIsValid = double.TryParse(
                textBoxTime.Text.Trim(),
                NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.InvariantCulture,
                out double parsedTime);

            if (!timeIsValid)
            {
                MessageBox.Show(
                    "Čas není platné číslo. Použij tečku jako desetinný oddělovač.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBoxTime.Focus();
                return;
            }

            if (parsedTime <= 0)
            {
                MessageBox.Show(
                    "Čas musí být větší než 0.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBoxTime.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}