using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRManager.Classes {
    public class Cluster {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdAgeCategory { get; set; }
        [ForeignKey("IdAgeCategory")]
        public AgeCategory AgeCategory { get; set; }
        public List<Person> Persons { get; set; }
    }
}
