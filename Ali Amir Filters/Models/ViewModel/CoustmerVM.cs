using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ali_Amir_Filters.Models.ViewModel
{
    public class CoustmerVM
    {
        
        public int Id { get; set; }
        [Required]
        [DisplayName("الاسم")]
        public string Name { get; set; }
        [Required]
        [DisplayName("العنوان")]
        public string adress { get; set; }
        [Required]
        [DisplayName("رقم التليفون")]
        public string phone_Number { get; set; }

        [DisplayName("رقم التليفون")]
        public string phone_Number2 { get; set; }
        [Required]
        [DisplayName("تاريخ التركيب")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime Dates { get; set; }
        [Required]
        [DisplayName("نوع الفلتر")]
        public int Filter_id { get; set; }
        [Required]
        [DisplayName("المنطقه")]
        public string area { get; set; }

        public virtual Filtersss Filtersss { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<syana> syanas { get; set; }
    }
}