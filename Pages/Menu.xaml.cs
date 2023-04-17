using PRManager.Classes;
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

namespace PRManager.Pages {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Menu : Page {
        Frame frame { get; set; }
        public Menu(Frame frame) {
            InitializeComponent();
            this.frame = frame;
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            Singleton.addClientPage = new AddClient(frame);
            frame.Navigate(Singleton.addClientPage);
        }

        private void button2_Click(object sender, RoutedEventArgs e) {
            Singleton.selectAccountPage = new SelectAccount(frame, 0);
            frame.Navigate(Singleton.selectAccountPage);
        }

        private void button3_Click(object sender, RoutedEventArgs e) {
            Singleton.selectAccountPage = new SelectAccount(frame, 1);
            frame.Navigate(Singleton.selectAccountPage);
        }

        private void button4_Click(object sender, RoutedEventArgs e) {
            Singleton.statsPage = new Stats(frame);
            frame.Navigate(Singleton.statsPage);
        }
    }
}
