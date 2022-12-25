using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BridgeAnim : MonoBehaviour
{
    PlayerManager pl;
    Animation anim;
    BoxCollider triggerCol;
    public GameObject rope;
    // Start is called before the first frame update
    void Start()
    {
        pl = FindObjectOfType<PlayerManager>();
        anim = GetComponent<Animation>();
        triggerCol= GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (pl.CheckHero(1) && other.gameObject.tag == "Player")
        {
                anim.Play("Bridge Anim");
                triggerCol.enabled = false;
            pl.CutRope();
            Destroy(rope);
        }
    }
}
