Feature: SignUpPageFeatures
	In order to contribute knowledge
	As a new user
	I want to create an account

@regression
Scenario: Verify that invalid email addresses are not allowed
	Given I am on the Sign up page
	When I enter "asdfasdfasdfasdf" for Email
	And I click on the Sign up button
	Then I will get an alert message

Scenario: Verify that a duplicate username cannot be created
	Given I am on the Sign up page
	And user "QAUser1" exists
	When I enter "QAUser1" for Username
	And I enter "QAUser1@gmail.com" for Email
	And I enter "test" for password on the sign up page
	And I click on the Sign up button
	Then I will get an error message "Username in use"