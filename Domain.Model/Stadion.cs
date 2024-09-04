using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Stadion
    {
        public int Id { get; set; }
        public string ImeStadiona { get; set; }
        public ICollection<Utakmica> Utakmice { get; set; }  // Navigacioni property za utakmice na stadionu
    }
}
