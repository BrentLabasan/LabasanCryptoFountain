using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TST_Fountain.Models;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.requests;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.operations;
using System.Diagnostics;
using System.Net.Http;

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

        public async Task<IActionResult> Details2(int? id)
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
        public async Task<string> Create([Bind("ID,Address,TokenName,Amount")] Send send)
        {
            send.Address = send.Address.ToUpper();
            send.TokenName = send.TokenName.ToUpper();

            if (send.Address[0] != 'G' || send.Address.Length != 56)
            {
                ModelState.AddModelError("Address", "Address is not in a proper format (begins with a G and is 56 characters long");
            }

            string[] tokenNames = { "XLM", "SECOND", "MINUTE", "HOUR", "DAY", "WEEK", "MONTH", "YEAR", "MASLOW1", "MASLOW2", "MASLOW3", "MASLOW4", "MASLOW5" };
            if (!(tokenNames.Contains(send.TokenName)))
            {
                ModelState.AddModelError("TokenName", "Token is not supported.");
            }

            if (!(send.Amount > 0))
            {
                ModelState.AddModelError("Amount", "The amount sent has to be a positive integer.");
            }

            string a1 = Environment.GetEnvironmentVariable("drake1");
            string a2 = Environment.GetEnvironmentVariable("Weeknd100");
            string a3 = Environment.GetEnvironmentVariable("brenttest");
            string a4 = Environment.GetEnvironmentVariable("SECRET_KEY_SECOND");

            // var transaction = new stellar_dotnetcore_sdk.Transaction.Builder(new stellar_dotnetcore_sdk.Account(KeyPair.FromAccountId("GA2C5RFPE6GCKMY3US5PAB6UZLKIGSPIUKSLRB6Q723BM2OARMDUYEJ5"), stellar_dotnetcore_sdk.requests.AccountsRequestBuilder))
            //     // this operation funds the new account with XLM
            //     .addOperation(stellar_dotnetcore_sdk.requests.PaymentsRequestBuilder({

            // })
            //     }))

            // KeyPair source = KeyPair.fromSecretSeed("SCZANGBA5YHTNYVVV4C3U252E2B6P6F5T3U6MM63WBSBZATAQI3EBTQ4");
            // KeyPair destination = KeyPair.fromAccountId("GA2C5RFPE6GCKMY3US5PAB6UZLKIGSPIUKSLRB6Q723BM2OARMDUYEJ5");

            // var transactions = await server.Transactions
            //     .ForAccount(KeyPair.FromAccountId("GAZHWW2NBPDVJ6PEEOZ2X43QV5JUDYS3XN4OWOTBR6WUACTUML2CCJLI"))
            //     .Execute();

            // var x = await server.Payments
            //     .ForAccount(KeyPair.FromAccountId("GAZHWW2NBPDVJ6PEEOZ2X43QV5JUDYS3XN4OWOTBR6WUACTUML2CCJLI"))
            //     .Execute();

            if (ModelState.IsValid)
            {
                Network.UsePublicNetwork();
                var server = new Server("https://horizon.stellar.org");

                AccountsRequestBuilder accReqBuilder = new AccountsRequestBuilder(new Uri("https://horizon.stellar.org/accounts/" + Environment.GetEnvironmentVariable("PUBLIC_KEY_" + send.TokenName)));
                var accountSending = await accReqBuilder.Account(new Uri("https://horizon.stellar.org/accounts/" + Environment.GetEnvironmentVariable("PUBLIC_KEY_" + send.TokenName)));

                
                // var transactionCallBuilder = await server.Transactions.ForAccount(stellar_dotnetcore_sdk.KeyPair.FromAccountId(send.Address)).Execute();

                _context.Add(send);
                await _context.SaveChangesAsync();
                return HtmlEncoder.Default.Encode($"SendsController POST CREATE {accountSending.SequenceNumber} 1 {a1} 2 {a2} 3 {a3} 4 {a4}");
                // return RedirectToAction(nameof(Index));
            }
            // return View(send);
            return HtmlEncoder.Default.Encode($"INVALID {send.ID}, {send.TokenName}");

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

        [HttpPost]
        public async Task ShowAccountTransactions()
        {
            Network.UsePublicNetwork();
            var server = new Server("https://horizon.stellar.org");


            Console.WriteLine("-- Show Account Transactions (ForAccount) --");

            var transactions = await server.Transactions
                .ForAccount(KeyPair.FromAccountId("GAZHWW2NBPDVJ6PEEOZ2X43QV5JUDYS3XN4OWOTBR6WUACTUML2CCJLI"))
                .Execute();

            ShowTransactionRecords(transactions.Records);
            Console.WriteLine();
        }

        private static void ShowTransactionRecords(List<TransactionResponse> transactions)
        {
            foreach (var tran in transactions)
                ShowTransactionRecord(tran);
        }
        private static void ShowTransactionRecord(TransactionResponse tran)
        {
            Console.WriteLine($"Ledger: {tran.Ledger}, Hash: {tran.Hash}, Fee Paid: {tran.FeePaid}");
        }
    }
}
