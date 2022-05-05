
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DVDManagement.Models;
namespace DVDManagement.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Seed();
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }
        
        public DbSet<ActorModel>? ActorModel { get; set; }
        public DbSet<CastMemberModel>? CastMemberModel { get; set; }
        public DbSet<DVDTitleModel>? DVDTitleModel { get; set; }
        public DbSet<DVDCopyModel>? DVDCopyModel { get; set; }
        public DbSet<StudioModel>? StudioModel { get; set; }
        public DbSet<ProducerModel>? ProducerModel { get; set; }
        public DbSet<DVDCategoryModel>? DVDCategoryModel { get; set; }
        public DbSet<DVDManagement.Models.LoanModel>? LoanModel { get; set; }
        public DbSet<LoanTypeModel>? LoanTypeModel { get; set; }
        public DbSet<MemberModel>? MemberModel { get; set; }
        public DbSet<MembershipCategoryModel>? MembershipCategoryModel { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<User> User { get; set; }
    }
}
