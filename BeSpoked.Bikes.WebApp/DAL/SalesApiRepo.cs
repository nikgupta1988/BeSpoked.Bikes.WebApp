using BeSpoked.Bikes.WebApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace BeSpoked.Bikes.WebApp.DAL
{
    public class SalesApiRepo
    {
        private readonly HttpClient _httpClient;
        public SalesApiRepo()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7052"); // Set the base URL for the API
        }
        /// <summary>
        /// This API will help to fetch sales list 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>      
        public async Task<List<T>> GetsaleListAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);  // Make a GET request to the API
            response.EnsureSuccessStatusCode();  // Throws an exception if the status code is not successful

            var responseContent = await response.Content.ReadAsStringAsync();  // Read response as string

            // Deserialize the JSON response into the specified type (T)
            var result = JsonConvert.DeserializeObject<List<T>>(responseContent);

            return result;
        }

        /// <summary>
        /// This will create the sales in database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> CreteSaleAsync(CreatesaleModel product)
        {
            var jsonContent = JsonConvert.SerializeObject(product);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Sales/Createsale", content);

            // Check if the POST request was successful
            return response.IsSuccessStatusCode;
        }
    }
}
