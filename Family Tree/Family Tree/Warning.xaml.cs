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
using System.Windows.Shapes;

namespace Family_Tree
{
    /// <summary>
    /// Логика взаимодействия для Warning.xaml
    /// </summary>
    public partial class Warning : Window
    {

        public Warning()
        {
            InitializeComponent();
        }

        public static void WarningShow(string text,string title)
        {
            Warning warning = new Warning();
            warning.warning_textblock.Text = $"{text}";
            warning.Title = $"{title}";
            warning.ShowDialog();
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void YesButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        public void NoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
