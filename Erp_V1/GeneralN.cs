using System;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public static class GeneralN
    {
        /// <summary>
        /// Validates if the key pressed for category, customer, or product name contains only letters and spaces.
        /// </summary>
        /// <param name="e">The KeyPressEventArgs containing the key pressed.</param>
        /// <returns>True if the key is a valid character (letter or space), otherwise false.</returns>
        public static bool IsValidName(KeyPressEventArgs e)
        {
            // Allow letters (upper and lower case), space, and control characters like backspace.
            return char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == ' ';
        }

        /// <summary>
        /// Checks if the category name is valid.
        /// </summary>
        /// <param name="categoryName">The category name to validate.</param>
        /// <returns>True if the category name is valid (non-empty and does not contain invalid characters).</returns>
        public static bool IsValidCategoryName(string categoryName)
        {
            // Check if the category name is not empty and contains only valid letters and spaces.
            return !string.IsNullOrWhiteSpace(categoryName) && categoryName.All(c => IsValidCharacter(c));
        }

        /// <summary>
        /// Checks if the customer name is valid.
        /// </summary>
        /// <param name="customerName">The customer name to validate.</param>
        /// <returns>True if the customer name is valid (non-empty and does not contain invalid characters).</returns>
        public static bool IsValidCustomerName(string customerName)
        {
            // Check if the customer name is not empty and contains only valid letters and spaces.
            return !string.IsNullOrWhiteSpace(customerName) && customerName.All(c => IsValidCharacter(c));
        }

        /// <summary>
        /// Checks if the product name is valid.
        /// </summary>
        /// <param name="productName">The product name to validate.</param>
        /// <returns>True if the product name is valid (non-empty and does not contain invalid characters).</returns>
        public static bool IsValidProductName(string productName)
        {
            // Check if the product name is not empty and contains only valid letters and spaces.
            return !string.IsNullOrWhiteSpace(productName) && productName.All(c => IsValidCharacter(c));
        }

        /// <summary>
        /// Determines if the character is valid (letter or space).
        /// </summary>
        /// <param name="c">The character to check.</param>
        /// <returns>True if the character is a letter or space, otherwise false.</returns>
        private static bool IsValidCharacter(char c)
        {
            // Check if the character is a letter (including letters from all languages) or a space.
            return Char.IsLetter(c) || c == ' ';
        }
    }
}
