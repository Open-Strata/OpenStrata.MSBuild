

$localFeedRegEx = '[0-9]+\.[ ]+local[ ]+\[Enabled\][\W]+(?<localFeeDir>[\w:\\]+)\n'

$localFeedRegEx = '[0-9]+\.[ ]+local[ ]+\[Enabled\][\W]+([\w:\\]+)'


$matches = dotnet nuget list source | Out-String | select-string -Pattern $localFeedRegEx 

$matches.Matches.Groups[1].Value