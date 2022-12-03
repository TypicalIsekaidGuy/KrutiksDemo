using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator; 
    [SerializeField] private GameObject[] heroes; 
    [SerializeField] private Transform mainCamera; 
    private PlayerMove playerInput;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 3.0f;
    private float gravityValue = -9.81f;
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
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput =playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y);
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

        mainCamera.position = new Vector3(transform.position.x,4,transform.position.z-4);
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
}
