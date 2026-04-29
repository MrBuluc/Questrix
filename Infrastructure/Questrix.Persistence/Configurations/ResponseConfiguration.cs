 using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questrix.Domain.Entities;

namespace Questrix.Persistence.Configurations
{
    public class ResponseConfiguration : IEntityTypeConfiguration<Response>
    {
        public void Configure(EntityTypeBuilder<Response> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();

            builder.Property(r => r.Answer).HasColumnType("jsonb");

            builder.HasOne(r => r.Session)
                .WithMany(s => s.Responses)
                .HasForeignKey(r => r.SessionId);

            // Common Fields
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate).IsRequired(false);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedDate).IsRequired(false);

            builder.ToTable("Responses");
        }
    }
}
