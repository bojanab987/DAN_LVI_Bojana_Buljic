using System;

namespace DAN_LVI_Bojana_Buljic
{
    /// <summary>
    /// Static class for Validation
    /// </summary>
    public static class CheckInput
    {
        /// <summary>
        /// Method checks if the forwarded string is correct url address
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool CheckURLValid(this string source) => Uri.TryCreate(source, UriKind.Absolute, out Uri uriResult) && uriResult.Scheme == Uri.UriSchemeHttps;
    }
}
