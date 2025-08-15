using System.Drawing;
using System.Drawing.Drawing2D;

namespace Erp_V1
{
    /// <summary>
    /// Provides centralized UI styling constants and helper methods for the application.
    /// This promotes consistency and ease of maintenance for the visual theme.
    /// </summary>
    public static class UIStyles
    {
        #region Color Palette
        // Primary & Accent Colors
        public static readonly Color PrimaryColorDark = Color.FromArgb(20, 30, 48);
        public static readonly Color PrimaryColorLight = Color.FromArgb(44, 62, 80);
        public static readonly Color AccentTeal = Color.FromArgb(26, 188, 156);
        public static readonly Color AccentTealHover = Color.FromArgb(22, 160, 133);

        // Neutral & Background Colors
        public static readonly Color LightGrayBackground = Color.FromArgb(236, 240, 241); // For input containers
        public static readonly Color FormBackground = Color.FromArgb(45, 52, 54);
        public static readonly Color RightPanelBackgroundStart = Color.FromArgb(248, 249, 250);
        public static readonly Color RightPanelBackgroundEnd = Color.FromArgb(255, 255, 255);
        public static readonly Color White = Color.White;
        public static readonly Color Black = Color.Black;

        // Text Colors
        public static readonly Color TextPrimary = Color.FromArgb(44, 62, 80); // Dark text for light backgrounds
        public static readonly Color TextSecondary = Color.FromArgb(127, 140, 141); // Placeholder text
        public static readonly Color TextLight = Color.White; // Text for dark backgrounds

        // Semantic Colors
        public static readonly Color WarningRed = Color.FromArgb(231, 76, 60);
        public static readonly Color WarningRedHoverBackground = Color.FromArgb(30, 231, 76, 60); // Semi-transparent red

        // Shadow/Highlight Colors
        public static readonly Color SubtleShadow = Color.FromArgb(10, 0, 0, 0); // Very light black for inner shadows
        public static readonly Color SubtleHighlight = Color.FromArgb(20, 255, 255, 255); // Very light white for highlights
        public static readonly Color DotPatternColor = Color.FromArgb(10, 255, 255, 255);
        #endregion

        #region Fonts
        public static readonly Font DefaultFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        public static readonly Font AppNameFont = new Font("Segoe UI", 28F, FontStyle.Bold);
        public static readonly Font SloganFont = new Font("Segoe UI Light", 14F, FontStyle.Regular);
        public static readonly Font LoginHeaderFont = new Font("Segoe UI Light", 32F, FontStyle.Regular);
        public static readonly Font InputTextFont = new Font("Segoe UI", 11F, FontStyle.Regular); // Assuming TextBox font
        public static readonly Font ButtonFont = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
        #endregion

        #region Border Styles
        public static readonly int InputBorderThickness = 2;
        public static readonly int ButtonBorderThickness = 2;
        #endregion

        #region Methods for Drawing (Optional Conveniences)

        /// <summary>
        /// Creates a vertical linear gradient brush.
        /// </summary>
        /// <param name="rect">The rectangle to fill.</param>
        /// <param name="startColor">The starting color of the gradient.</param>
        /// <param name="endColor">The ending color of the gradient.</param>
        /// <returns>A LinearGradientBrush.</returns>
        public static LinearGradientBrush CreateVerticalGradientBrush(Rectangle rect, Color startColor, Color endColor)
        {
            return new LinearGradientBrush(rect, startColor, endColor, LinearGradientMode.Vertical);
        }
        #endregion
    }
}