using System;
using System.Numerics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Phongs_Balls
{
    class PhongSphere
    {
        readonly static int radius = 400;
        readonly static int width = 1000;
        readonly static int height = 1000;

        public string Name { get; set; }
        public Vector3 LightPosition { get; set; }
        public Vector3 LightAmbient { get; set; }
        public Vector3 LightDiffuse { get; set; }
        public Vector3 LightSpecular { get; set; }
        public Vector3 Ka { get; set; }
        public Vector3 Kd { get; set; }
        public Vector3 Ks { get; set; }
        public float Shininess { get; set; }
        public bool IsRendered { get; set; } = false;
        public WriteableBitmap Bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);

        public static PhongSphere[] Presets { get; } = new PhongSphere[24]
        {
            new PhongSphere("emerald", new Vector3(0.0215f, 0.1745f, 0.0215f), new Vector3(0.07568f, 0.61424f, 0.07568f), new Vector3(0.633f, 0.727811f, 0.633f), 0.6f),
            new PhongSphere("jade", new Vector3(0.135f, 0.2225f, 0.1575f), new Vector3(0.54f, 0.89f, 0.63f), new Vector3(0.316228f, 0.316228f, 0.316228f), 0.1f),
            new PhongSphere("obsidian", new Vector3(0.05375f, 0.05f, 0.06625f), new Vector3(0.18275f, 0.17f, 0.22525f), new Vector3(0.332741f, 0.328634f, 0.346435f), 0.3f),
            new PhongSphere("pearl", new Vector3(0.25f, 0.20725f, 0.20725f), new Vector3(1f, 0.829f, 0.829f), new Vector3(0.296648f, 0.296648f, 0.296648f), 0.088f),
            new PhongSphere("ruby", new Vector3(0.1745f, 0.01175f, 0.01175f), new Vector3(0.61424f, 0.04136f, 0.04136f), new Vector3(0.727811f, 0.626959f, 0.626959f), 0.6f),
            new PhongSphere("turquoise", new Vector3(0.1f, 0.18725f, 0.1745f), new Vector3(0.396f, 0.74151f, 0.69102f), new Vector3(0.297254f, 0.30829f, 0.306678f), 0.1f),
            new PhongSphere("brass", new Vector3(0.329412f, 0.223529f, 0.027451f), new Vector3(0.780392f, 0.568627f, 0.113725f), new Vector3(0.992157f, 0.941176f, 0.807843f), 0.21794872f),
            new PhongSphere("bronze", new Vector3(0.2125f, 0.1275f, 0.054f), new Vector3(0.714f, 0.4284f, 0.18144f), new Vector3(0.393548f, 0.271906f, 0.166721f), 0.2f),
            new PhongSphere("chrome", new Vector3(0.25f, 0.25f, 0.25f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.774597f, 0.774597f, 0.774597f), 0.6f),
            new PhongSphere("copper", new Vector3(0.19125f, 0.0735f, 0.0225f), new Vector3(0.7038f, 0.27048f, 0.0828f), new Vector3(0.256777f, 0.137622f, 0.086014f), 0.1f),
            new PhongSphere("gold", new Vector3(0.24725f, 0.1995f, 0.0745f), new Vector3(0.75164f, 0.60648f, 0.22648f), new Vector3(0.628281f, 0.555802f, 0.366065f), 0.4f),
            new PhongSphere("silver", new Vector3(0.19225f, 0.19225f, 0.19225f), new Vector3(0.50754f, 0.50754f, 0.50754f), new Vector3(0.508273f, 0.508273f, 0.508273f), 0.4f),
            new PhongSphere("black plastic", new Vector3(0f, 0f, 0f), new Vector3(0.01f, 0.01f, 0.01f), new Vector3(0.5f, 0.5f, 0.5f), 0.25f),
            new PhongSphere("cyan plastic", new Vector3(0f, 0.1f, 0.06f), new Vector3(0f, 0.50980392f, 0.50980392f), new Vector3(0.50196078f, 0.50196078f, 0.50196078f), 0.25f),
            new PhongSphere("green plastic", new Vector3(0f, 0f, 0f), new Vector3(0.1f, 0.35f, 0.1f), new Vector3(0.45f, 0.55f, 0.45f), 0.25f),
            new PhongSphere("red plastic", new Vector3(0f, 0f, 0f), new Vector3(0.5f, 0f, 0f), new Vector3(0.7f, 0.6f, 0.6f), 0.25f),
            new PhongSphere("white plastic", new Vector3(0f, 0f, 0f), new Vector3(0.55f, 0.55f, 0.55f), new Vector3(0.7f, 0.7f, 0.7f), 0.25f),
            new PhongSphere("yellow plastic", new Vector3(0f, 0f, 0f), new Vector3(0.5f, 0.5f, 0f), new Vector3(0.6f, 0.6f, 0.5f), 0.25f),
            new PhongSphere("black rubber", new Vector3(0.02f, 0.02f, 0.02f), new Vector3(0.01f, 0.01f, 0.01f), new Vector3(0.4f, 0.4f, 0.4f), 0.078125f),
            new PhongSphere("cyan rubber", new Vector3(0f, 0.05f, 0.05f), new Vector3(0.4f, 0.5f, 0.5f), new Vector3(0.04f, 0.7f, 0.7f), 0.078125f),
            new PhongSphere("green rubber", new Vector3(0f, 0.05f, 0f), new Vector3(0.4f, 0.5f, 0.4f), new Vector3(0.04f, 0.7f, 0.04f), 0.078125f),
            new PhongSphere("red rubber", new Vector3(0.05f, 0f, 0f), new Vector3(0.5f, 0.4f, 0.4f), new Vector3(0.7f, 0.04f, 0.04f), 0.078125f),
            new PhongSphere("white rubber", new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.7f, 0.7f, 0.7f), 0.078125f),
            new PhongSphere("yellow rubber", new Vector3(0.05f, 0.05f, 0f), new Vector3(0.5f, 0.5f, 0.4f), new Vector3(0.7f, 0.7f, 0.04f), 0.078125f)
        };

        public PhongSphere(string name, Vector3 ka, Vector3 kd, Vector3 ks, float shininess) : this(name, new Vector3(1, -1, 2.5f), new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1), ka, kd, ks, shininess) { }

        public PhongSphere(string name, Vector3 lightPosition, Vector3 lightAmbient, Vector3 lightDiffuse, Vector3 lightSpecular, Vector3 ka, Vector3 kd, Vector3 ks, float shininess)
        {
            Name = name;
            LightPosition = lightPosition;
            LightAmbient = lightAmbient;
            LightDiffuse = lightDiffuse;
            LightSpecular = lightSpecular;
            Ka = ka;
            Kd = kd;
            Ks = ks;
            Shininess = shininess;
        }

        public WriteableBitmap RenderSphere()
        {
            var pixels = new uint[width * height];
            for (var y = -radius; y <= radius; y++)
            {
                for (var x = -radius; x <= radius; x++)
                {
                    var red = 16;
                    var green = 16;
                    var blue = 16;
                    var alpha = 255;

                    if (x * x + y * y < radius * radius)
                    {
                        var z = (float)Math.Sqrt(radius * radius - x * x - y * y);
                        var point = new Vector3(x, y, z);
                        var pointN = Vector3.Normalize(point);
                        var normal = Vector3.Normalize(new Vector3(x, y, z));
                        var V = Vector3.Normalize(new Vector3(0, 0, 0) - point);
                        var L = LightPosition - pointN;
                        var LN = Vector3.Normalize(L);
                        var R = normal * (2 * Vector3.Dot(normal, LN)) - LN;

                        var diffuseFactor = Vector3.Dot(L, normal);
                        var specularFactor = Math.Pow(Vector3.Dot(R, V), (int)(Shininess * 128));

                        red = Math.Min(Math.Max((int)((LightAmbient.X * Ka.X + (LightDiffuse.X * Kd.X * diffuseFactor) + (LightSpecular.X * Ks.X * specularFactor)) * 255), 0), 255);
                        green = Math.Min(Math.Max((int)((LightAmbient.Y * Ka.Y + (LightDiffuse.Y * Kd.Y * diffuseFactor) + (LightSpecular.Y * Ks.Y * specularFactor)) * 255), 0), 255);
                        blue = Math.Min(Math.Max((int)((LightAmbient.Z * Ka.Z + (LightDiffuse.Z * Kd.Z * diffuseFactor) + (LightSpecular.Z * Ks.Z * specularFactor)) * 255), 0), 255);
                    }

                    pixels[(y + radius + 100) * width + x + radius + 100] = (uint)((alpha << 24) + (red << 16) + (green << 8) + blue);
                }
            }

            Bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * 4, 0);
            IsRendered = true;
            return Bitmap;
        }
    }
}
