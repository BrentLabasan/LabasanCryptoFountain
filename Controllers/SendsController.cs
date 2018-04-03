using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabasanCryptoFountain.Models;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.requests;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.operations;
using System.Diagnostics;
using System.Net.Http;

namespace LabasanCryptoFountain.Controllers
{
    public class SendsController : Controller
    {
        private readonly LabasanCryptoFountainContext _context;

        public SendsController(LabasanCryptoFountainContext context)
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
        public async Task<string> Create([Bind("ID,TokenName,Amount,Source,Destination,SendStart,SendEnd")] Send send)
        {
            send.Destination = send.Destination.ToUpper();
            send.TokenName = send.TokenName.ToUpper();

            if (send.Destination[0] != 'G' || send.Destination.Length != 56)
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


            Network.UsePublicNetwork();
            var server = new Server("https://horizon.stellar.org");

            KeyPair source = KeyPair.FromSecretSeed(Environment.GetEnvironmentVariable("SECRET_KEY_" + send.TokenName));
            KeyPair destination = KeyPair.FromAccountId(send.Destination);

            send.Source = Environment.GetEnvironmentVariable("SECRET_KEY_" + send.TokenName);

            await server.Accounts.Account(destination);

            AccountResponse sourceAccount = await server.Accounts.Account(source);
            var sendingAccountPubKey = Environment.GetEnvironmentVariable("PUBLIC_KEY_" + send.TokenName);
            AccountsRequestBuilder accReqBuilder = new AccountsRequestBuilder(new Uri("https://horizon.stellar.org/accounts/" + sendingAccountPubKey));
            var accountResponse = await accReqBuilder.Account(new Uri("https://horizon.stellar.org/accounts/" + sendingAccountPubKey));

            Asset tst;
            if (send.TokenName == "XLM")
            {
                // TODO implement this in the future
                tst = new AssetTypeNative(); // https://elucidsoft.github.io/dotnet-stellar-sdk/api/stellar_dotnetcore_sdk.AssetTypeNative.html
            }
            else if (send.TokenName.Length <= 4)
            {
                tst = new AssetTypeCreditAlphaNum4(send.TokenName, KeyPair.FromAccountId(Environment.GetEnvironmentVariable("ISSUER_KEY_" + send.TokenName)));
            }
            else
            {
                tst = new AssetTypeCreditAlphaNum12(send.TokenName, KeyPair.FromAccountId(Environment.GetEnvironmentVariable("ISSUER_KEY_" + send.TokenName)));
            }

            Transaction transaction = new Transaction.Builder(new stellar_dotnetcore_sdk.Account(KeyPair.FromAccountId(sendingAccountPubKey), accountResponse.SequenceNumber))
                    .AddOperation(new PaymentOperation.Builder(destination, tst, Convert.ToString(send.Amount)).Build())
                    .AddMemo(Memo.Text("Test Transaction"))
                    .Build();
            transaction.Sign(source);

            string status = "";
            try
            {
                if (ModelState.IsValid)
                {
                    SubmitTransactionResponse response = await server.SubmitTransaction(transaction);
                    status += "Success!";
                    return HtmlEncoder.Default.Encode($"SendsController POST CREATE {status} 1 {source} 2  3  4 ");
                }
            }
            catch (Exception e)
            {
                status += "ERROR" + e.Message;
            }
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
