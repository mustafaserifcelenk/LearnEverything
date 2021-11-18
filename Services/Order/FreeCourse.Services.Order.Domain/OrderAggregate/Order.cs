using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Domain hangi ORM'le çalıştığını bilmemeli.
namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    // EfCore'un DDD için özellikleri;
    // 1) field üzerinden bağlı sınıfların verilerini alabildik
    // 2) ShadowProperty yardımıyla ilişki için gerekli id'leri belirtmemize gerek kalmadı
    // 3) OwnedType ile kendine ait bir id'si olmayan tipleri ayrı bir sınıfta tanımlayabildik
    public class Order : Entity, IAggregateRoot
    {
        // Address propertisi default olarak Order tablosunun içinde sütunlar olarak bulunacak(owned) ancak ayrı bir tablo olarak tutulmasını istiyorsak onu da yapabiliyoruz.
        // prop : get/reset var 
        // field : get/reset yok
        public DateTime CreatedTime { get; private set; }
        public Address Address { get; private set; }
        public string BuyerId { get; private set; }

        // Ef Core da okuma ve yazma işlemini bir prop yerine field üzerinden gerçekleştiriyorsanız buna backingfield deniyor, encapsulation için yani eğer orderItem'a Order üzerinden bir data eklenecekse bunu ancak bizim sağladığımız metotlar üzerinden yapabilsin ki  kontrollerini sağlayabilelim. 
        private readonly List<OrderItem> _orderItems;

        // Burada orderItemları dış dünyaya açıyoruz, okuma işlemini buradan yapabiliyorlar. '_orderItems' ı Ef Core dolduruyor.
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedTime = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }

        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);
            if (existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(newOrderItem);
            } 
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);  
    }
}
