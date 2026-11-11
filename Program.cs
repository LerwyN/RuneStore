using Microsoft.EntityFrameworkCore;
using Npgsql;
using RUNE.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var versionFile = Path.Combine(builder.Environment.ContentRootPath, "Version.txt");
var versionLine = File.ReadLines(versionFile)
    .FirstOrDefault(line => line.StartsWith("Version:", StringComparison.OrdinalIgnoreCase));
var versionNumber = versionLine?.Split(':', 2).Last().Trim();
if (string.IsNullOrWhiteSpace(versionNumber))
{
    throw new InvalidDataException("Version.txt must contain a 'Version:' line.");
}

var currentVersion = "v" + versionNumber;

var connectionString = builder.Configuration.GetConnectionString("DATABASE_URL");
if (string.IsNullOrWhiteSpace(connectionString))
{
    connectionString = builder.Configuration["DATABASE_URL"];
}

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "PostgreSQL connection string is missing. Set ConnectionStrings:DATABASE_URL or DATABASE_URL.");
}

connectionString = NormalizePostgresConnectionString(connectionString);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    const long startupLockId = 0x52554E4553544F52;
    var startupLockAcquired = false;

    await context.Database.OpenConnectionAsync();
    try
    {
        await context.Database.ExecuteSqlRawAsync("SELECT pg_advisory_lock({0})", startupLockId);
        startupLockAcquired = true;

        await context.Database.MigrateAsync();
        await GameSeeder.SeedAsync(context);
        await ReleaseNoteSeeder.SeedAsync(context, currentVersion);
    }
    finally
    {
        try
        {
            if (startupLockAcquired)
            {
                await context.Database.ExecuteSqlRawAsync("SELECT pg_advisory_unlock({0})", startupLockId);
            }
        }
        finally
        {
            await context.Database.CloseConnectionAsync();
        }
    }
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

static string NormalizePostgresConnectionString(string value)
{
    if (!Uri.TryCreate(value, UriKind.Absolute, out var databaseUri)
        || (databaseUri.Scheme != "postgres" && databaseUri.Scheme != "postgresql"))
    {
        return value;
    }

    var connectionBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = databaseUri.Host,
        Port = databaseUri.IsDefaultPort || databaseUri.Port < 0 ? 5432 : databaseUri.Port,
        Database = Uri.UnescapeDataString(databaseUri.AbsolutePath.TrimStart('/'))
    };

    var userInfo = databaseUri.UserInfo.Split(':', 2);
    if (userInfo.Length > 0 && !string.IsNullOrEmpty(userInfo[0]))
    {
        connectionBuilder.Username = Uri.UnescapeDataString(userInfo[0]);
    }

    if (userInfo.Length > 1)
    {
        connectionBuilder.Password = Uri.UnescapeDataString(userInfo[1]);
    }

    foreach (var parameter in databaseUri.Query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries))
    {
        var parts = parameter.Split('=', 2);
        var key = Uri.UnescapeDataString(parts[0].Replace('+', ' '));
        var parameterValue = parts.Length > 1
            ? Uri.UnescapeDataString(parts[1].Replace('+', ' '))
            : string.Empty;

        connectionBuilder[key] = parameterValue;
    }

    return connectionBuilder.ConnectionString;
}
