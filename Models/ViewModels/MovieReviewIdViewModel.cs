namespace VividRasV2.Models.ViewModels
{
    public class MovieReviewIdViewModel
    {

        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
