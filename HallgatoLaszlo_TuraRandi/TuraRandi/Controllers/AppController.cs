using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.ObjectModel;
using TuraRandi.Data;
using TuraRandi.DTOs;
using TuraRandi.Models;

namespace TuraRandi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppController(TuraRandiDbContext context) : ControllerBase
    {
        //Adatbázis kapcsolat példányosítása _context néven

        public readonly TuraRandiDbContext _context = context;

        // GET: app
        //Bejegyzések listájának lekérése

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Testimonial>>> GetTestimonials()
        {
            return await _context.Testimonials.ToListAsync();
        }

        // GET: app/5
        //Bejegyzés lekérése
        [HttpGet("{id}")]
        public async Task<ActionResult<Testimonial>> GetTestimonial(int id)
        {
            var t = _context.Testimonials.Find(id);
            if (t == null)
                return NotFound("Nincs ilyen bejegyzés!");
            else return Ok(t);
        }

        // POST: app
        //Bejegyzés hozzáadása
        [HttpPost]
        public async Task<ActionResult> PostTestimonial(PostTestimonialDTO request)
        {
            Testimonial testimonial = new Testimonial
            {
                Name = request.Name,
                Age = request.Age,
                Location = request.Location ?? null,
                TestimonialText = request.TestimonialText,
                ImageUrl = request.ImageUrl ?? null
            };

            await _context.Testimonials.AddAsync(testimonial);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT: app/5
        //Bejegyzés módosítása
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestimonial(int id, Testimonial testimonial)
        {
            if (id != testimonial.Id)
            {
                return BadRequest();
            }
            _context.Entry(testimonial).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: app/5
        //Bejegyzés törlése
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);

            if (testimonial == null)
            {
                return NotFound();
            }

            _context.Testimonials.Remove(testimonial);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
