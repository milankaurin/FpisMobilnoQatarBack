namespace Domain.Model
{
    public class Grupa
    {
        public int Id { get; set; }
        public string ImeGrupe { get; set; }
        public ICollection<Tim> Timovi { get; set; }  // Navigacioni property za timove u grupi
    }
}
