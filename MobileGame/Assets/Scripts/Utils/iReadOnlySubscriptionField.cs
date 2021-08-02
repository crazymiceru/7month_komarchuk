using System;

public interface iReadOnlySubscriptionField<T>
{
    public T Value { get; }
    public void Subscribe(Action<T> func);
    public void UnSubscribe(Action<T> func);
}
