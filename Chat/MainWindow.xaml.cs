using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
//----------------------------------------------------------------------------
namespace Chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<object> collection = new ObservableCollection<object>();

        Dictionary<bool, SolidColorBrush> color;
        Dictionary<bool, HorizontalAlignment> alignment;


        bool rightLeft = false;
        string path = "Messages.txt";

        //----------------------------------------------------------------------------
        public MainWindow()
        {
            InitializeComponent();
            lbMessages.ItemsSource = collection;

            color = new Dictionary<bool, SolidColorBrush>()
            {
                { false, new SolidColorBrush(Colors.White)},
                { true, new SolidColorBrush(Color.FromRgb(220, 248, 198))}
            };

            alignment = new Dictionary<bool, HorizontalAlignment>()
            {
                { false, HorizontalAlignment.Left},
                { true, HorizontalAlignment.Right}
            };
        }
        //----------------------------------------------------------------------------
        void listFill(string str)
        {
            collection.Add(new ListBoxItem()
            {
                HorizontalAlignment = alignment[rightLeft], 
                IsTabStop = false,
                Tag = str,

                Content = new Border()
                {
                    BorderThickness = new Thickness(2),
                    CornerRadius = new CornerRadius(50),

                    Child = new TextBlock
                    {
                        Background = color[rightLeft],
                        Text = ' ' + str + ' ',
                        TextWrapping = TextWrapping.Wrap,
                        FontSize = 15,
                        FontFamily = new FontFamily("Segoe Print")
                    }
                }
            });

            lbMessages.ScrollIntoView(lbMessages.Items.Cast<ListBoxItem>().Last());

            rightLeft = !rightLeft;

            tbUser.Clear();
        }
        //----------------------------------------------------------------------------
        void SendMessage()
        {
            if (tbUser.Text != String.Empty)
                listFill(tbUser.Text);
        }
        //----------------------------------------------------------------------------
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.Enter))
                SendMessage();
        }
        //----------------------------------------------------------------------------
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }
        //----------------------------------------------------------------------------
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                int n = Int16.Parse(sr.ReadLine());

                for (int i = 0; i < n; i++)
                    listFill(sr.ReadLine());
            }
        }
        //----------------------------------------------------------------------------
        private void Window_Closed(object sender, EventArgs e)
        {
            if (collection.Count != 0)
            {
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    sw.WriteLine(collection.Count);

                    foreach (ListBoxItem item in collection)
                    {
                        sw.WriteLine(item.Tag);
                    }
                }
            }
        }
    }
}
//----------------------------------------------------------------------------