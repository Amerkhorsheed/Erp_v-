// File: Erp.WebApp/Services/FontResolver.cs

using PdfSharp.Fonts;
using System;
using System.IO;
using System.Reflection;

namespace Erp.WebApp.Services
{
    // This is the implementation of the IFontResolver.
    public class FontResolver : IFontResolver
    {
        public byte[] GetFont(string faceName)
        {
            // The logic to find the font file. For a web app, it's best to embed
            // the font as a resource. We will assume a 'Fonts' folder in the project.
            // Note: This implementation is a simplified one. For production,
            // embedding fonts as resources is more robust.
            // For now, we will try to find it in the system's font directory.

            // On Windows, fonts are typically in C:\Windows\Fonts
            var fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), faceName + ".ttf");
            if (File.Exists(fontPath))
            {
                return File.ReadAllBytes(fontPath);
            }
            // Fallback for Arial Bold, Italic, etc.
            if (faceName.EndsWith("bi", StringComparison.OrdinalIgnoreCase))
                fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), faceName.Substring(0, faceName.Length - 2) + "bd.ttf"); // Arial Bold Italic -> Arial Bold
            else if (faceName.EndsWith("b", StringComparison.OrdinalIgnoreCase))
                fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), faceName.Substring(0, faceName.Length - 1) + "bd.ttf"); // Arial Bold
            if (File.Exists(fontPath))
            {
                return File.ReadAllBytes(fontPath);
            }
            // Add more fallbacks if needed for other styles.

            // If you deploy to a non-Windows server (like Linux), this will fail.
            // In that case, you must embed the .ttf files in your project.
            throw new Exception($"Font '{faceName}' not found. Ensure the font is installed on the server or embedded in the project.");
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            // Ignore case of font names.
            var name = familyName.ToLower().Trim();

            // Deal with the fonts we know.
            switch (name)
            {
                case "arial":
                    if (isBold && isItalic)
                        return new FontResolverInfo("arialbi");
                    if (isBold)
                        return new FontResolverInfo("arialb");
                    if (isItalic)
                        return new FontResolverInfo("ariali");
                    return new FontResolverInfo("arial");
            }

            // We don't know the font face.
            return null;
        }
    }
}