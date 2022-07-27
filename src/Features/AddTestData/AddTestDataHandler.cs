using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data;
using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Brandaris.Features.AddTestData;

public class AddTestDataHandler : IRequestHandler<AddTestDataQuery, bool>
{
    private readonly DataContext _dataContext;
    private readonly ILogger<AddTestDataHandler> _logger;
    private readonly ICommand<Person> _personCommand;
    private readonly ICommand<Product> _productCommand;

    public AddTestDataHandler(
        DataContext dataContext,
        ICommand<Person> personCommand,
        ICommand<Product> productCommand,
        ILogger<AddTestDataHandler> logger)
    {
        _dataContext = dataContext;
        _personCommand = personCommand;
        _productCommand = productCommand;
        _logger = logger;
    }

    public async Task<bool> Handle(AddTestDataQuery request, CancellationToken cancellationToken)
    {
        await ClearData(cancellationToken);
        await _dataContext.Database.MigrateAsync(cancellationToken);

        //// await AddPersons(cancellationToken);
        //// await AddProducts(cancellationToken);

        return true;
    }

    private async Task AddPersons(CancellationToken stoppingToken)
    {
        Person[] persons =
        {
            new() { FirstName = "Dennis", LastName = "Schreur" }, new() { FirstName = "Tess", LastName = "Schreur" }, new() { FirstName = "Daan", LastName = "Schreur" }
        };

        _personCommand.Add(persons);
        int addedRecords = await _personCommand.SaveChangesAsync(stoppingToken);

        _logger.LogInformation("Added {Number} persons.", addedRecords);
    }

    private async Task AddProducts(CancellationToken stoppingToken)
    {
        Product[] products = { new() { Name = "Appel" }, new() { Name = "Banaan" }, new() { Name = "Peer" }, new() { Name = "Sinasappel" } };

        _productCommand.Add(products);
        int addedRecords = await _productCommand.SaveChangesAsync(stoppingToken);

        _logger.LogInformation("Added {Number} products.", addedRecords);
    }

    private Task ClearData(CancellationToken cancellationToken)
    {
        string sql = @"
IF  EXISTS (SELECT *
FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[Orderline]') AND type in (N'U'))
BEGIN
    ALTER TABLE [dbo].[Orderline] SET ( SYSTEM_VERSIONING = OFF)
    DROP TABLE [dbo].[Orderline]
END

IF  EXISTS (SELECT *
FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[OrderLineHistory]') AND type in (N'U'))
    DROP TABLE [dbo].[OrderLineHistory]

IF  EXISTS (SELECT *
FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[Order]') AND type in (N'U'))
BEGIN
    ALTER TABLE [dbo].[Order] SET ( SYSTEM_VERSIONING = OFF)
    DROP TABLE [dbo].[Order]
END

IF  EXISTS (SELECT *
FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[OrderHistory]') AND type in (N'U'))
DROP TABLE [dbo].[OrderHistory]

IF  EXISTS (SELECT *
FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
BEGIN
    ALTER TABLE [dbo].[Product] SET ( SYSTEM_VERSIONING = OFF)
    DROP TABLE [dbo].[Product]
END

IF  EXISTS (SELECT *
FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[ProductHistory]') AND type in (N'U'))
DROP TABLE [dbo].[ProductHistory]

IF  EXISTS (SELECT *
FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[PersonPreCheck]') AND type in (N'U'))
BEGIN
    ALTER TABLE [dbo].[PersonPreCheck] SET ( SYSTEM_VERSIONING = OFF)
    DROP TABLE [dbo].[PersonPreCheck]
END
IF  EXISTS (SELECT *
FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[PersonPreCheckHistory]') AND type in (N'U'))
DROP TABLE [dbo].[PersonPreCheckHistory]

IF  EXISTS (SELECT *
FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND type in (N'U'))
BEGIN
    ALTER TABLE [dbo].[Person] SET ( SYSTEM_VERSIONING = OFF)
    DROP TABLE [dbo].[Person]
END

IF  EXISTS (SELECT *
FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[PersonHistory]') AND type in (N'U'))
DROP TABLE [dbo].[PersonHistory]

IF  EXISTS (SELECT *
FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type in (N'U'))
TRUNCATE TABLE [dbo].__EFMigrationsHistory
";

        return _dataContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);
    }
}