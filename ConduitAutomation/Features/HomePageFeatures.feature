Feature: HomePageFeatures
	In order to gain knowledge
	As a curious person
	I want to access various articles

@smoke
Scenario: Verify navigation bar
	Given I am on the home page
	Then I should see the navigation bar at the top of the page
	And I should see the brand link on the navigation bar
	And I should see the links "Home|Sign in|Sign up" on the right side of the navigation bar
	And I should see the banner

@smoke
@regression
Scenario: Verify article list
	Given I am on the home page
	And There is at least one article in the system
	Then I should see "Global Feed" tab
	And I should see article preview

@regression
Scenario: Verify that clicking a tag shows only articles with that tag
	Given I am on the home page
	And There is at least one article in the system having tag "test"
	When I click the "test" tag
	Then I should see article previews for articles having tag "test"