namespace RTC_Core_Tests {
    using NUnit.Framework;

    using RTC_Core;

    public class ColorTests {
        [Test]
        public void Colors_Are_RGBTuples() {
            var c = new Color(-0.5, 0.4, 1.7);
            Assert.AreEqual(-0.5, c.R);
            Assert.AreEqual(0.4, c.G);
            Assert.AreEqual(1.7, c.B);
        }

        [Test]
        public void AddingColors() {
            var c1 = new Color(0.9, 0.6, 0.75);
            var c2 = new Color(0.7, 0.1, 0.25);
            var c3 = c1 + c2;
            Assert.True(c3 is Color, c3.GetType().Name);
            Assert.AreEqual(new Color(1.6, 0.7, 1.0), c3);
        }

        [Test]
        public void SubtractingColors() {
            var c1 = new Color(0.9, 0.6, 0.75);
            var c2 = new Color(0.7, 0.1, 0.25);
            var c3 = c1 - c2;
            Assert.True(c3 is Color, c3.GetType().Name);
            Assert.AreEqual(new Color(0.2, 0.5, 0.5), c3);
        }

        [Test]
        public void MultiplyColorByScalar() {
            var c = new Color(0.2, 0.3, 0.4);
            var c1 = c * 2;
            Assert.True(c1 is Color, c1.GetType().Name);
            Assert.AreEqual(new Color(0.4, 0.6, 0.8), c1);
        }

        [Test]
        public void DivideColorByScalar() {
            var c = new Color(0.2, 0.3, 0.4);
            var c1 = c / 2;
            Assert.True(c1 is Color, c1.GetType().Name);
            Assert.AreEqual(new Color(0.1, 0.15, 0.2), c1);
        }

        [Test]
        public void MultiplyingColors() {
            var c1 = new Color(1, 0.2, 0.4);
            var c2 = new Color(0.9, 1, 0.1);
            Assert.AreEqual(new Color(0.9, 0.2, 0.04), c1*c2 );
        }
    }
}