using BLL.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL.Modals
{
    public class Member
    {
        [DisplayName("ID")]
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [DisplayName("Birthday Date")]
        [UIHint("DateEditor")]
        public DateTime DOB { get; set; }
        [DisplayName("Type")]
        [UIHint("TypeEditor")]
        public MemberType Type { get; set; }
        [DisplayName("Status")]
        [UIHint("StatusEditor")]
        public MemberStatus Status { get; set; }

        [DisplayName("Age")]
        public int Age { get; set; }
        [DisplayName("Notional ID")]
        public decimal NotionalID { get; set; }
    }
}
