using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using Yeg.Utilities.RegularExpressions;

namespace Yeg.Utilities.Extensions
{
    public static partial class StringExtensions
    {
        #region Is, IsNot Extensions

        /// <summary>
        /// Determines whether the specified string is a valid Base64 encoded string.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string is a valid Base64 encoded string; otherwise, false.</returns>
        public static bool IsBase64String(this string @this)
        {
            if (string.IsNullOrWhiteSpace(@this))
                return false;

            return @this.Trim().Length % 4 == 0 &&
                   RegexHelper.Base64StringRegex().IsMatch(@this);
        }

        /// <summary>
        /// Determines whether the specified string is a valid GUID.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a valid GUID; otherwise, false.</returns>
        public static bool IsGuid(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
                return false;

            return Guid.TryParse(@this, out _);
        }

        /// <summary>
        /// Determines whether the specified string is equal to another string using the specified string comparison type.
        /// </summary>
        /// <param name="this">The input string.</param>
        /// <param name="comparingStr">The string to compare with.</param>
        /// <param name="stringComparison">The type of string comparison to use (default: StringComparison.InvariantCultureIgnoreCase).</param>
        /// <returns>true if the specified strings are equal; otherwise, false.</returns>
        public static bool IsEquals(this string @this,
            string comparingStr,
            StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
        {
            if (string.IsNullOrEmpty(@this))
                return false;

            return @this.Trim().Equals(comparingStr, stringComparison);
        }

        /// <summary>
        /// Determines whether the specified string is not null or empty.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string is not null or empty; otherwise, false.</returns>
        public static bool IsNotNullOrEmpty(this string @this) =>
            !string.IsNullOrEmpty(@this);


        /// <summary>
        /// Determines whether the specified string is null or empty.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string is null or empty; otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string @this)
        {
            return !@this.IsNotNullOrEmpty();
        }

        /// <summary>
        /// Determines whether the specified string is a valid email address.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string is a valid email address; otherwise, false.</returns>
        public static bool IsEmail(this string @this)
        {
            if (@this.IsNullOrEmpty())
                return false;

            return RegexHelper.EmailRegex().IsMatch(@this);
        }

        /// <summary>
        /// Determines whether the specified string is a valid phone or fax number.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string is a valid phone or fax number; otherwise, false.</returns>
        public static bool IsPhoneOrFaxNumber(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
                return false;

            return RegexHelper.FaxOrPhoneNumberRegex().IsMatch(@this);
        }

        /// <summary>
        /// Determines whether the specified string contains only alphabetic characters and spaces.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string contains only alphabetic characters and spaces; otherwise, false.</returns>
        public static bool IsAlphaAndSpace(this string @this) =>
            RegexHelper.AlphaAndSpaceRegex().IsMatch(@this);


        /// <summary>
        /// Determines whether the specified string contains only alphanumeric characters.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string contains only alphanumeric characters; otherwise, false.</returns>
        public static bool IsAlphaNumeric(this string @this) =>
            !RegexHelper.AlphaNumericRegex().IsMatch(@this);

        /// <summary>
        /// Determines whether the specified string contains only alphanumeric characters.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>false if the specified string contains only alphanumeric characters; otherwise, true.</returns>
        public static bool IsNotAlphaNumeric(this string @this) =>
            !IsAlphaNumeric(@this);

        /// <summary>
        /// Determines whether the specified string is empty.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string is empty; otherwise, false.</returns>
        public static bool IsEmpty(this string @this) =>
            @this == "";

        /// <summary>
        /// Determines whether the specified string contains only numeric characters.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string contains only numeric characters; otherwise, false.</returns>
        public static bool IsNumeric(this string @this) =>
            !RegexHelper.NumericRegex().IsMatch(@this);

        /// <summary>
        /// Determines whether the specified string does not contain any numeric characters.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string does not contain any numeric characters; otherwise, false.</returns>
        public static bool IsNotNumeric(this string @this) =>
            !@this.IsNumeric();

        /// <summary>
        /// Checks whether the string is a valid URL.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a valid URL, otherwise false.</returns>
        public static bool IsUrl(this string @this) =>
            RegexHelper.UrlRegex().IsMatch(@this);

        /// <summary>
        /// Checks whether the string is not a valid URL.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is not a valid URL, otherwise false.</returns>
        public static bool IsNotUrl(this string @this) =>
            !IsUrl(@this);

        /// <summary>
        /// Checks whether the string is a valid date in the format "YYYY-MM-DD".
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a valid date, otherwise false.</returns>
        public static bool IsDate(this string @this) =>
            RegexHelper.DateRegex().IsMatch(@this);

        /// <summary>
        /// Checks whether the string is not a valid date in the format "YYYY-MM-DD".
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is not a valid date, otherwise false.</returns>
        public static bool IsNotDate(this string @this) =>
            !IsDate(@this);

        /// <summary>
        /// Checks whether the string is a valid IP address.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a valid IP address, otherwise false.</returns>
        public static bool IsIpAddress(this string @this) =>
            RegexHelper.IpAddressRegex().IsMatch(@this);

        /// <summary>
        /// Checks whether the string is not a valid IP address.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is not a valid IP address, otherwise false.</returns>
        public static bool IsNotIpAddress(this string @this) =>
            !IsIpAddress(@this);

        /// <summary>
        /// Checks whether the string is a valid postal code.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a valid postal code, otherwise false.</returns>
        public static bool IsPostalCode(this string @this) =>
            RegexHelper.PostalCodeRegex().IsMatch(@this);

        /// <summary>
        /// Checks whether the string is not a valid postal code.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is not a valid postal code, otherwise false.</returns>
        public static bool IsNotPostalCode(this string @this) =>
            !IsPostalCode(@this);

        /// <summary>
        /// Checks whether the string is a valid credit card number.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a valid credit card number, otherwise false.</returns>
        public static bool IsCreditCardNumber(this string @this) =>
            IsNumeric(@this) && RegexHelper.CreditCardNumberRegex().IsMatch(@this);

        /// <summary>
        /// Checks whether the string is not a valid credit card number.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is not a valid credit card number, otherwise false.</returns>
        public static bool IsNotCreditCardNumber(this string @this) =>
            !IsCreditCardNumber(@this);
        #endregion

        #region To Extensions

        /// <summary>
        /// Converts the specified string to a decimal number.
        /// </summary>
        /// <param name="this">The string to be converted.</param>
        /// <returns>A decimal number representation of the specified string, or 0 if the conversion fails.</returns>
        public static decimal ToDecimal(this string @this)
        {
            if (!@this.IsNullOrEmpty())
            {
                if (decimal.TryParse(@this, out decimal converted))
                    return converted;
            }

            return 0;
        }

        /// <summary>
        /// Converts the specified string to a FileInfo object.
        /// </summary>
        /// <param name="this">The string to be converted.</param>
        /// <returns>A FileInfo object representing the specified string.</returns>
        public static FileInfo ToFileInfo(this string @this) =>
            new(@this);

        /// <summary>
        /// Converts the specified string to a Guid.
        /// </summary>
        /// <param name="this">The string to be converted.</param>
        /// <returns>The Guid representation of the string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the specified string is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the specified string cannot be parsed as a Guid.</exception>
        public static Guid ToGuid(this string @this)
        {
            if (@this.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(@this));

            if (Guid.TryParse(@this, out var guidResult))
                return guidResult;

            throw new InvalidOperationException(nameof(@this));
        }

        /// <summary>
        /// Converts the string representation of a key to an instance of the specified LikeEnum type.
        /// </summary>
        /// <typeparam name="TLikeEnum">The type of the LikeEnum.</typeparam>
        /// <param name="key">The string representation of the key.</param>
        /// <returns>An instance of the specified LikeEnum type if a match is found; otherwise, null.</returns>
        public static LikeEnum? ToLikeEnum<TLikeEnum>(this string key) where TLikeEnum : LikeEnum =>
            LikeEnum.GetAll<TLikeEnum>().FirstOrDefault(p => p.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Converts the specified string to a valid DateTime object or returns null if the conversion fails.
        /// </summary>
        /// <param name="this">The string to be converted.</param>
        /// <returns>A DateTime object representing the specified string, or null if the conversion fails.</returns>
        public static DateTime? ToValidDateTimeOrNull(this string @this)
        {
            DateTime date;

            if (DateTime.TryParse(@this, out date))
            {
                return date;
            }

            return null;
        }

        /// <summary>
        /// Converts the specified string to an XDocument object.
        /// </summary>
        /// <param name="this">The string to be converted.</param>
        /// <returns>An XDocument object representing the specified string.</returns>
        public static XDocument ToXDocument(this string @this)
        {
            Encoding encoding = Activator.CreateInstance<ASCIIEncoding>();
            using (var ms = new MemoryStream(encoding.GetBytes(@this)))
            {
                return XDocument.Load(ms);
            }
        }

        /// <summary>
        /// Converts the specified string to an XmlDocument object.
        /// </summary>
        /// <param name="this">The string to be converted.</param>
        /// <returns>An XmlDocument object representing the specified string.</returns>
        public static XmlDocument ToXmlDocument(this string @this)
        {
            var doc = new XmlDocument();
            doc.LoadXml(@this);
            return doc;
        }

        #endregion

        #region Manipulating Extensions
        /// <summary>
        /// Removes specified characters from the beginning of a string.
        /// </summary>
        /// <param name="this">The source string.</param>
        /// <param name="chars">The characters to be removed.</param>
        /// <returns>A new string with the specified characters removed from the beginning.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source string is null or empty.</exception>
        public static string RemoveFirstChars(this string @this, params string[] chars)
        {
            if (string.IsNullOrEmpty(@this))
                throw new ArgumentNullException($"Invalid string");

            var result = @this;

            chars.ToList().ForEach(str =>
            {
                if (str == result.Substring(0, 1))
                    result = result.Substring(1, result.Length - 1);
            });

            return result;
        }

        /// <summary>
        /// Replaces the first character of a string with a new character.
        /// </summary>
        /// <param name="this">The source string.</param>
        /// <param name="replaceThis">The character to be replaced.</param>
        /// <param name="replaceWith">The character to replace with.</param>
        /// <returns>A new string with the first character replaced.</returns>
        public static string ReplaceFirstChar(this string @this, char replaceThis, char replaceWith)
        {
            if (@this.IsNullOrEmpty())
                return @this;

            var resultCharArr = @this.ToCharArray();
            var isFirstCharAmpersand = resultCharArr[0] == '&';

            if (isFirstCharAmpersand)
                resultCharArr[0] = '?';

            return new string(resultCharArr);

        }

        /// <summary>
        /// Replaces multiple substrings in a string based on specified patterns and replacements.
        /// </summary>
        /// <param name="this">The source string.</param>
        /// <param name="sourceDatas">A list of patterns and replacements.</param>
        /// <returns>A new string with the specified substrings replaced.</returns>
        public static string Manipulate(this string @this, List<(string, string)> sourceDatas) =>
            @this.Manipulate(sourceDatas.ToArray());

        /// <summary>
        /// Replaces multiple substrings in a string based on specified patterns and replacements.
        /// </summary>
        /// <param name="this">The source string.</param>
        /// <param name="sourceDatas">An array of patterns and replacements.</param>
        /// <returns>A new string with the specified substrings replaced.</returns>
        public static string Manipulate(this string @this, params (string, string)[] sourceDatas)
        {
            var result = @this ?? throw new ArgumentNullException(nameof(@this));

            for (int i = 0; i < sourceDatas.Length; i++)
            {
                if (sourceDatas[i].IsNull()
                    || sourceDatas[i].Item1.IsNull()
                    || sourceDatas[i].Item2.IsNull())
                    break;

                if (Regex.IsMatch(result, sourceDatas[i].Item1))
                    result = Regex.Replace(result, sourceDatas[i].Item1, sourceDatas[i].Item2);
            }

            return result;
        }

        /// <summary>
        /// Splits the specified string into an array of substrings based on a specified separator.
        /// </summary>
        /// <param name="this">The string to be split.</param>
        /// <param name="separator">The separator string.</param>
        /// <param name="option">Options for removing empty entries from the result (default: StringSplitOptions.None).</param>
        /// <returns>An array of substrings.</returns>
        public static string[] Split(this string @this, string separator, StringSplitOptions option = StringSplitOptions.None)
        {
            return @this.Split(new[] { separator }, option);
        }
        #endregion

        /// <summary>
        /// Extracts the domain from an email address.
        /// </summary>
        /// <param name="email">The email address.</param>
        /// <returns>The domain portion of the email address.</returns>
        /// <exception cref="Exception">Thrown if the email is null or invalid.</exception>
        public static string ExtractDomainFromEmail(this string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception("Email is null");

            if (!email.IsEmail())
                throw new Exception($"{email} > Email is invalid");

            int indexOfAt = email.IndexOf('@');
            return email.Substring(indexOfAt + 1);
        }
    }
}
