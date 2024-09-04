namespace MobilnoQatarBack.DTO
{
    public class TimDTO
    {
        public int Id { get; set; }
        public string ImeTima { get; set; }
        public string Zastavica { get; set; }
        public int? GrupaId { get; set; }
        public int BrojPoena { get; set; }
        public int BrojPobeda { get; set; }
        public int BrojPoraza { get; set; }
        public int BrojNeresenih { get; set; }
        public int BrojDatihGolova { get; set; }
        public int BrojPrimljenihGolova { get; set; }
    }
}
