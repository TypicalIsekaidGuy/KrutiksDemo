using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Rigidbody[] Rb;
    private Collider[] Col;
    [SerializeField] private GameObject Vines;
    [SerializeField] private GameObject VinesTriger;
    private bool hasVines;
    private void Start()
    {
        Rb = GetComponentsInChildren<Rigidbody>();
        Col = GetComponentsInChildren<Collider>();
        hasVines = Vines;
    }
    public void Explode()
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
        if (hasVines)
        {
            Destroy(Vines);
            Destroy(VinesTriger);
        }
    }
    //public void TurnOffColliders() // ”беру пока что отдельный метод, переместив экземпл€р этого метода в метод Explode
    //{
    //    foreach (var col in GetComponents<Collider>())
    //    {
    //        col.enabled = false;
    //    }
    //}
}
