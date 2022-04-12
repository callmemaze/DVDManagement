using Microsoft.EntityFrameworkCore;

namespace DVDManagement.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ActorModel>? ActorModel { get; set; }
        public DbSet<CastMemberModel>? CastMemberModel { get; set; }
        public DbSet<DVDTitleModel>? DVDTitleModel { get; set; }
        public DbSet<DVDCopyModel>? DVDCopyModel { get; set; }
        public DbSet<StudioModel>? StudioModel { get; set; }
        public DbSet<ProducerModel>? ProducerModel { get; set; }
        public DbSet<DVDCategoryModel>? DVDCategoryModel { get; set; }
        public DbSet<LoanModel>? LoanModel { get; set; }
        public DbSet<LoanTypeModel>? LoanTypeModel { get; set; }
        public DbSet<MemberModel>? MemberModel { get; set; }
        public DbSet<MembershipCategoryModel>? MembershipCategoryModel { get; set; }
    

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
