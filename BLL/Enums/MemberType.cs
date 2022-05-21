using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Enums
{
    public enum MemberType
    {
        [Display(Name = "Individual")]
        Individual = 1,
        [Display(Name = "Corporate")]
        Corporate = 2,
    }
}
