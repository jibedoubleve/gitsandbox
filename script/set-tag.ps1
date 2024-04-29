$ver = $(dn gitversion | ConvertFrom-Json).SemVer

Write-Host "Setting tag '$ver'"

git tag $ver
