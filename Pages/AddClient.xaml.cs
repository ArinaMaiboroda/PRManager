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
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient : Page {
        Frame frame { get; set; }
        string radio { get; set; } = "Фейсбук";
        public AddClient(Frame frame) {
            InitializeComponent();
            this.frame = frame;
            comboBox1.ItemsSource = Singleton.db.Clusters.Select(c => c.Name).Distinct().ToList();
            comboBox2.ItemsSource = Singleton.db.AgeCategories.Select(c => c.Name).Distinct().ToList();
            comboBox3.ItemsSource = new List<string> { "7 днів", "14 днів", "28 днів" };
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            if (textBox1.Text != "" && comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex != -1) {
                Cluster cluster = Singleton.db.Clusters.Single(c => c.Name == comboBox1.SelectedItem.ToString() && c.AgeCategory.Name == comboBox2.SelectedItem.ToString());
                List<Factor> factors = Singleton.db.Factors.ToList().OrderBy(arg => Guid.NewGuid()).Take(3).ToList();
                Network network = Singleton.db.Networks.Single(n => n.Name == radio);
                int days = 0;
                if(comboBox3.SelectedIndex == 0) {
                    days = 7;
                }
                if (comboBox3.SelectedIndex == 1) {
                    days = 14;
                }
                if (comboBox3.SelectedIndex == 2) {
                    days = 28;
                }
                Client client = new Client(0, textBox1.Text, cluster, network, DateTime.Now, days, cluster.Persons.Count, 0, cluster.Persons.Count * 2, 0, Convert.ToInt32(cluster.Persons.Count * 0.5), 0);
                Singleton.db.Clients.Add(client);
                Singleton.db.SaveChanges();
                client = Singleton.db.Clients.OrderBy(c => c.Id).Last(c => c.Account == textBox1.Text);
                foreach (Factor f in factors) {
                    ClientFactor cf = new ClientFactor(0, client.Id, f.Id);
                    Singleton.db.ClientFactors.Add(cf);
                    Singleton.db.SaveChanges();
                }
                MessageBox.Show("Просування облікового запису запущено!");
                frame.Navigate(Singleton.menuPage);
                return;
            }
            MessageBox.Show("Заповніть усі поля!");
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            frame.Navigate(Singleton.menuPage);
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e) {
            var tempradio = sender as RadioButton;
            if (tempradio != null) {
                if(tempradio.Content != null) {
                    radio = tempradio.Content.ToString();
                }
            }
        }
    }
}
