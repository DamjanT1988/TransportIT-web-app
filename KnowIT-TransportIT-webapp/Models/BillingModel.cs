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
        [Display(Name = "Billing information:")]
        public string? Order { get; set; }

        [Display(Name = "Customer email:")]
        public string? Email { get; set; }

        [Display(Name = "Customer telephone number:")]
        public string? Telephone { get; set; }

        [Display(Name = "Customer name:")]
        public string? Customer_name { get; set; }

        [Display(Name = "Customer social number:")]
        public int? Customer_sol_no { get; set; }
        
        [Display(Name = "Customer adress:")]
        public string? Customer_adress { get; set; }
                
        [Display(Name = "Status of the trip:")]
        public string? Status { get; set; }

        [Display(Name = "Internal notes:")]
        public string? Internal_note { get; set; }
    }
}
