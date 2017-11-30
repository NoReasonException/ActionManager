using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiProject.DBClasses.DB_EFContext
{
    public class Context : DbContext
    {
        public Context() : base("LocalConnection") {
        }
        public DbSet<ApiProject.DBClasses.Activity> ActivityContainer { get; set; }
        public DbSet<ApiProject.DBClasses.Customer> CustomerContainer { get; set; }

    }
    
    public sealed class DatabaseContextSiglenton {
        //Access    |Prop  |Type                                    |Identifier     |Access_Level
        private     static Context                                  con;
        private     static Newtonsoft.Json.JsonSerializerSettings   jsonSettings;


        public      static Context                                  Context         { get { return con; } }
        public      static Newtonsoft.Json.JsonSerializerSettings   JsonSettings    { get { return jsonSettings; } }


        static DatabaseContextSiglenton()
        {
            con = new Context();
            jsonSettings = new Newtonsoft.Json.JsonSerializerSettings();
            jsonSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        }
    }
}