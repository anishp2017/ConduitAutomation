Feature: SignInPageFeatures
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Verify Sign in page
	Given I am on the sign in page
	Then I should see the navigation bar at the top of the page for Sign In page
	And I should see the brand link on the navigation bar for Sign In page
	And I should see the links "Home|Sign in|Sign up" on the right side of the navigation bar for Sign In page
	And I should see "Sign In" as the header for the page
	And I should see "Need an account?" link
	And I should see a field to input email address
	And I should see a field to input password
	And I should see a "Sign in" button
