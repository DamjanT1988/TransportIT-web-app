﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KnowIT_TransportIT_webapp.Models
{
    public class PassangerModel
    {
        //data fields
        [Display(Name = "Passanger ID no.")]
        public int Id { get; set; }

        //user input and object properties
        [Required]
        [Display(Name = "Passanger full name")]
        public string? PassangerName { get; set; }

        [Required]
        [Display(Name = "Passanger social no.")]
        public int? PassangerSocNo { get; set; }

        [Required]
        [Display(Name = "Passanger password")]
        public string? PassangerPassword { get; set; }

        [Required]
        [Display(Name = "Passanger email")]
        public string? PassangerEmail { get; set; }
        
        [Display(Name = "Passanger tel. no.")]
        public string? PassangerTel { get; set; }

        [Display(Name = "Account creation")]
        [DataType(DataType.Date)]
        public DateTime? CreationAccount { get; set; }

    }
}
