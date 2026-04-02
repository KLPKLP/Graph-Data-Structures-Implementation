using System.Drawing;
using System.Windows.Forms;

namespace SemA.GUI
{
    public class GraphCanvasPanel : Panel
    {
        public GraphCanvasPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.WhiteSmoke;
        }
    }
}