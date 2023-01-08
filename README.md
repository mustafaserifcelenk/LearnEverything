# LearnEverything
This is a microservice project that clone of Udemy. Here is structure of project:

![image](https://user-images.githubusercontent.com/72551126/211204842-23cd0b0f-48d3-46d9-b93c-4a632735f78e.png)

## Project Contents

Services and databases are launched via Docker and comminicate via IdentityServer Token tool.
<ul>
<li>
### Asp.Net Web
</li>
Comunicate with services and show UI to users.

Gateway Service
Direct client requests to related services via Ocelot.

IdentityServer
Authentiacation process is running in it via SQL server and EF Core.

Basket Service
Basket functions via RabbitMQ and Redis.

Catalog Service
Catalog functions via RabbitMQ and MongoDB.

Discount Service
Discount functions via Dapper and Npgsql.

FakePayment Service
Payment functions via RabbitMQ.

Order Service
Domain driven design (DDD) ile yazılmıştır. Ödemesi başarılı olan işlemleri sipariş olarak eklemiştik. Burada CommandConsumer veya EventConsumer lar aracılığı ile işlemleri yapıyoruz. RabbitMQ kulanılmıştır. Burada EntityFrameworkCore ve SqlServer kullanılmıştır. Ayrıca bu db işlemleri için CQRS Pattern, MediatR Kütüphanesi kullanılmıştır.

PhotoStock Service
Kursa eklenen resim işlemlerini yürüten projedir. Bu proje özelinde herhangi bir sunucu veya veritabanı işlemi yapılmamıştır. Eklenen resimler local olarak tutulmuştur.

Course.Shared
Tüm projenin ortak işlemlerinin yürütülmesinden sorumludur.


Message Broker
Mesaj kuyruk sistemi olarak RabbitMQ kullanıyor.
RabbitMQ ile haberleşmek için MassTransit kütüphanesini kullanıyor.
RabbitMQ (MassTransit Library)
</ul>

