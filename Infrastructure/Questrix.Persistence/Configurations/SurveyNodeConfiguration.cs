using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questrix.Domain.Entities;

namespace Questrix.Persistence.Configurations
{
    public class SurveyNodeConfiguration : IEntityTypeConfiguration<SurveyNode>
    {
        public void Configure(EntityTypeBuilder<SurveyNode> builder)
        {
            builder.HasKey(sn => sn.Id);
            builder.Property(sn => sn.Id).ValueGeneratedOnAdd();

            builder.Property(sn => sn.Type).IsRequired();
            builder.Property(sn => sn.Metadata).HasColumnType("jsonb");

            builder.HasOne(sn => sn.Survey)
                .WithMany(s => s.Nodes)
                .HasForeignKey(sn => sn.SurveyId);

            // Common Fields
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate).IsRequired(false);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedDate).IsRequired(false);

            builder.ToTable("SurveyNodes");
        }
    }
}
