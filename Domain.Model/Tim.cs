using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Tim
    {
        public int Id { get; set; }
        public string ImeTima { get; set; }
        public string Zastavica { get; set; }
        public int? GrupaId { get; set; }
        public Grupa Grupa { get; set; }
        public int BrojPoena { get; set; }
        public int BrojPobeda { get; set; }
        public int BrojPoraza { get; set; }
        public int BrojNeresenih { get; set; }
        public int BrojDatihGolova { get; set; }
        public int BrojPrimljenihGolova { get; set; }
    }
}
