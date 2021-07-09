using System.Threading;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Features.AddTestData
{
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
            await AddPersons(cancellationToken);
            await AddProducts(cancellationToken);

            return true;
        }

        private async Task AddPersons(CancellationToken stoppingToken)
        {
            Person[] persons =
            {
                new()
                {
                    FirstName = "Dennis", LastName = "Schreur"
                },
                new()
                {
                    FirstName = "Tess", LastName = "Schreur"
                },
                new()
                {
                    FirstName = "Daan", LastName = "Schreur"
                }
            };

            _personCommand.Add(persons);
            int addedRecords = await _personCommand.SaveChanges(stoppingToken);

            _logger.LogInformation("Added {number} persons.", addedRecords);
        }

        private async Task AddProducts(CancellationToken stoppingToken)
        {
            Product[] products =
            {
                new()
                {
                    Name = "Appel"
                },
                new()
                {
                    Name = "Banaan"
                },
                new()
                {
                    Name = "Peer"
                },
                new()
                {
                    Name = "Sinasappel"
                }
            };

            _productCommand.Add(products);
            int addedRecords = await _productCommand.SaveChanges(stoppingToken);

            _logger.LogInformation("Added {number} products.", addedRecords);
        }

        private Task ClearData(CancellationToken cancellationToken)
        {
            string sql = @"
DELETE FROM dbo.OrderLine
DBCC CHECKIDENT ('dbo.Orderline',RESEED, 0)

DELETE FROM [dbo].[Order]
DBCC CHECKIDENT ('dbo.Order',RESEED, 0)

DELETE FROM dbo.Person
DBCC CHECKIDENT ('dbo.Person',RESEED, 0)

DELETE FROM dbo.Product
DBCC CHECKIDENT ('dbo.Product',RESEED, 0)
";

            return _dataContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);
        }
    }
}
