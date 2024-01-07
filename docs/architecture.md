# Music Practice Planner 

## Disclaimer
This is a project to gather knowledge on the Domain Driven Design Architecture based on .Net Core - Not intended to be published by anyways
A big thanks to Michael Simons for his [biking architecture document](https://biking.michael-simons.eu/docs/index.html). His document acts as a model for my learning process.
Do not copy this project. This 

### Introduction and Goals

The Music Practice Planner is a project built with the intention of learning software architecture design by practicing it. 

The application itself aims to track my music practice easily, following the AAR Rule defined by [Jazzer Lee](https://www.youtube.com/watch?v=13Qa9mpCHo0). 

The mainly achievements for this project are:

* Learn about software architecture and design

* Register What Musics I want to have in my repertoire.

* Track my daily practice session 

## 1.1. Requirements Overview

The Music Practice Planner, as a software has the following features:

* Create Student Accounts
* Register the musics that students want to learn
* To allow users to create a practice session plan / routine 
* Track student progression on his created daily practice
* Display a music sheet music and allow student to practice sections of the music according to the AAR Practice.
* Desirable - student will be able to upload a midi of the music and then covert it to a format that system can show and work with.     

## 1.2. Quality Goals 

| Nr | Quality | Motivation |
|----|---------|------------|
| 1  | Backend Development Knowledge | The project is simple and intends to be developed following the best standards in C# and ASP\.NET Core |
| 2  | Frontend Development Knowledge | Despite simple, the frontend can grow in complexity by using libraries to render sheet musics and read midi files, React will be used, so the focus is to learn usage of it in a "real" application |
| 3  | Systems Design | Create and Document the software in a way that everyone in touch of this project will understand it's needs and keeps tracks of its involvement  |
| 4  | Testability | The project architecture must allow testing all the building blocks. |

## 1.3. Stakeholders

Below are the important personas for the application

| Role | Goal |
|---------|------------|
| Backend Developer | Who wants to learn about software development, Asp.Net |
| Frontend Developer | Who wants to learn about usage of frameworks such as react and usage of music libraries |
| Music Students | Who wants an application that they can use to learn how to play piano |
| Software Aspirants | Who want to see how this application was developed |
| Me (André Lelis) | Who wants to learn about all the previous aspects |

## 2. Architecture Constraints

A few constrains of the architecture. 

### 2.1 Technical Constraints

|  | Constrain | Motivation |
|----|---------|------------|
| T1  | Backend Development using \.Net Core | Since this is a part of learning process, the Backend APIs structure must follow that language. During research tests, I found some features that are exclusive in other platforms, if this is used, a service must be created to perform the Interoperability communications |
| T2  | Frontend must use React | Also part of the learning process React must be used. If mobile is built someday, React Native usage will be a constraint and also Typescript as a main language for the frontend projects |
| T3  | Automated deploy | An automated deployment process must be created |
| Hardware constrains ||
| T4  | Deploy on a Free Tier Cloud Provider | Since this project won't be hosted a "demo" with all the features could be deployed on a free tier level |

### 2.2 Organization Constraints

|  | Constrain | Motivation |
|----|---------|------------|
| OC1  | Team | Me |
| OC2  | Time schedule | Started in December 2023 - Intend to have a MVP version with most of the features on the roadmap by March 2024, from there I will plan the next MVP and Date |
| OC3  | Version Control | Public git repository for now. Can be locked in the future if the project grows to a more serious thing of if I start to have external issues |
| OC4  | Configuration | All Configuration must be grouped on a single configuration |
| OC5  | Testing | xUnit will be used for testing background features and Jest will test the frontend - Coverage will be managed using SonarQube and a 90% of code coverage is needed. |

### 2.3 Conventions

|  | Convention | Motivation |
|----|---------|------------|
| C1  | Architecture Documentation | Based on the arc42-template version 6.5 following Michael Simmon's [Biking2 Architecture Documentation](https://biking.michael-simons.eu/docs/index.html) |
| C2  | Coding Convention | For the Backend .Net usage of [Common C# code convetions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions). On The Frontend a complete code convention document can be found on [this post](https://levelup.gitconnected.com/react-code-conventions-and-best-practices-433e23ed69aa) from Gaspar Nagy |
| C3  | Language | English for all the project |
| C4  | Music Language | English Grammar for Music Notes (ABCDEFG) - More details can be found on (MusicTheory.Net)[https://www.musictheory.net/lessons] |
| C5  | Sheet Music | Sheet music notation [ABCNotation](https://abcnotation.com/) will be used in order to store music details to be rendered, this option was motivated due to this be a human-readable notation |


## 3. System Scope and Context

The environment and context of the software is defined below.


## 3.1. Business Context

```plantuml
@startuml Business Context

package "Music Practice Planner Business Context" {
 
package MusicPracticePlanner{
    [API]
    [ClientAPP]
}
[StudentMidiInterface]
[EasyABC]
actor Student

Student --> MusicPracticePlanner : Uploads his Repertoire, \nCreates Practice Plan, \nExecute Practice Routines Daily 

 EasyABC <-- MusicPracticePlanner : Extract Music \nInformation from Midi Files

 StudentMidiInterface --> MusicPracticePlanner  : send realtime information\n about the students\n practice  

}

@enduml
```



#### Student
A music student that wants to practice. He defines his repertoire, creates a new practice plan and daily execute his practice plan.

#### StudentMidiInterface
The entrypoint for the student's musical instrument. Student will be able to connect his instrument to track his progression when he is playing.

#### EasyABC
A python application that converts midi music to a "human-readable" format to display sheet musics

## 3.2. Technical Context

```plantuml
@startuml Technical Context

package "Music Practice Planner Business Context" {
 

 rectangle "ApplicationServer" {
    [MusicPracticePlannerAPI]
    [EasyABCService]
    [MessageQueue]
    [FileStorage]
    [DataStorage]
    EasyABCService -- MessageQueue : Receives and Converts \n Midi to ABC
    MusicPracticePlannerAPI -left-- MessageQueue : Queue MIDI Infomation\nReceives ABC File
    MusicPracticePlannerAPI -down-- DataStorage : Data
    MusicPracticePlannerAPI -down-- FileStorage : MIDI\nfiles 
 }


rectangle "<<Browser Device>>" {
    [MidiAPI]
    package client-app {
        [PracticeRoutine]
        [SheetMusic]
        [MidiConnection]
    }

    MidiAPI -down--> MidiConnection : sends MIDI information 
} 

 "client-app" -up-- MusicPracticePlannerAPI : <<async communication>> \n <<jwt authentication>>
}

@enduml
```
#### Backend / Application Server

##### MusicPracticePlannerAPI
The API of the music practice planner, used by the frontend application "client-app" to send and receive data 

##### EasyABCService
The Service Wrapper for the EasyABC application, needed if we want to add the desired feature to covert midi file to abc notation. 

##### MessageQueue
Since the conversion is an internal application it uses resources from the application server, therefore a messaging system is needed to maintain the efficiency of the application flow.

##### FileStorage
File storage is used to store the midi files for the project. 

##### DataStorage
Data storage for all the application data. 

#### Frontend

##### MidiAPI
The HTML5 Midi API interface to allow midi connection on application

##### "client-app"
The React application of music planner 

##### PracticeRoutine
Components for the Student to manage the Practice Session

##### SheetMusic
Display the Sheet music using ABC notation

##### MidiConnection
Connects and listens the Midi API to track the notes


