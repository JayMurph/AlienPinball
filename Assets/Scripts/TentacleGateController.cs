using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleGateController : MonoBehaviour
{
    [SerializeField]
    private string ballTag;

    [SerializeField]
    private string isOpenPropertyName;

    [SerializeField]
    private Animator animator;

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
