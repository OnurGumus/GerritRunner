Feature: Download changes

Scenario: Valid GerritConfig
Given 3 changes fetched
When the download flow started
And then 3 changes should be downloaded
Then total of 3 changes should be fetched
And Some long time passes
Then total of 4 changes should be fetched
