# Project02
Project02.csproj - Question 2 implementation. \n
Project02.Tests.csproj - Question 2 unit tests. \n

Models:
  Deck - model for card deck
  Card - model for card
  Suit - enum of suits
  GameSettings - settings for the game, base on different tasks from Question 2 (allow tie, resolve tie by suit precedence etc.)

Services: 
  GameService - Service responsible for game (or game session). It will call necessary behaviour (via factory);
  DeckService - Its job to create decks (one or many depending on Game Settings) and play card from deck
  SuitService - Its job to resolve tie by suit precedence if any provided

Services.Abstractions:
  Interfaces for services above. Mainly created them for Unit Test here. In real project will create them to use DI.
  
Behaviours:
  SingleDeckGameBehaviour/MultiDeckGameBehaviour - classes responsible for result (Win, Lose, Tie) calculation for single or multiple decks based on game settings.
  GameBehaviourFactory - factory which will provide needed IGameBehaviour based on game settings.
Behaviours.Abstractions:
  Interfaces for classes above.
  
