﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cinema.Base
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class cinemaEntities : DbContext
    {
        public cinemaEntities()
            : base("name=cinemaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Hall> Hall { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Seat> Seat { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
