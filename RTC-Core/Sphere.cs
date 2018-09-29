namespace RTC_Core {
    public class Sphere {
        public Matrix Transform { get; set; }

        public Sphere() {
            Transform = Matrix.Identity;
        }
    }
}