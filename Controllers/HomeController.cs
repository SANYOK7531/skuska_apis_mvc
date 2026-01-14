using Microsoft.AspNetCore.Mvc;
using skuska_01.Models;
using skuska_01.Services;
using System.Text.Json;

namespace skuska_01.Controllers
{
    public class HomeController : Controller
    {
        private readonly BankService _bankService = new();

        // ?? cesta k lok·lnemu JSON s˙boru
        private readonly string _filePath = "data.json";

        // ?? GET ñ naËÌtanie d·t z FILE + API
        public async Task<IActionResult> Index()
        {
            // 1?? naËÌtaj lok·lne uloûenÈ d·ta
            var localData = LoadFromFile();

            // 2?? naËÌtaj novÈ d·ta z API
            var apiData = await _bankService.GetTransactionsAsync();

            // 3?? pridaj API d·ta na KONIEC lok·lneho zoznamu
            localData.AddRange(apiData);

            // 4?? uloû sp‰ù do s˙boru
            SaveToFile(localData);

            return View(localData);
        }

        // ?? POST ñ pridanie d·t z formul·ra
        [HttpPost]
        public IActionResult AddTransaction(Transaction transaction)
        {
            var localData = LoadFromFile();

            transaction.id = localData.Count + 1;
            transaction.transactionDate = DateTime.Now;

            // pridanie NA KONIEC
            localData.Add(transaction);

            SaveToFile(localData);

            return RedirectToAction("Index");
        }

        // ?? ËÌtanie zo s˙boru
        private List<Transaction> LoadFromFile()
        {
            if (!System.IO.File.Exists(_filePath))
                return new List<Transaction>();

            var json = System.IO.File.ReadAllText(_filePath);

            return JsonSerializer.Deserialize<List<Transaction>>(json)
                   ?? new List<Transaction>();
        }

        // ?? z·pis do s˙boru
        private void SaveToFile(List<Transaction> data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            System.IO.File.WriteAllText(_filePath, json);
        }
    }
}
