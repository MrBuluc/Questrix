using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questrix.Domain.Entities;

namespace Questrix.Persistence.Configurations
{
    public class SurveyOptionConfiguration : IEntityTypeConfiguration<SurveyOption>
    {
        public void Configure(EntityTypeBuilder<SurveyOption> builder)
        {
            builder.HasKey(so => so.Id);
            builder.Property(so => so.Id).ValueGeneratedOnAdd();

            builder.HasOne(so => so.Node)
                .WithMany(n => n.Options)
                .HasForeignKey(so => so.NodeId);

            // Common Fields
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate).IsRequired(false);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedDate).IsRequired(false);

            builder.ToTable("SurveyOptions");
        }
    }
}
