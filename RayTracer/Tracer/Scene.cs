using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using SlimDX;

namespace RayTracer.Tracer {
    public class Scene {
        public Camera Camera { get; set; }
        public List<Surface> Surfaces = new List<Surface>();
        public List<Light> Lights= new List<Light>();

        public bool Intersect(Ray ray, ref Intersection intersection) {
            var z = float.MaxValue;
            var hit = false;
            foreach (var surface in Surfaces) {
                var i = new Intersection();
                if (!surface.Intersect(ray, ref i)) {
                    continue;
                }
                hit = true;
                if (i.Distance < z) {
                    z = i.Distance;
                    intersection = i;
                }
            }
            return hit;
        }

        public static Scene LoadFromFile(string filename) {
            var json = File.ReadAllText(filename);

            var ret = JsonConvert.DeserializeObject<Scene>(json, new JsonSerializerSettings() {
                TypeNameHandling = TypeNameHandling.Auto
            });

            foreach (var surface in ret.Surfaces) {
                surface.Setup();
            }
            ret.Camera.Setup();

            return ret;
        }
        public void WriteToFile(string filename) {
            var json = JsonConvert.SerializeObject(this,
                Formatting.Indented,
                new JsonSerializerSettings() {
                    TypeNameHandling = TypeNameHandling.Auto
                });

            File.WriteAllText(filename, json);
        }
    }
}
