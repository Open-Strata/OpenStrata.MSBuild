// Copyright (c) 74Bravo LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project or repository root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenStrata.Solution.Publisher
{
    public static class PublisherTools
    {

        public static string GenerateOptionValuePrefixForPublisher(string customizationPrefix)
        {
            if (customizationPrefix == null)
                throw new ArgumentNullException(nameof(customizationPrefix));
            return customizationPrefix.ToUpper(CultureInfo.InvariantCulture).Equals("new", StringComparison.InvariantCultureIgnoreCase) ? 10000.ToString((IFormatProvider)CultureInfo.InvariantCulture) : GenerateOptionValuePrefixForPublisherInternal(customizationPrefix.GetDeterministicHashCode());
        }


        private static string GenerateOptionValuePrefixForPublisherInternal(
         int customizationPrefixHashCode)
        {
            return (Math.Abs(customizationPrefixHashCode % 90000) + 10000).ToString((IFormatProvider)CultureInfo.InvariantCulture);
        }


        public static bool FixedSolutionUniqueName(string uniqueName, out string fixedUniqueName, out string message, bool dontFixJustWarn = false, string invalidCharacterDelimter = "", bool fixNamesThatStartWithNumbers = true)
        {

            fixedUniqueName = null;
            message = null;

            // TODO: write code to fix publisher name
            // Only characters within the ranges [A-Z], [a-z] or [0-9] or _ are allowed.  The first character may only be in the ranges [A-Z], [a-z] or _.Detail: 

            var uniqueNameRule = "Only characters within the ranges[A - Z], [a-z] or[0 - 9] or _ are allowed.  The first character may only be in the ranges[A - Z], [a - z] or _";

            var pattern = @"[a-zA-Z0-9_]+";
            var rgx = new Regex(pattern);

            var matchDelimeter = "";

            var fixedNameBuilder = new StringBuilder();

            var matches = rgx.Matches(uniqueName);

            if (matches.Count == 0)
            {

                message = new StringBuilder()
                    .AppendLine($"The provided dataverse solution unique name \"{uniqueName}\" is not valid")
                    .AppendLine(uniqueNameRule)
                    .ToString();

                return false;
            }
            else if (dontFixJustWarn)
            {
                message = new StringBuilder()
                    .AppendLine($"The provided dataverse solution unique name \"{uniqueName}\" contains invalid characters.")
                    .AppendLine(uniqueNameRule)
                    .ToString();

                return false;
            }


            foreach (Match match in matches)
            {
                fixedNameBuilder.Append($"{matchDelimeter}{match.Value}");
                matchDelimeter = invalidCharacterDelimter;
            }

            var workingUniqueName = fixedNameBuilder.ToString();

            //Check to see if first character is number.

            var firstChar = workingUniqueName.Substring(0, 1);

            if (int.TryParse(firstChar, out int success))
            {
                if (fixNamesThatStartWithNumbers)
                {
                    workingUniqueName = $"_{workingUniqueName}";
                }
                else
                {

                    message = new StringBuilder()
                        .AppendLine($"The Dataverse Solution unique name cannot start with an integer.  \"{uniqueName}\" is not valid.  Modify the name or set the UseUnderScorePrefixWhenNameStartsWithNumber parameter to true ")
                        .AppendLine(uniqueNameRule)
                        .ToString();

                    return false;
                }
            }

            // fixedUniqueName = workingUniqueName.ToLower();
            fixedUniqueName = workingUniqueName.ToLower();
            return true;

        }



        public static bool FixedPublisherUniqueName(string uniqueName, out string fixedUniqueName, out string message, bool dontFixJustWarn = false, string invalidCharacterDelimter = "", bool fixNamesThatStartWithNumbers = true)
        {

                    fixedUniqueName = null;
                    message = null;

            // TODO: write code to fix publisher name
            // Only characters within the ranges [A-Z], [a-z] or [0-9] or _ are allowed.  The first character may only be in the ranges [A-Z], [a-z] or _.Detail: 

                var uniqueNameRule = "Only characters within the ranges[A - Z], [a-z] or[0 - 9] or _ are allowed.  The first character may only be in the ranges[A - Z], [a - z] or _";

                var pattern = @"[a-zA-Z0-9_]+";
                var rgx = new Regex(pattern);

                var matchDelimeter = "";

                var fixedNameBuilder = new StringBuilder();

                var matches = rgx.Matches(uniqueName);

                    if (matches.Count == 0)
                    {

                        message = new StringBuilder()
                            .AppendLine($"The provided publisher unique name \"{uniqueName}\" is not valid")
                            .AppendLine(uniqueNameRule)
                            .ToString();

                        return false;
                    }
                    else if (dontFixJustWarn)
                    {
                        message = new StringBuilder()
                            .AppendLine($"The provided publisher unique name \"{uniqueName}\" contains invalide characters.")
                            .AppendLine(uniqueNameRule)
                            .ToString();

                          return false;
                    }


                    foreach (Match match in matches)
                    {
                        fixedNameBuilder.Append($"{matchDelimeter}{match.Value}");
                        matchDelimeter = invalidCharacterDelimter;
                    }

                    var workingUniqueName = fixedNameBuilder.ToString();

                    //Check to see if first character is number.

                    var firstChar = workingUniqueName.Substring(0, 1);

                    if (int.TryParse(firstChar, out int success))
                    {
                        if (fixNamesThatStartWithNumbers)
                        {
                            workingUniqueName = $"_{workingUniqueName}";
                        }
                        else
                        {

                        message = new StringBuilder()
                            .AppendLine ($"The publisher unique name cannot start with an integer.  \"{uniqueName}\" is not valid.  Modify the name or set the UseUnderScorePrefixWhenNameStartsWithNumber parameter to true ")
                            .AppendLine (uniqueNameRule)
                            .ToString();

                            return false;
                        }
                    }

            // fixedUniqueName = workingUniqueName.ToLower();
            fixedUniqueName = workingUniqueName.ToLower();
            return true;

        }



        // This is the string GetHashCode algorithm from Microsoft.NET\Framework64\v4.0.30319\mscorlib.dll
        // The UseRandomized option is removed.

        [SecuritySafeCritical]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static unsafe int GetDeterministicHashCode(this string str)
        {
            //if (HashHelpers.s_UseRandomizedStringHashing)
            //    return string.InternalMarvin32HashString(this, this.Length, 0L);
            fixed (char* chPtr1 = str)
            {
                int num1 = 5381;
                int num2 = num1;
                int num3;
                for (char* chPtr2 = chPtr1; (num3 = (int)*chPtr2) != 0; chPtr2 += 2)
                {
                    num1 = (num1 << 5) + num1 ^ num3;
                    int num4 = (int)chPtr2[1];
                    if (num4 != 0)
                        num2 = (num2 << 5) + num2 ^ num4;
                    else
                        break;
                }
                return num1 + num2 * 1566083941;
            }
        }


    }
}
