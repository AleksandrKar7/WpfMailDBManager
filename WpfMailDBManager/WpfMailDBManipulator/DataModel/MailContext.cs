namespace WpfMailDBManager.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;
    using WpfMailDBManager;

    public class MailContext : DbContext
    {
        public MailContext(/*string connectionString*/)
            : base(/*"name=WpfMailDBManipulator.Properties.Settings.MailDBConnectionString"*/)
        {

            //string connectionString = null;
            //SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder()
            //{
            //    DataSource = @"COMP1\DB",
            //    InitialCatalog = "MailDB",
            //    MultipleActiveResultSets = true,
            //    UserID = "sa",
            //    Password = "Sasha12031999"
            //};
            //connectionString = scsb.ConnectionString;
            Database.Connection.ConnectionString = Connect.ConnectionString;
            //Database.Connection.ConnectionString = Program.ConnectionString;

        }

        //public void SetConnectionString(string connectionString)
        //{
        //    //this.connectionString = connectionString;
        //    //MailContext()
        //}

        public DbSet<Operation> Operations { get; set; }
        public DbSet<Mailing> Mailings { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Periodical> Periodicals { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Mail> Mail { get; set; }

    }
}