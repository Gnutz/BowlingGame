# Bowling AppID principles and Clean architecture

Generally I have tried to adhere to the SOLID principles and write code that is easy to read and maintainable.
I have decided to divide the solution into three projects in a effort to follow the clean architecture
BowlingGame: This is the core and the center of the architecture containig the the business logic
BowlingGame.Application: This contains a client of Bowling


<img src="https://miro.medium.com/v2/resize:fit:1400/1*0u-ekVHFu7Om7Z-VTwFHvg.png" alt="drawing" width="600"/>

## BowlingGame:
The core of the application of this solution is the `Frame` and `BowlingGame` entity classes which implements the business logic, keeping track of frame state and game loop for a bowling Game.
In order to further separate the frame entities from the other layer and the client a simple Frame Data Trasfer Object class is also added, since this class and
the entity might need to change at different rates.
* Looking at [ten-pin_bowling](https://en.wikipedia.org/wiki/Ten-pin_bowling) the wiki describes different ways of scoring a game and so herefore I decided encapsulate to implement the Strategy pattern in regards to the behaviour of scoring frames. 
Simillarly is this done for the ´RollProvider´ interface used by the BowlingGame class

The benifits of adhering to the clean architecture is low coupling and inwards dependency. This core should easily plugged into different
UI frameworks where a simple one is made for demonstration purpuses but I believed the BowlingGame would easily wrapped and
deliver scores through a web api as an dependence to appropriate controller.

## Bowling.Application

## BowlingGame.Test




## Adding a UI and further improvements

Next steps for development could be a more robust and developed UI. This could either be either a web or desktop application. A web application with a RESTful api would probably be the preferred choice.
My next thoughts would probably be on creation patterns encaplsulate the instanciation for some of the classes e.g. the builder pattern to encaplsulate the inst