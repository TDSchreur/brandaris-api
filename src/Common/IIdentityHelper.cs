using System;

namespace Brandaris.Common;

public interface IIdentityHelper
{
    string GetName();

    Guid GetOid();
}