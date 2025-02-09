Feature: Meal Plan Templates

    Background:
        Given User go to login page
        When User enter valid credentials
        And User select "Library"
        And User goes to "Meal Plan Templates" screen

    @library
    @mealplantemplates
    Scenario: Create private meal Plan with all fields
        Given User click Create Meal Plan
        When User enter meal plan name
        And User upload Cover Image
        And User enter Number Of Weeks
        And User choose Owner option
        And User choose Share with org option
        And User click Create button
        Then User is in Meal Plan details screen
        And Meal Plan have "Draft" status

    @library
    @mealplantemplates
    Scenario: Update draft meal plan name
        Given Existing meal plan with
          | CoverImage           | MealPlanName | NumberOfWeeks | Owner                    | ShareWithOrg |
          | TestData/testimg.jpg | ATE_MEALPLAN | 2             | 6569b222973988001f31b804 | 0            |
        When User goes to Draft screen
        And User goes to meal plan details
        And User click Edit Info button
        And User update new name for meal plan
          | MealPlanName         |
          | ATE_MEALPLAN _UPDATE |
        Then Meal Plan name updated successfully

    @library
    @mealplantemplates
    Scenario: Delete draft meal plan
        Given Existing meal plan with
          | CoverImage           | MealPlanName | NumberOfWeeks | Owner                    | ShareWithOrg |
          | TestData/testimg.jpg | ATE_MEALPLAN | 2             | 6569b222973988001f31b804 | 0            |
        When User goes to Draft screen
        And User click "Remove" in context menu
        And User Confirm remove meal plan