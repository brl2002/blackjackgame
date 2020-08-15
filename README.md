# blackjackgame

Design Doc:
https://docs.google.com/document/d/1Jrapj4DCRUwRQ8jhN-5sNaVtyrHQgVImMdY1_AJuneY/edit?usp=sharing

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
