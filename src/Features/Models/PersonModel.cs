using System;

namespace Features.Models;

public class PersonModel : IResponseModel
{
    public string FirstName { get; init; }

    public int Id { get; init; }

    public string LastName { get; init; }

#pragma warning disable CA1305
    public DateTimeOffset Date { get; init; } = DateTimeOffset.Parse("1983-10-03T23:00:00Z");

    public double Number { get; init; } = 1.04;
}
