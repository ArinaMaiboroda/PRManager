using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRManager.Classes {
    public class ClientFactor {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public int IdFactor { get; set; }
        public ClientFactor() { }
        public ClientFactor(int id, int idClient, int idFactor) {
            Id = id;
            IdClient = idClient;
            IdFactor = idFactor;
        }
    }
}
