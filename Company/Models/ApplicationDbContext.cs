using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Company.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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
            : base("CetmConnection", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("UserProfiles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }

        public DbSet<Areas.Manage.Models.Menu> Menu { get; set; }
        public DbSet<Areas.Manage.Models.MenuLanguage> MenuLanguage { get; set; }
        public DbSet<Areas.Manage.Models.Category> Category { get; set; }
        public DbSet<Areas.Manage.Models.CategoryLanguage> CategoryLanguage { get; set; }
        public DbSet<Areas.Manage.Models.Article> Article { get; set; }
        public DbSet<Areas.Manage.Models.ArticleLanguage> ArticleLanguage { get; set; }
        public DbSet<Areas.Manage.Models.Client> Client { get; set; }
        public DbSet<Areas.Manage.Models.ClientLanguage> ClientLanguage { get; set; }
        public DbSet<Areas.Manage.Models.Option> Option { get; set; }
        public DbSet<Areas.Manage.Models.OptionLanguage> OptionLanguage { get; set; }
        public DbSet<Areas.Manage.Models.Language> Language { get; set; }
        public DbSet<Areas.Manage.Models.LanguageLanguage> LanguageLanguage { get; set; }
        public DbSet<Areas.Manage.Models.Slider> Slider { get; set; }
        public DbSet<Areas.Manage.Models.SliderLanguage> SliderLanguage { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Areas.Manage.Models.SProduct> SProduct { get; set; }
        public DbSet<Areas.Manage.Models.SProductLanguage> SProductLanguage { get; set; }
        public DbSet<Areas.Manage.Models.Product> Product { get; set; }
        public DbSet<Areas.Manage.Models.ProductLanguage> ProductLanguage { get; set; }
        public DbSet<Areas.Manage.Models.ProductImage> ProductImage { get; set; }

    }
}