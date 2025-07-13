using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptables/Commands/Plane Commands")]
public class SOCommand : ScriptableObject
{

    private UnityEvent commandEvent = new UnityEvent();

    [Tooltip("Command to be executed (lower case input for ease)")]
    public string command;
    public string output;

    public void RegisterListener(UnityAction listener) => commandEvent.AddListener(listener);

    public void UnregisterListener(UnityAction listener) => commandEvent.RemoveListener(listener);

    public void Invoke() => commandEvent.Invoke();
}
