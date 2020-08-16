# blackjackgame

Design Doc:
https://docs.google.com/document/d/1Jrapj4DCRUwRQ8jhN-5sNaVtyrHQgVImMdY1_AJuneY/edit?usp=sharing

## How To Play  
UI feature is currently not implemented
Use keyboard to play:
- 'a' to take seats
- 'h' to hit
- 's' to stand
- 'd' to double down
  - double down feature not yet complete along with player bet and cash logic

## Features  

- Single player game
- Standard blackjack game rule
- Player cash with ability to bet using their cash pool
- Ability to play multiple rounds with persistent cash pool
- Ability to save game state and restore after a restart
- Customizable game settings
- Minimum bet amount
- Player’s start cash amount

## Functionalities  

### Cards  
- Randomly setting value of the card
- Flip (front/back)  

### Rule  
- Determine numeric value of a card
- Configure game settings
  - Minimum bet amount
  - Player’s start cash amount

### Game  
- Player is able to join a table/round
- A deck of card is shuffled before starting
  - Dealer
    - Hands out cards and controls the flow of the game

### Pool of cards  
- Consists of 52 x 8 (416) cards

### Game state  
- Save current game state
  - List of possible state values/object
    - Player’s money

## Code Design

### Card:  
- Set card type on construction
- Flip
  - States
    - Front
      - Show front of the card with proper image based on the value of the card
    - Back
      - Show back of the card

### CardPool:  
- Stores images of cards
- Store a container of 52 x 8 cards
- Shuffle
  - Shuffle the container of cards randomly
- Get card
  - Returns a card from the front

### Seat:  
- Can be player or dealer type
- Determine type (player/dealer) on construction
- Holds container of cards that have been dealt to the player/dealer hand

### Rule:  
- Get card value
  - For a given card return the value of the card
- Get game settings
  - Read game settings from a scriptable object

### Game:  
- Dealer
  - Ability to control the flow of the game
  - Control what happens when player “hit”
  - Control what happens when player “stand”
  - Control what happens when player “double down”
- Control seats; assign, place, and bust/remove
- Clear seat
  - Clear card in hand
  - Clear bet
- Clear table
  - Reset pool of cards
- Determine outcome

## Player UI & MVC  

### Model:  
- Player cards in hand
- Player cash in hand
- Player total betting amount

## View:  
- Card game object(s)
  - Represents player’s cards in hand
- Cash in hand display
  - UI displaying how much cash player has in hand
- Accumulated betting amount
  - UI displaying player’s current accumulated betting amount
- UI Buttons

## Controller:  
- Start
  - Player receives cash to start
- Join table
- Gameplay
  - Hit
  - Stand
  - Double Down
- Leave table

## Player UI & MVC Design  

### Model/BlackjackModel  
- PlayerBlackjackModel
  - Cards in hand
  - Cash in hand
  - Total betting amount
- DealerBlackjackModel

### View/BlackjackView
- StartBlackjackView
  - UI elements
    - Start button
    - Player cash
- JoinBlackjackView
  - UI elements
    - Join button
    - Player cash
- PlayBlackjackView
  - UI elements
    - Hit button
    - Stand button
    - Double down button
    - Player cash
    - Player betting amount
- ExitBlackjackView
  - UI elements
    - Continue button
    - Player cash
    
### Controller/BlackjackController
- Derived classes
  - SinglePlayerBlackjackController
  - MultiplayerBlackjackController
