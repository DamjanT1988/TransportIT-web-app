﻿using System.ComponentModel.DataAnnotations.Schema;
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
        public string? TripTitle { get; set; }

        [Required]
        [Display(Name = "Unique ticket ID number:")]
        public int? TicketNumber { get; set; }

        [Required]
        [Display(Name = "Description:")]
        public string? TicketDescription { get; set; }

        [Required]
        [Display(Name = "Day price (SEK):")]
        public double? Price { get; set; }

        [Required]
        [Display(Name = "Available:")]
        public bool? TicketAvailable { get; set; }

        [Required]
        [Display(Name = "Category (Transport):")]
        public string? Category { get; set; }

        [Required]
        [Display(Name = "Week day:")]
        public string? WeekDay { get; set; }

        [Display(Name = "Image file name (with .jpg/.png):")]
        public string? ImagePath { get; set; }

        //not stored in DB, but shown in UI
        [NotMapped]
        [Display(Name = "Image file")]
        public IFormFile? ImageFile { get; set; }

        //save data from transfer by API
        [NotMapped]
        public byte[]? ImageData { get; set; }
    }
}
