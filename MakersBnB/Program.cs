var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(600);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ** AUTH FILTER CONFIG **
builder.Services.AddScoped<MakersBnB.ActionFilters.AuthenticationFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Mostra página de erro detalhada em ambiente de desenvolvimento
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Redireciona para página de erro padrão em ambiente de produção
    app.UseHsts(); // Aplica HSTS (HTTP Strict Transport Security)
}

app.UseHttpsRedirection(); // Redireciona todas as requisições HTTP para HTTPS (se aplicável)
app.UseStaticFiles(); // Permite o uso de arquivos estáticos como HTML, CSS, imagens, etc.

app.UseRouting();

app.UseAuthorization();

app.UseSession(); // Ativa o uso de sessões

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
