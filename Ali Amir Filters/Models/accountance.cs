//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class accountance
    {
        public int id { get; set; }
        public System.DateTime accountance_date { get; set; }
        public string type { get; set; }
        public decimal money { get; set; }
        public int coustmer_id { get; set; }
    
        public virtual Coustmer Coustmer { get; set; }
    }
}
