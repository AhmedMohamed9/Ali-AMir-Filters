﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ali_Amir_Filters.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class sp_history6_Result
    {
        public int id { get; set; }
       
        [DisplayName("تاريخ الصيانه")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public System.DateTime date_syana_done { get; set; }
        
        [DisplayName("النوع")]
   
        public string type { get; set; }
      
        [DisplayName("المال")]
      
        public decimal money { get; set; }
    }
}
