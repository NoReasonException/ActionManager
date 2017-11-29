using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace ApiProject.DBClasses
{
    public class Activity
    {


        /// <summary>
        /// Describes the entity <<Activity>>
        /// Uses DataAnnons for combabillity with EF6
        /// </summary>
        //Attr/             |Mod    |Type           |Name       |Access Level         

        [Required][Key]     public System.Int32     ActivityID  { get; set; }                           //PRIMARY KEY NOT NULL 
        [Required]          public Customer         Customer    { get; set; }                      //Foreign Key NOT NULL
        [Required]          public System.String    Description { get; set; }                      //Description NVACHAR(MAX)
        [Required]          public System.DateTime  StartDate   { get; set; }                      //not null of course
        [Required]          public System.DateTime  EndDate     { get; set; }                      //same as above
        //Note , Set ActivityID to zero , is Auto Increment PK ! 
        public Activity(System.Int32 ActivityID,Customer Customer,System.String Description,System.DateTime StartDate,System.DateTime EndDate)
        {
            this.ActivityID     = ActivityID;
            this.Customer     = Customer;
            this.Description    = Description;
            this.StartDate      = StartDate;
            this.EndDate        = EndDate;
        }
        public override string ToString()
        {
            return System.String.Format("Activity ID:{0} Description:{1}  StartDate:{2} EndDate{3} ", Utills.Utills.IFNULL(this.ActivityID),
                Utills.Utills.IFNULL(this.Description), Utills.Utills.IFNULL(this.StartDate),Utills.Utills.IFNULL(this.EndDate));
        }
        public Activity() { }
    }
}