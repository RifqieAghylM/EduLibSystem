using System;
using System.Collections.Generic;
using System.Linq;
using eduLib.Core.Entities;
using eduLib.Core.Enums;

namespace eduLib.Application.Auth
{
    public class AuthService
    {
        private readonly AuthStateMachine _stateMachine;
        private readonly List<User> _mockDatabase; 

        public AuthService(List<User> mockDatabase)
        {
            // DbC: Precondition
            if (mockDatabase == null)
                throw new ArgumentNullException(nameof(mockDatabase), "Database simulasi tidak boleh null.");

            _stateMachine = new AuthStateMachine();
            _mockDatabase = mockDatabase;
        }

        public SessionState GetCurrentState() => _stateMachine.CurrentState;

        public User Login(string username, string password)
        {
            // DbC: Preconditions
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username tidak boleh kosong atau spasi.");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password tidak boleh kosong.");

            if (_stateMachine.CurrentState == SessionState.Locked)
            {
                throw new InvalidOperationException("Akun terkunci karena terlalu banyak percobaan gagal.");
            }


            var user = _mockDatabase.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                && u.Password == password);
            bool isSuccess = user != null;

            _stateMachine.Transition(isSuccess);

            if (!isSuccess)
            {
                throw new UnauthorizedAccessException("Username atau password salah.");
            }

            // DbC: Postcondition
            if (_stateMachine.CurrentState != SessionState.LoggedIn)
            {
                throw new Exception("Terjadi kesalahan internal: State gagal bertransisi ke LoggedIn.");
            }

            return user;
        }

        public List<string> GetUserMenus(User user)
        {
            // DbC: Preconditions
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (_stateMachine.CurrentState != SessionState.LoggedIn)
                throw new InvalidOperationException("Sesi tidak valid. Harus login untuk melihat menu.");

            return RoleAccessTable.GetAccessibleMenus(user.UserRole);
        }

        public void Logout()
        {
            _stateMachine.Logout();
        }
    }
}