using eduLib.Core.Enums;

namespace eduLib.Application.Auth
{
    public class AuthStateMachine
    {
        public SessionState CurrentState { get; private set; } = SessionState.LoggedOut;
        private int _failedAttempts = 0;
        private const int MaxFailedAttempts = 3;

        // Transisi Automata
        public void Transition(bool isLoginSuccess)
        {
            if (CurrentState == SessionState.Locked) return;

            if (isLoginSuccess)
            {
                CurrentState = SessionState.LoggedIn;
                _failedAttempts = 0; // Reset jika sukses
            }
            else
            {
                _failedAttempts++;
                if (_failedAttempts >= MaxFailedAttempts)
                {
                    CurrentState = SessionState.Locked; // Transisi ke Locked
                }
            }
        }

        public void Logout()
        {
            if (CurrentState == SessionState.LoggedIn)
            {
                CurrentState = SessionState.LoggedOut; // Transisi ke LoggedOut
            }
        }
    }
}