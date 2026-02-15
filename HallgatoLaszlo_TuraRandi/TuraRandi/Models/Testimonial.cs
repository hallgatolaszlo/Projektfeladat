using System.ComponentModel.DataAnnotations;

namespace TuraRandi.Models;

//Az osztály szerkezete: Testimonial(Id, Name, Age, Location, TestimonialText, ImageUrl)
public class Testimonial
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string? Location { get; set; }
    public string TestimonialText { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}
