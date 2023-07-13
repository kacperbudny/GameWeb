namespace GameWeb.Models
{
    public class FavouriteGame
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
