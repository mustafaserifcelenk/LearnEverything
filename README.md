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

<a href="https://www.w3schools.com/cs/index.php" rel="nofollow"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="csharp" width="40" height="40" style="max-width: 100%;"></a>
<a href="https://dotnet.microsoft.com/en-us/" rel="nofollow"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dot-net/dot-net-original-wordmark.svg" alt="dotnet" width="40" height="40" style="max-width: 100%;">
<a href="https://www.javascript.com/" rel="nofollow"> <img src="https://user-images.githubusercontent.com/25181517/117447155-6a868a00-af3d-11eb-9cfe-245df15c9f3f.png" alt="javascript" width="40" height="40" style="max-width: 100%;"></a>
<a href="https://openlayers.org/" rel="nofollow"> <img src="https://user-images.githubusercontent.com/72551126/211200225-2c0ef133-4f93-4479-9840-25479dd4e5cc.png" alt="openlayers" width="40" height="40" style="max-width: 100%;"></a>
<a href="https://www.docker.com/" rel="nofollow"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/docker/docker-original-wordmark.svg" alt="docker" width="40" height="40" style="max-width: 100%;"></a>
<a href="https://www.mongodb.com/" rel="nofollow"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/mongodb/mongodb-original-wordmark.svg" alt="mongodb" width="40" height="40" style="max-width: 100%;"></a>
<a href="https://www.postgresql.org/" rel="nofollow"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/postgresql/postgresql-original-wordmark.svg" alt="postgresql" width="40" height="40" style="max-width: 100%;">
<a href="https://www.rabbitmq.com/" rel="nofollow"> <img src="https://camo.githubusercontent.com/52efcb7f1ba0a82b322c4d1eb8d33ebe886627b405013ed2f1d1c3cf818abbeb/68747470733a2f2f7777772e766563746f726c6f676f2e7a6f6e652f6c6f676f732f7261626269746d712f7261626269746d712d69636f6e2e737667" alt="rabbitmq" width="40" height="40" style="max-width: 100%;"></a>
<a href="https://redis.io/" rel="nofollow"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/redis/redis-original-wordmark.svg" alt="redis" width="40" height="40" style="max-width: 100%;"></a>
