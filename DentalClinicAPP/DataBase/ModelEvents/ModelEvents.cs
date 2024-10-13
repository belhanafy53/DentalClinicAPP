using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DentalClinicAPP.DataBase.ModelEvents
{
    public partial class ModelEvents : DbContext
    {
        public ModelEvents()
            : base("name=ModelEvents")
        {
        }

        public virtual DbSet<SecurityEvent> SecurityEvents { get; set; }
        public virtual DbSet<SecurityUserActivity> SecurityUserActivities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
