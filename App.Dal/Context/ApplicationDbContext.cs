using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using App.Dal.Model;

namespace App.Dal.Context
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
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
        public ApplicationDbContext() : base("Website")
        {
        }
        
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryCnotent> CategoryCnotents { get; set; }
        public virtual DbSet<LogosTechnology> LogosTechnology { get; set; }
        public virtual DbSet<CompanyWord> CompanyWord { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<AboutUs> AboutUs { get; set; }
        public virtual DbSet<Features> Features { get; set; }
        public virtual DbSet<SubFeatures> SubFeatures { get; set; }
        public virtual DbSet<Portfolio> Portfolio { get; set; }
        public virtual DbSet<PortfolioType> PortfolioType { get; set; }
        public virtual DbSet<SubPortfolios> SubPortfolios { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<SubServices> SubServices { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TeamMembers> TeamMembers { get; set; }
        public virtual DbSet<UserSubscribed> UserSubscribed { get; set; }
        public virtual DbSet<Users> _Users { get; set; }
    }
}
