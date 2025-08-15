using System;
using System.Windows.Forms;

namespace Erp_V1
{
    public static class General
    {
        /// <summary>
        /// Validates if the key pressed is a number.
        /// </summary>
        /// <param name="e">The KeyPressEventArgs containing the key pressed.</param>
        /// <returns>True if the key pressed is not a number or control key, otherwise false.</returns>
        public static bool isNumber(KeyPressEventArgs e)
        {
            // Return false if the key is neither a control character nor a digit.
            return !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }
    }
}
