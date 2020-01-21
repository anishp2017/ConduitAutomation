Feature: HomePageFeatures
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Verify navigation bar when not signed in
	Given I am on the home page
	And I am not signed in
	Then I should see the navigation bar at the top of the page
	And I should see the brand link on the navigation bar
	And I should see the links "Home|Sign in|Sign up" on the right side of the navigation bar
	And I should see the banner

Scenario: Verify article list when not signed in
	Given I am on the home page
	And I am not signed in
	And There is at least one article in the system
	Then I should see "Global Feed" tab
	And I should see article preview

Scenario: Verify navigation bar when signed in
	Given I am on the home page
	And I am signed in with email "QAUser1@gmail.com" and password "test"
	Then I should see the navigation bar at the top of the page
	And I should see the brand link on the navigation bar
	And I should see the links "Home| New Post| Settings|QAUser1" on the right side of the navigation bar
