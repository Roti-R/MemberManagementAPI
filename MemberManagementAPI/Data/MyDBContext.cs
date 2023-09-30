using Microsoft.EntityFrameworkCore;

namespace MemberManagementAPI.Data
{
    public class MyDBContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationManager> OrganizationManagers { get; set; }
        public MyDBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                        .HasOne(s => s.Member)
                        .WithOne(s => s.Account)
                        .HasForeignKey<Account>(s => s.MemberID);

            modelBuilder.Entity<OrganizationManager>()
                        .HasKey(s => new { s.ManagerID, s.OrgID });
            modelBuilder.Entity<OrganizationManager>()
                        .HasOne(s => s.Manager)
                        .WithMany(s => s.OrganizationManagers)
                        .HasForeignKey(s => s.ManagerID);
            modelBuilder.Entity<OrganizationManager>()
                        .HasOne(s => s.Organization)
                        .WithMany(s => s.OrganizationManagers)
                        .HasForeignKey(s => s.OrgID);




            modelBuilder.Entity<Organization>()
                        .HasOne(s => s.ParentOrganization)
                        .WithMany(s => s.ChildOrganizations)
                        .HasForeignKey(s => s.ParentID)
                        .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Organization>()
                        .HasMany(s => s.Members)
                        .WithOne(s => s.Organization)
                        .HasForeignKey(s => s.CurrentOrganizationID)
                        .OnDelete(DeleteBehavior.Restrict);

        }


    }
}
