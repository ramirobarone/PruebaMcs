using Azure.Identity;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string clientId = "25aa5cfe-8ce5-425a-92fa-1780e3a411fc";
string clientSecret = "Lwc8Q~59UuQTm.3kjhJCDaLgm~MNieWDY4ujtc0W";
string tenantId = "84d11978-3c07-4fcb-867d-6a2e0b94887f";

var keyVaultName = "masassessmentvault";

ClientSecretCredential clientSecretCredential = new(tenantId, clientId, clientSecret);
builder.Configuration.AddAzureKeyVault(new System.Uri($"https://{keyVaultName}.vault.azure.net/"), clientSecretCredential);

var keyVaultSection = builder.Configuration.GetSection("JwtMas");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
