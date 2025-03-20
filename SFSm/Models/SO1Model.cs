using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFSm.Models
{
    public class SO1Model
    {
        [Key]
        public int Tseqno { get; set; }
        public string Customer { get; set; }
        public string Pono { get; set; }
        public string Remarks { get; set; }
        public int Active { get; set; } //purpose neto para 1 SO lang pwedeng i download per phone alisin na lang for future update
    }
}
