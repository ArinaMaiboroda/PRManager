using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRManager.Classes {
    public class Person {
        public int Id { get; set; }
        public int? IdPreference { get; set; }
        [ForeignKey("IdPreference")]
        public Preference Preference { get; set; }
        public int? IdCluster { get; set; }
        [ForeignKey("IdCluster")]
        public Cluster Cluster { get; set; }
        public int Age { get; set; }
        public Person() { }
        public Person(int id, Preference preference, Cluster cluster, int age) {
            Id = id;
            Preference = preference;
            Cluster = cluster;
            Age = age;
        }
    }
}
