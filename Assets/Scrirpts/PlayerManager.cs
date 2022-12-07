using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Animator animator; 
    [SerializeField] private GameObject[] heroes; 
    [SerializeField] private Transform mainCamera; 
    private PlayerMove playerInput;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 3.0f;
    private float climbingSpeed = 1.0f;
    private float gravityValue = -9.81f;
    private bool isClimbing;
    private bool isPushingRock;
    // Start is called before the first frame update
    private void Awake()
    {
        playerInput = new PlayerMove();
        controller = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Start()
    {}

    void Update()
    {
        if (!isClimbing && !isPushingRock)
            Walking();
        else if (isClimbing)
            Climbing();
        else PushingRock();
            

        mainCamera.position = new Vector3(transform.position.x,4,transform.position.z-4);
    }
    private void Walking()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    private void Climbing()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    private void PushingRock()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    public void ChangeRig()
    {
        foreach (var hero in heroes) {
            if (hero.activeSelf)
                animator = hero.GetComponent<Animator>();
        }

    }
    public void ChangeCharacter(int i)
    {
        foreach (var hero in heroes) 
            hero.SetActive(false);
        heroes[i].SetActive(true);
        ChangeRig();
    }
    public bool CheckHero(int index)// проверка, какой персонаж юзается
    {
        for (int i = 0; i < heroes.Length; i++)
        {
            if (heroes[i].activeSelf)
            {
                if (i == index)
                    return true;
            }
        }
        return false;
    }
    private void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)//обработка всех препятствий, которые может обойти игрок через тег тригера
        {
            case "Ladder":
                if (CheckHero(1))
                {
                    animator.SetBool("isClimbing",true);
                }
                else { Debug.Log("ss"); break; }
                break;
            case "Creepers":
                if (CheckHero(1))
                {
                    Debug.Log("sadasdsad");
                }
                else { Debug.Log("ss"); break; }
                break;
            case "Rock":
                if (CheckHero(0))
                {
                    Debug.Log("sadasdsad");
                }
                else { Debug.Log("ss"); break; }
                break;
            case "Wall":
                if (CheckHero(0))
                {
                    Debug.Log("sadasdsad");
                }
                else { Debug.Log("ss"); break; }
                break;
        }
    }
}
