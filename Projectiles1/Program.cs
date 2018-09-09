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
            Console.Write("Enter your power:");
            var power = double.Parse(Console.ReadLine());
            var p = new Projectile(new Point(0,1,0), new Vector(1,1,0).Normalize()*power );
            var world = new World(new Vector(0,-0.1, 0), new Vector(-0.01, 0, 0) );
            var c = new Canvas(900, 550);
            var ticks = 0;
            var green = new Color(0, 1, 0);
            while (p.Position.Y >= 0) {
                Console.WriteLine($"{ticks}: Projectile's position is {p.Position}");
                c[(int)p.Position.X, c.Height-(int)p.Position.Y] = green;
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
        
    }
}
