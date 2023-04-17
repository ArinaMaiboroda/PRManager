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

namespace PRManager {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Singleton.db.Clients.ToList();
            Singleton.menuPage = new Pages.Menu(frame);
            frame.Navigate(Singleton.menuPage);
            //foreach(Preference pr in Singleton.db.Preferences.ToList()) {
            //    foreach(AgeCategory ac in Singleton.db.AgeCategories.ToList()) {
            //        Random rand = new Random();
            //        int count = rand.Next(400, 2000);
            //        Cluster cluster = Singleton.db.Clusters.Single(c => c.Name == pr.Name && c.AgeCategory == ac);
            //        for (int i = 0; i < count; i++) {
            //            int age = rand.Next(14, 85);
            //            Person p = new Person(0, pr, cluster, age);
            //            Singleton.db.Add(p);
            //            Singleton.db.SaveChanges();
            //        }
            //    }
            //    MessageBox.Show(pr.Name);
            //}
        }
    }
}
