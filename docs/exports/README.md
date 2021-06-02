# Munchkin

## Game Architecture using Orleans

![munchkin grains][1]

### Table

This is an aggregate entity that has the state references to all other entities. Basically, it represents a "gaming table" where all the players sit and play cards.

### Card

There is a large variety of cards in the deck. Each of them has its own rules encapsulated. A card entity represents the real card that can be played by any player that owns it. Card entity also holds a reference to its owner (player, table, discard, deck, etc.).

Cards do not have a reference for a table, but can interact with it when passed as a parameter. Cards do change the table state.

### Player

The player entity represents an avatar of the actual user that is playing the game. The player is the one who is performing any kind of actions, triggers actions and interacts with cards, table and other entities.

### Card Deck and Discard Deck

The deck represents a collection of cards that that are available or that are discarded. The card deck entity holds a collection of references to a set of cards available in the expansion.

### Player Collection

The collection of users that are 'sitting' around the table are all stored in a player collection. The collection object exposes a set of methods to use when changing the current player, or finding other players. The collection works in a circular manner, meanning that when reached the last item in collection, next item will be the first and so on.

### Dungeon

The dungeon is an object that holds the state of the player's turn. When the pllayer plays a card, or executes an action, the state of dungeon changes accordingly. 

[1]: munchkin.orleans.grains.communication.jpg
