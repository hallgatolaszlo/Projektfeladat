namespace TuraRandi.DTOs
{
    public class PostTestimonialDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Location { get; set; }
        public string TestimonialText { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }
}
