using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flower_Project.Areas.Admin.Models
{
    public class Member
    {
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public MemberStatus Status { get; set; }

        public enum MemberStatus
        {
            Active = 1,
            Deactivate = 0,
            Deleted = -1
        }

        public bool IsDeleted()
        {
            return this.Status == MemberStatus.Deleted;
        }
    }
}