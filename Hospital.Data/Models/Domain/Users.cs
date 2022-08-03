using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.Models.Domain
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string IdentityId { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Gender { get; set; }
        public decimal ConsultancyFee { get; set; }
        public string Specialization { get; set; }
        public string PatientId { get; set; }

    }
}
