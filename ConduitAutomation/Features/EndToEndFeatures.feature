Feature: EndToEndFeatures
	In order to verify the application
	As a QA Tester
	I want to ensure that all functionality behaves correctly

@end2end
Scenario: Verify article page when clicking on an article
	Given I am signed in with email "QAUser1@gmail.com" and password "test" endToEnd
	And I am on the home page endToEnd
	And The article "Test" exists in the system
	When I click the preview link for article "Test"
	Then I will land on the article page for "Test"
