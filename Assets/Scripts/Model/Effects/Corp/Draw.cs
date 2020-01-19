﻿using System.Threading.Tasks;

namespace model.effects.corp
{
    public class Draw : IEffect
    {
        private int cards;

        public Draw(int cards)
        {
            this.cards = cards;
        }

        async Task IEffect.Resolve(Game game)
        {
            var rd = game.corp.zones.rd;
            var hq = game.corp.zones.hq;
            rd.Draw(cards, hq);
            await Task.CompletedTask;
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}