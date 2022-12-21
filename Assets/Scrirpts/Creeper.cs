using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creeper : MonoBehaviour
{
    private Rigidbody[] Rb;
    private Collider[] Col;
    private void Start()
    {
        Rb = GetComponentsInChildren<Rigidbody>();
        Col = GetComponentsInChildren<Collider>();
    }
    public void Cut()
    {
        foreach (var col in GetComponents<Collider>())
        {
            col.isTrigger = false;
        }
        foreach (var rb in Rb)
        {
            rb.isKinematic = false;
        }
        foreach (var col in GetComponents<Collider>())
        {
            col.enabled = false;
        }
    }
}
