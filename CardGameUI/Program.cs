using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGameUI
{
   internal class Program
   {
      static void Main(string[] args)
      {
         //PokerDeck deck = new PokerDeck();

         //var hand = deck.DealCard();

         //foreach (var card in hand)
         //{
         //   Console.WriteLine($" {card.Value.ToString()} of {card.Suit.ToString()}");
         //}

         BlackjackDeck deck = new BlackjackDeck();

         var hand = deck.DealCard();

         foreach (var card in hand)
         {
            Console.WriteLine($" {card.Value.ToString()} of {card.Suit.ToString()}");
         }
         Console.ReadLine();
      }
   }
   public abstract class Deck
   {
      protected List<PlayingCardModel> fullDeck = new List<PlayingCardModel>();
      protected List<PlayingCardModel> drawPile = new List<PlayingCardModel>();
      protected List<PlayingCardModel> discardPile = new List<PlayingCardModel>();

      protected void CreateDeck()
      {
         fullDeck.Clear();

         for (int suit = 0; suit < 4; suit++)
         {
            for (int val = 0; val < 13; val++)
            {
               // If you have a number and you know for sure that it is one of the enum values, you can cast it
               fullDeck.Add(new PlayingCardModel { Suit = (CardSuit)suit, Value = (CardValue)val });
            }
         }
      }

      public virtual void ShuffleDeck() // A shuffle can be different from game to game becuause of that its virtual
      {
         var rnd = new Random();
         drawPile = fullDeck.OrderBy(x => rnd.Next()).ToList();

      }

      public abstract List<PlayingCardModel> DealCard();  // Every game has different number of cards to deal for each player

      protected virtual PlayingCardModel DrawOneCard()
      {
         PlayingCardModel output = drawPile.Take(1).First(); // Take just take (not take and remove)
         drawPile.Remove(output);
         return output;

      }
   }

   public class PokerDeck : Deck
   {
      public PokerDeck()
      {
         CreateDeck();
         ShuffleDeck();

      }
      public override List<PlayingCardModel> DealCard()
      {
         List<PlayingCardModel> output = new List<PlayingCardModel>();

         for (int i = 0; i < 5; i++)
         {
            output.Add(DrawOneCard());
         }

         return output;
      }

      public List<PlayingCardModel> RequestCards(List<PlayingCardModel> cardsToDiscard)
      {
         List<PlayingCardModel> output = new List<PlayingCardModel>();
         foreach (var card in cardsToDiscard)
         {
            output.Add(DrawOneCard());
            discardPile.Add(card);
         }

         return output;
      }

   }

   public class BlackjackDeck : Deck
   {
      public BlackjackDeck()
      {
         CreateDeck();
         ShuffleDeck();
      }
      public override List<PlayingCardModel> DealCard()
      {
         List<PlayingCardModel> output = new List<PlayingCardModel>();

         for (int i = 0; i < 2; i++)
         {
            output.Add(DrawOneCard());
         }

         return output;
      }

      public PlayingCardModel RequestCard()
      {
         return DrawOneCard();
      }
   }
}
