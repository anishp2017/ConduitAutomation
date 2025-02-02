﻿Feature: SignInPageFeatures
	In order to personalize my experience
	As a user
	I want to sign in to the application

@smoke
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

@regression
Scenario: Verify that sign in works
	Given I am on the sign in page
	When I enter "QAUser1@gmail.com" for email
	And I enter "test" for password
	And I click the Sign in button
	Then I will be redirected to the home page
	And I should see the links "Home| New Post| Settings|QAUser1" on the right side of the navigation bar for Sign In page