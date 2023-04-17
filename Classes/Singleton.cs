using PRManager.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRManager.Classes {
    public static class Singleton {
        public static PRContext db { get; private set; } = new PRContext();
        public static Menu menuPage { get; set; }
        public static AddClient addClientPage { get; set; }
        public static SelectAccount selectAccountPage { get; set; }
        public static History historyPage { get; set; }
        public static CalculateKPI calculatePage { get; set; }
        public static Stats statsPage { get; set; }
    }
}
