using Microsoft.AspNetCore.Mvc;
using skuska_01.Models;
using skuska_01.Services;

namespace skuska_01.Controllers
{
    public class HomeController : Controller
    {
        private static List<Transaction> LocalTransactionList = new();
        private readonly BankService _bankService = new();

        public async Task<IActionResult> Index()
        {
            var newTransactions = await _bankService.GetTransactionsAsync();

            // pri refreshi sa pridajú nové transakcie na koniec
            LocalTransactionList.AddRange(newTransactions);

            return View(LocalTransactionList);
        }
    }
}
