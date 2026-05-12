using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using eduLib.Core.Entities;
using eduLib.Core.Enums;
using eduLib.Application.Auth;

namespace eduLib.Tests.AuthTests
{
    [TestClass]
    public class AuthServiceTests
    {
        private List<User> _mockDb;
        private AuthService _authService;

        [TestInitialize]
        public void Setup()
        {
            _mockDb = new List<User>
        {
            new User { Username = "azka_admin", Password = "123", UserRole = Role.Admin },
            new User { Username = "rifqie_pelajar", Password = "abc", UserRole = Role.Pelajar },
            new User { Username = "guru", Password = "456", UserRole = Role.Guru }
        };
            _authService = new AuthService(_mockDb);
        }

        [TestMethod]
        public void Login_ValidCredentials_ReturnsUserAndSetsStateToLoggedIn()
        {
            // Act
            var user = _authService.Login("azka_admin", "123");

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(SessionState.LoggedIn, _authService.GetCurrentState());
        }

        [TestMethod]
        public void Login_UsernameCapital_StillLogsIn()
        {
            // Act
            var user = _authService.Login("AZKA_ADMIN", "123");

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(SessionState.LoggedIn, _authService.GetCurrentState());
        }

        // PERBAIKAN 1: Menggunakan pola Try-Catch-Fail
        [TestMethod]
        public void Login_InvalidCredentials_ThrowsUnauthorizedAccess()
        {
            try
            {
                // Act
                _authService.Login("azka_admin", "salah_password");

                // Jika baris di atas lolos (tidak error), paksa tes ini gagal
                Assert.Fail("Seharusnya melempar UnauthorizedAccessException, tapi malah lolos.");
            }
            catch (UnauthorizedAccessException)
            {
                // Assert sukses! Exception yang diharapkan benar-benar dilempar.
            }
        }

        // PERBAIKAN 2: Menggunakan pola Try-Catch-Fail
        [TestMethod]
        public void Login_Failed3Times_LocksAccount()
        {
            // Arrange
            try { _authService.Login("azka_admin", "salah1"); } catch { }
            try { _authService.Login("azka_admin", "salah2"); } catch { }
            try { _authService.Login("azka_admin", "salah3"); } catch { }

            try
            {
                // Act
                _authService.Login("azka_admin", "123");

                Assert.Fail("Seharusnya melempar InvalidOperationException karena state akun terkunci.");
            }
            catch (InvalidOperationException)
            {
                // Assert sukses! Automata berhasil mengunci akun.
            }
        }

        // PERBAIKAN 3: Menggunakan pola Try-Catch-Fail
        [TestMethod]
        public void Login_EmptyUsername_TriggersDefensiveProgramming()
        {
            try
            {
                // Act
                _authService.Login("", "123");

                Assert.Fail("Seharusnya melempar ArgumentException karena username kosong.");
            }
            catch (ArgumentException)
            {
                // Assert sukses! Defensive programming (DbC) bekerja dengan baik.
            }
        }

        // PERFORMANCE TESTING 
        [TestMethod]
        public void LoginAndGetMenus_PerformanceTest_ExecutesUnder100Milliseconds()
        {
            // Arrange
            var stopwatch = new System.Diagnostics.Stopwatch();

            // Lakukan pemanasan (warm-up) agar memori siap
            _authService.Login("azka_admin", "123");
            _authService.Logout();

            // Act: Mulai hitung waktu performa
            stopwatch.Start();

            var user = _authService.Login("azka_admin", "123");
            var menus = _authService.GetUserMenus(user);

            stopwatch.Stop();

            // Assert: Memastikan eksekusi Login + Table-Driven Menu sangat cepat (di bawah 100ms)
            // Ini jauh memenuhi standar NFR-02 (< 2 detik)
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 100, $"Performa terlalu lambat! Waktu eksekusi: {stopwatch.ElapsedMilliseconds} ms");
        }

        [TestMethod]
        public void Profiling_Auth_StateAndMenuAllocation()
        {
            // Stress test: Melakukan transisi Automata dan Table-driven sebanyak 100.000 kali
            // Ini akan memicu grafik CPU dan alokasi memori
            for (int i = 0; i < 100000; i++)
            {
                var user = new User { Username = "user" + i, Password = "123", UserRole = Role.Pelajar };

                // Memanggil Table-Driven
                var menus = RoleAccessTable.GetAccessibleMenus(user.UserRole);

                // Memanggil Automata
                _authService.Login("azka_admin", "123");
                _authService.Logout();
            }
        }
    }
}
