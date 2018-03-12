using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
//----------------------------------------------------------------------------
namespace Chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<object> collection = new ObservableCollection<object>(); 

        public MainWindow()
        {
            InitializeComponent();
            lbMessages.Items = collection;
        }
        //----------------------------------------------------------------------------
        void SendMessage()
        {
            if (tbUser.Text != String.Empty)
            {
                collection.Add(new ListBoxItem()
                {
                    Content = new Border()
                    {
                        BorderThickness = new Thickness(2),
                        CornerRadius = new CornerRadius(25),
                        Child = new TextBlock
                        {
                            Text = tbUser.Text,
                            TextWrapping = TextWrapping.Wrap
                        }
                    }
                });

                lbMessages.ScrollIntoView(lbMessages.Items.Cast<ListBoxItem>().Last());
                tbUser.Clear();
            }
        }
        //----------------------------------------------------------------------------
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendMessage();
        }
        //----------------------------------------------------------------------------
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }
    }
}
//----------------------------------------------------------------------------