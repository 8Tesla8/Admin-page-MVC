using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Uspa.Domain.ModelCodeFirst.IdentityCodeFirst
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public int prevId { get; set; }
        public int name { get; set; }
        public System.DateTime? registerDate { get; set; }
        public System.DateTime? lastVisitDate { get; set; }
        public bool block { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    //В базе называется IdentityRole
    public class Role : IdentityRole
    {
        public virtual ICollection<Category> roleCategories { get; set; }
        //public virtual ICollection<User> users { get; set; }

        public Role() : base() { }
        public Role(string name) : base(name) { }
        //public string Description { get; set; }
    }



    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseAlways<ApplicationDbContext>());
        }

        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuType> MenuType { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });


            //modelBuilder.Entity<Category>()
            //    .HasMany(c => c.allowedRole)
            //    .WithMany(u => u.roleCategories)
            //    .Map(m =>
            //    {
            //        m.MapLeftKey("RoleId");
            //        m.MapRightKey("CategoryId");
            //        m.ToTable("CategoryToRole");
            //    });
        }
    }
}
