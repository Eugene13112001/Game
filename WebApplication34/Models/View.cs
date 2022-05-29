namespace WebApplication34.Models
{
    public class View
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Zapis> Zapiss { get; set; } = new List<Zapis>();
    }
}
