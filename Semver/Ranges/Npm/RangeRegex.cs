using System;
using System.Text.RegularExpressions;

namespace Semver.Ranges.Npm
{
    internal static class RangeRegex
    {
#if COMPILED_REGEX
        private const RegexOptions CompiledOptions = RegexOptions.Compiled;
#else
        private const RegexOptions CompiledOptions = RegexOptions.None;
#endif

        private static readonly TimeSpan MatchTimeout = TimeSpan.FromSeconds(1);

        public static readonly Regex Operator = new Regex(@"(?'operator'<=|<|>=|>|\^|~>|~|=)?", CompiledOptions, MatchTimeout);

        public static readonly Regex PartialVersion = new Regex(
            @"[vV]?(?'major'x|X|\*|[0-9]\d*)" +
            @"(?:\.(?'minor'x|X|\*|[0-9]\d*))?" +
            @"(?:\.(?'patch'x|X|\*|[0-9]\d*))?" +
            @"(?:-(?'prerelease'(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?" +
            @"(?:\+(?'buildmetadata'[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?",
            CompiledOptions,
            MatchTimeout
        );

        public static readonly Regex HyphenRange = new Regex($@"^(?'minVersion'(?:{PartialVersion}))\s*-\s*(?'maxVersion'(?:{PartialVersion}))$", CompiledOptions, MatchTimeout);
        public static readonly Regex OperatorRange = new Regex($@"{Operator}(?:\s*)(?'version'{PartialVersion})\s*", CompiledOptions, MatchTimeout);

        public static readonly Regex OperatorRangeTest = new Regex($@"^(?:(?:\s*{OperatorRange}))+\s*$", CompiledOptions, MatchTimeout);
    }
}
