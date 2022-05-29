namespace WebApplication34.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }

        public List<Zapis> Zapiss { get; set; } = new List<Zapis>();
    }
}
