namespace Brandaris.Features.Models;

public record PersonModel(int Id, string FirstName, string LastName, int? ParentId = null) : IResponseModel;