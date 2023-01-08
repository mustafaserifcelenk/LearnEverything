# LearnEverything
This is a microservice project that clone of Udemy. Here is structure of project:

![image](https://user-images.githubusercontent.com/72551126/211204842-23cd0b0f-48d3-46d9-b93c-4a632735f78e.png)

## Project Contents

Services and databases are launched via Docker and comminicate via IdentityServer Token tool.

<ul>
<li>
Asp.Net Web
</li>
Comunicate with services and show UI to users.

<li>
Gateway Service
</li>
Direct client requests to related services via Ocelot.

<li>
IdentityServer
  </li>
Authentiacation process is running in it via SQL server and EF Core.

<li>
Basket Service
  </li>
Basket functions via RabbitMQ and Redis.

<li>
Catalog Service
  </li>
Catalog functions via RabbitMQ and MongoDB.

<li>
Discount Service
  </li>
Discount functions via Dapper and Npgsql.

<li>
FakePayment Service
  </li>
Payment functions via RabbitMQ.

<li>
Order Service
  </li>
Order functions via DDD, CQRS pattern and MediatR.

<li>
PhotoStock Service
  </li>
Photo upload functions to local.

<li>
Shared
  </li>
Shared functions.
  
</ul>

