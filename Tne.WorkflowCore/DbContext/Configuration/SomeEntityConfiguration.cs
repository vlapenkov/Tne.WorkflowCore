using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tne.WorkflowCore.DbContext;

namespace Tne.WorkflowCore.DbContext.Configuration;

internal class SomeEntityConfiguration : IEntityTypeConfiguration<SomeEntity>
{
    public void Configure(EntityTypeBuilder<SomeEntity> builder)
    {
        builder.HasKey(t => t.Id);
    }
}
