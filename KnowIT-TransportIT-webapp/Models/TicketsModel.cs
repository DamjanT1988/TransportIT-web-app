using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KnowIT_TransportIT_webapp.Models
{
    public class TicketsModel
    {
        //data fields
        [Display(Name = "Object ID number:")]
        public int Id { get; set; }

        //user input and object properties
        [Required]
        [Display(Name = "Title of trip:")]
        public string? Trip_title { get; set; }

        [Required]
        [Display(Name = "Unique ticket ID number:")]
        public string? Ticket_number { get; set; }

        [Required]
        [Display(Name = "Description:")]
        public string? Ticket_description { get; set; }

        [Required]
        [Display(Name = "Day price (SEK):")]
        public double? Price { get; set; }

        [Required]
        [Display(Name = "Available:")]
        public bool? Ticket_available { get; set; }

        [Required]
        [Display(Name = "Category (Transport):")]
        public string? Category { get; set; }

        [Display(Name = "Image file name (with .jpg/.png):")]
        public string? Image_path { get; set; }

        //not stored in DB, but shown in UI
        [NotMapped]
        [Display(Name = "Image file")]
        public IFormFile? Image_file { get; set; }

        //save data from transfer by API
        [NotMapped]
        public byte[]? Image_data { get; set; }
    }
}
