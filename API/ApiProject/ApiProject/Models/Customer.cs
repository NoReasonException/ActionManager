using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ApiProject.Utills;
namespace ApiProject.DBClasses
{

    /// <summary>
    /// Describes the <<Customers>> Entity 
    /// Uses Attributes To inject into EF6
    /// </summary>
    public class Customer
    {
        //Attr/                     |Mofifier       |Type                               |Name        |Access Level         
        [Required][Key]             public          System.Int32                        CustomerID  { get; set; }           //Primary key , int , NOT NULL(Of course?)
        [Required][MaxLength(30)]   public          System.String                       Name        { get; set; }          //Name , NOT NULL
        [Required][MaxLength(70)]   public          System.String                       Address     { get; set;}           //Address , NOT NULL
                                    public          List<Activity>                      Activities  { get; set; }
        public Customer(System.Int32 CustomerID,System.String Name,System.String Address)
        {
            this.CustomerID = CustomerID;
            this.Name = Name;
            this.Address = Address;
            this.Activities = new System.Collections.Generic.List<Activity>();
        }
        public Customer() { }
        public override string ToString()
        {
            return System.String.Format("Customer ID:{0} Name:{1} , Addr:{2} ", Utills.Utills.IFNULL(this.CustomerID), 
                Utills.Utills.IFNULL(this.Name), Utills.Utills.IFNULL(this.Address));
        }
        
    }
}