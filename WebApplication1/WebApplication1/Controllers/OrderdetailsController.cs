using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OrderdetailsController : Controller
    {
        private readonly NorthwindContext _context;

        public OrderdetailsController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: Orderdetails
        public async Task<IActionResult> Index()
        {
            var northwindContext = _context.Orderdetails.Include(o => o.Order).Include(o => o.Product);
            return View(await northwindContext.ToListAsync());
        }

        // GET: Orderdetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orderdetails == null)
            {
                return NotFound();
            }

            var orderdetail = await _context.Orderdetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderdetail == null)
            {
                return NotFound();
            }

            return View(orderdetail);
        }

        // GET: Orderdetails/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: Orderdetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] Orderdetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderdetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderdetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderdetail.ProductId);
            return View(orderdetail);
        }

        // GET: Orderdetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orderdetails == null)
            {
                return NotFound();
            }

            var orderdetail = await _context.Orderdetails.FindAsync(id);
            if (orderdetail == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderdetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderdetail.ProductId);
            return View(orderdetail);
        }

        // POST: Orderdetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] Orderdetail orderdetail)
        {
            if (id != orderdetail.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderdetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderdetailExists(orderdetail.OrderId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderdetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderdetail.ProductId);
            return View(orderdetail);
        }

        // GET: Orderdetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orderdetails == null)
            {
                return NotFound();
            }

            var orderdetail = await _context.Orderdetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderdetail == null)
            {
                return NotFound();
            }

            return View(orderdetail);
        }

        // POST: Orderdetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orderdetails == null)
            {
                return Problem("Entity set 'NorthwindContext.Orderdetails'  is null.");
            }
            var orderdetail = await _context.Orderdetails.FindAsync(id);
            if (orderdetail != null)
            {
                _context.Orderdetails.Remove(orderdetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderdetailExists(int id)
        {
          return (_context.Orderdetails?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
