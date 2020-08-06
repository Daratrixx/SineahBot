using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Room : IObservable
    {

        public string description { get; set; }

        List<Entity> entities;
        List<Character> characters;
        List<Player> players;
        List<Item> items;

        public IEnumerable<IObservable> observables
        {
            get { return entities.Where(x => x is IObservable).Select(x => x as IObservable); }
        }
        public IEnumerable<IAttackable> attackables
        {
            get { return entities.Where(x => x is IAttackable).Select(x => x as IAttackable); }
        }
        public IEnumerable<IPickable> pickables
        {
            get { return entities.Where(x => x is IPickable).Select(x => x as IPickable); }
        }
        public IEnumerable<IObserver> observers
        {
            get { return entities.Where(x => x is IObserver).Select(x => x as IObserver); }
        }

        public string GetNormalDescription()
        {
            return description + String.Join(" ", observables.Select(x => x.GetNormalDescription()));
        }

        public string GetSearchDescription()
        {
            throw new NotImplementedException();
        }

        public bool IsHidden(int detectionValue)
        {
            return false;
        }

        public void OnObserved(IAgent agent)
        {
            throw new NotImplementedException();
        }

        public void OnSearched(IAgent agent)
        {
            throw new NotImplementedException();
        }
    }
}
