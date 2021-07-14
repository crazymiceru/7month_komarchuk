using System;

public interface IReadOnlySubscriptionAction
{
    public void Subscribe(Action func);
    public void UnSubscribe(Action func);

}
