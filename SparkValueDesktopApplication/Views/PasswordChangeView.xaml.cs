using SparkValueDesktopApplication.AttachedProperties;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SparkValueDesktopApplication.Views
{
    /// <summary>
    /// Interaction logic for PasswordChangeView.xaml
    /// </summary>
    public partial class PasswordChangeView : UserControl
    {
        public PasswordChangeView()
        {
            InitializeComponent();
        }

        private void PasswordEntry_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            PasswordBoxAttachedProperty.SetEncryptedPassword(passwordBox, passwordBox.SecurePassword);
        }
    }
}
