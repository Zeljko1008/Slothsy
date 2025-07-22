using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slothsy.Common.Helpers
{
    public class EnumHelpers
    {

        public static List<KeyValuePair<int, string>> GetEnumValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new KeyValuePair<int, string>(Convert.ToInt32(e), e.ToString()))
                .ToList();
        }
    }
}
