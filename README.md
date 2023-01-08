# LearnEverything
This is a microservice project that clone of Udemy. Here is structure of project:

![image](https://user-images.githubusercontent.com/72551126/211204842-23cd0b0f-48d3-46d9-b93c-4a632735f78e.png)

Neler yapıyor?
Udemy tarzı bir projedir. Kullanıcı olarak; kurs ekleyebilir, silebilir, güncelleyebilir, sepete ekleyebilir, indirim uygulayabilir ve satın alabilirsiniz.

Hangi teknolojiler kullanıldı?
Servisler ve veritabanları Docker Container lar aracılığı ile ayağa kaldırılıyor. İşlemler IdentityServer Token teknolojisi ile haberleşmektedir.

Gateway
Client isteklerini ilgili servislere yönlendirir. Bu yönlendirmeler için Ocelot kullanılmıştır.

Asp.Net Core MVC Web
Microservice'lerden almış olduğu dataları kullanıcıya gösterecek ve kullanıcı ile etkileşime geçmekten sorumlu olacak UI mikroservisimiz

Course.Shared
Tüm projenin ortak işlemlerinin yürütülmesinden sorumludur.

IdentityServer
Yapılan tüm işlemler için gerekli olan Kimlik Doğrulamalarının yapıldığı proje. Burada EntityFrameworkCore ve SqlServer kullanılmıştır.

Basket Service
Sepet işlemlerinin yapıldığı projedir. Burada Redis ve kuyruk haberleşmesi için RabbitMQ kullanılmıştır.

Catalog Service
Kursların sahip olduğu katalog işlemlerinin yapıldığı projedir. Burada MongoDB ve kuyruk haberleşmesi için RabbitMQ kullanılmıştır.

Discount Service
Sepete uygulanan indirim işlemlerinin yapıldığı projedir. Burada Npgsql ve Dapper kullanılmıştır.

FakePayment Service
Sepet ödemesi işlemlerinin yapıldığı projedir. Gerçek bir işlem olmadığından isim olarak "Fake" tercih edildi. Sanki işlem yapılıyormuş gibi yazılmıştır. RabbitMQ kullanılarak başarılı olan ödemeler sipariş olarak kuyruğa eklenir.

Order Service
Domain driven design (DDD) ile yazılmıştır. Ödemesi başarılı olan işlemleri sipariş olarak eklemiştik. Burada CommandConsumer veya EventConsumer lar aracılığı ile işlemleri yapıyoruz. RabbitMQ kulanılmıştır. Burada EntityFrameworkCore ve SqlServer kullanılmıştır. Ayrıca bu db işlemleri için CQRS Pattern, MediatR Kütüphanesi kullanılmıştır.

PhotoStock Service
Kursa eklenen resim işlemlerini yürüten projedir. Bu proje özelinde herhangi bir sunucu veya veritabanı işlemi yapılmamıştır. Eklenen resimler local olarak tutulmuştur.

Message Broker
Mesaj kuyruk sistemi olarak RabbitMQ kullanıyor.
RabbitMQ ile haberleşmek için MassTransit kütüphanesini kullanıyor.
RabbitMQ (MassTransit Library)
