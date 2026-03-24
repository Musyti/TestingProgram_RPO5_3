using System;
using System.Collections.Generic;

namespace WinFormsApp1.Models
{
    public enum UserRole
    {
        Admin,
        Tested
    }

    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> CompletedTests { get; set; } = new List<string>();

        public User()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
        }

        public bool ValidatePassword(string password)
        {
            return Password == password;
        }
    }
}