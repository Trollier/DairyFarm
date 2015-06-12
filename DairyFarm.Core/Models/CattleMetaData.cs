﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Core.DAL
{
    [MetadataType(typeof(CattleMetaData))]
    public partial class Cattle
    {

    }
    public class CattleMetaData
    {

        [Required(ErrorMessage = "Entrez un {0}")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Code Bête")]
        public string CodeCattle { get; set; }

        [Required(ErrorMessage = "Select un {0}")]
        [Display(Name = "Troupeau")]
        public int IdHerd { get; set; }

        [Required(ErrorMessage = "Entrez une {0}")]
        [Display(Name = "Date de naissance")]
        public System.DateTime DateBirth { get; set; }

        [Display(Name = "Parent Mal")]
        public Nullable<int> MalParent { get; set; }

        [Display(Name = "Parent femelle")]
        public Nullable<int> FemaleParent { get; set; }
    }
}