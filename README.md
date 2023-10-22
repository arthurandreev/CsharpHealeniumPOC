# Intro
I have come across a machine learning powered library called Healenium that integrates seamlessly with Selenium and solves a major test automation headache of regression tests failling due to small changes in web locators. 

Below you will find information on how to build and run the project. You will also find explanations on key components and code blocks in the project. 

# Project overview
This project has been created to showcase the capability of integrating Healenium library into a DotNet Selenium project. 

This is a proof of concept project to showcase machine learning capability that can be integrated into a Selenium DotNet project. These capabilities are provided by an open source project called Healenium. Healenium is language agnostic and can be used with other languages such as Java. I have another POC project which I built with Java. It is called MLPoweredSeleniumJavaPOC and you can find it in my pinned repositories.

# What is Healenium
Healenium is an open-source library that aims to reduce the maintenance efforts required for Selenium-based UI tests by providing self-healing capabilities. 
It identifies the elements that were not found during test execution and tries to locate them using alternative locators. 
Healenium then provides recommendations to update the locator strategy for the elements that were not found.
When a test fails due to an element not being found, Healenium will search for the element using different attributes or by comparing the element's structure to what it was in the past. 
If it finds a match, it will update the locator information at run time and proceed with running the test using the updated web locator. 
The test will pass if the element is found, and Healenium will log the updated locator information for future reference.
## Benefits of Healenium
The main benefit of using Healenium is that it helps reduce the time and effort spent on maintaining and updating UI tests when the application under test undergoes changes in its user interface. 
By automatically adapting to changes in the UI, Healenium can reduce the number of false-negative test results caused by outdated locators.
Healenium can be used as a plugin for Java-based Selenium projects and is available on GitHub. It can also be used with other programming languages such as C# and Javascript.
To use it, you need to add Healenium as a dependency in your project, add small codes changes to your web driver and make some configuration changes. 
More information and usage instructions can be found in the official documentation: https://healenium.io/

# Introduction 

There are three self contained projects in this solution:

Application under test - my-angular-app <br>

Healenium backend - healenium-backend <br>

Selenium based test project - SelfHealeningSelenium 

# Getting Started

my-angular-app is based on a starter project for angular which can be found here - https://angular.io/guide/setup-local <br>

I made some changes to the starter project. I added an id attribute #angular-material to one of the buttons in app.component.html as per below <br>
```shell
 <a class="card" target="_blank" rel="noopener" href="https://material.angular.io" id="angular-material">
      <svg xmlns="http://www.w3.org/2000/svg" style="margin-right: 8px" width="21.813" height="23.453" viewBox="0 0 179.2 192.7"><path fill="#ffa726" d="M89.4 0 0 32l13.5 118.4 75.9 42.3 76-42.3L179.2 32 89.4 0z"/><path fill="#fb8c00" d="M89.4 0v192.7l76-42.3L179.2 32 89.4 0z"/><path fill="#ffe0b2" d="m102.9 146.3-63.3-30.5 36.3-22.4 63.7 30.6-36.7 22.3z"/><path fill="#fff3e0" d="M102.9 122.8 39.6 92.2l36.3-22.3 63.7 30.6-36.7 22.3z"/><path fill="#fff" d="M102.9 99.3 39.6 68.7l36.3-22.4 63.7 30.6-36.7 22.4z"/></svg>
      <span>Angular Material</span>
      <svg class="material-icons" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z"/></svg>
    </a>
```
Another change I made is to the angular.json configuration file. I set the host to "0.0.0.0" (allowing external access), port to 4200, and allowed all hosts. This change was required to accomodate docker. <br>

```shell
        "serve": {

          "builder": "@angular-devkit/build-angular:dev-server",

          "options": {

            "host": "0.0.0.0",

            "port": 4200,

            "allowedHosts": ["all"]

          },

          "configurations": {

            "production": {

              "browserTarget": "my-app:build:production"

            },

            "development": {

              "browserTarget": "my-app:build:development"

            }

          },

          "defaultConfiguration": "development"

        },
```

healenium-backend is based on a sample project of using healenium with dotnet which can be found here - https://github.com/healenium/healenium-example-dotnet <br>

SelfHealingSelenium is a Selenium test project which contains a single test. There is a click button action that interacts with the button that has our #angular-material. Please check out the Feature file in the project <br>

# Build and Test

## Prerequisites

### Tooling

Docker desktop is installed <br>

Docker is running(open the docker desktop application) <br>

Angular is installed(you will need it to run the angular app on your local machine) <br>

 

### Code

Healenium backend - healenium-backend <br>

Navigate to the healenium-backend folder in CMD or in developer powershell and run the following commands: <br>

```shell

1. docker-compose -f docker-compose-selenoid.yaml up -d

2. docker pull selenoid/vnc:chrome_111.0

```

The first command pulls in all the required docker images and start running containers based on these images. <be>

The second command pulls a chrome image of version 111.0. <br> 

Make sure that whichever image version you specify in this docker command match with your chrome browser settings outlined in the browsers json file located here - ..\healenium-backend\selenoid-config

 ```shell

    "chrome": {

        "default": "111.0",

        "versions": {

            "111.0": {

                "image": "selenoid/vnc:chrome_111.0",

                "port": "4444"

            },

            "110.0": {

                "image": "selenoid/vnc:chrome_110.0",

                "port": "4444"

            }

        }

    }
```

Application under test - my-angular-app <br>

Navigate to the folder that contains the angualr project and run the following command in CMD: <br>

```shell

ng serve --open 

``` 

This command will start angular server and launch the application on localhost 4200. 

 
Selenium based test project - SelfHealeningSelenium  <br>

Before proceeding please make sure the following prerequisites have been met:

### Docker <br>

The required docker images are installed and containers are running. <br>

You should have the following images installed: <br>

postgres, healenium/hlm-proxy, healenium/hlm-backend, aerokube/selenoid, healenium/hlm-selector-imitator, aerokube/selenoid-ui, selenoid/vnc <br>

You have have the following containers running: <br>

selenoid-ui-1, selenoid-1, hlm-proxy, selector-imitator, postgres-db, healenium. Chrome container will run during a test run only and does not run constantly like the other containers. <br> 
![image](https://github.com/arthurandreev/MLPoweredSeleniumDotNetPOC/assets/35194143/950fd165-9bce-405a-8241-3b0c2bdf08b3)
![image](https://github.com/arthurandreev/MLPoweredSeleniumDotNetPOC/assets/35194143/d136455c-d661-4a65-9d5d-f39b9556b83a)

### Angular <br>

You have angular application under test running on localhost 4200 <br>

### Running tests

Run the test by navigating to test explorer and clicking Run. There is only one scenario in the single Feature file. <br>

### First run - make sure the button id #angular-material is the same in the angular application and in your test project. 

This is required for healenium to capture a baseline image from a passing test. 

### Second run - change the button id in the angular project AND then re run the test. Notice that test still passes. 

### Third run with debug - place a breakpoint on the second line in the scenario in the Feature file. 

Open http://localhost:8080, run the test with debug and watch a selenoid chrome session appear in the selenoid ui. <br>

As you step through the scenario, you can watch selenium interacting with a chrome browser in the selenoid docker container. <be>

# DEMO 

## Use case - change in an id used to locate a button breaks a test
The id that my navigateToAngularMaterialPageTest is using to click on the button that takes me to a new tab has been changed and this will normally make my test fail with no element found exception. The web element itself hasn't changed but because the id has been changed it will make the tests that rely on it to fail resulting in a failed regression test. 

To simulate this scenario, firstly you need to run the test at least once when it passes to enable Healenium to save a snapshot of the elements on the page to a Postgres DB. In this scenario the web locators in the Selenium project and the angular application must match. This baseline snapshot image will be used in subsequent test runs to heal elements that face element not found exceptions. 

Selenium project
```shell
public class HomePage {

    private SelfHealingDriver driver;

    @FindBy(id = "angular-material")
    public WebElement angularMaterial;

    public HomePage(SelfHealingDriver driver) {
        this.driver = driver;
        PageFactory.initElements(driver, this);
    }
}
```

Angular project
```shell
 <a class="card" target="_blank" rel="noopener" href="https://material.angular.io" id="angular-material">
      <svg xmlns="http://www.w3.org/2000/svg" style="margin-right: 8px" width="21.813" height="23.453" viewBox="0 0 179.2 192.7"><path fill="#ffa726" d="M89.4 0 0 32l13.5 118.4 75.9 42.3 76-42.3L179.2 32 89.4 0z"/><path fill="#fb8c00" d="M89.4 0v192.7l76-42.3L179.2 32 89.4 0z"/><path fill="#ffe0b2" d="m102.9 146.3-63.3-30.5 36.3-22.4 63.7 30.6-36.7 22.3z"/><path fill="#fff3e0" d="M102.9 122.8 39.6 92.2l36.3-22.3 63.7 30.6-36.7 22.3z"/><path fill="#fff" d="M102.9 99.3 39.6 68.7l36.3-22.4 63.7 30.6-36.7 22.4z"/></svg>
      <span>Angular Material</span>
      <svg class="material-icons" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z"/></svg>
    </a>
```

To verify that the baseline snapshot of the elements that Selenium interacts with has been saved correctly, you can navigate to this url http://localhost:7878/healenium/selectors/ where you should see the following:
![image](https://github.com/arthurandreev/MLPoweredSeleniumJavaPOC/assets/35194143/655fbf76-2496-4c55-b8a1-8c22512ec340)



Secondly, you need to navigate to app.component.html class in my-angular-app, change the id for the element below from angular-material to react material. Save this code change. Angular normally does a live reload and your id change is reflected immediatly in [localhost](http://localhost:4200/). However it is a good idea to inspect the element in dev tools to make sure the change has been reflected in the localhost as expected before continuing. 

Before the change
```shell
 <a class="card" target="_blank" rel="noopener" href="https://material.angular.io" id="angular-material">
      <svg xmlns="http://www.w3.org/2000/svg" style="margin-right: 8px" width="21.813" height="23.453" viewBox="0 0 179.2 192.7"><path fill="#ffa726" d="M89.4 0 0 32l13.5 118.4 75.9 42.3 76-42.3L179.2 32 89.4 0z"/><path fill="#fb8c00" d="M89.4 0v192.7l76-42.3L179.2 32 89.4 0z"/><path fill="#ffe0b2" d="m102.9 146.3-63.3-30.5 36.3-22.4 63.7 30.6-36.7 22.3z"/><path fill="#fff3e0" d="M102.9 122.8 39.6 92.2l36.3-22.3 63.7 30.6-36.7 22.3z"/><path fill="#fff" d="M102.9 99.3 39.6 68.7l36.3-22.4 63.7 30.6-36.7 22.4z"/></svg>
      <span>Angular Material</span>
      <svg class="material-icons" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z"/></svg>
    </a>
```
After the change
```shell
 <a class="card" target="_blank" rel="noopener" href="https://material.angular.io" id="react-material">
      <svg xmlns="http://www.w3.org/2000/svg" style="margin-right: 8px" width="21.813" height="23.453" viewBox="0 0 179.2 192.7"><path fill="#ffa726" d="M89.4 0 0 32l13.5 118.4 75.9 42.3 76-42.3L179.2 32 89.4 0z"/><path fill="#fb8c00" d="M89.4 0v192.7l76-42.3L179.2 32 89.4 0z"/><path fill="#ffe0b2" d="m102.9 146.3-63.3-30.5 36.3-22.4 63.7 30.6-36.7 22.3z"/><path fill="#fff3e0" d="M102.9 122.8 39.6 92.2l36.3-22.3 63.7 30.6-36.7 22.3z"/><path fill="#fff" d="M102.9 99.3 39.6 68.7l36.3-22.4 63.7 30.6-36.7 22.4z"/></svg>
      <span>Angular Material</span>
      <svg class="material-icons" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z"/></svg>
    </a>
```

Element under test
![image](https://user-images.githubusercontent.com/35194143/232344213-9ce1400a-9831-489c-a305-24fcf28f765c.png)
![image](https://user-images.githubusercontent.com/35194143/232346966-d58c01fc-ac08-42be-934e-0a1c5397ed8c.png)  

Test case  
![image](https://user-images.githubusercontent.com/35194143/233724460-99fedcc9-0f56-4a95-a93e-c7f9add1064c.png)    
![image](https://user-images.githubusercontent.com/35194143/233773830-b9c1ab70-b8cc-4aa1-becd-1e56028aad6e.png)  

## Scenario 1 
### Matching locator(#angular-material in both Angular and Selenium). Test passes.

![image](https://user-images.githubusercontent.com/35194143/232344657-25a1ea69-b5e4-473e-b17b-65767be4fca9.png)    
Angular  
![image](https://user-images.githubusercontent.com/35194143/232348107-a8fbc511-9e97-42f9-b75f-341498dee22a.png)    
Selenium   
![image](https://user-images.githubusercontent.com/35194143/232348144-aa9ecc7f-3f5f-4081-8018-e1102da6953b.png)  
Docker  
![image](https://user-images.githubusercontent.com/35194143/233727440-ba546c39-1daf-428f-b8cf-803c576a5b52.png)  

## Scenario 2
### Non matching locator(#react-material in Angular vs #angular-material in Selenium) AND self healing capability is switched OFF. Test fails with no such element exception.

Id is changed in the Angular project from #angular-material to #react-material
![image](https://user-images.githubusercontent.com/35194143/232347461-86fe0044-51ea-424d-8f84-bfd24af25793.png)  
![image](https://user-images.githubusercontent.com/35194143/232347359-28ec0ceb-ca8c-41b5-b36f-61280a58d430.png)  
![image](https://user-images.githubusercontent.com/35194143/232347447-e66165c3-1cde-4b44-9466-6ddc1917f765.png)  

## Scenario 3 
### Non matching locator(#react-material in Angular vs #angular-material in Selenium) AND self healing capability is switched ON. Test passes.

![image](https://user-images.githubusercontent.com/35194143/232345734-14672335-f6a0-4b5f-b808-c5fca9e2a825.png)  
Screenshot of the healed web element  
![image](https://user-images.githubusercontent.com/35194143/232347195-f5b458d7-eacc-45f2-83d8-95fc84a04fa4.png)  
Screenshots of all the healed elements can be found in this folder ..\SeleniumWithSelfHealingCodeGen\infra\screenshots

Verify that the element has been healed successfully by navigating to http://localhost:7878/healenium/report/ where you will see the following report along with a screenshot of the healed web element
![image](https://github.com/arthurandreev/MLPoweredSeleniumJavaPOC/assets/35194143/03148051-ce83-4276-b6c7-3bbaf77297a8)

With the standard Selenium implementation my test will fail in this scenario. With Healenium wrapper on top of WebDriver, it catches NoSuchElement exception, triggers the LSC algorithm, passes the current page state, gets previous successful locator path, compares them, and generates the list of healed locators. From this list, Healenium selects the locator with the highest score and proceeds to perform an action using this locator. Upon test completion, Healenium compiles a comprehensive report. This report contains detailed information about the healed locator, includes a screenshot illustrating the successful healing process, and offers a feedback button for users to provide insights on the healing success. 

More information on how to wrap WebDriver with Healenium can be found in WebDriverFactory class
![image](https://github.com/arthurandreev/MLPoweredSeleniumJavaPOC/assets/35194143/740f7119-258e-4410-827c-f789945cd428)

More information on Healenium can be found here - https://healenium.io/docs/how_healenium_works <br>
More information on the LSC algorithm can be found here - https://www.geeksforgeeks.org/longest-common-subsequence-dp-4/ 

Last thing to note is that Healenium needs a healenium.properties file in your solution where you configure its settings. Below is the healenium.properties file I have in my solution with explanation of each line.  
![image](https://user-images.githubusercontent.com/35194143/232717543-50ff96b5-bc21-43b6-995d-f5e124f1f4d1.png)
- recovery-tries=1: This line indicates the number of attempts Healenium should make to locate a missing element using its self-healing mechanism.  
- score-cap=.6: This sets the minimum similarity score (between 0 and 1) required for an alternative locator to be considered a match. The value of 0.6 means that Healenium will only consider locators with a similarity score of 60% or higher.  
- heal-enabled=true: This line enables or disables Healenium's self-healing feature. When set to true, Healenium's self-healing capabilities will be active.  
- serverHost=localhost: This line specifies the server host for the Healenium backend service. In this case, it is set to localhost, meaning the service will run on the same machine as the tests.  
- serverPort=7878: This line defines the port on which the Healenium backend service will listen. Here, it is set to port 7878.  
- imitatePort=8000: This line sets the port used for communication between the Healenium backend service and the client (Selenium tests). In this case, it is set to port 8000.  


