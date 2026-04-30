using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questrix.Domain.Entities;

namespace Questrix.Persistence.Configurations
{
    public class ExceptionLogConfiguration : IEntityTypeConfiguration<ExceptionLog>
    {
        public void Configure(EntityTypeBuilder<ExceptionLog> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Common Fields
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.Ignore(x => x.UpdatedDate);
            builder.Ignore(x => x.IsDeleted);
            builder.Ignore(x => x.DeletedDate);

            builder.ToTable("ExceptionLogs");
        }
    }
}
