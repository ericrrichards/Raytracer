namespace RTC_Core_Tests {
    using System;

    using NUnit.Framework;

    using RTC_Core;

    public class CanvasTests {
        [Test]
        public void CreateACanvas() {
            var c = new Canvas(10, 20);
            Assert.AreEqual(10, c.Width);
            Assert.AreEqual(20, c.Height);
            for (var y = 0; y < c.Height; y++) {
                for (var x = 0; x < c.Width; x++) {
                    Assert.AreEqual(new Color(0,0,0), c[x,y]);
                    
                }
            }
        }

        [Test]
        public void WriteAPixel() {
            var c = new Canvas(10, 20);
            var red = new Color(1,0,0);
            c[2,3] = red;
            Assert.AreEqual(red, c[2,3]);
        }
        [Test]
        public void WriteAPixelOutOfRange() {
            var c = new Canvas(10, 20);
            var red = new Color(1,0,0);
            Assert.DoesNotThrow(()=>c[20,3] = red);
            Assert.AreEqual(null, c[20,3]);
        }

        [Test]
        public void Construct_PPMHeader() {
            var c = new Canvas(5,3);
            string ppm = c.ToPPM();
            Assert.True(ppm.StartsWith("P3\r\n5 3\r\n255"));
        }

        [Test]
        public void ConstructPPMPixelData() {
            var c = new Canvas(5,3);
            var c1 = new Color(1.5, 0, 0);
            var c2 = new Color(0, 0.5, 0);
            var c3 = new Color(-0.5, 0, 1);
            c[0, 0] = c1;
            c[2, 1] = c2;
            c[4, 2] = c3;
            var ppm = c.ToPPM();
            Assert.True(ppm.EndsWith(
               "255 0 0 0 0 0 0 0 0 0 0 0 0 0 0\r\n" + 
               "0 0 0 0 0 0 0 128 0 0 0 0 0 0 0\r\n" + 
               "0 0 0 0 0 0 0 0 0 0 0 0 0 0 255\r\n"), ppm);
        }

        [Test]
        public void LongLinesAreSplit() {
            var c = new Canvas(10,2);
            for (var y = 0; y < c.Height; y++) {
                for (var x = 0; x < c.Width; x++) {
                    c[x,y] = new Color(1,0.8, 0.6);
                }
            }
            var ppm = c.ToPPM();
            Assert.True(ppm.EndsWith(
                "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\r\n" + 
                "153 255 204 153 255 204 153 255 204 153 255 204 153\r\n" + 
                "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\r\n" + 
                "153 255 204 153 255 204 153 255 204 153 255 204 153\r\n"), ppm);
        }

        [Test]
        public void PPM_TerminatedWithNewline() {
            var c = new Canvas(10,2);
            var ppm = c.ToPPM();
            Assert.True(ppm.EndsWith("\n"));
        }

    }
}