using Data_.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Data_.Validators
{
    public static class ModelsValidator
    {
        public static readonly Regex RegExpPassword = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{8,32}$");
        public static readonly Regex RegExpEmail = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        public static bool AchievementIsValid(AchievementUpdateDTO achievement) 
        {
            return !(achievement == null || achievement.Xp <= 0 ||
                    achievement.Name?.Length > 70 || achievement.Name?.Length == 0 ||
                    achievement.Description?.Length > 250 || achievement.Description?.Length == 0);
        }
        public static bool UserCreateIsValid(UserCreateDTO newUser)
        {
            return !(newUser == null || newUser.LastName?.Length > 32 || newUser.LastName?.Length == 0 ||
                    newUser.FirstName?.Length > 32 || newUser.FirstName?.Length == 0 ||
                    newUser.UserName?.Length > 32 || newUser.UserName?.Length == 0 ||
                    newUser.Email?.Length > 250 || !RegExpEmail.IsMatch(newUser.Email) ||
                    !RegExpPassword.IsMatch(newUser.Password));
        }

        public static bool UserUpdateIsValid(UserUpdateDTO user)
        {
            return !(user == null || user.LastName?.Length > 32 || user.LastName?.Length == 0 ||
                    user.FirstName?.Length > 32 || user.FirstName?.Length == 0 ||
                    user.UserName?.Length > 32 || user.UserName?.Length == 0 ||
                    user.Email?.Length > 250 || !RegExpEmail.IsMatch(user.Email) ||
                    user.Status?.Length > 250);
        }


    }
}
