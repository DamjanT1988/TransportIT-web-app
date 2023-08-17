using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KnowIT_TransportIT_webapp.Models
{
    public class BillingModel
    {
        //data fields

        //data fields
        [Display(Name = "Billing ID number:")]
        public int Id { get; set; }

        //user input and object properties
        [Display(Name = "Ticket information")]
        public string? Order { get; set; }

        [Display(Name = "Total ticket cost")]
        public double? TicketCost { get; set; }

        [Display(Name = "Customer email")]
        public string? Email { get; set; }

        [Display(Name = "Customer telephone number")]
        public string? Telephone { get; set; }

        [Display(Name = "Customer name")]
        public string? CustomerName { get; set; }

        [Display(Name = "Customer social number")]
        public int? CustomerSocNo { get; set; }
        
        [Display(Name = "Check in/out")]
        public bool? CheckTransport { get; set; }
                
        [Display(Name = "Status of ticket")]
        public bool? Status { get; set; }

        [Display(Name = "Internal notes")]
        public string? InternalNote { get; set; }

        [Display(Name = "Start Date:")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date:")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Start Time:")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? StartTime { get; set; }

        [Display(Name = "End Time:")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? EndTime { get; set; }

    }
}
