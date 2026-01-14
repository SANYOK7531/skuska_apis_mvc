using System.Text.Json;
using skuska_01.Models;

namespace skuska_01.Services
{
    public class BankService
    {
        private readonly HttpClient _httpClient = new();

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            var json = await _httpClient.GetStringAsync(
                "https://api.sampleapis.com/fakebank/accounts");

            return JsonSerializer.Deserialize<List<Transaction>>(json);
        }
    }
}
