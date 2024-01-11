# Music Practice Planner - Architecture Design Document

## Disclaimer
This is a project to gather knowledge on the Domain Driven Design Architecture based on .Net Core - Not intended to be published by anyways
A big thanks to Michael Simons for his [biking architecture document](https://biking.michael-simons.eu/docs/index.html). His document acts as a model for my learning process.
Do not copy this project. 

This document is best viewed with a markdown viewer with plantUML support.


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


## 1.3. Quality Goals 

| Nr | Quality | Motivation |
|----|---------|------------|
| 1  | Backend Development Knowledge | The project is simple and intends to be developed following the best standards in C# and ASP\.NET Core |
| 2  | Frontend Development Knowledge | Despite simple, the frontend can grow in complexity by using libraries to render sheet musics and read midi files, React will be used, so the focus is to learn usage of it in a "real" application |
| 3  | Systems Design | Create and Document the software in a way that everyone in touch of this project will understand it's needs and keeps tracks of its involvement  |
| 4  | Testability | The project architecture must allow testing all the building blocks. |

## 1.4. Stakeholders

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

In order to understand the context of the system some session about understanding the domain was defined.

## 3.1. Understanding the Domain

### 3.1.2. BDD Matrix
In order to Understand the Domain, a BDD Scenario Matrix was Generated with all the Software Intentions, since is a single user software the actor of this BDD Matrix is only one, The Music Student. 

| Feature | Scenario |
|----|------------|
| FEAT 01 | As a student I want to create my repertoire of musics on the system |
| FEAT 02 | As a student I want to be able to upload midi files as musics on my repertoire and I wish that the system converts this midi files into sheet musics. |
| FEAT 03 | As a student I want to create practice routines on the system |
| FEAT 04 | As a student I want to create execute practice routines daily |
| FEAT 05 | As a student, on a practice session, I want to view the sheet music of the music that I am practicing |
| FEAT 06 | As a student, on a practice session, I want to practice with the absolute accuracy method |
| FEAT 07 | As a student, on a practice session, I want to practice from the last measure that I got right using the absolute accuracy method |
| FEAT 08 | As a student, I want to be alerted by the system to advance to the next music of my practice session |
| FEAT 09 | As a student I want to receive statistics of my piano practice sessions |

### 3.1.3. Event Storming
With the features defined an event storm session was realized to understand all the business flow and section was represented using some simple notation as follows:

```
==============================================
How to read:
    "A Command ->" 
    "... An Event that triggers -> A Command ->" 
    "| A Query (one liner) | ->  ..."
    "# Comments"
==============================================

CONTEXT MUSIC PRACTICE PLANNER
    PRACTICE
        ROUTINES 

            Create Practice Routine ->
                A Practice Routine Was Created

                Add Music To Practice Routine -> 
                    A Music Was Added to the Practice Routine

            ... The music was removed from repertoire -> Remove Music From Practice Routines -> 
                The music was removed from practice routine

            |List Practice Routines| ->  A List of Practice Routines was loaded 
            
            |Select Practice Routine| ->  A Practice Routine was selected (for practice)

        SESSIONS 
            Start Daily Practice Session -> 
                The Daily Practice Session Started
                    
                Start Practicing Next Music ->  #First if not have any at the beginning 
                    Music Practice Started 
                    The Time Counter was started
                    The Next Music was Not Found // Error Handling

                #Practice Starts with the last learned section
            ... Music Practice Started -> Load Last Music Section Learned -> #First if not exists any
                    The Music Section was Loaded
            ... The Music Section was Loaded -> Load The Sheet Music For Section  
                    The Sheet Music For Section was Loaded
                    
                # The student plays the section and the system evaluates using AAR Method
                # This might be front end only 
                Evaluate Music Section Execution ->                 
                    The Music Section was Executed Correctly 
                    The Music Section was Incorrectly 

            ... The Music Section was Executed Correctly (7x) -> Register Music Section Learned -> 
                The Music Section was learned
            
            ... The Music Section was Incorrectly -> Restart Practice in Music Session
                Practice in Music Session was restarted
        
            ... The Time Counter expires -> End Music Practice -> 
                The Music Practice Has Ended

            ... The Music Practice Has Ended (All musics)
            ... The Next Music was Not Found -> End Practice Session -> 
                    The Daily Practice Session Ended

    REPERTOIRE 
        Upload Music -> 
            A Music was Saved 
            A Midi File was Saved

        Create Music ->  
            A Music was Saved

        Update Music ->  
            The Music Was Updated 

        ... A ABC Conversion Has Ended -> Update Music ->
            The Music Was Updated

        Remove Music -> 
            The music was removed from repertoire 

        | Get the List of Musics | ->  List my learning musics


CONTEXT ABC CONVERSION
    ...A Midi File was Saved -> Convert Midi to Abc Notation
        A ABC Conversion Has Ended 


```

## 3.1,4. Music Section Selection
To Select the music sections the following algorithm must be applied:
The Music is divided in measures, so the main rule is to train the first measure, train the next one, train both of them together and then add the next measure until the music ends
IE: Suppose a music with  MeasureA, MeasureB, MeasureC
   
The training order will follow: 

    MeasureA, MeasureB, MeasureC
    A B C
    1 2
     3  4
       5

The Musical Practice Sections will be
1. MeasureA
2. MeasureB
3. MeasureA + MeasureB
4. MeasureC
5. MeasureA + MeasureB + MeasureC


## 3.2. Business Context

With the domain we could extract the business context 


```plantuml
@startuml Business Context

package "Music Practice Planner Business Context" {
 
package MusicPracticePlanner{
    [API]
    [ClientAPP]
}
[StudentInstrumentMidiInterface]
[EasyABC]
actor Student

Student --> MusicPracticePlanner : Uploads his Repertoire, \nCreates Practice Plan, \nExecute Practice Routines Daily 

 EasyABC <-- MusicPracticePlanner : Extract Music \nInformation from Midi Files

 MusicPracticePlanner <-- StudentInstrumentMidiInterface  : send realtime information\n about the students\n practice  

}

@enduml
```



#### Student
A music student that wants to practice. He defines his repertoire, creates a new practice plan and daily execute his practice plan.

#### StudentMidiInterface
The entrypoint for the student's musical instrument. Student will be able to connect his instrument to track his progression when he is playing.

#### EasyA
A python application that converts midi music to a "human-readable" format to display sheet musics

## 3.2. Technical Context

```plantuml
@startuml Technical Context

package "Music Practice Planner Business Context" {


rectangle "ApplicationServer" {
    [MusicPracticePlannerAuthAPI]
    [MusicPracticePlannerAPI]
    [EasyABCService]
    [EasyABC]
    [MessageQueue]
    [FileStorage]
    [DataStorage]
    EasyABCService -right. EasyABC
    EasyABCService .. MessageQueue : Receives and Converts \n Midi to ABC
    MusicPracticePlannerAPI -left. MessageQueue : Queue MIDI Infomation\nReceives ABC File
    MusicPracticePlannerAPI -down. DataStorage : Data
    MusicPracticePlannerAPI -down. FileStorage : MIDI\nfiles 
}


rectangle "<<Browser Device>>" {
    [MidiAPI]
    package client-app {
    }

    MidiAPI -u.> "client-app" : sends MIDI information 
} 
"client-app" -. MusicPracticePlannerAPI 
"client-app" -l. MusicPracticePlannerAuthAPI : <<jwt authenticattion >>
MusicPracticePlannerAPI -u.> MusicPracticePlannerAuthAPI : <<token validation>>

}

@enduml
```


#### Backend / Application Server

* **MusicPracticePlannerAPI**: The API of the music practice planner, used by the frontend application "client-app" to send and receive data 

* **MusicPracticePlannerAuthAPI**: The Authentication API providing Account creation and JWT Tokens for the application

* **EasyABCService**: The Service Wrapper for the EasyABC application, needed if we want to add the desired feature to covert midi file to abc notation. 

* **MessageQueue**: Since the conversion is an internal application it uses resources from the application server, therefore a messaging system is needed to maintain the efficiency of the application flow.

* **FileStorage**: File storage is used to store the midi files for the project. 

* **DataStorage**: Data storage for all the application data. 

#### Frontend

* **MidiAPI**: The HTML5 Midi API interface to allow midi connection on application

* **client-app**: The React application of music planner 

* **PracticeRoutine**: Components for the Student to manage the Practice Session

* **SheetMusic**: Display the Sheet music using ABC notation

* **MidiConnection**: Connects and listens the Midi API to track the notes


# 4. Solution Strategy 

## 4.1. Frontend
The music tracker application will have some little complex features on the frontend such as:

* Display the Sheet Music on the section that the student is currently learning

* Connect Midi Interface and listen to the instrument information

* Keep track of the music section while the user is playing

Since the frontend needs to display the sheet music information the best is to use some libraries to render that. During the research phase I found the [ABCjs](https://github.com/paulrosen/abcjs) that can render sheet music based on the ABC Notation. The ABC Notation will be used as a main base to the musics in order for the system to understand it.    

As mentioned, React will be used as a main Frontend Framework, the main purpose of this is to improve my learning skills in React framework. 


## 4.2. Backend

On the backend the application will have a domain model with the models "Music", "PracticeRoutine", "PracticeSession" as the most important ones. 

Since there will be a feature to perform upload of midi file and convert it to ABC music notation, the backend also will use a tool in python to perform this conversion. 
Gateway
In order to use the tool a Messaging Queue System will be used to periodically send requests to the conversion system. That will not block the performance of the application while we are waiting for the conversion to take place. 


## 5. Building Blocks View

Here is the high level of the application

```plantuml
@startuml Level 0

package "cmp Level 0" {
    package "client-app" {
        [ConfigureRoutine]
        [PracticeSession]
        [PracticeMusic]
    }
    package MusicPracticePlannerAPI{
        [RepertoireAPI]
        [PracticeRoutineAPI]
        [PracticeSessionAPI]
    }
    package EasyABCConversionService {
        [EasyABCService]
        [EasyABC]
    }
    package MusicPracticePlannerAuthAPI{
        [AuthenticationAPI]
        [LoginAPI]
    }    
    [MessageQueue]

    MessageQueue -u... MusicPracticePlannerAPI : midi <-> abc conversion events
    MessageQueue -r... EasyABCConversionService : midi <-> abc conversion events
    "client-app" -d..> MusicPracticePlannerAPI 
    "client-app" -r..> MusicPracticePlannerAuthAPI : <login, craete new account>
    MusicPracticePlannerAPI ..> MusicPracticePlannerAuthAPI : <jwt validation>
}
@enduml
```

## 5.1. Whitebox MusicPracticePlannerAPI

```plantuml
@startuml MusicPracticePlannerAPI

        package MusicPracticePlannerAPI {

            [FileSystemStorage]

            [Gateway]

            [PracticeAPI]
            [PracticeService]
            [PracticePersistence]

            [RepertoireAPI]
            [MusicMessageQueueListener]
            [RepertoireService]
            [RepertoirePersistence]

            Gateway ..> RepertoireAPI
            Gateway ..> PracticeAPI

            RepertoireAPI -d.> RepertoireService
            RepertoireService -r.> FileSystemStorage
            PracticeAPI -d.> PracticeService
            MusicMessageQueueListener -d.> RepertoireService
            RepertoirePersistence -u. RepertoireService
            PracticePersistence -u. PracticeService
        }
        
        MusicMessageQueueListener -up.> [MessageQueue] 
        Gateway -up. [client-app] 
        Gateway -up. [MusicPracticePlannerAuthAPI] 
        RepertoirePersistence -down. RepertoireDatabase
        PracticePersistence -down. PracticeDatabase
        PracticeService -l.> RepertoireService



@enduml
```

The diagram shows a decomposition of the whole Music Practice API in a project structured level. 
APIs and MessageQueueListener are the main entrypoints of the package, responsible to gather the information on the Music
For now, since the application will run in a single server, same database system can be used for the PracticeDatabase and RepertoireDatabase


* **[Gateway](#511-gateway-blackbox)**: The entrypoint of the application, it will use MusicPracticePlannerAuthAPI as authority on the JWT Token validation 

* **[PracticeAPI](#512-practiceapi-blackbox)**: API for the student to register and perform music practice sessions

* **[PracticeService](#513-practiceservice-blackbox)**:  Service for the practice sessions 

* **PracticePersistence**: Persistence of the practice sessions 

* **[RepertoireAPI](#514-Repertoireapi-blackbox)**: API for the student to register music and perform upload of the midi files 

* **[MusicMessageQueueListener](#515-musicmessagequeuelistener-blackbox)**: The listener will be responsible to receive event notifications of converted midi files 

* **[RepertoireService](#516-Repertoireservice-blackbox)**: Music service will register the music details, save midi file on filesystem and send notification to the message queue about midi uploded

* **RepertoirePersistence**: Persistence of the saved 

* **FileSystemStorage**: Used to store the midi files


## 5.1.1. Gateway (blackbox)

Provides full access of internal entrypoints on the system, performs the action of token validation communicating with the token authority service

| Interface | Description |
|----|---------|
| RESTinterface <br> /api/v1/repertoire/musics/*   | maps to the Repertoire API |
| RESTinterface <br> /api/v1/practiceroutines/*   | maps to the Practice API |


## 5.1.2. PracticeAPI (blackbox)

Provides access to the practice API functionality

| Interface | Description |
|----|---------|
| RESTinterface <br> /api/practiceroutines/*   | Containing CRUD operations of Practice Routines |
| RESTinterface <br> /api/practiceroutines/{id}/sessions/*   | Containing Operations for practice sessions |

## 5.1.3. PracticeService (blackbox)

Provides CQRS methods used by Practice API to accomplish its features

| Commands | Description |
|----|---------|
|CreatePracticeRoutine| Creates a Practice Routine |
|AddMusicToPracticeRoutine| Adds Music to the Practice Routine | 
|RemoveMusicFromPracticeRoutine| Removes the Music From the Practice Routine
|||
|StartDailyPracticeSession| Starts a daily practice session |
|StartPracticingNextMusic| Starts the practice on the next music |
|EvaluateMusicSectionExecution| Evaluates the Music Session Execution|
|RegisterMusicSectionLearning | Registers the Music Section as Learned |
|RestartPracticeInMusicSession| Restarts the Music Section Learning |
|EndMusicPractice | Ends the Practice Session for the Music |
|EndPracticeSession | Ends the daily Practice Session |


| Queries | Description |
|----|---------|
| ListPracticeRoutines   | Lists the Practice Routines  |
|SelectPracticeRoutine| Select Practice Routine for Practice Session |


## 5.1.4. RepertoireAPI (blackbox)

Provides access to the music API functionality

| Interface | Description |
|----|---------|
| RESTinterface <br> /api/musics/*   | Containing CRUD operations of Music Information |

## 5.1.5. MusicMessageQueueListener (blackbox)
Connects to the MessageQueue and listens for events

| Event | Description |
|----|---------|
| ABCNotationCreated | Sends the information to the RepertoireService to update the music's abc notation |


## 5.1.6. RepertoireService (blackbox)

Provides CQRS methods used by Music API to accomplish its features

| Commands | Description |
|----|---------|
|UploadMusic| Uploads a Midi File and Creates a Music for it  | 
|UpdateMusicsABCNotation| Update the Existent Music's ABC Notation  |
|CreateMusic| Creates A Music  |  
|UpdateMusic|  Updates the Music Information |  
|RemoveMusic|  Remove the music from Repertoire | 

| Queries | Description |
|----|---------|
| GetMusics   | Lists the musics  |

