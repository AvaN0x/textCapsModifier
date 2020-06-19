using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace textCapsModifier
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnTop_Checked(object sender, RoutedEventArgs e)
        {
            Topmost = true;
        }

        private void OnTop_Unchecked(object sender, RoutedEventArgs e)
        {
            Topmost = false;
        }

        private void toClipBoard_Click(object sender, RoutedEventArgs e)
        {
            textEdit();
            Clipboard.SetText(txtbx_input.Text);
        }

        private void textEdit()
        {
            var value = txtbx_input.Text;
            var selected = ((ComboBoxItem) cmbobox_modification.SelectedItem).Name;

            if (selected == "oneOutOfTwo")
                value = getOneOutOfTwo(value);
            else if (selected == "firstWordUp")
                value = getFirstWordUp(value);
            txtbx_input.Text = value;
        }

        private string getOneOutOfTwo(String value)
        {
            var result = "";
            bool toUpper = true;
            foreach (var c in value)
                if (char.IsLetter(c))
                    result += (toUpper = !toUpper) ? char.ToLower(c) : char.ToUpper(c);
                else
                    result += c;

            txtbx_input.Text = result;
            return result;
        }
        private string getFirstWordUp(String value)
        {
            return Regex.Replace(value.ToLower(), @"\b[a-z]", m => m.Value.ToUpper());
        }


    }
}
