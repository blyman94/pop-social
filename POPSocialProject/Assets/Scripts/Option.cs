using System;
using UnityEngine;

/// <summary>
/// Represents one of the options a player has when deciding what to do in a 
/// scenario.
/// </summary>
[Serializable]
public class Option
{
    /// <summary>
    /// What will this option say to the player?
    /// </summary>
    [TextArea(2,2)]
    [Tooltip("What will this option say to the player?")]
    public string OptionText;

    /// <summary>
    /// Which scenario will result from this option?
    /// </summary>
    [Tooltip("Which scenario will result from this option?")]
    public Scenario ResultScenario;

    /// <summary>
    /// Does selecting this option change the image the player sees?
    /// </summary>
    [Tooltip("Does selecting this option change the image the player sees?")]
    public bool ChangeImage = false;
}
