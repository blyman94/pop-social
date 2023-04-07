using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Scenario _startScenario;
    [SerializeField] private ScenarioVariable _currentScenario;

    private void Start()
    {
        _currentScenario.Value = _startScenario;
    }
}
