using Domain.Model;

namespace MobilnoQatarBack.DTO
{
    public class StadionDTO 
    {
        public int Id { get; set; }
        public string ImeStadiona { get; set; }
        //public ICollection<Utakmica> Utakmice { get; set; }  // Navigacioni property za utakmice na stadionu
    }
}
