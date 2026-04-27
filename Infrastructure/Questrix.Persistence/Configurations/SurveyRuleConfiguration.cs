using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questrix.Domain.Entities;

namespace Questrix.Persistence.Configurations
{
    public class SurveyRuleConfiguration : IEntityTypeConfiguration<SurveyRule>
    {
        public void Configure(EntityTypeBuilder<SurveyRule> builder)
        {
            builder.HasKey(sr => sr.Id);
            builder.Property(sr => sr.Id).ValueGeneratedOnAdd();

            builder.HasOne(sr => sr.Node)
                .WithMany(n => n.Rules)
                .HasForeignKey(sr => sr.NodeId);

            // Common Fields
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate).IsRequired(false);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeletedDate).IsRequired(false);

            builder.ToTable("SurveyRules");
        }
    }
}
