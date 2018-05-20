﻿using NUnit.Framework;
using model;
using model.cards;
using System.Collections.Generic;
using model.cards.runner;
using System.Linq;
using tests.observers;

namespace tests
{
    public class SureGambleTest
    {
        [Test]
        public void ShouldPlay()
        {
            var runnerCards = new List<ICard>();
            for (int i = 0; i < 5; i++)
            {
                runnerCards.Add(new SureGamble());
            }
            var sureGamble = runnerCards.First();
            var game = new Game(new Decks().DemoCorp(), new Deck(runnerCards));
            game.Start();
            SkipCorpTurn(game);
            var balance = new LastBalanceObserver();
            var clicks = new SpentClicksObserver();
            var grip = new GripObserver();
            var heap = new HeapObserver();
            game.runner.credits.Observe(balance);
            game.runner.clicks.Observe(clicks);
            game.runner.zones.grip.ObserveRemovals(grip);
            game.runner.zones.heap.Observe(heap);
            var play = game.runner.actionCard.Play(sureGamble);

            play.Trigger(game);

            Assert.AreEqual(9, balance.LastBalance);
            Assert.AreEqual(1, clicks.Spent);
            Assert.AreEqual(sureGamble, grip.LastRemoved);
            Assert.AreEqual(sureGamble, heap.LastAdded);
        }

        private void SkipCorpTurn(Game game)
        {
            var clickForCredit = game.corp.actionCard.credit;
            for (int i = 0; i < 3; i++)
            {
                clickForCredit.Trigger(game);
            }
        }
    }
}