ZamZamZoom OO Racing Game
Aidan Bertram, Harrison Pena

Mid-Project Review
As of now this project is a simple Unity racing simulation where cars spawn, move around a track, and the fastest one wins after a set number of laps. 
The main focus at this point was building a template to build upon using clean object oriented design which we will implement gameplay and features ontop of for the final submission.

The core logic is separated from Unity in the Assets->Scripts folder. Classes like Race, Racer, and RaceTrack handle the actual race behavior, while RaceController and CarView just handle setup and visuals. This keeps responsibilities clear and avoids putting everything into one script.

Basic Design patterns:
A Factory (RacerFactory) is used to create racers. The Observer pattern is used so LeaderboardUI updates automatically from the Race. Polymorphism is shown through the RaceTrack base class with CircularTrack and OvalTrack. Dependency injection is used when RaceController creates objects and passes them into Race.

Demonstrated core OO principles:
Each class has a clear job, objects manage their own data, and the system is built around abstractions instead of hardcoded types. There are no large conditional blocks controlling everything and logic is split across the correct classes.

Tests are included in Assets/Tests/EditMode/ and cover  behavior like racer movement, race completion, winner selection, standings order and track calculations.

