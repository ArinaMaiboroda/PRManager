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
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : Page {
        Frame frame { get; set; }
        public Stats(Frame frame) {
            InitializeComponent();
            this.frame = frame;
            List<Client> clients = Singleton.db.Clients.Where(c => c.StartDate.Month == DateTime.Now.Month).ToList();
            double result = 0;
            foreach (Client c in clients) {
                result += Math.Round(((double)c.CurrentFollowerCount / c.ExpectedFollowerCount +
                    (double)c.CurrentViewCount / c.ExpectedViewCount + (double)c.CurrentLikeCount / c.ExpectedLikeCount)
                    / 3 * 100, 3);
            }
            result /= clients.Count;
            label1.Content = "Середній KPI за місяць: " + Math.Round(result, 3);
            int n1 = clients.Where(c => c.Network.Name == "Фейсбук").Count();
            int progress = Convert.ToInt32((double)n1 / clients.Count * 100);
            progressbar.Value = progress;
            label2.Content = "Кількість просувань за місяць: " + clients.Count;
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            frame.Navigate(Singleton.menuPage);
        }
    }
}
