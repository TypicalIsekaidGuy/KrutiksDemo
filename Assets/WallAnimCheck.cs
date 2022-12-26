using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAnimCheck : MonoBehaviour
{
    public bool isAnimCheck = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isAnimCheck = true;
        }
    }
}
