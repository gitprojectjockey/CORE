using OdeToFood.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        void Add(Restaurant restaurant);
        int Commit();
    }

    public class SqlRestaurantData : IRestaurantData
    {
        OdeToFoodDbContext _context;
        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }

        public void Add(Restaurant restaurant)
        {
            _context.Add(restaurant);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public Restaurant Get(int id) => _context.Restaurants.FirstOrDefault(r => r.Id == id);

        public IEnumerable<Restaurant> GetAll() => _context.Restaurants.ToList();
    }

    //public class RestaurantData : IRestaurantData
    //{
    //    List<Restaurant> _restaurants;

    //    public RestaurantData()
    //    {
    //        _restaurants = new List<Restaurant>()
    //        {
    //            new Restaurant(){Id=1,Name="Eric's Cafe" },
    //            new Restaurant(){Id=2,Name="Tonies" },
    //            new Restaurant(){Id=3,Name="Mikes Fish House" },
    //            new Restaurant(){Id=4,Name="Rick's Ocean Side" },
    //        };
    //    }

    //    public Restaurant Get(int id)
    //    {
    //        return _restaurants.FirstOrDefault(r => r.Id == id);
    //    }

    //    public IEnumerable<Restaurant> GetAll()
    //    {
    //        return _restaurants;
    //    }

    //    public void Add(Restaurant restaurant)
    //    {
    //        restaurant.Id = _restaurants.Max(r => r.Id) + 1;
    //        _restaurants.Add(restaurant);
    //    }

    //    public int Commit()
    //    {
    //        return 0;
    //    }
    //}
}
