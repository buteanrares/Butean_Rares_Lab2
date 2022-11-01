using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Butean_Rares_Lab2.Data;
using Butean_Rares_Lab2.Models;
using Butean_Rares_Lab2.Models.LibraryViewModels;
using System.Net;

namespace Butean_Rares_Lab2.Controllers
{
    public class PublishersController : Controller
    {
        private readonly LibraryContext _context;
        private PublisherIndexData _viewModel;

        public PublishersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Publishers
        public async Task<IActionResult> Index(int? id, int? bookID)
        {
            _viewModel = new PublisherIndexData();
            _viewModel.Publishers = await _context.Publishers
            .Include(i => i.PublishedBooks)
            .ThenInclude(i => i.Book)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.PublisherName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["PublisherID"] = id.Value;
                Publisher publisher = _viewModel.Publishers.Where(
                i => i.ID == id.Value).Single();
                _viewModel.Books = publisher.PublishedBooks.Select(s => s.Book);
            }
            if (bookID != null)
            {
                ViewData["BoookID"] = bookID.Value;
                _viewModel.Orders = _viewModel.Books.Where(
                x => x.BookId == bookID).Single().Orders;
                _viewModel.Customers = from customer in _context.Customers
                                       join order in _context.Orders
                                       on customer.CustomerId equals order.CustomerId
                                       where order.BookId == bookID
                                       select customer;
            }
            return View(_viewModel);
        }


        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Publishers == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PublisherName,Adress")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Publishers == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PublisherName,Adress")] Publisher publisher)
        {
            if (id != publisher.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publisher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherExists(publisher.ID))
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
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Publishers == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Publishers == null)
            {
                return Problem("Entity set 'LibraryContext.Publishers'  is null.");
            }
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.ID == id);
        }
    }
}
