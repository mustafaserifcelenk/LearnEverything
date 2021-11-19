using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Mapping
{
    public class ObjectMapper
    {
        // Lazy sınıfı mappinge istek yapılana kadar map sınıflarını oluşturmaz
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>();
            });
            return config.CreateMapper();
        });
        
        // bu Mapper sınıfı çağrıldığı an lazy sınıfının değerini dön demek
        public static IMapper Mapper => lazy.Value;

}
}
