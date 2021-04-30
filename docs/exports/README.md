# Munchkin

## Game Architecture using Orleans

![munchkin grains][1]

### Table

This is an aggregate entity that has the state references to all other entities. Basically, it represents a "gaming table" where all the players sit and play cards.

### Card

There is a large variety of cards in the deck. Each of them has its own rules encapsulated. A card entity represents the real card that can be played by any player that owns it. Card entity also holds a reference to its owner (player, table, discard, deck, etc.).

Cards do not have a reference for a table, but can interact with it when passed as a parameter. Cards do change the table state.

### Card Deck and Discard Deck

The deck represents a collection of cards that that are available or that are discarded. The card deck entity holds a collection of references to a set of cards available in the expansion.

### Player




[1]: munchkin.orleans.grains.communication.jpg
