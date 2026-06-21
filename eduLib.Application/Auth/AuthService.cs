using System;
using System.Collections.Generic;
using System.Linq;
using eduLib.Core.Entities;
using eduLib.Core.Enums;

namespace eduLib.Application.Auth
{
    public class AuthService
    {
        // Menggunakan Static agar data user konsisten di seluruh aplikasi
        private static readonly List<User> _sharedMockDatabase = new List<User>
        {
            new User { Username = "admin", Password = "admin123", UserRole = Role.Admin },
            new User { Username = "azka", Password = "azka123", UserRole = Role.Admin },
            new User { Username = "user",  Password = "user123",  UserRole = Role.User },
            new User { Username = "rifqie",  Password = "rifqie123",  UserRole = Role.User }
        };

        // Menggunakan static agar status (Locked/LoggedOut) tersimpan global
        private static readonly AuthStateMachine _stateMachine = new AuthStateMachine();

        public AuthService() { }

        public AuthService(List<User> userDatabase)
        {
        }

        public SessionState GetCurrentState() => _stateMachine.CurrentState;

        public User Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Username atau Password tidak boleh kosong.");

            if (_stateMachine.CurrentState == SessionState.Locked)
                throw new InvalidOperationException("Akun terkunci karena terlalu banyak percobaan gagal.");

            var user = _sharedMockDatabase.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                && u.Password == password);

            bool isSuccess = user != null;
            _stateMachine.Transition(isSuccess);

            if (!isSuccess)
                throw new UnauthorizedAccessException("Username atau password salah.");

            return user;
        }

        public void Logout() => _stateMachine.Logout();

        public List<string> GetUserMenus(User user)
        {
            if (_stateMachine.CurrentState != SessionState.LoggedIn)
                throw new InvalidOperationException("Sesi tidak valid.");
            return RoleAccessTable.GetAccessibleMenus(user.UserRole);
        }
    }
}
