using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CabernetDBContext;

namespace EmmploymeNet.Model 
{
    public partial class Entities : DbEntities
    {

        public Entities() : base()
        {

        }

        public Entities(DbContextOptions<DbEntities> options) : base(options)
        {
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {

            this.CreateModel(modelBuilder);
            
            /*
            modelBuilder.Entity<Docket>(entity =>
            {
                entity.Property(e => e.DocketNumber)
                    .HasComment("Código")
                    .ValueGeneratedOnAddOrUpdate();
            });
            */
        }

    }

}