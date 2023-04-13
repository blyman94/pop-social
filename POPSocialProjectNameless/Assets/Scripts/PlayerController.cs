using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ScenarioDisplay _scenarioDisplay;

    public void OnSkipAnimationInput(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _scenarioDisplay.SkipAnimation();
        }
    }
}
