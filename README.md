﻿# Bowling AppID principles and Clean architecture

Generally I have tried to adhere to the SOLID principles and write code that is easy to read and maintainable.
I have decided to divide the solution into three projects in a effort to follow the clean architecture
BowlingGame: This is the core and the center of the architecture containig the the business logic
BowlingGame.Application: This contains a client of Bowling

<img src="https://miro.medium.com/v2/resize:fit:1400/1*0u-ekVHFu7Om7Z-VTwFHvg.png" alt="clean-architecture" width="500"/>

## BowlingGame:

The core of the application of this solution is the `Frame` and `BowlingGame` entity classes which implements the business logic, keeping track of frame state and game loop for a bowling Game.
In order to further separate the frame entities from the other layer and the client a simple Frame Data Trasfer Object class is also added, since this class and
the entity might need to change at different rates.

Looking at [ten-pin_bowling](https://en.wikipedia.org/wiki/Ten-pin_bowling) the wiki describes different ways of scoring a game and so herefore I decided encapsulate to implement the Strategy pattern in regards to the behaviour of scoring frames.
Simillarly is this done for the `RollProvider` interface used by the BowlingGame class

The benifits of adhering to the clean architecture is low coupling and inwards dependency. This core should easily plugged into different UI frameworks where a simple one is made for demonstration purpuses but I believed the BowlingGame would easily wrapped and
deliver scores through a web api as an dependency to appropriate controller.

## Bowling.Application

This contains a client application of BowlingGame project in this case a console application, it's plugged into Bowling game as an outer layer.
In this application a BowlingGame instiation with a default Traditional Scoring Strategy and a MaualRollProvider object which lets BowlingGame class take in roll via the console,

The also uses a Presenter class to display the series of frames after each roll.
This implementation hasn't been ny main focus.

## BowlingGame.Test

An inital set of tests are contained in this project which also sits in the perifery of the BowlingGame. I have tried following the principle of red-green-refactor doing some initial testing of the Frame entity and then testing the integration of BowlingGame class from the 3 examples given in the challenge documentation.

More is necessary they would be my focus should I work on this further.

NSubsitute is installed as a dependency for this project.

## Adding a UI and further improvements

Next steps for development could be a more robust and developed UI. This could either be either a web or desktop application. A web application with a RESTful api would probably be the preferred choice.
My next thoughts would probably be on creation patterns encaplsulate the instanciation for some of the classes e.g. the builder pattern build the BowlingGame object with specified strategies for scoring and genersting rolls.

New features to focus on would also be support for mutiple players and data persistence.
