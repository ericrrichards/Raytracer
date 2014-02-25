using System.Collections.Generic;
using System.Drawing;
using SlimDX;

namespace RayTracer {
    public class TextureStore {
        private static readonly Dictionary<string, ColorImage> Textures;

        static TextureStore() {
            Textures = new Dictionary<string, ColorImage>();
            var img = new ColorImage(1, 1);
            img[0,0] = Color.White;
            Textures[""]=img;
        }

        public static ColorImage GetTexture(string textureID) {
            if (textureID == null) {
                textureID = "";
            }
            if ( !Textures.ContainsKey(textureID)) {
                var tex = (Bitmap)Image.FromFile(textureID);
                var img = new ColorImage(tex.Width, tex.Height);
                for (var y = 0; y < tex.Height; y++) {
                    for (var x = 0; x < tex.Width; x++) {
                        var pixel = tex.GetPixel(x, y);

                        img[x, y] = pixel;
                    }
                }

            }
            return Textures[textureID];
        }
    }
}