param(
    [Parameter(Mandatory = $false)]
    [string]$BaseUrl = "https://localhost:5001"
)

Write-Host "GET $BaseUrl/authors"
Invoke-RestMethod -Method Get -Uri "$BaseUrl/authors" | ConvertTo-Json -Depth 5

Write-Host "GET $BaseUrl/books"
Invoke-RestMethod -Method Get -Uri "$BaseUrl/books" | ConvertTo-Json -Depth 5

Write-Host "POST $BaseUrl/loans"
Invoke-RestMethod -Method Post -Uri "$BaseUrl/loans" -ContentType "application/json" -Body '{"bookId":1}' | ConvertTo-Json -Depth 5

