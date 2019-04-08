using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace WeddingPlanner.Models
{
    public class Guest
    {
        [Key]
        public int guestid {get;set;}

        public int userid {get;set;}
        [ForeignKey("userid")]
        public User User {get;set;}
        public int weddingid {get;set;}
        [ForeignKey("weddingid")]
        public Wedding Wedding {get;set;}
        
       
    }
}