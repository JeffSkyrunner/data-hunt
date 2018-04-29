﻿using System.Collections.Generic;

namespace model.costs
{
    public class RunnerCreditCost : ICost, IBalanceObserver
    {
        private int credits;
        private HashSet<IPayabilityObserver> observers = new HashSet<IPayabilityObserver>();

        public RunnerCreditCost(int credits)
        {
            this.credits = credits;
        }

        bool ICost.CanPay(Game game)
        {
            return game.runner.credits.CanPay(credits);
        }

        void ICost.Observe(IPayabilityObserver observer, Game game)
        {
            observers.Add(observer);
            game.runner.credits.Observe(this);
        }

        void ICost.Pay(Game game)
        {
            game.runner.credits.Pay(credits);
        }

        public void NotifyBalance(int balance)
        {
            var payable = balance >= credits;
            foreach (var observer in observers)
            {
                observer.NotifyPayable(payable);
            }
        }
    }
}