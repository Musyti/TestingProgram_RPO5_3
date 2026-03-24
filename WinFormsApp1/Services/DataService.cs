using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public class DataService
    {
        private readonly string _usersFile = "users.json";
        private readonly string _categoriesFile = "categories.json";
        private List<User> _users;
        private List<Category> _categories;

        public DataService()
        {
            LoadData();
        }

        private void LoadData()
        {
            // Загрузка пользователей
            if (File.Exists(_usersFile))
            {
                var json = File.ReadAllText(_usersFile);
                _users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            else
            {
                _users = new List<User>();
                // Создание администратора по умолчанию
                _users.Add(new User
                {
                    Username = "admin",
                    Password = "admin123",
                    Role = UserRole.Admin
                });
                SaveUsers();
            }

            // Загрузка категорий
            if (File.Exists(_categoriesFile))
            {
                var json = File.ReadAllText(_categoriesFile);
                _categories = JsonSerializer.Deserialize<List<Category>>(json) ?? new List<Category>();
            }
            else
            {
                _categories = new List<Category>();
                SaveCategories();
            }
        }

        private void SaveUsers()
        {
            var json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_usersFile, json);
        }

        private void SaveCategories()
        {
            var json = JsonSerializer.Serialize(_categories, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_categoriesFile, json);
        }

        // Работа с пользователями
        public bool RegisterUser(string username, string password, UserRole role)
        {
            if (_users.Any(u => u.Username == username))
                return false;

            _users.Add(new User
            {
                Username = username,
                Password = password,
                Role = role
            });
            SaveUsers();
            return true;
        }

        public User Login(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        // Работа с категориями
        public List<Category> GetAllCategories()
        {
            return _categories;
        }

        public Category GetCategoryById(string id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }

        public void AddCategory(Category category)
        {
            if (category.IsValid())
            {
                _categories.Add(category);
                SaveCategories();
            }
        }

        public void UpdateCategory(Category category)
        {
            var index = _categories.FindIndex(c => c.Id == category.Id);
            if (index >= 0 && category.IsValid())
            {
                _categories[index] = category;
                SaveCategories();
            }
        }

        public void DeleteCategory(string id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _categories.Remove(category);
                SaveCategories();
            }
        }

        public void SaveTestResult(string username, string categoryId, int points)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                if (!user.CompletedTests.Contains(categoryId))
                    user.CompletedTests.Add(categoryId);
                SaveUsers();
            }
        }
    }
}