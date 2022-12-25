using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    GameManager gameManager;
    Animation anim;
    BoxCollider triggerCol;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        triggerCol = GetComponent<BoxCollider>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.isGetKeyB && other.gameObject.CompareTag("Player"))
        {
            anim.Play("DoorAnim");
            triggerCol.enabled = false;
        }
    }
}
