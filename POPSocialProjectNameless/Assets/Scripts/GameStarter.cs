using Blyman94.CommonSolutions;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Scenario _startScenario;
    [SerializeField] private ScenarioVariable _currentScenario;
    [SerializeField] private BoolVariable[] _boolVariablesToReset;

    private void Start()
    {
        _currentScenario.Value = _startScenario;
        foreach (BoolVariable boolVar in _boolVariablesToReset)
        {
            boolVar.Value = false;
        }
    }
}
