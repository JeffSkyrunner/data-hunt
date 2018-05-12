﻿using model.play.runner;
using view;

namespace model
{
    public class Game
    {
        public Corp corp;
        public readonly Runner runner;

        public Game(Deck runnerDeck)
        {
            var actionCard = new ActionCard();
            var grip = new Grip();
            var stack = new Stack(runnerDeck);
            var heap = new Heap();
            var rig = new Rig();
            var clicks = new ClickPool();
            var credits = new CreditPool();
            runner = new Runner(this, actionCard, grip, stack, heap, rig, clicks, credits);
        }

        async public void Start()
        {
            await runner.StartGame();
        }
    }
}