//namespace Uspa.Domain.ModelCodeFirst
//{
//    using System;
//    using System.Data.Entity;
//    using System.Data.Entity.ModelConfiguration;
//    using System.Linq;

//    public class UspaDbContextEntity : DbContext
//    {

//        public UspaDbContextEntity()
//            : base("name=UspaDbContextEntity")
//        {
//           Database.SetInitializer<UspaDbContextEntity>(new DropCreateDatabaseAlways<UspaDbContextEntity>());
//        }
//        public virtual DbSet<Banner> Banners { get; set; } //+
//        public virtual DbSet<Category> Categories { get; set; }
//        public virtual DbSet<Content> Contents { get; set; }
//        public virtual DbSet<Language> Languages { get; set; } //+
//        public virtual DbSet<Menu> Menu { get; set; } //+
//        public virtual DbSet<MenuType> MenuType { get; set; } //+
//        public virtual DbSet<Image> Images { get; set; } //+
//        public virtual DbSet<Album> Albums { get; set; } 
//        public virtual DbSet<Site> Sites { get; set; } //+ blank не все города
//        public virtual DbSet<User> Users { get; set; } //+ 
//        public virtual DbSet<UserGroup> UserGroups { get; set; } //+



//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Category>()
//                .HasMany(c => c.allowedUserGroup)
//                .WithMany(u => u.userGroupCategories)
//                .Map(m =>
//                {
//                    m.MapLeftKey("UserGroupId");
//                    m.MapRightKey("CategoryId");
//                    m.ToTable("CategoryToUserGroup");
//                });

//            modelBuilder.Entity<UserGroup>()
//               .HasMany(c => c.users)
//               .WithMany(u => u.userGroup)
//               .Map(m =>
//               {
//                   m.MapLeftKey("UserGroupId");
//                   m.MapRightKey("UserId");
//                   m.ToTable("UserToUserGroup");
//               });


//            //modelBuilder.Entity<Image>()
//            //      .HasRequired<Album>(s => s.album)
//            //      .WithMany(s => s.images)
//            //      .HasForeignKey(s => s.);
//        }
//        public class TreeConfiguration : EntityTypeConfiguration<Menu>
//        {
//            //public TreeConfiguration()
//            //{
//            //    HasOptional(x => x.Parent)
//            //        .WithMany(x => x.menu)
//            //        .Map(m => m.MapKey("PARENT_ID"));

//            //    HasMany(x => x.menu)
//            //        .WithOptional(x => x.Parent);

//                //HasMany(x => x.Ancestors)
//                //    .WithMany(x => x.Descendants)
//                //    .Map(m => m.ToTable("Tree_Hierarchy").MapLeftKey("PARENT_ID").MapRightKey("CHILD_ID"));

//                //HasMany(x => x.Descendants)
//                //    .WithMany(x => x.Ancestors)
//                //    .Map(m => m.ToTable("Tree_Hierarchy").MapLeftKey("CHILD_ID").MapRightKey("PARENT_ID"));
//            //}
//        }
//    }
//}
