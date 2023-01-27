using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TentacleGateController : MonoBehaviour
{
    [SerializeField]
    private string ballTag;

    [SerializeField]
    private string isOpenPropertyName;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnValidate()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        animator.SetBool(isOpenPropertyName, true);
    }

    public void Close()
    {
        animator.SetBool(isOpenPropertyName, false);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == ballTag)
        {
            Close();     
        }
    }
}
