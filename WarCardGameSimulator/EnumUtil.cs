using System;
using System.Collections.Generic;
using System.Linq;

namespace WarCardGameSimulator
{
    /// <summary>
    /// Class holding utility methods about enumerations
    /// </summary>
    public class EnumUtil
    {
        /// <summary>
        /// Returns enum values of TEnum.
        /// 
        /// See https://stackoverflow.com/questions/23794691/extension-method-to-get-the-values-of-any-enum
        /// </summary>
        public static IEnumerable<TEnum> Values<TEnum>() where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var enumType = typeof(TEnum);

            // Optional runtime check for completeness    
            if (!enumType.IsEnum)
            {
                throw new ArgumentException();
            }

            return Enum.GetValues(enumType).Cast<TEnum>();
        }
    }
}
