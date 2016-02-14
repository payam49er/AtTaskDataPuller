using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AtTaskDataModel
    {
    public class AtTaskContext :DbContext
        {
        public AtTaskContext() : base()
            {
            var cs = ConfigurationManager.ConnectionStrings["AtTaskConnectionString"].ConnectionString;
            this.Database.Connection.ConnectionString = cs;
            }

        public DbSet<AtTaskModel> AtTaskModels { get; set; }

        protected override void OnModelCreating( DbModelBuilder modelBuilder )
            {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            }
        }
    }
