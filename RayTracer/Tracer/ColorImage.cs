using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using SlimDX;

namespace RayTracer {
    public class ColorImage {
        public List<Color4> Colors;
        public int Width { get; set; }
        public int Height { get; set; }

        public ColorImage(int width, int height) {
            Width = width;
            Height = height;
            Colors = Enumerable.Repeat(new Color4(), width*height).ToList();
        }
        public Color4 this[ int x, int y] {
            get { return Colors[y*Width + x]; }
            set { Colors[y*Width + x] = value; }
        }

        public void SaveToFile(string filename) {
            var img = new Bitmap(Width, Height);
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    img.SetPixel(x,y, this[x,y].ToColor());
                }
            }
            img.Save(filename, ImageFormat.Png);
        }

        public Bitmap ToBitmap() {
            var img = new Bitmap(Width, Height);
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    img.SetPixel(x, y, NormalizeColor(this[x, y]).ToColor());
                }
            }
            return img;
        }

        private Color4 NormalizeColor(Color4 c) {
            c.Alpha = 1.0f;
            c.Red = Math.Min(1.0f, c.Red);
            c.Green = Math.Min(1.0f, c.Green);
            c.Blue = Math.Min(1.0f, c.Blue);
            return c;
        }
    }
}