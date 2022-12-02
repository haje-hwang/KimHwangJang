using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public interface ISubject{
        public void AddObserver(IObserver observer);
        public void RemoveObserver(IObserver observer);
        public void NotifyToObserver();
    }

    public interface IObserver{
        public void Notified(GameObject controlling_Obj);
    }

}