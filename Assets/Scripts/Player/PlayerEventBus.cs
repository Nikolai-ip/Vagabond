using System;
using System.Collections.Generic;
using Abstract.EventBus;

public class PlayerEventBus
{
    private static Dictionary<Type, List<Delegate>> _signalCallbacks = new();

    public static void Subscribe<T>(Action<T> callBack) where T:ISignal
    {
        var key = typeof(T);
        if (_signalCallbacks.ContainsKey(key))
        {
            _signalCallbacks[key].Add(callBack);   
        }
        else
        {
            _signalCallbacks.Add(key,new List<Delegate>{callBack});
        }
    }

    public static void Invoke<T>(T signal) where T:ISignal
    {
        if (_signalCallbacks.TryGetValue(signal.GetType(), out var actions))
        {
            InvokeActions(actions,signal);
        }
        else
        {
            throw new Exception($"The EventBus has not a value with the key: {signal.ToString()}");
        }
    }
    private static void InvokeActions(List<Delegate> actions, ISignal signal)
    {
        foreach (var action in actions)
        {
            action.DynamicInvoke(signal);
        }
    }
}
