namespace SemA.GUI
{
    public class GraphPreviewForm : Form
    {
        private readonly Action<Graphics, Rectangle, bool> drawGraphAction;

        private readonly Panel topPanel = new();
        private readonly Panel previewPanel = new();
        private readonly CheckBox checkBoxShowRoadTimes = new();

        public GraphPreviewForm(
            Action<Graphics, Rectangle, bool> drawGraphAction,
            bool initialShowRoadTimes)
        {
            this.drawGraphAction = drawGraphAction;

            Text = "Velký náhled grafu";
            StartPosition = FormStartPosition.CenterParent;
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(900, 700);

            topPanel.Dock = DockStyle.Top;
            topPanel.Height = 42;
            topPanel.Padding = new Padding(12, 10, 12, 6);

            checkBoxShowRoadTimes.AutoSize = true;
            checkBoxShowRoadTimes.Text = "Zobrazit časy průjezdu";
            checkBoxShowRoadTimes.Checked = initialShowRoadTimes;
            checkBoxShowRoadTimes.CheckedChanged += checkBoxShowRoadTimes_CheckedChanged;

            topPanel.Controls.Add(checkBoxShowRoadTimes);

            previewPanel.Dock = DockStyle.Fill;
            previewPanel.BackColor = Color.White;
            previewPanel.Paint += previewPanel_Paint;

            Controls.Add(previewPanel);
            Controls.Add(topPanel);
        }

        private void checkBoxShowRoadTimes_CheckedChanged(object? sender, EventArgs e)
        {
            previewPanel.Invalidate();
        }

        private void previewPanel_Paint(object? sender, PaintEventArgs e)
        {
            drawGraphAction(
                e.Graphics,
                previewPanel.ClientRectangle,
                checkBoxShowRoadTimes.Checked);
        }
    }
}