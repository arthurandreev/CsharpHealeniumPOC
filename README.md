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
```
 <a class="card" target="_blank" rel="noopener" href="https://material.angular.io" id="angular-material">
      <svg xmlns="http://www.w3.org/2000/svg" style="margin-right: 8px" width="21.813" height="23.453" viewBox="0 0 179.2 192.7"><path fill="#ffa726" d="M89.4 0 0 32l13.5 118.4 75.9 42.3 76-42.3L179.2 32 89.4 0z"/><path fill="#fb8c00" d="M89.4 0v192.7l76-42.3L179.2 32 89.4 0z"/><path fill="#ffe0b2" d="m102.9 146.3-63.3-30.5 36.3-22.4 63.7 30.6-36.7 22.3z"/><path fill="#fff3e0" d="M102.9 122.8 39.6 92.2l36.3-22.3 63.7 30.6-36.7 22.3z"/><path fill="#fff" d="M102.9 99.3 39.6 68.7l36.3-22.4 63.7 30.6-36.7 22.4z"/></svg>
      <span>Angular Material</span>
      <svg class="material-icons" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><path d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z"/></svg>
    </a>
```
Another change I made is to the angular.json configuration file. I set the host to "0.0.0.0" (allowing external access), port to 4200, and allowed all hosts. This change was required to accomodate docker. <br>

``` 
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

``` 

1. docker-compose -f docker-compose-selenoid.yaml up -d

2. docker pull selenoid/vnc:chrome_111.0

```

The first command pulls in all the required docker images and start running containers based on these images. <br>

The second command pulls a chrome image of version 111.0. <br> 

Make sure that whichever image version you specify in this docker command match with your chrome browser settings outlined in the browsers json file located here - ..\healenium-backend\selenoid-config

 ```

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

``` 

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

selenoid-ui-1, selenoid-1, hlm-proxy, selector-imitator, postgres-db, healenium. Chrome container will run during a test run only and does not run contantly like the other containers. <br> 

### Angular <br>

You have angular application under test running on localhost 4200 <br>

### Running tests

Run the test by navigating to test explorer and clicking Run. There is only one scenario in the single Feature file. <br>

### First run - make sure the button id #angular-material is the same in the angular application and in your test project. 

This is required for healenium to capture a baseline image from a passing test. 

### Second run - change the button id in the angular project AND then re run the test. Notice that test still passes. 

### Third run with debug - place a breakpoint on the second line in the scenario in the Feature file. 

Open http://localhost:8080, run the test with debug and watch a selenoid chrome session appear in the selenoid ui. <br>

As you step through the scenario, you can watch selenium interacting with a chrome browser in the selenoid docker container. <br>
