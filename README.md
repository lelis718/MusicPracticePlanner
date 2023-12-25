
# Music Practice Planner 

This is a project to gather knowledge on the Domain Driven Design Architecture based on .Net Core - Not intended to be published by anyways


### Description

A project to keep track on the piano practice sessions

The project consists on provide a way to keep track on the practice sessions of the pianist monitoring when he starts to practice some music and give him a time to practice on.



### Features

* FEAT-01: Student will be able to upload the music and the sheet music to the software

* FEAT-02: Student will be able to create a practice plan by choosing which music it intends to practice

* FEAT-03: Student will be able to start the practice on the system.

* FEAT-04: The system will be able to tell the student when is the time to move to the other music based on the defined practice plan

* FEAT-05: The Student will be able to create your profile and all the music that he register will be linked to his profile

### Projects 
This section is always a work in progress
* [Music Service](src/MusicService/README.md) - Music Service Application providing backend support for FEAT-01
* [File Service](src/FileService/README.md) - File Service Application providing file upload support for FEAT-01
* [Music Practice Planner API](src/MusicService/README.md) - Main Music Planner API acting as a monolithic API to provide all the needed services 
* [L718Framework](src/L718Framework/README.md) - Framework for Domain Driven Design basis


[Roadmap]

* Frontend for registering songs (FEAT-01) 
* Backend Services for the Planner (FEAT-02)
* Frontend for the Planner (FEAT-02)
* Backend Services for the Practice System (FEAT-03)
* Frontend for the Practice System (FEAT-03)
* Backend Services for the Practice Monitor (FEAT-04)
* Frontend for the Practice Monitor (FEAT-04)
* User Authentication (FEAT-05)
* Frontend for the User Authentication (FEAT-05)


### Installation

To install the project configure a vault and update the API on the MusicPracticePlannerAPI [appsettings](src/MusicPracticePlannerAPI/appsettings.json) file

Run the MusicPracticePlannerAPI with 

    dotnet run 