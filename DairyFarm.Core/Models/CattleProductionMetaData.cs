﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Core.DAL
{
    [MetadataType(typeof(CattleProductionMetaData))]
    public partial class CattleProduction
    {

        [RegularExpression(@"^([0-9]{1,2})([,][0-9]{1,2})?$", ErrorMessage = "Code invalide. ex: MA-12-1234567")]
        [DataType(DataType.Text)]
        public string  Quantity2 { get; set; }

    }
    public class CattleProductionMetaData
    {
        [Display(Name = "Code Bête")]
        public int IdCattle { get; set; }

        [Display(Name = "Date de Production")]
        [DataType(DataType.Text)]
        public System.DateTime Dateprod { get; set; }

        [Display(Name = "période")]
        [DataType(DataType.Text)]
        public System.DateTime Period { get; set; }

        [Display(Name = "Quantité")]
        public decimal Quantity { get; set; }
    }
}
