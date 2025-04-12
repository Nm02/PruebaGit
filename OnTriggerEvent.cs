using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider))]
public class OnTriggerEvent : MonoBehaviour
{

    [SerializeField] TagEvent[] TagEvents = { new TagEvent() };
     

    private void OnTriggerEnter(Collider other)
    {

        // Verificamos en todos los eventos si hay un tag coincidenete
        foreach (TagEvent tagEvent in TagEvents)
        {
            // Si el tag concide invocamos el evento
            if (other.CompareTag(tagEvent.tag))
            {
                tagEvent.onEnter.Invoke();
                break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificamos en todos los eventos si hay un tag coincidenete
        foreach (TagEvent tagEvent in TagEvents)
        {
            // Si el tag concide invocamos el evento
            if (other.CompareTag(tagEvent.tag))
            {
                tagEvent.onExit.Invoke();
                break;
            }
        }
    }

}

[System.Serializable]
class TagEvent
{
    public string tag = "Player";
    public UnityEvent onEnter;
    public UnityEvent onExit;
}

