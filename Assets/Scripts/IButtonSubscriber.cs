using UnityEngine.UI;

public interface IButtonSubscriber
{
    void OnButtonPressed(ButtonController b);
    void OnButtonReleased(ButtonController b);
}