using System.Drawing;
using System.Windows.Forms;

namespace RayTracer.GUI {
    public sealed partial class Form1 : Form {
        public Form1(Image bitmap, double elapsed) {
            InitializeComponent();

            ClientSize = bitmap.Size;
            pictureBox1.Image = bitmap;
            Text = Text + string.Format(" - {0:F2} seconds", elapsed);
        }
    }
}
