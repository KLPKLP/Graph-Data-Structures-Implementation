using System.Globalization;

namespace SemA.GUI
{
    public partial class AddTownForm : Form
    {
        public string TownName => textBoxTownName.Text.Trim();

        public double PositionX
        {
            get
            {
                return double.Parse(
                    textBoxPositionX.Text.Trim(),
                    CultureInfo.InvariantCulture);
            }
        }

        public double PositionY
        {
            get
            {
                return double.Parse(
                    textBoxPositionY.Text.Trim(),
                    CultureInfo.InvariantCulture);
            }
        }

        public AddTownForm()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxTownName.Text))
            {
                MessageBox.Show(
                    "Zadej název města.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBoxTownName.Focus();
                return;
            }

            bool xIsValid = double.TryParse(
                textBoxPositionX.Text.Trim(),
                NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.InvariantCulture,
                out _);

            if (!xIsValid)
            {
                MessageBox.Show(
                    "Souřadnice X není platné číslo. Použij tečku jako desetinný oddělovač.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBoxPositionX.Focus();
                return;
            }

            bool yIsValid = double.TryParse(
                textBoxPositionY.Text.Trim(),
                NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.InvariantCulture,
                out _);

            if (!yIsValid)
            {
                MessageBox.Show(
                    "Souřadnice Y není platné číslo. Použij tečku jako desetinný oddělovač.",
                    "Chyba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBoxPositionY.Focus();
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

        private void AddTownForm_Load(object sender, EventArgs e)
        {

        }
    }
}