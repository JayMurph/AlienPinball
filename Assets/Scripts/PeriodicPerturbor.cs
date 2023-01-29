using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PeriodicPerturbor : MonoBehaviour
{
    public Vector3 Axis;
    public float MinSecs;
    public float MaxSecs;
    public float Force;

    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        StartCoroutine(Perturb());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Axis);
    }

    private IEnumerator Perturb()
    {
        while (true)
        {
            float t = Random.Range(MinSecs, MaxSecs);
            yield return new WaitForSeconds(t);
            float angle = Random.Range(0, 360);
            Vector3 direction = Quaternion.AngleAxis(angle, Axis).eulerAngles;
            body.AddForce(direction * Force, ForceMode.Force);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
