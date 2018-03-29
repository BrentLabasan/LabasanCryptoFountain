using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TST_Fountain.Models;

namespace TST_Fountain.Controllers
{
    public class SendsController : Controller
    {
        private readonly TST_FountainContext _context;

        public SendsController(TST_FountainContext context)
        {
            _context = context;
        }

        // GET: Sends
        public async Task<IActionResult> Index()
        {
            return View(await _context.Send.ToListAsync());
        }

        // GET: Sends/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var send = await _context.Send
                .SingleOrDefaultAsync(m => m.ID == id);
            if (send == null)
            {
                return NotFound();
            }

            return View(send);
        }

        // GET: Sends/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<string> Create([Bind("ID,TokenName,Amount")] Send send)
        {
            if (ModelState.IsValid)
            {
                _context.Add(send);
                await _context.SaveChangesAsync();
                return HtmlEncoder.Default.Encode($"SEND {send}");
                // return RedirectToAction(nameof(Index));
            }
            // return View(send);
            return HtmlEncoder.Default.Encode($"INVALID {send.ID}, NumTimes is: {send.TokenName}");

        }

        // GET: Sends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var send = await _context.Send.SingleOrDefaultAsync(m => m.ID == id);
            if (send == null)
            {
                return NotFound();
            }
            return View(send);
        }

        // POST: Sends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TokenName,Amount")] Send send)
        {
            if (id != send.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(send);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SendExists(send.ID))
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
            return View(send);
        }

        // GET: Sends/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var send = await _context.Send
                .SingleOrDefaultAsync(m => m.ID == id);
            if (send == null)
            {
                return NotFound();
            }

            return View(send);
        }

        // POST: Sends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var send = await _context.Send.SingleOrDefaultAsync(m => m.ID == id);
            _context.Send.Remove(send);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SendExists(int id)
        {
            return _context.Send.Any(e => e.ID == id);
        }
    }
}
