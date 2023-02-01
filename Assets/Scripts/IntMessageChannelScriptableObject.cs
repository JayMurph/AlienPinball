/**
 * FILE             : IntMessageChannelScriptableObject.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the IntMessageChannelScriptableObject class
 */
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allows publishing and subscribing to integer value events across all scenes
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/IntMessageChannelScriptableObject")]
public class IntMessageChannelScriptableObject : ScriptableObject
{
    public UnityEvent<int> Event;
}
