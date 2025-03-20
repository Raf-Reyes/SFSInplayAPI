using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFSm.Models
{
    public class SOPick2Model
    {
        public int Refsono { get; set; }
        public string Description { get; set; }
        public decimal Qty { get; set; }
        public string Barcode { get; set; }
        public string Serialno { get; set; }
        public string Pickby { get; set; }
        public DateTime Date { get; set; }
    }
}
