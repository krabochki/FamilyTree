using System;
using System.Collections.Generic;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.IO;

namespace Family_Tree
{
    /// <summary>
    /// Логика взаимодействия для ShowTree.xaml
    /// </summary>
    public partial class ShowTree : Window
    {
        string database = "";
        Tree parentForm;
        public ShowTree(string database, Tree parent)
        {
            InitializeComponent();
            parentForm = parent;
            this.database = database;
             scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = 0.1;
            scaleTransform.ScaleY = 0.1;

            imageviewer.LayoutTransform = scaleTransform;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        
        ScaleTransform scaleTransform;

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (imageviewer != null)
            {
                Binding bindX = new Binding
                {
                    Source = slider,
                    Path = new PropertyPath("Value")
                };
                BindingOperations.SetBinding(scaleTransform, ScaleTransform.ScaleXProperty, bindX);
                Binding bindY = new Binding
                {
                    Source = slider,
                    Path = new PropertyPath("Value")
                };
                BindingOperations.SetBinding(scaleTransform, ScaleTransform.ScaleYProperty, bindY);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "Image.png",  // Начальное название файла по умолчанию
                DefaultExt = ".png",  // Расширение файла по умолчанию
                Filter =
                   "Файлы изображений (*.png)|*.png"
            };

            if (dialog.FileName == "Image.png")
            {
                File.Delete(dialog.FileName);
            }

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    string sourceFilePath = "graph.png";
                    string targetFilePath = dialog.FileName;

                    // Копирование файла
                    File.Copy(sourceFilePath, targetFilePath, overwrite: true);
                    Close();
                    parentForm.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
          
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
            

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
            parentForm.Close();
        }

      
    }
}
