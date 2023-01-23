using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FancyLibTests;

public sealed class ConnectionOpeningInterceptor : DbConnectionInterceptor
{
    private readonly Action onOpening;

    public ConnectionOpeningInterceptor(Action onOpening) => this.onOpening = onOpening;

    public override InterceptionResult ConnectionOpening(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
    {
        onOpening();
        return base.ConnectionOpening(connection, eventData, result);
    }

    public override async ValueTask<InterceptionResult> ConnectionOpeningAsync(DbConnection connection, ConnectionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = new())
    {
        onOpening();
        return await base.ConnectionOpeningAsync(connection, eventData, result, cancellationToken);
    }
}