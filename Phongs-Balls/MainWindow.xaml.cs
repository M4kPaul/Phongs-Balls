using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Phongs_Balls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Image SelectedCanvas { get; set; }
        private PhongSphere[] Spheres { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = PhongSphere.Presets;

            Spheres = new PhongSphere[4]
            {
                PhongSphere.Presets[0],
                PhongSphere.Presets[5],
                PhongSphere.Presets[11],
                PhongSphere.Presets[23]
            };

            Canvas1.Source = Spheres[0].RenderSphere();
            Canvas2.Source = Spheres[1].RenderSphere();
            Canvas3.Source = Spheres[2].RenderSphere();
            Canvas4.Source = Spheres[3].RenderSphere();
        }

        private void Canvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border;
            if (SelectedCanvas != null)
            {
                border = (SelectedCanvas.Parent as Border);
                border.BorderThickness = new Thickness(1);
                border.BorderBrush = new SolidColorBrush(Colors.White);
            }
            SelectedCanvas = (sender as Image);
            border = SelectedCanvas.Parent as Border;
            border.BorderThickness = new Thickness(2);
            border.BorderBrush = new SolidColorBrush(Colors.Lime);
            UpdateText();
        }

        private void Canvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                SelectedCanvas = (sender as Image);
                var lightPosition = new Vector3((float)((-SelectedCanvas.ActualWidth / 2 + e.GetPosition(SelectedCanvas).X) * 0.01), (float)((-SelectedCanvas.ActualHeight / 2 + e.GetPosition(SelectedCanvas).Y) * 0.01), 2.5f);
                Spheres[int.Parse(SelectedCanvas.Uid)].LightPosition = lightPosition;
                SelectedCanvas.Source = Spheres[int.Parse(SelectedCanvas.Uid)].RenderSphere();
                UpdateText();
            }
        }

        private void ComboBoxPresets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedCanvas != null)
            {
                Spheres[int.Parse(SelectedCanvas.Uid)] = (e.AddedItems[0] as PhongSphere);
                if (!Spheres[int.Parse(SelectedCanvas.Uid)].IsRendered) Spheres[int.Parse(SelectedCanvas.Uid)].RenderSphere();
                SelectedCanvas.Source = Spheres[int.Parse(SelectedCanvas.Uid)].Bitmap;
                UpdateText();
            }
        }

        private void Text_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SelectedCanvas != null)
            {
                var lol = float.Parse(LX.Text);
                if (float.TryParse(LX.Text, out var fLX) && float.TryParse(LY.Text, out var fLY) && float.TryParse(LZ.Text, out var fLZ) &&
                    float.TryParse(LAr.Text, out var fLAr) && float.TryParse(LAg.Text, out var fLAg) && float.TryParse(LAb.Text, out var fLAb) &&
                    float.TryParse(LDr.Text, out var fLDr) && float.TryParse(LDg.Text, out var fLDg) && float.TryParse(LDb.Text, out var fLDb) &&
                    float.TryParse(LSr.Text, out var fLSr) && float.TryParse(LSg.Text, out var fLSg) && float.TryParse(LSb.Text, out var fLSb) &&
                    float.TryParse(MAr.Text, out var fMAr) && float.TryParse(MAg.Text, out var fMAg) && float.TryParse(MAb.Text, out var fMAb) &&
                    float.TryParse(MDr.Text, out var fMDr) && float.TryParse(MDg.Text, out var fMDg) && float.TryParse(MDb.Text, out var fMDb) &&
                    float.TryParse(MSr.Text, out var fMSr) && float.TryParse(MSg.Text, out var fMSg) && float.TryParse(MSb.Text, out var fMSb) &&
                    float.TryParse(Shi.Text, out var fShi))
                {
                    Spheres[int.Parse(SelectedCanvas.Uid)] = new PhongSphere("", new Vector3(fLX, fLY, fLZ),
                                                                                 new Vector3(fLAr, fLAg, fLAb),
                                                                                 new Vector3(fLDr, fLDg, fLDb),
                                                                                 new Vector3(fLSr, fLSg, fLSb),
                                                                                 new Vector3(fMAr, fMAg, fMAb),
                                                                                 new Vector3(fMDr, fMDg, fMDb),
                                                                                 new Vector3(fMSr, fMSg, fMSb),
                                                                                 fShi);
                    SelectedCanvas.Source = Spheres[int.Parse(SelectedCanvas.Uid)].RenderSphere();
                }
            }
        }

        private void UpdateText()
        {
            if (SelectedCanvas != null)
            {
                var sphere = Spheres[int.Parse(SelectedCanvas.Uid)];
                LX.Text = sphere.LightPosition.X.ToString(); LY.Text = sphere.LightPosition.Y.ToString(); LZ.Text = sphere.LightPosition.Z.ToString();
                LAr.Text = sphere.LightAmbient.X.ToString(); LAg.Text = sphere.LightAmbient.Y.ToString(); LAb.Text = sphere.LightAmbient.Z.ToString();
                LDr.Text = sphere.LightDiffuse.X.ToString(); LDg.Text = sphere.LightDiffuse.Y.ToString(); LDb.Text = sphere.LightDiffuse.Z.ToString();
                LSr.Text = sphere.LightSpecular.X.ToString(); LSg.Text = sphere.LightSpecular.Y.ToString(); LSb.Text = sphere.LightSpecular.Z.ToString();
                MAr.Text = sphere.Ka.X.ToString(); MAg.Text = sphere.Ka.Y.ToString(); MAb.Text = sphere.Ka.Z.ToString();
                MDr.Text = sphere.Kd.X.ToString(); MDg.Text = sphere.Kd.Y.ToString(); MDb.Text = sphere.Kd.Z.ToString();
                MSr.Text = sphere.Ks.X.ToString(); MSg.Text = sphere.Ks.Y.ToString(); MSb.Text = sphere.Ks.Z.ToString();
                Shi.Text = sphere.Shininess.ToString();
            }
        }
    }
}
