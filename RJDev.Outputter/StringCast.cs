using System;

namespace RJDev.Outputter
{
    public readonly struct StringCast
    {
        /// <summary>
        /// Original string
        /// </summary>
        public readonly string String;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="str"></param>
        private StringCast(String str)
        {
            this.String = str;
        }

        /// <summary>
        /// Implicit operator from FormattableString.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static implicit operator StringCast(string str)
        {
            return new(str);
        }

        /// <summary>
        /// Forbidden operator.
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static implicit operator StringCast(FormattableString _)
        {
            throw new InvalidOperationException("FormattableString cannot be casted to StringCast.");
        }
    }
}