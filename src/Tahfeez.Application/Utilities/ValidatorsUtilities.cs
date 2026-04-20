using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Tahfeez.Application.Utilities
{
    public static class ValidatorsUtilities
    {
        public static bool PasswordValidator(string password)
        {
            var regex = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
            return Regex.IsMatch(password, regex);
        }
    }
}
