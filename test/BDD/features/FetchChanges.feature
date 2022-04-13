Feature: Fetch changes

Scenario: Valid GerritConfig
Given A valid gerrit configuration 
And there are 3 changes
When the fetch flow started
Then total of 3 changes should be fetched
When another change is added
Then total of 4 changes should be fetched

Scenario: Invalid GerritConfig
Given an invalid gerrit configuration 
And there are 3 changes
When the fetch flow started
Then application should error out 'invalid config'