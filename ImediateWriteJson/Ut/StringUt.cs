using System.Globalization;
using System.Linq;
using System.Text;

namespace ImediateWriteJson.Ut
{
    public static class StringUt
    {
        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        /// <summary>Removes given characters from string. It is better to call this function once, then calling out of box stringRemove multiple times.</summary>
        /// <param name="s">The string</param>
        /// <param name="chars">The characters to be removed.</param>
        /// <returns>The string with removed characters.</returns>
        public static string CapsRemove(string s, char[] chars)
        {
            var reqUpper = true;
            var sb = new StringBuilder(s.Length);
            foreach (char ch in s)
            {
                var chToAdd = ch;
                if (!chars.Any(c => c == ch))
                {
                    if (reqUpper)
                    {
                        chToAdd = char.ToUpperInvariant(ch);
                        reqUpper = false;
                    }
                    sb.Append(chToAdd);
                }
                else
                {
                    reqUpper = true;
                }
            }
            return sb.ToString();
        }
    }
}