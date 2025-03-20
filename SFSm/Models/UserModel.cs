using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFSm.Models
{
    public class UserModel
    {
        public static string UsernameProfile { get; set; }
        public static string EmpIDProfile { get; set; }

        [Key]
        public string EmpID { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
        public bool Active { get; set; }
    }
}
