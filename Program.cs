using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("SQLAZURECONNSTR_AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<OrderRecordContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// HTTP methods for OrderRecord
// GET
app.MapGet("/OrderRecord", (OrderRecordContext context) =>
{
    return context.OrderRecord.ToList();
})
.WithName("GetOrderRecords")
.WithOpenApi();

// GET {id}
app.MapGet("/OrderRecord/{id}", (int id, OrderRecordContext context) =>
{
    var orderRecord = context.OrderRecord.Find(id);
    if (orderRecord == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(orderRecord);
})
.WithName("GetOrderRecordById")
.WithOpenApi();

// POST
app.MapPost("/OrderRecord", (OrderRecord orderRecord, OrderRecordContext context) =>
{
    context.OrderRecord.Add(orderRecord);
    context.SaveChanges();
    return Results.Created($"/orderrecords/{orderRecord.ID}", orderRecord);
})
.WithName("CreateOrderRecord")
.WithOpenApi();

// PUT
app.MapPut("/OrderRecord/{id}", (int id, OrderRecord inputOrderRecord, OrderRecordContext context) =>
{
    var orderRecord = context.OrderRecord.Find(id);
    if (orderRecord == null)
    {
        return Results.NotFound();
    }

    orderRecord.Username = inputOrderRecord.Username;
    orderRecord.DateTime = inputOrderRecord.DateTime;
    orderRecord.Status = inputOrderRecord.Status;

    context.SaveChanges();
    return Results.NoContent();
})
.WithName("UpdateOrderRecord")
.WithOpenApi();

// DELETE
app.MapDelete("/OrderRecord/{id}", (int id, OrderRecordContext context) =>
{
    var orderRecord = context.OrderRecord.Find(id);
    if (orderRecord == null)
    {
        return Results.NotFound();
    }

    context.OrderRecord.Remove(orderRecord);
    context.SaveChanges();
    return Results.Ok(orderRecord);
})
.WithName("DeleteOrderRecord")
.WithOpenApi();


app.Run();


public class OrderRecord
{
    public int ID { get; set; }
    public string Username { get; set; }
    public DateTime DateTime { get; set; }
    public string Status { get; set; }
}

public class OrderRecordContext : DbContext
{
    public OrderRecordContext(DbContextOptions<OrderRecordContext> options)
        : base(options)
    {
    }

    public DbSet<OrderRecord> OrderRecord { get; set; }
}