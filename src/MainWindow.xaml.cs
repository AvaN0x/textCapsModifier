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
            handleEdit();
        }

        private void txtbx_input_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Keyboard.Modifiers.HasFlag(ModifierKeys.Shift) && e.Key == Key.Enter)
            {
                e.Handled = true;
                handleEdit();
            }
        }

        private void handleEdit()
        {
            textEdit();
            Clipboard.SetDataObject(txtbx_input.Text);
        }

        private void textEdit()
        {
            var value = txtbx_input.Text;
            var selected = ((ComboBoxItem)cmbobox_modification.SelectedItem).Name;

            switch (selected)
            {
                case "allUp":
                    value = value.ToUpper();
                    break;

                case "allLow":
                    value = value.ToLower();
                    break;

                case "capsSwap":
                    value = getCapsSwap(value);
                    break;

                case "firstSentenceUp":
                    value = getFirstSentenceUp(value);
                    break;

                case "firstWordUp":
                    value = getFirstWordUp(value);
                    break;

                case "oneOutOfTwo":
                    value = getOneOutOfTwo(value);
                    break;
            }

            txtbx_input.Text = value.Trim();
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
            return result;
        }

        private string getCapsSwap(String value)
        {
            var result = "";
            foreach (var c in value)
                if (char.IsLetter(c))
                    result += (char.IsUpper(c)) ? char.ToLower(c) : char.ToUpper(c);
                else
                    result += c;
            return result;
        }

        private string getFirstSentenceUp(String value)
        {
            var result = value.Substring(0, 1).ToUpper() + value.Substring(1);
            int position = 0;
            do
            {
                position = value.IndexOf('.', position);
                if (position >= 0)
                {
                    if ((position = getNextLetter(position, value)) != -1)
                        result = result.Substring(0, position) + char.ToUpper(result[position]) + result.Substring(position + 1);
                }
            } while (position > 0);
            return result;
        }

        private int getNextLetter(int start, string value)
        {
            int position = start;
            while (position < value.Length && !char.IsLetter(value[position]))
            {
                position++;
            }
            if (position != start && position < value.Length)
                return position;
            else
                return -1;
        }

        private string getFirstWordUp(String value)
        {
            return Regex.Replace(value.ToLower(), @"\b[a-z]", m => m.Value.ToUpper());
        }
    }
}