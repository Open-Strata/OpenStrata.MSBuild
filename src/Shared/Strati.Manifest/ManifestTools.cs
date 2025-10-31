using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenStrata.Strati.Manifest
{
    public static class ManifestTools
    {


        public static string GenerateManifestUniqueName(string packageId, string removeSuffix = "strati")
        {

            var pattern = @"[a-zA-Z0-9_]+";
            var rgx = new Regex(pattern);

            var matchDelimeter = "";

            var fixedNameBuilder = new StringBuilder();

            var matches = rgx.Matches(packageId);

            foreach (Match match in matches)
            {
                fixedNameBuilder.Append($"{matchDelimeter}{match.Value}");
            }

            var workingUniqueName = fixedNameBuilder
                .ToString()
                .ToLower();

            if (workingUniqueName.EndsWith(removeSuffix))
                workingUniqueName = workingUniqueName.Substring(0, workingUniqueName.Length - removeSuffix.Length);

            return workingUniqueName;

        }




    }
}
