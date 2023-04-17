using PRManager.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
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
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Page {
        Frame frame;
        string login { get; set; }
        public History(Frame frame, string login) {
            InitializeComponent();
            this.frame = frame;
            this.login = login;
            SetKPI();
            List<Client> clients = Singleton.db.Clients.Where(c => c.Account == login).ToList();
            DataGridTextColumn[] cols = new DataGridTextColumn[10];
            for (int i = 0; i < cols.Count(); i++) {
                cols[i] = new DataGridTextColumn();
                dataGrid.Columns.Add(cols[i]);
            }
            cols[0].Binding = new Binding("Id");
            cols[1].Binding = new Binding("Login");
            cols[2].Binding = new Binding("Date");
            cols[3].Binding = new Binding("Period");
            cols[4].Binding = new Binding("CurrentFollowerCount");
            cols[5].Binding = new Binding("ExpectedFollowerCount");
            cols[6].Binding = new Binding("CurrentViewCount");
            cols[7].Binding = new Binding("ExpectedViewCount");
            cols[8].Binding = new Binding("CurrentLikeCount");
            cols[9].Binding = new Binding("ExpectedLikeCount");
            dataGrid.Columns[0].Header = "№";
            dataGrid.Columns[1].Header = "Логін";
            dataGrid.Columns[2].Header = "День початку";
            dataGrid.Columns[3].Header = "Період";
            dataGrid.Columns[4].Header = "Пот. підпис.";
            dataGrid.Columns[5].Header = "Очк. підпис.";
            dataGrid.Columns[6].Header = "Пот. перег.";
            dataGrid.Columns[7].Header = "Очк. перег.";
            dataGrid.Columns[8].Header = "Пот. лайки";
            dataGrid.Columns[9].Header = "Очк. лайки";
            int index = 1;
            foreach (Client c in clients) {
                ClientData cd = new ClientData() {
                    Id = index++,
                    Login = login,
                    Date = DateOnly.FromDateTime(c.StartDate),
                    ExpectedFollowerCount = c.ExpectedFollowerCount,
                    CurrentFollowerCount = c.CurrentFollowerCount,
                    ExpectedViewCount = c.ExpectedViewCount,
                    CurrentViewCount = c.CurrentViewCount,
                    ExpectedLikeCount = c.ExpectedLikeCount,
                    CurrentLikeCount = c.CurrentLikeCount,
                    Period = c.Days
                };
                dataGrid.Items.Add(cd);
            }
        }
        private void SetKPI() {
            List<Client> clients = Singleton.db.Clients.Where(c => c.Account == login).ToList();
            for (int i = 0; i < clients.Count; i++) {
                Client client = clients[i];
                TimeSpan ts = DateTime.Now - client.StartDate;
                Random rand = new Random();
                if (ts.Days >= client.Days) {
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
            Singleton.selectAccountPage = new SelectAccount(frame, 0);
            frame.Navigate(Singleton.selectAccountPage);
        }
    }
    public struct ClientData {
        public int Id { get; set; }
        public string Login { get; set; }
        public DateOnly Date { get; set; }
        public int Period { get; set; }
        public int CurrentFollowerCount { get; set; }
        public int ExpectedFollowerCount { get; set; }
        public int CurrentViewCount { get; set; }
        public int ExpectedViewCount { get; set;}
        public int CurrentLikeCount { get; set; }
        public int ExpectedLikeCount { get; set; }
        public ClientData() { }
    }
}
