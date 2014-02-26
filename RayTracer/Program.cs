using System;
using System.Diagnostics;
using System.Windows.Forms;
using RayTracer.GUI;
using RayTracer.Tracer;

namespace RayTracer {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {

            var scene = Scene.LoadFromFile("scenes/scene3.json");
            var rt = new Tracer.RayTracer(scene, 1);
            var start = Stopwatch.GetTimestamp();
            var img = rt.Render();
            var end = Stopwatch.GetTimestamp();

            var elapsed = ( end- start)/(double)Stopwatch.Frequency;

            //img.SaveToFile("scene1.png");
            var i = img.ToBitmap();
            i.Save("scene1.png");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(i,elapsed));
        }
    }
}
