public interface INotifier<in TSubscriber>
{
    void Subscribe(TSubscriber subscriber);
    void Unsubscribe(TSubscriber subscriber);
}