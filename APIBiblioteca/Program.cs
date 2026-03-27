using APIBiblioteca.Data;
using APIBiblioteca.Mapping;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryDbContext>(options =>
	options.UseSqlite(
		builder.Configuration.GetConnectionString("LibraryConnection") ?? "Data Source=library.db"));

builder.Services.AddSingleton(provider =>
{
	var configuration = new MapperConfiguration(cfg => cfg.AddProfile<LibraryProfile>(), provider.GetRequiredService<ILoggerFactory>());
	return configuration;
});

builder.Services.AddSingleton<IMapper>(provider =>
	provider.GetRequiredService<MapperConfiguration>().CreateMapper());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
	dbContext.Database.EnsureCreated();
	await DbInitializer.SeedAsync(dbContext);
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
