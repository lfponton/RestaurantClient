using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RestaurantClientApp.Model;

namespace RestaurantClientApp.Data
{
    public class OrdersService : IOrdersService
    {
        private readonly HttpClient client;
        private JsonSerializerOptions options;

        public OrdersService()
        {
            client = new HttpClient();
            options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        
        public async Task<IList<Order>> GetOrdersAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"http://localhost:8080/orders");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            string result = await response.Content.ReadAsStringAsync();
            IList<Order> orders = JsonSerializer.Deserialize<List<Order>>(result, options);
            return orders;
        }

        public async Task CreateOrderAsync(Order order)
        {
            Order newOrder = new()
            {
                Description = order.Description,
                Customer = order.Customer,
                Menu = order.Menu,
                Price = order.Price,
                Status = order.Status
            };
            string orderAsJson = JsonSerializer.Serialize(newOrder, options);
            HttpContent  content = new StringContent(
                orderAsJson,
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await client.PostAsync("http://localhost:8080/orders", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }
    }
}