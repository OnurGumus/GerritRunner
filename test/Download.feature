Feature: Process changes


Scenario: Start up
When Application started
Then Temp folders should be clean

Scenario: Fetch changes success
Given a config
When Application started
Then It should fetch changes


