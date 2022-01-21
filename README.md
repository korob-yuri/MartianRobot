# MartianRobot

Assumptions

Architecture 

- create the project as a console application
- use .Net 6 for creating a cross-platform app

Design

- classes "grid" and "robot" and "command" were created  to represent main entities of the app
- supporting class "position" was created to indicate a position on a grid
- per requirements, "command" class was created in a way to easily extend it, to support additional types
- based on the complexitiy of the project and formats of the user inputs, it is ok to use regular expressions for the parsing of the input
- test coverage for the most important scenarios, including negative paths and edge cases

Estimations

- I would estimate it round 8 hours of work of a developer time and 2 hours of a tester.