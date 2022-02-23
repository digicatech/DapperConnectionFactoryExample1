// See https://aka.ms/new-console-template for more information

//setup our DI
using DapperConnectionFactoryExample1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dapper;
using Newtonsoft.Json;

var serviceProvider = new ServiceCollection()
    .AddLogging(opt =>
    {
        opt.AddConsole();
    })
    //.AddSingleton<IFooService, FooService>()
    .AddSingleton<DapperBaseRepository>()
    .BuildServiceProvider();



var logger = serviceProvider.GetService<ILoggerFactory>()
    .CreateLogger<Program>();


logger.LogInformation("Starting application");


Console.WriteLine("Running Application");


/*using (var connection = new ConnectionFactory("Host=localhost;Database=postgres;Username=postgres;Password=123456").GetConnection(DataAccessProviderTypes.PostgreSql))
{
    //connection.Open();
    connection.BeginTransaction();
    var result = connection.Query<User>("select *from payment.users");

    if (result != null)
    {
        var strResult = JsonConvert.SerializeObject(result);
        Console.WriteLine(strResult);
    }

    //connection.Close();
}*/


var dapperBaseRepository = serviceProvider.GetService<DapperBaseRepository>();

dapperBaseRepository.ConenctionString = "Host=localhost;Database=postgres;Username=postgres;Password=123456";
dapperBaseRepository.dataAccessProviderTypes = DataAccessProviderTypes.PostgreSql;
var result = dapperBaseRepository.Query<User>("select *from payment.users");

if (result != null)
{
    var strResult = JsonConvert.SerializeObject(result);
    Console.WriteLine(strResult);
}