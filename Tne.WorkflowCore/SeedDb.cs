using Sample.TransactionalOutbox.Persistence;

namespace Tne.WorkflowCore;

public static class SeedDb
{
    public static void Initialize(IApplicationBuilder app)
    {
        using var serviceScope = app
            .ApplicationServices.GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        var context =
            serviceScope.ServiceProvider.GetService<ShopDbContext>()
            ?? throw new NullReferenceException(
                $"Cannot find any service for {nameof(ShopDbContext)}"
            );

        context.Database.EnsureCreated();

        context.SomeEntities.Add(new DbContext.SomeEntity { Id = 1, Name = "First" });
        context.SomeEntities.Add(new DbContext.SomeEntity { Id = 2, Name = "Second" });

        context.SaveChanges();
    }
}
