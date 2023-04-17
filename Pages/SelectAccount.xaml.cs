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
    /// Interaction logic for SelectAccount.xaml
    /// </summary>
    public partial class SelectAccount : Page {
        int index { get; set; }
        Frame frame { get; set; }
        public SelectAccount(Frame frame, int index) {
            InitializeComponent();
            this.frame = frame;
            this.index = index;
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            if (textBox.Text != "" && Singleton.db.Clients.Where(c => c.Account == textBox.Text).Count() != 0) {
                if (index == 0) {
                    Singleton.historyPage = new History(frame, textBox.Text);
                    frame.Navigate(Singleton.historyPage);
                }
                if(index == 1) {
                    Singleton.calculatePage = new CalculateKPI(frame, textBox.Text);
                    frame.Navigate(Singleton.calculatePage);
                }
                return;
            }
            MessageBox.Show("Поле незаповнене, або такого логіну не існує!");
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            frame.Navigate(Singleton.menuPage);
        }
    }
}
