Feature: Download changes

Scenario: Valid GerritConfig
Given 3 changes fetched
When the download flow started
Then 3 changes should be downloaded
When 1 new change arrives
And after some long time passes
Then total of 4 changes should be fetched
