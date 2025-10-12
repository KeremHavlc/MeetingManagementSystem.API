using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManagementSystem.Domain.Entities
{
    public class AppUserMeetingRoles
    {
        public Guid UserId { get; set; }
        public Guid MeetingRoleId { get; set; }
    }
}
