using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Utakmica
    {
        public int Id { get; set; }
        public int Tim1Id { get; set; }
        public int Tim2Id { get; set; }
        public Tim Tim1 { get; set; }
        public Tim Tim2 { get; set; }
        public int? Tim1Golovi  { get; set; }  // Goals scored by Tim1
        public int? Tim2Golovi { get; set; }  // Goals scored by Tim2
        public DateTime VremePocetka { get; set; }
        public bool Predato { get; set; }
        public int? StadionId { get; set; }
        public Stadion? Stadion { get; set; }

        

    }
}
