Feature: EndToEndFeatures
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@end2end
Scenario: Verify article page when clicking on an article
	Given I am signed in with email "QAUser1@gmail.com" and password "test" endToEnd
	And I am on the home page endToEnd
	And The article "Test" exists in the system
	When I click the preview link for article "Test"
	Then I will land on the article page for "Test"
