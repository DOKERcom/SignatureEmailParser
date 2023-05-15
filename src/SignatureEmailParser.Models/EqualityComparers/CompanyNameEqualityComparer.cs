using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace SignatureEmailParser.Models.EqualityComparers
{
    public class CompanyNameEqualityComparer : IEqualityComparer<string>
    {
        private const string SPACE_PATTERN = @"\s+";
        public bool Equals([AllowNull] string x, [AllowNull] string y)
        {
            string first = Regex.Replace(x, SPACE_PATTERN, string.Empty);
            string second = Regex.Replace(y, SPACE_PATTERN, string.Empty);

            return first.Equals(second, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode([DisallowNull] string value)
        {
            return value.GetHashCode();
        }
    }
}
