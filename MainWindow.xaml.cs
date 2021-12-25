using System;
using System.Collections.Generic;
using System.Diagnostics;
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

/*
 * Made by Dragon of Shuu#1119 on Discord!
 */

namespace FakeRansomWare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void MakeNumber(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            // Trace.WriteLine(e.Text);
            e.Handled = re.IsMatch(e.Text);
        }

        private bool chkDate(string[] month_year_string)
        {
            int[] month_year = new int[] { int.Parse(month_year_string[0]), int.Parse(month_year_string[1]) };
            string year_str = DateTime.Now.Year.ToString();
            int year = int.Parse(year_str.Substring(year_str.Length - 2));
            Trace.WriteLine(year);
            if (month_year[0] > 0 && month_year[0] < 13 && month_year[1] >= year && month_year[1] < year+4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreditEnter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string theText = textBox.Text;
            if (
                textBox != null && // Make sure the Text Box does in fact exist
                theText != "" &&  // Make sure the Text Box does in fact have text
                theText.Last() != ' ' &&  // Make sure that the last character is not a space (This is so stack overflow doesn't occur where this function replays itself repeatedly)
                (theText.Length - theText.Count(f => f == ' ')) % 4 == 0 && // Check to see if this is the end of this section of the credit card (e.g. 1234<-- here)
                theText.Length < textBox.MaxLength // Make sure that the text isn't at the limit
                )
            {
                textBox.Text += " "; // Add the new space to the end
                textBox.Select(textBox.Text.Length, 0); // Reposition the cursor to be at the end of the textbox
            }
            if (textBox.Text.Length == textBox.MaxLength)
            {
                ExpiryDate.Focus();
            }
        }

        private void ExpiryDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string theText = textBox.Text;

            if (
                textBox != null &&
                theText != "" &&
                theText.Last() != ' ' &&
                (theText.Length - theText.Count(f => f == ' ')) % 2 == 0 &&
                theText.Length < textBox.MaxLength / 2
                )
            {
                textBox.Text += " / ";
                textBox.Select(textBox.Text.Length, 0);
            }
            if (textBox.Text.Length == textBox.MaxLength)
            {
                if (chkDate(textBox.Text.Split(" / ")))
                {
                    CVV.Focus();
                }
            }
        }

        private void CVV_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length == 3)
            {
                SubmitButton.Focus();
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (CreditEnter.Text.Length == CreditEnter.MaxLength &&
                ExpiryDate.Text.Length == ExpiryDate.MaxLength &&
                chkDate(ExpiryDate.Text.Split(" / ")) &&
                CVV.Text.Length == CVV.MaxLength)
            {
                Close();
            }
            else
            {
                MessageBox.Show("You haven't filled all the data out correctly Onii~");
            }
        }

        private void TB_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (textbox == ExpiryDate)
            {
                if (textbox.Text.Length == textbox.MaxLength && chkDate(textbox.Text.Split(" / ")))
                {
                    textbox.BorderBrush = Brushes.Green;
                }
                else
                {
                    textbox.BorderBrush = Brushes.Red;
                }
            } 
            else
            {
                if (textbox.Text.Length == textbox.MaxLength)
                {
                    textbox.BorderBrush = Brushes.Green;
                }
                else
                {
                    textbox.BorderBrush = Brushes.Red;
                }
            }
        }
    }
}