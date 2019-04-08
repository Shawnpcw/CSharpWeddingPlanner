using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int weddingid {get;set;}
        [DateInTheFuture]
        public DateTime date {get;set;}
        public string address {get;set;}
        public int brideid {get;set;}

        [ForeignKey("brideid")]
        public User bride {get;set;}
        public int groomid {get;set;}

        [ForeignKey("groomid")]
        public User groom {get;set;}

        public List<Guest> Guests {get;set;}
                        
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DateInTheFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var futureDate = value as DateTime?;
            var memberNames = new List<string>() { context.MemberName };

            if (futureDate != null)
            {
                if (futureDate.Value.Date < DateTime.UtcNow.Date)
                {
                    return new ValidationResult("This must be a date in the future", memberNames);
                }
            }

            return ValidationResult.Success;
        }
    }
}