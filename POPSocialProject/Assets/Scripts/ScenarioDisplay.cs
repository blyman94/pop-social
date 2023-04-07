using Blyman94.CommonSolutions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioDisplay : MonoBehaviour
{
    /// <summary>
    /// Reference to the current scenario. Display will update when the value 
    /// of this reference changes.
    /// </summary>
    [Header("Data References")]
    [Tooltip("Reference to the current scenario. Display will update" +
        "when the value of this reference changes.")]
    [SerializeField] private ScenarioVariable _currentScenario;

    [SerializeField] private BoolVariable _FLVisited;
    [SerializeField] private BoolVariable _TJVisited;
    [SerializeField] private BoolVariable _YZVisited;

    /// <summary>
    /// Image that displays the scenario visual.
    /// </summary>
    [Header("Component References")]
    [Tooltip("Image that displays the scenario visual.")]
    [SerializeField] private Image _scenarioDisplayImage;

    /// <summary>
    /// Text to display scenario text on.
    /// </summary>
    [Tooltip("Text to display scenario text on.")]
    [SerializeField] private TextMeshProUGUI _scenarioDisplayText;

    /// <summary>
    /// Text to display scenario prompt text on.
    /// </summary>
    [Tooltip("Text to display scenario prompt text on.")]
    [SerializeField] private TextMeshProUGUI _scenarioPromptDisplayText;

    /// <summary>
    /// Transform to which all option button instances are parented.
    /// </summary>
    [Tooltip("Transform to which all option button instances are parented.")]
    [SerializeField] private Transform _optionButtonParentTransform;

    [SerializeField] private OptionButtonBinding[] _optionButtonBindings;

    /// <summary>
    /// Paintbrush reveal animator.
    /// </summary>
    [Header("Animator References")]
    [SerializeField] private Animator _scenarioImageRevealerAnimator;
    [SerializeField] private Animator _scenarioTextRevealerAnimator;

    [Header("Scenario References")]
    [SerializeField] private Scenario _triggerOverviewScenario;
    [SerializeField] private Scenario _eventOverview_FLTJ;
    [SerializeField] private Scenario _eventOverview_FL;
    [SerializeField] private Scenario _eventOverview_FLYZ;
    [SerializeField] private Scenario _eventOverview_YZ;
    [SerializeField] private Scenario _eventOverview_TJYZ;
    [SerializeField] private Scenario _eventOverview_TJ;

    /// <summary>
    /// Is the image currently revealed to the player?
    /// </summary>
    private bool _imageShown = false;

    /// <summary>
    /// Is the MoveToNewScenario sequence currently revealing?
    /// </summary>
    private bool _isRevealing = false;

    /// <summary>
    /// Is the MoveToNewScenario sequence currently Covering?
    /// </summary>
    private bool _isCovering = false;

    public bool ChangeImage { get; set; } = true;

    public bool AnimationPlaying { get; set; } = false;

    private bool _skipTextFade = true;

    /// <summary>
    /// Current coroutine running.
    /// </summary>
    private Coroutine _currentCoroutine;

    private void OnEnable()
    {
        _currentScenario.ValueUpdated += MoveToNextScenario;
    }

    private void OnDisable()
    {
        _currentScenario.ValueUpdated -= MoveToNextScenario;
    }

    public void MoveToNextScenario()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
        _currentCoroutine = StartCoroutine(MoveToNewScenarioRoutine());
    }

    public void SkipAnimation()
    {
        if (_isRevealing)
        {
            StopCoroutine(_currentCoroutine);

            _scenarioTextRevealerAnimator.SetTrigger("IdleIn");

            _scenarioImageRevealerAnimator.SetTrigger("ShowImage");
            _isRevealing = false;
            _imageShown = true;
        }

        if (_isCovering)
        {
            StopCoroutine(_currentCoroutine);

            _scenarioTextRevealerAnimator.SetTrigger("IdleOut");

            if (ChangeImage)
            {
                _scenarioImageRevealerAnimator.SetTrigger("HideImage");
            }

            _isCovering = false;
            _imageShown = false;
            _skipTextFade = true;
            MoveToNextScenario();
        }
    }

    private IEnumerator MoveToNewScenarioRoutine()
    {
        _isCovering = true;

        Scenario currentScenario = _currentScenario.Value;

        if (currentScenario == _triggerOverviewScenario)
        {
            bool fl = _FLVisited.Value;
            bool tj = _TJVisited.Value;
            bool yz = _YZVisited.Value;
            if (!fl && !tj && yz)
            {
                currentScenario = _eventOverview_FLTJ;
            }
            else if (!fl && !yz && tj)
            {
                currentScenario = _eventOverview_FLYZ;
            }
            else if (!tj && !yz && fl)
            {
                currentScenario = _eventOverview_TJYZ;
            }
            else if (!fl && tj && yz)
            {
                currentScenario = _eventOverview_FL;
            }
            else if (!tj && fl && yz)
            {
                currentScenario = _eventOverview_TJ;
            }
            else if (!yz && fl && tj)
            {
                currentScenario = _eventOverview_YZ;
            }
        }

        if (!_skipTextFade)
        {
            yield return FadeOutAllTextRoutine();
        }
        else
        {
            _skipTextFade = false;
        }

        if (ChangeImage)
        {
            if (_imageShown)
            {
                yield return HideImageRoutine();
            }
        }

        _isCovering = false;
        _imageShown = false;

        _scenarioDisplayText.text = currentScenario.ScenarioText;
        _scenarioPromptDisplayText.text = currentScenario.ScenarioPromptText;
        if (currentScenario.ScenarioVisualSprite != null)
        {
            _scenarioDisplayImage.sprite = currentScenario.ScenarioVisualSprite;
        }

        for (int i = _optionButtonParentTransform.childCount - 1; i >= 0; i--)
        {
            _optionButtonParentTransform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < currentScenario.Options.Count; i++)
        {
            _optionButtonBindings[i].BoundOption = currentScenario.Options[i];
            _optionButtonBindings[i].gameObject.SetActive(true);
        }

        _isRevealing = true;

        if (ChangeImage)
        {
            yield return ShowImageRoutine();
        }

        yield return FadeInTextSequentialRoutine();

        _isRevealing = false;
        _imageShown = true;
        ChangeImage = false;
    }

    private IEnumerator HideImageRoutine()
    {
        AnimationPlaying = true;
        _scenarioImageRevealerAnimator.SetTrigger("CoverImage");
        while (AnimationPlaying)
        {
            yield return null;
        }
    }

    private IEnumerator FadeInTextSequentialRoutine()
    {
        AnimationPlaying = true;
        _scenarioTextRevealerAnimator.SetTrigger("FadeSequenceIn");
        while (AnimationPlaying)
        {
            yield return null;
        }
    }

    private IEnumerator FadeOutAllTextRoutine()
    {
        AnimationPlaying = true;
        _scenarioTextRevealerAnimator.SetTrigger("FadeAllOut");
        while (AnimationPlaying)
        {
            yield return null;
        }
    }

    private IEnumerator ShowImageRoutine()
    {
        AnimationPlaying = true;
        _scenarioImageRevealerAnimator.SetTrigger("RevealImage");
        while (AnimationPlaying)
        {
            yield return null;
        }
    }
}
