using BeSpoked.Bikes.WebApp.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BeSpoked.Bikes.WebApp.DAL
{
    public class ProductApiRepo
    {
        private readonly HttpClient _httpClient;
        public ProductApiRepo()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7052"); // Set the base URL for the API
        }
        /// <summary>
        /// This Action will fetch the all product list form API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<List<T>> GetProductListAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);  // Make a GET request to the API
            response.EnsureSuccessStatusCode();  // Throws an exception if the status code is not successful

            var responseContent = await response.Content.ReadAsStringAsync();  // Read response as string

            // Deserialize the JSON response into the specified type (T)
            var result = JsonConvert.DeserializeObject<List<T>>(responseContent);

            return result;
        }

        /// <summary>
        /// This Action will help to post product info and will add to database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>

        public async Task<bool> PostProductAsync(CreateProduct product)
        {
            var jsonContent = JsonConvert.SerializeObject(product);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Product/AddProduct", content);

            // Check if the POST request was successful
            return response.IsSuccessStatusCode;
        }
        /// <summary>
        /// This action will help to fetch product based on unique ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductList> GetProductByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Product/{id}");  // API endpoint to fetch product by ID

            // If the response is successful, deserialize it to a Product object
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<ProductList>(responseContent);
                return product;
            }

            // If the API call fails, return null (or handle error appropriately)
            return null;
        }

        /// <summary>
        /// This action will help to update the product 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProductAsync(ProductList product)
        {
            var jsonContent = JsonConvert.SerializeObject(product);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Product/{product.ProductID}", content); // PUT request to update product

            return response.IsSuccessStatusCode;
        }


    }
}
