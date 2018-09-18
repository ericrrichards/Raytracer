namespace Projectiles {
    using System;
    using System.Diagnostics;
    using System.IO;

    using RTC_Core;

    public class Projectile {
        public Projectile(Point position, Vector velocity) {
            Position = position;
            Velocity = velocity;
        }
        public Point Position { get; }
        public Vector Velocity { get; }
    }

    public class World {
        public World(Vector gravity, Vector wind) {
            Gravity = gravity;
            Wind = wind;
        }
        public Vector Gravity { get; }
        public Vector Wind { get; }
    }
    class Program {
        static void Main(string[] args) {
            //Projectiles();
            Clock();
        }



        private static void Projectiles() {
            Console.Write("Enter your power:");
            var power = double.Parse(Console.ReadLine());
            var p = new Projectile(new Point(0, 1, 0), new Vector(1, 1, 0).Normalize() * power);
            var world = new World(new Vector(0, -0.1, 0), new Vector(-0.01, 0, 0));
            var c = new Canvas(900, 550);
            var ticks = 0;
            var green = new Color(0, 1, 0);
            while (p.Position.Y >= 0) {
                Console.WriteLine($"{ticks}: Projectile's position is {p.Position}");
                c[(int)p.Position.X, c.Height - (int)p.Position.Y] = green;
                p = Tick(world, p);
                ticks++;
            }
            Console.WriteLine($"Projectile hit the ground after {ticks} ticks");
            File.WriteAllText("projectile.ppm", c.ToPPM());
            new Process() { StartInfo = new ProcessStartInfo("projectile.ppm") { UseShellExecute = true } }.Start();
        }

        private static Projectile Tick(World world, Projectile p) {
            Point pos = p.Position + p.Velocity;
            Vector velocity = p.Velocity + world.Gravity + world.Wind;
            return new Projectile(pos, velocity);
        }

        private static void Clock() {
            var c = new Canvas(400, 400);
            var center = Point.Zero();
            var twelve = new Point(0, 0, 1);
            for (int i = 0; i < 12; i++) {
                var r = Matrix.RotationY(Math.PI / 6 * i);
                var p = r * twelve;
                var radius = c.Width * 3.0 / 8;
                var x = (int)(p.X * radius) + c.Width / 2;
                var y = (int)(p.Z * radius) + c.Height / 2;
                c[x, y] = new Color(1, 1, 1);
            }
            File.WriteAllText("clock.ppm", c.ToPPM());
            new Process() { StartInfo = new ProcessStartInfo("clock.ppm") { UseShellExecute = true } }.Start();
        }

    }
}
