using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MessageChannelScriptableObject")]
public class MessageChannelScriptableObject : ScriptableObject
{
    public UnityEvent Event;
}
