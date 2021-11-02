using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantClientApp.Model;

namespace RestaurantClientApp.Data
{
    public interface IOrdersService
    {
        Task<IList<Order>> GetOrdersAsync();
        Task CreateOrderAsync(Order order);
    }
}