namespace RTC_Core {
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Canvas {
        public int Width { get; }
        public int Height { get; }
        public Color[] Pixels { get; }

        public Canvas(int width, int height) {
            Width = width;
            Height = height;
            Pixels = new Color[Width*Height];
            for (int i = 0; i < Width*Height; i++) {
                Pixels[i] = new Color(0,0,0);
            }
        }

        public Color this[int x, int y] {
            get => Pixels[x + y * Width];
            set => Pixels[x + y * Width] = value;
        }

        public string ToPPM() {
            var sb = new StringBuilder();
            sb.AppendLine("P3");
            sb.AppendLine($"{Width} {Height}");
            sb.AppendLine("255");

            for (var y = 0; y < Height; y++) {
                var lineCounter = 0;
                for (var x = 0; x < Width; x++) {
                    var c = this[x,y];
                    var s = $"{Clamp(c.R)} {Clamp(c.G)} {Clamp(c.B)}";
                    if (lineCounter + s.Length > 70) {
                        s = $"{Clamp(c.R)} {Clamp(c.G)}";
                        var s2 = $"{Clamp(c.B)} ";
                        if (lineCounter + s.Length > 70) {
                            s = $"{Clamp(c.R)}";
                            s2 = $"{Clamp(c.G)} {Clamp(c.B)} ";
                            sb.AppendLine(s);
                            sb.Append(s2);
                            lineCounter = s2.Length;

                        } else {
                            sb.AppendLine(s);
                            sb.Append(s2);
                            lineCounter = s2.Length;
                        }

                        
                    } else {
                        sb.Append(s);
                        lineCounter += s.Length+1;
                        if (x != Width - 1) {
                            sb.Append(" ");
                        }
                    }
                    
                    
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private int Clamp(double c) {
            return (int)Math.Round(Math.Max(0, Math.Min(1, c)) * 255);
        }
    }
}