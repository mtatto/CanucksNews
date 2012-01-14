using System.Reflection;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Canucks_NewsReader_Phone
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();

            versionNumber.Text = GetVersionNumber();
        }

        private static string GetVersionNumber()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] parts = asm.FullName.Split(',');
            return parts[1].Split('=')[1];
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            var emailcomposer = new EmailComposeTask
                                    {
                                        To = "feedback@theTatto.com",
                                        Subject = "Feedback - Canucks News"
                                    };
            emailcomposer.Show();
        }
    }
}