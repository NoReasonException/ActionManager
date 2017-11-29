using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiProject.DBClasses.DB_EFContext
{
    public class Context:DbContext
    {
        public Context():base("LocalConnection"){
        }
        public DbSet<ApiProject.DBClasses.Activity> ActivityContainer { get; set; }
        public DbSet<ApiProject.DBClasses.Customer> CustomerContainer { get; set; }

    }
}