using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KnowIT_TransportIT_webapp.Models
{
    public class FreeDayClass
    {
        //data fields
        [Display(Name = "Free day ID no.")]
        public int Id { get; set; }

        //user input and object properties
        [Required]
        [Display(Name = "Holiday name/reason")]
        public string? FreeDayReason { get; set; }

        [Required]
        [Display(Name = "Active/inactive")]
        public bool? StatusFreeDay { get; set; }

        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        public DateTime? StartDateFreeDay { get; set; }

        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        public DateTime? EndDateFreeDay { get; set; }
    }
}
