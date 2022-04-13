Feature: Execute changes

Scenario: Execute changes all pass
Given 3 changes downloaded
When the execute flow started
Then when 0 fail 3 pass
Then total of 3 v+ and 0 v- requests should be sent

Scenario: Execute changes all not pass
Given 3 changes downloaded
When the execute flow started
Then when 1 fail 2 pass
Then total of 2 v+ and 1 v- requests should be sent