using System;
using System.Threading.Tasks;
using Qmmands;

namespace GarlicBread.Parsers
{
    public class UriTypeParser : TypeParser<Uri>
    {
        public override ValueTask<TypeParserResult<Uri>> ParseAsync(Parameter parameter, string value,
            CommandContext context)
        {
            value = value.Replace("<", "").Replace(">", "");
            return Uri.IsWellFormedUriString(value, UriKind.Absolute)
                ? TypeParserResult<Uri>.Successful(new Uri(value))
                : TypeParserResult<Uri>.Unsuccessful("Unknown URL.");
        }
    }
}