﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCRestaurant27Tem2022.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RestaurantDBEntities : DbContext
    {
        public RestaurantDBEntities()
            : base("name=RestaurantDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<fdType> fdType { get; set; }
        public virtual DbSet<PriceUpdates> PriceUpdates { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<ROrder> ROrder { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Waiter> Waiter { get; set; }
        public virtual DbSet<FoodDrink> FoodDrink { get; set; }
        public virtual DbSet<RTable> RTable { get; set; }
        public virtual DbSet<Bill> Bill { get; set; }
    }
}
