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
    /// Interaction logic for CalculateKPI.xaml
    /// </summary>
    public partial class CalculateKPI : Page {
        Frame frame { get; set; }
        string login { get; set; }
        public CalculateKPI(Frame frame, string login) {
            InitializeComponent();
            this.login = login;
            this.frame = frame;
            SetKPI();
            List<Client> clients = Singleton.db.Clients.Where(c => c.Account == login).ToList();
            DataGridTextColumn[] cols = new DataGridTextColumn[6];
            for (int i = 0; i < cols.Count(); i++) {
                cols[i] = new DataGridTextColumn();
                dataGrid.Columns.Add(cols[i]);
            }
            cols[0].Binding = new Binding("Id");
            cols[1].Binding = new Binding("Login");
            cols[2].Binding = new Binding("StartDate");
            cols[3].Binding = new Binding("Period");
            cols[4].Binding = new Binding("Result");
            cols[5].Binding = new Binding("Factors");
            dataGrid.Columns[0].Header = "№";
            dataGrid.Columns[1].Header = "Логін";
            dataGrid.Columns[2].Header = "День початку";
            dataGrid.Columns[3].Header = "Період/дн";
            dataGrid.Columns[4].Header = "KPI у відсотках";
            dataGrid.Columns[5].Header = "Фактори";
            int index = 1;
            foreach (Client c in clients) {
                List<string> factors;
                List<int> idfactors = Singleton.db.ClientFactors.Where(f => f.IdClient == c.Id).Select(f => f.IdFactor).ToList();
                factors = Singleton.db.Factors.Where(f => idfactors.Contains(f.Id) == true).Select(f => f.Name).ToList();
                string sfactors = factors[0] + ", " + factors[1] + ", " + factors[2];
                KPIData kd = new KPIData() {
                    Id = index++,
                    Login = login,
                    StartDate = DateOnly.FromDateTime(c.StartDate),
                    Result = Math.Round(((double)c.CurrentFollowerCount / c.ExpectedFollowerCount + 
                    (double)c.CurrentViewCount / c.ExpectedViewCount + (double)c.CurrentLikeCount / c.ExpectedLikeCount) 
                    / 3 * 100, 3),
                    Factors = sfactors,
                    Period = c.Days
                };
                dataGrid.Items.Add(kd);
            }
        }
        private void SetKPI() {
            List<Client> clients = Singleton.db.Clients.Where(c => c.Account == login).ToList();
            for(int i = 0; i < clients.Count; i++) {
                Client client = clients[i];
                TimeSpan ts = DateTime.Now - client.StartDate;
                Random rand = new Random();
                if(ts.Days >= client.Days) {
                    int followers = rand.Next(client.ExpectedFollowerCount - 50, client.ExpectedFollowerCount + 1);
                    int views = rand.Next(client.ExpectedViewCount - 100, client.ExpectedViewCount + 1);
                    int likes = rand.Next(client.ExpectedLikeCount - 25, client.ExpectedLikeCount + 1);
                    client.CurrentFollowerCount = followers;
                    client.CurrentViewCount = views;
                    client.CurrentLikeCount = likes;
                }
                else {
                    double d = (double)ts.Days / client.Days;
                    int followers = rand.Next(Convert.ToInt32((client.ExpectedFollowerCount - 50) * d), Convert.ToInt32((client.ExpectedFollowerCount + 1) * d));
                    int views = rand.Next(Convert.ToInt32((client.ExpectedViewCount - 100) * d), Convert.ToInt32((client.ExpectedViewCount + 1) * d));
                    int likes = rand.Next(Convert.ToInt32((client.ExpectedLikeCount - 25) * d), Convert.ToInt32((client.ExpectedLikeCount + 1) * d));
                    client.CurrentFollowerCount = followers;
                    client.CurrentViewCount = views;
                    client.CurrentLikeCount = likes;
                }
                Singleton.db.SaveChanges();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            Singleton.selectAccountPage = new SelectAccount(frame, 1);
            frame.Navigate(Singleton.selectAccountPage);
        }
    }
    public struct KPIData {
        public int Id { get; set; }
        public string Login { get; set; }
        public DateOnly StartDate { get; set; }
        public int Period { get; set; }
        public double Result { get; set; }
        public string Factors { get; set; }
        public KPIData() { }
    }
}
