Zimzamzoom!

OO Project by
Aidan Bertram

This is a simple Unity racing simulation. Cars spawn into the scene, move around a track and the first racer to finish the required laps wins. The project also includes a small betting system where the player can choose a racer, place a bet and see if they won or lost after the race ends.



The  goal of this project is to show object oriented design. The core logic is separated from Unity in the Assets->Scripts folder. The tests are found in the Assets->Tests->EditMode folder. Classes like Race, Racer, RaceTrack, CircularTrack, OvalTrack and the betting classes handle the actual logic. Unity scripts like RaceController, CarView, LeaderboardUI and BettingUI mainly handle setup, visuals and display.



This project uses at least five design patterns. The Factory pattern is used with RacerFactory and BetFactory to create objects in one place. The Observer pattern is used because Race notifies LeaderboardUI and BettingService when the race updates or finishes. The Strategy pattern is used with IPayoutStrategy, StandardPayoutStrategy and UnderdogPayoutStrategy so payout rules can change without rewriting the betting system. The Facade pattern is used with BettingService which gives the UI one simple class to use for betting instead of exposing all betting logic. The Singleton pattern is used with GameSession which stores shared game state like player money and the last winner.



The project  demonstrates core OO principles. Encapsulation is used because objects like Racer, Bet and BetResult own their own data. Abstraction is used through RaceTrack which lets the race use a general track type instead of caring whether it is a circle or oval. Polymorphism is shown through CircularTrack and OvalTrack since both inherit from RaceTrack but implement track movement differently. Dependency injection is used when RaceController builds objects like the track, racers, payout strategy and betting service then passes them into the classes that need them.



The project has a UI. The leaderboard shows current standings during the race and the betting UI lets the player choose a racer, enter a bet amount and see the result after the race. This satisfies the UI requirement while keeping UI code separate from the core logic.

The project includes tests for the core logic which cover racer movement, race completion, winner selection, standings order, winner stability after the race ends and circular track calculations. 

