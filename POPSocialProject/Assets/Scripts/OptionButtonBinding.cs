using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Binds an option to the button such that when the button is clicked, it's 
/// assigned scenario is assigned to the CurrentScenario.
/// </summary>
public class OptionButtonBinding : MonoBehaviour
{
    /// <summary>
    /// The option this button represents.
    /// </summary>
    public Option BoundOption
    {
        get
        {
            return _boundOption;
        }
        set
        {
            _boundOption = value;
            UpdateButtonDisplay();
        }
    }

    [SerializeField] private ScenarioDisplay _scenarioDisplay;

    /// <summary>
    /// The current scenario variable to update.
    /// </summary>
    [Tooltip("The current scenario variable to update.")]
    [SerializeField] private ScenarioVariable _currentScenario;

    [SerializeField] private TextMeshProUGUI _buttonText;

    /// <summary>
    /// The button used to select the bound option.
    /// </summary>
    [Tooltip("The button used to select the bound option.")]
    [SerializeField] private Button _boundButton;

    private Option _boundOption;

    private void Awake()
    {
        _boundButton.onClick.AddListener(UpdateScenarioDisplay);
        _boundButton.onClick.AddListener(AssignNewCurrentScenario);
    }

    /// <summary>
    /// Assigns the result of the bound option to the current scenario
    /// </summary>
    private void AssignNewCurrentScenario()
    {
        _currentScenario.Value = BoundOption.ResultScenario;
    }

    private void UpdateScenarioDisplay()
    {
        _scenarioDisplay.ChangeImage = BoundOption.ChangeImage;
    }

    private void UpdateButtonDisplay()
    {
        _buttonText.text = BoundOption.OptionText;
    }
}
