Feature: AtClientTests
	Invoking the KPI test scenarios

@AT_KPI
Scenario: Random test
	Given Invoke random test
	Then Response status code is 200
	And Response test result is Success