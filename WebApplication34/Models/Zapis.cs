namespace WebApplication34.Models
{
    public class Zapis
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int ViewId { get; set; }
        public View View { get; set; }

    }
}
