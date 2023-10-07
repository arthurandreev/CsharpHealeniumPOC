Feature: HomePageTests

 Scenario: Successfully navigate to Angular Material page from Home page
    Given the user is on the Home page
    When the user clicks on the Angular Material button
    Then a new tab should open with the Angular Material page
    And the page title should be "Angular Material UI component library"