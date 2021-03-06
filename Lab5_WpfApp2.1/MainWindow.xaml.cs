using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab5_WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            field.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            field.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            field.EditingMode = InkCanvasEditingMode.Select;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Relative));
                field.Background = imageBrush;
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg";
            if (saveFileDialog.ShowDialog() == true)
            {
                int width = (int)this.field.ActualWidth;
                int height = (int)this.field.ActualHeight;
                RenderTargetBitmap rtb = new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
                field.Measure(new Size(width, height));
                field.Arrange(new Rect(new Size(width, height)));
                rtb.Render(field);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                FileStream fileStream = File.Open(saveFileDialog.FileName, FileMode.Create);
                encoder.Save(fileStream);
                fileStream.Close();
            }
        }
    }
}
