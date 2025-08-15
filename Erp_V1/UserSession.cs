using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Erp_V1
{
    /// <summary>
    /// Provides a global, static way to access the current user's session information
    /// and enforces session timeout and token security.
    /// </summary>
    public static class UserSession
    {
        private static DateTime _lastActivity;
        private const int TimeoutMinutes = 15;

        public static int UserId { get; private set; }
        public static string UserName { get; private set; }
        public static int RoleId { get; private set; }
        public static List<string> Permissions { get; private set; } = new List<string>();
        public static string SessionToken { get; private set; }

        /// <summary>
        /// Starts a new user session with a cryptographically secure token.
        /// </summary>
        public static void StartSession(int userId, string userName, int roleId, List<string> permissions)
        {
            UserId = userId;
            UserName = userName;
            RoleId = roleId;
            Permissions = permissions ?? new List<string>();
            SessionToken = GenerateSessionToken();
            RenewActivity();
        }

        /// <summary>
        /// Updates the last activity timestamp to now.
        /// </summary>
        public static void RenewActivity()
        {
            _lastActivity = DateTime.Now;
        }

        /// <summary>
        /// Checks if the session is still valid (not timed out).
        /// </summary>
        public static bool IsSessionValid()
        {
            return (DateTime.Now - _lastActivity).TotalMinutes <= TimeoutMinutes;
        }

        /// <summary>
        /// Clears the current user session (e.g., on logout).
        /// </summary>
        public static void EndSession()
        {
            UserId = 0;
            UserName = null;
            RoleId = 0;
            Permissions = new List<string>();
            SessionToken = null;
            _lastActivity = DateTime.MinValue;
        }

        /// <summary>
        /// Checks if the current user has a specific permission.
        /// </summary>
        public static bool HasPermission(string permissionName)
        {
            return Permissions?.Contains(permissionName) ?? false;
        }

        /// <summary>
        /// Generates a secure random session token.
        /// </summary>
        private static string GenerateSessionToken()
        {
            // Use explicit using block for C# 7.3 compatibility
            using (var rng = new RNGCryptoServiceProvider())
            {
                var tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }
    }
}
