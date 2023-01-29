using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/IntMessageChannelScriptableObject")]
public class IntMessageChannelScriptableObject : ScriptableObject
{
    public UnityEvent<int> Event;
}
