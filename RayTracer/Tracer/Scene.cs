using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SlimDX;

namespace RayTracer.Tracer {
    public class Scene {
        public Camera Camera { get; set; }
        public List<Surface> Surfaces = new List<Surface>();
        public List<Light> Lights= new List<Light>();

        public Intersection Intersect(Ray ray) {
            var z = float.MaxValue;
            var intersection = new Intersection(false);
            foreach (var surface in Surfaces) {
                Intersection i;
                if (!(i = surface.Intersect(ray))) {
                    continue;
                }
                if (i.Distance < z) {
                    z = i.Distance;
                    intersection = i;
                }
            }
            return intersection;
        }

        public static Scene LoadFromFile(string filename) {
            var json = File.ReadAllText(filename);

            var ret = JsonConvert.DeserializeObject<Scene>(json, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Auto
            });

            foreach (var surface in ret.Surfaces) {
                surface.Setup();
                TextureStore.GetTexture(surface.Material.TextureID);
            }
            ret.Camera.Setup();



            return ret;
        }
        public void WriteToFile(string filename) {
            var json = JsonConvert.SerializeObject(this,
                Formatting.Indented,
                new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.Auto
                });

            File.WriteAllText(filename, json);
        }
    }
}
