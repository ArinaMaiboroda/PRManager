using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRManager.Classes {
    public class Client {
        public int Id { get; set; }
        public string Account { get; set; }
        public int? IdCluster { get; set; }
        [ForeignKey("IdCluster")]
        public Cluster Cluster { get; set; }
        public int? IdNetwork { get; set; }
        [ForeignKey("IdNetwork")]
        public Network Network { get; set; }
        public DateTime StartDate { get; set; }
        public int Days { get; set; }
        public int ExpectedFollowerCount { get; set; }
        public int CurrentFollowerCount { get; set; }
        public int ExpectedViewCount { get; set; }
        public int CurrentViewCount { get; set; }
        public int ExpectedLikeCount { get; set; }
        public int CurrentLikeCount { get; set; }
        public Client() { }
        public Client(int id, string account, Cluster cluster, Network network, DateTime startDate, int days, int expectedFollowerCount, int currentFollowerCount, int expectedViewCount, int currentViewCount, int expectedLikeCount, int currentLikeCount) {
            Id = id;
            Account = account;
            Cluster = cluster;
            Network = network;
            StartDate = startDate;
            Days = days;
            ExpectedFollowerCount = expectedFollowerCount;
            CurrentFollowerCount = currentFollowerCount;
            ExpectedViewCount = expectedViewCount;
            CurrentViewCount = currentViewCount;
            ExpectedLikeCount = expectedLikeCount;
            CurrentLikeCount = currentLikeCount;
        }
    }
}
