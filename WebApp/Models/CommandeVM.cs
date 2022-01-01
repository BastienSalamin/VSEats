﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CommandeVM
    {
        public int IdUtilisateur { get; set; }
        public int IdPlat { get; set; }
        public string Nom { get; set; }
        public double Prix { get; set; }
        [Required]
        public int Quantite { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")]
        [DataType(DataType.Date)]
        public DateTime HeureLivraison { get; set; }
    }
}
