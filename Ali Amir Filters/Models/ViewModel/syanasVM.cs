using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ali_Amir_Filters.Models.ViewModel
{
    public class syanasVM
    {
        public int Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public System.DateTime date_syana { get; set; }
        public int customer_id { get; set; }
        [Required]
        [DisplayName("الشمعه")]
        public string candel_name { get; set; }
        [Required]
        [DisplayName("اسم الفلتر")]
        public string filter_name { get; set; }
        [Required]
        [DisplayName("تم الانتهاء؟ ")]
        public bool is_done { get; set; }
        [Required]
        [DisplayName("شهور التأجيل")]
        public Nullable<int> months_late { get; set; }
        [Required]
        [DisplayName("عدد الشهور")]
        public Nullable<int> number_of_months { get; set; }

        public virtual Coustmer Coustmer { get; set; }
    }
}