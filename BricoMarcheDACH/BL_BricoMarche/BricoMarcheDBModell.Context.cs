﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BL_BricoMarche
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BricoMarcheDBEntities : DbContext
    {
        public BricoMarcheDBEntities()
            : base("name=BricoMarcheDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Artikel> AlleArtikel { get; set; }
        public DbSet<Benutzer> AlleBenutzer { get; set; }
        public DbSet<Gruppe> AlleGruppen { get; set; }
        public DbSet<Kategorie> AlleKategorien { get; set; }
        public DbSet<Land> AlleLaender { get; set; }
        public DbSet<Ort> AlleOrt { get; set; }
        public DbSet<Schlagwort> AlleSchlagwoerter { get; set; }
        public DbSet<Video> AlleVideos { get; set; }
    }
}
