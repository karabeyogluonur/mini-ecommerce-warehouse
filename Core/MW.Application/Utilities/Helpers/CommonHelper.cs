using MW.Application.Utilities.Defaults;
using System.Text.RegularExpressions;

namespace MW.Application.Utilities.Helpers
{
    public static class CommonHelper
    {
        public static string CharacterRegularity(string text)
        {
            text = text.Replace(" ", "_")
                       .Replace("-","_");
            Regex.Replace(text, RegexDefaults.CharacterRegularity, "", RegexOptions.Compiled);
            return text;
        }
    }
}
