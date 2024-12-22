# Task Overview

## Competencies

- 4041.4.1 : Database and File Server Applications
The graduate produces database and file server applications using advanced constructs in a high-level programming language to meet business requirements.

- 4041.4.2 : Lambda
The graduate incorporates lambda expressions in application development to meet business requirements more efficiently.

- 4041.4.3 : Collections
The graduate incorporates nongeneric collections and generic collections in application development to manipulate data more efficiently.

- 4041.4.4 : Localization and Globalization
The graduate applies application programming interfaces (APIs) in application development to support end users in various geographic regions.

- 4041.4.5 : Advanced Exception Control
The graduate incorporates advanced exception control mechanisms in application development for improving user experience and application stability.

## Introduction

Throughout your career in software design and development, you will be asked to create applications with various features and criteria based on a variety of business requirements. For this assessment, you will create your own C# application with requirements that will imitate those you will encounter in a real-world job assignment.

The skills you will demonstrate in this assessment are also directly relevant to technical interview questions for future employment. The C# application that you create should become a portfolio piece that you show to future employers.

Several supporting documents and links have been included to help you complete this task. The attached “Database ERD” document shows the entity relationship diagram (ERD) for this database, which you can reference as you create your application.

You will also use the “Performance Assessment Lab Area” web link to access the virtual lab environment needed to complete this task. The preferred integrated development environment (IDE) for this assignment is Visual Studio. If you choose to use another IDE, you must export your project into Visual Studio format for submission.

Your submission should include a ZIP file with all the necessary code files to compile, support, and run your application. The ZIP file submission must also keep the project file and folder structure intact for the Visual Studio IDE.

## Scenario

You are working for a software company that has been contracted to develop a scheduling desktop user interface application. The contract is with a global consulting organization that conducts business in multiple languages and has main offices in the following locations: Phoenix, Arizona; New York, New York; and London, England. The consulting organization has provided a MySQL database that your C# application must pull data from. However, this database is used for other systems, so its structure cannot be modified.

The organization has outlined specific business requirements that must be included as part of the application. From these requirements, a system analyst at your company created solution statements for you to implement when developing the application. These statements are listed in the “Requirements” section.

## Requirements

Your submission must be your original work. No more than a combined total of 30% of the submission and no more than a 10% match to any one individual source can be directly quoted or closely paraphrased from sources, even if cited correctly. The similarity report that is provided when you submit your task can be used as a guide.

You must use the rubric to direct the creation of your submission because it provides detailed criteria that will be used to evaluate your work. Each requirement below may be evaluated by more than one rubric aspect. The rubric aspect titles may contain hyperlinks to relevant portions of the course.

Tasks may not be submitted as cloud links, such as links to Google Docs, Google Slides, OneDrive, etc., unless specified in the task requirements. All other submissions must be file types that are uploaded and submitted as attachments (e.g., .docx, .pdf, .ppt).

Note: You are not allowed to use frameworks or external libraries, except for the .NET Framework. The database does not contain data, so it needs to be populated. The word “test” must be used as the username and password to login to the C# application.

1. Create an application by completing the following tasks in C#:
    1. Create a login form that has the ability to do the following:
        1. Determine a user’s location.
        2. Translate login and error control messages (e.g., “The username and password do not match.”) into English and one additional language.
        3. Verify the correct username and password.
    2. Provide the ability to add, update, and delete customer records.
        1. Validate each of the following requirements for customer records:
            - that a customer record includes name, address, and phone number fields
            - that fields are trimmed and non-empty
            - that the phone number field allows only digits and dashes
        2. Add exception handling that can be used when performing each of the following operations for customer records:
            - “add” operations
            - “update” operations
            - “delete database” operations
    3. Provide the ability to add, update, and delete appointments, capture the type of appointment, and link to a specific customer record in the database.
        1. Validate each of the following requirements for appointments:
            - Require appointments to be scheduled during the business hours of 9:00 a.m. to 5:00 p.m., Monday–Friday, eastern standard time.
            - Prevent the scheduling of overlapping appointments.
        2. Add exception handling that can be used when performing each of the following operations for appointments:
            - “add” operations
            - “update” operations
            - “delete database” operations
    4. Create a calendar view feature, including the ability to view appointments on a specific day by selecting a day of the month from a calendar of the months of the year.
    5. Provide the ability to automatically adjust appointment times based on user time zones and daylight saving time.
    6. Create a function that generates an alert whenever a user who has an appointment within 15 minutes logs in to their account.
    7. Create a function that allows users to generate the three reports listed using collection classes, incorporating a lambda expression into the code for each of the following reports:
        - the number of appointment types by month
        - the schedule for each user
        - one additional report of your choice
    8. Record the timestamp and the username of each login in a text file named “Login_History.txt,” ensuring that each new record is appended to the log file.
2. Submit the project by doing the following:
    1. Export the project in Visual Studio format.
    2. Export your project from the IDE as a ZIP file.
3. Demonstrate professional communication in the content and presentation of your submission.
