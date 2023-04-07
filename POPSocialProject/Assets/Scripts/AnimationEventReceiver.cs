using UnityEngine;
using UnityEngine.Events;

public class AnimationEventReceiver : MonoBehaviour
{
    [SerializeField] private UnityEvent Response;

    public void InvokeAnimationEventResponse()
    {
        Response.Invoke();
    }
}
