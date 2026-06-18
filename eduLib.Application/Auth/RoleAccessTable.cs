using System.Collections.Generic;
using eduLib.Core.Enums;

namespace eduLib.Application.Auth
{
    public static class RoleAccessTable
    {
        private static readonly Dictionary<Role, List<string>> _accessTable = new Dictionary<Role, List<string>>
        {
            { Role.Admin, new List<string> { "Kelola File PDF", "Manajemen Akun", "Koleksi Buku" } },
            { Role.Guru, new List<string> { "Koleksi Buku", "Materi Ajar Tambahan", "Riwayat Bacaan Siswa" } },
            { Role.Pelajar, new List<string> { "Koleksi Buku", "Riwayat Bacaan", "Ulasan/Komentar" } }
        };

        public static List<string> GetAccessibleMenus(Role role)
        {
            return _accessTable.ContainsKey(role) ? _accessTable[role] : new List<string>();
        }
    }
}