using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Text;
using UI_Layer.Resources;

namespace UI_Layer.Globalizer
{
    public static class HttGlobalizer
    {
       
        public static string GetErrorMessage(ModelStateDictionary? modelState, IStringLocalizer<Resource> localizer)
        {
            string errorMessage = string.Empty;


            List<ModelStateEntry> sortedEntries = new List<ModelStateEntry>();




            if (modelState != null && !modelState.IsValid)
            {
                foreach (var entry in modelState)
                {
                    if (entry.Value.Errors.Count > 0)
                    {
                        errorMessage = localizer.GetString(entry.Value.Errors[0].ErrorMessage) ;
                        break;
                    }
                }
            }
            return errorMessage;
        }

        public static string GenerateRandomString(int length)
        {
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string specialCharacters = "@#!$%&*?";
            const string digits = "0123456789";
            Random random = new Random();
            StringBuilder result = new StringBuilder(length);

            // Ensure at least 3 lowercase characters
            for (int i = 0; i < 3; i++)
            {
                result.Append(lowercase[random.Next(lowercase.Length)]);
            }

            // Ensure at least 1 uppercase character
            result.Append(uppercase[random.Next(uppercase.Length)]);

            // Ensure at least 1 special character
            result.Append(specialCharacters[random.Next(specialCharacters.Length)]);

            // Ensure at least 1 digit
            result.Append(digits[random.Next(digits.Length)]);

            // Fill the remaining characters
            for (int i = 0; i < length - 6; i++)
            {
                string pool = lowercase + uppercase + specialCharacters + digits;
                result.Append(pool[random.Next(pool.Length)]);
            }

            // Shuffle the characters to make the result random
            for (int i = 0; i < result.Length; i++)
            {
                int swapIndex = random.Next(result.Length);
                char temp = result[i];
                result[i] = result[swapIndex];
                result[swapIndex] = temp;
            }

            return result.ToString();
        }

    }
}
