namespace MobilnoQatarBack.DTO
{
    public class UtakmicaDTO
    {
        public int Id { get; set; }
        public int Tim1Id { get; set; }
    
        public int Tim2Id { get; set; }
        
        public DateTime VremePocetka { get; set; }= DateTime.Now;
        public int? Tim1Golovi { get; set; }
        public int? Tim2Golovi { get; set; }
        public int? StadionId { get; set; }
        public bool Predato { get; set; } = false;
        public bool Tim1Predao { get; set; } = false;
        public bool Tim2Predao { get; set; } = false;
    }
}
