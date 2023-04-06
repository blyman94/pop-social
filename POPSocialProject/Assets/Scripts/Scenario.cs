using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A data object describing a scenario, or a player turn in the game. In each
/// scenario the player must choose an option that will lead to a new scenario.
/// </summary>
[CreateAssetMenu]
public class Scenario : ScriptableObject
{
    /// <summary>
    /// The description of the scenario to be displayed to the player.
    /// </summary>
    [TextArea(10,10)]
    [Tooltip("The description of the scenario to be displayed to the player.")]
    public string ScenarioText;

    /// <summary>
    /// The visual associated with this scenario.
    /// </summary>
    [Tooltip("The visual associated with this scenario.")]
    public Sprite ScenarioImage;

    /// <summary>
    /// Options to be displayed to the player in this scenario. There should be
    /// no more than 4 options.
    /// </summary>
    [Tooltip("Options to be displayed to the player in this scenario. There " + 
        "should be no more than 4 options.")]
    public List<Option> Options;
}
