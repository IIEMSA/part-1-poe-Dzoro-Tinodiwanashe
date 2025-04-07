using CloudPOEpart1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BookingController : Controller
{
    private readonly DataBaseContext _context;

    public BookingController(DataBaseContext context)
    {
        _context = context;
    }

    // Index: Displays all bookings
    public async Task<IActionResult> Index()
    {
        var bookings = await _context.Booking.Include(b => b.Event).Include(b => b.Venue).ToListAsync();
        return View(bookings);
    }

    // Create (GET): Display the form to create a booking
    public async Task<IActionResult> Create()
    {
        // Fetch all Events and Venues asynchronously and pass to ViewData for dropdowns
        ViewData["Events"] = await _context.Event.ToListAsync();
        ViewData["Venues"] = await _context.Venue.ToListAsync();
        return View();
    }

    // Create (POST): Handle the form submission to create a booking
    [HttpPost]
    public async Task<IActionResult> Create(Booking booking)
    {
        // Check for double bookings
        bool isDoubleBooked = _context.Booking.Any(b =>
            b.VenueID == booking.VenueID &&
            b.EventID == booking.EventID &&
            b.BookingDate == booking.BookingDate);

        if (isDoubleBooked)
        {
            ModelState.AddModelError("", "This venue is already booked for the selected event on this date.");
            return View(booking);
        }

        // If ModelState is valid, save the booking
        if (ModelState.IsValid)
        {
            _context.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // If validation fails, repopulate the dropdowns
        ViewData["Events"] = await _context.Event.ToListAsync();
        ViewData["Venues"] = await _context.Venue.ToListAsync();
        return View(booking);
    }

    // Edit (GET): Display the form to edit a booking
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // Fetch the booking along with Event and Venue data
        var booking = await _context.Booking
            .Include(b => b.Event)
            .Include(b => b.Venue)
            .FirstOrDefaultAsync(m => m.BookingID == id);

        if (booking == null)
        {
            return NotFound();
        }

        // Fetch all Events and Venues for dropdowns
        ViewData["Events"] = await _context.Event.ToListAsync();
        ViewData["Venues"] = await _context.Venue.ToListAsync();

        return View(booking);
    }

    // Edit (POST): Handle form submission to update a booking
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Booking booking)
    {
        if (id != booking.BookingID)
        {
            return NotFound();
        }

        // Check for double bookings
        bool isDoubleBooked = _context.Booking.Any(b =>
            b.VenueID == booking.VenueID &&
            b.EventID == booking.EventID &&
            b.BookingDate == booking.BookingDate &&
            b.BookingID != id);

        if (isDoubleBooked)
        {
            ModelState.AddModelError("", "This venue is already booked for the selected event on this date.");
            return View(booking);
        }

        // If ModelState is valid, update the booking
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(booking.BookingID))
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

        // If validation fails, repopulate the dropdowns
        ViewData["Events"] = await _context.Event.ToListAsync();
        ViewData["Venues"] = await _context.Venue.ToListAsync();
        return View(booking);
    }

    // Helper method to check if a booking exists
    private bool BookingExists(int id)
    {
        return _context.Booking.Any(e => e.BookingID == id);
    }
}
