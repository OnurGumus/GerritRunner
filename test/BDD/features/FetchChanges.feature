Feature: FetchChanges

Scenario: Valid GerritConfig
Given A valid gerrit configuration 
And there are 3 changes
When Application started
Then Then the 3 changes should be fetched


