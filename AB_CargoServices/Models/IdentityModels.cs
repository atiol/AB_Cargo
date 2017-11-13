using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AB_CargoServices.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // Here we add a byte to Save the user Profile Picture  
        public byte[] UserPhoto { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("AB_CargoDB", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // MUST go first.

            modelBuilder.HasDefaultSchema("ATIOL"); // Use uppercase!

            modelBuilder.Entity<ApplicationUser>().ToTable("ATIOL.Users");
            modelBuilder.Entity<IdentityRole>().ToTable("ATIOL.Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("ATIOL.UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("ATIOL.UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("ATIOL.UserLogins");
        }

        public System.Data.Entity.DbSet<AB_CargoServices.Models.SENDER> SENDERs { get; set; }
    }
}