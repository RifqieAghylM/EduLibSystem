using System.Collections.Generic;
using eduLib.Core.Enums;

namespace eduLib.Application.Auth
{
    public static class RoleAccessTable
    {
        private static readonly Dictionary<Role, List<string>> _accessTable = new Dictionary<Role, List<string>>
        {
            {
                Role.Admin, new List<string>
                {
                    "Manage Book",
                    "Read And Download Book",
                    "Review",
                    "Bookmark"
                }
            },
            {
                Role.User, new List<string>
                {
                    "Read And Download Book",
                    "Review",
                    "Bookmark"
                }
            }
        };

        public static List<string> GetAccessibleMenus(Role role)
        {
            return _accessTable.ContainsKey(role) ? _accessTable[role] : new List<string>();
        }
    }
}
