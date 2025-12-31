using System.Reflection.PortableExecutable;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();




//app.MapGet("/", () => "Hello World!");
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #1 Before calling Next");
    await next(context);
    await context.Response.WriteAsync("Middleware #! After calling Next");

    await context.Response.WriteAsync("Middleware #2 Before calling Next");
    //await next(context);
    await context.Response.WriteAsync("Middleware #2 After calling Next");

   

    await context.Response.WriteAsync("Middleware #3 Before calling Next");
    await next(context);
    await context.Response.WriteAsync("Middleware #3 After calling Next");
});

app.Map("/employee", (appBuilder) =>
    {
        appBuilder.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Middleware #5 Before calling Next");
            await next(context);
            await next(context);
            await context.Response.WriteAsync("Middleware #5 After calling Next");
        });

        appBuilder.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Middleware #6 Before calling Next");
            await next(context);
            await next(context);
            await context.Response.WriteAsync("Middleware #6 After calling Next");
        });
    }
    );

app.Run();
