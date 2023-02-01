/**
 * FILE             : GateController.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the TentacleGateController class
 */
using UnityEngine;

/// <summary>
/// Emulates an animated gate object that can automatically close when a
/// particular object through its collider
/// </summary>
public class GateController : MonoBehaviour
{
    /// <summary>
    /// Tag of object that will cause the gate to close when exiting its
    /// trigger collider
    /// </summary>
    [SerializeField]
    private string closingObjectTag;

    /// <summary>
    /// name of animator property to toggle when changing from open to closed
    /// states and vice-versa
    /// </summary>
    [SerializeField]
    private string isOpenPropertyName;

    /// <summary>
    /// Animates the gate opening and closing
    /// </summary>
    [SerializeField]
    private Animator animator;

    /// <summary>
    /// Makes the gate animate itself open
    /// </summary>
    public void Open()
    {
        animator.SetBool(isOpenPropertyName, true);
    }

    /// <summary>
    /// Makes the gate animate itself close 
    /// </summary>
    public void Close()
    {
        animator.SetBool(isOpenPropertyName, false);
    }

    /// <summary>
    /// Closes the gate when a particular object passes through
    /// </summary>
    /// <param name="other">Collider that exited the trigger</param>
    void OnTriggerExit(Collider other)
    {
        if (other.tag == closingObjectTag)
        {
            Close();     
        }
    }
}
