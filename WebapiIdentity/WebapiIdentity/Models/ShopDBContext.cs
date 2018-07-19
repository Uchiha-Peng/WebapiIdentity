using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebapiIdentity.Models
{
    public class ShopDBContext : DbContext
    {
        public ShopDBContext() : base(@"SQLiteCoon")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions
                .Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Orderdetail> Orderdetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Shopcart> Shopcart { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<User> User { get; set; }
    }
}