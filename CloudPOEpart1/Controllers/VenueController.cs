using CloudPOEpart1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace CloudPOEpart1.Controllers
{
    public class VenueController : Controller
    {
        private readonly DataBaseContext _context;

        public VenueController(DataBaseContext context)
        {
            _context = context;
        }

        // Index: Displays all venues
        public async Task<IActionResult> Index()
        {
            var venues = await _context.Venue.ToListAsync();
            return View(venues);
        }

        // Create (GET): Display the form to create a venue
        public IActionResult Create()
        {
            return View();
        }

        // Create (POST): Handle form submission to create a new venue
        [HttpPost]
        public async Task<IActionResult> Create(Venue venue)
        {
            // Placeholder image URL
            venue.ImageUrl = "https://via.placeholder.com/150"; // You can replace this with a different placeholder image link

            if (ModelState.IsValid)
            {
                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }


        // Edit (GET): Display the form to edit a venue
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venue.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // Edit (POST): Handle the form submission to update a venue
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Venue venue)
        {
            if (id != venue.VenueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.VenueID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(venue);
        }

        // Delete (GET): Display the form to confirm deletion of a venue
        public async Task<IActionResult> Delete(int? id)
        {
            var venue = await _context.Venue.FirstOrDefaultAsync(m => m.VenueID == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // Delete (POST): Handle the deletion of a venue
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venue.FindAsync(id);
            _context.Venue.Remove(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueExists(int id)
        {
            return _context.Venue.Any(e => e.VenueID == id);
        }
    }
}
