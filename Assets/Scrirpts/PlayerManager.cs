using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] heroes;
    [SerializeField] private PlayerMovement PlayerMovement;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeCharacter(int i)
    {
        foreach (var hero in heroes)
            hero.SetActive(false);
        heroes[i].SetActive(true);
        PlayerMovement.ChangeRig();
    }
}
