using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class ObservableMonoBehaviour : MonoBehaviour {
    List<IObserver> observers = new List<IObserver>();
    public void Attach(IObserver observer) {
        observers.Add(observer);
    }
    public void Detach(IObserver observer) {
        observers.Remove(observer);
    }
    protected void Notify(Property property) {
        observers.ForEach(y => y.UpdateProperty(property));
    }
    protected void NotifyProgress(float progress) {
        observers.ForEach(y => y.UpdateProgress(progress));
    }
}
public interface IObserver {
    void UpdateProperty(Property property);
    void UpdateProgress(float progress);
}