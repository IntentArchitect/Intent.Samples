using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddMongoDB("mongo").WithDataVolume();
var rabbit = builder.AddRabbitMQ("rabbit").WithManagementPlugin();

var identityapi = builder.AddProject<eShop_Identity_Api>("identityapi");

var basketapi = builder.AddProject<eShop_Basket_Api>("basketapi")
                 .WithReference(mongo)
                 .WithReference(rabbit)
                 .WithReference(identityapi);

var catalogapi = builder.AddProject<eShop_Catalog_Api>("catalogapi")
                 .WithReference(rabbit);


var orderingapi = builder.AddProject<eShop_Ordering_Api>("orderingapi")
                 .WithReference(rabbit)
                 .WithReference(identityapi);

var blazor = builder.AddProject<eShop_Web>("blazor")
                    .WithReference(basketapi)
                    .WithReference(catalogapi)
                    .WithReference(identityapi)
                    .WithReference(orderingapi);

builder.Build().Run();
