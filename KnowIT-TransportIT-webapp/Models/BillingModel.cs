// Required namespaces for the model attributes and other functionalities.
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

// Defined namespace for the web application's models.
namespace KnowIT_TransportIT_webapp.Models
{
    // BillingModel class which represents the data model for billing in the application.
    public class BillingModel
    {
        // These are the properties (or data fields) of the BillingModel.

        // Represents the unique identifier for each billing entry.
        [Display(Name = "Billing ID no.")]
        public int Id { get; set; }

        // The ticket information, provided by the user. It's a nullable string property.
        [Required]
        [Display(Name = "Ticket information")]
        public string? Order { get; set; }

        // Represents the cost of the ticket. It's a nullable double property.
        //[Required]
        [Display(Name = "Ticket cost")]
        public double? TicketCost { get; set; }

        // Represents the unique identifier for each passenger.
        [Required]
        [Display(Name = "Passanger ID no.")]
        public int? PassangerNo { get; set; }

        // Boolean property indicating the check-in or check-out status.
        // True if checked-in, false if checked-out, and null if not boarded.
        [Display(Name = "Check in/out")]
        public bool? CheckTransport { get; set; }

        // Indicates the status of the ticket. 
        // True for active, false for inactive, and null if the status is unknown.
        [Display(Name = "Status of ticket")]
        public bool? Status { get; set; }

        // Represents the starting date for the validity of the ticket.
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        // Represents the ending date for the validity of the ticket.
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        // Indicates the start time for a particular service or journey.
        [Display(Name = "Start Time:")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? StartTime { get; set; }

        // Indicates the end time for a particular service or journey.
        [Display(Name = "End Time:")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? EndTime { get; set; }

        // Represents the date when the ticket was purchased.
        [Display(Name = "Purchase date")]
        [DataType(DataType.Date)]
        public DateTime? PurchaseDate { get; set; }

    }
}
