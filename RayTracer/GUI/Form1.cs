using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RayTracer {
    public partial class Form1 : Form {
        public Form1(Bitmap bitmap, double elapsed) {
            InitializeComponent();

            ClientSize = bitmap.Size;
            pictureBox1.Image = bitmap;
            Text = Text + string.Format(" - {0:F2} seconds", elapsed);
        }
    }
}
