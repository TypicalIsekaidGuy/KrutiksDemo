using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // � ������� ���������, ������� ����� ����������� � ���������� ����� ��� ���������
    [Header("MainObjects")]
    [SerializeField] private Animator animator; 
    [SerializeField] private GameObject[] heroes; 
    [SerializeField] private Transform mainCamera; 
    [SerializeField] private UIManager uimanager; 
    [SerializeField] private GameManager gameManager; 
    private PlayerMove playerInput;
    private CharacterController controller;

    [Header("Movement")]
    private bool isClimbing;
    private bool isPushingRock;
    [Header("Walking")]
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 3.0f;
    private float gravityValue = -9.81f;
    [Header("Climbing")]
    private float climbingSpeed = 5.0f;
    private Vector3 climbingVector;
    [Header("Pushing rock")]
    private float pushingRockSpeed = 1.0f;
    [Header("Break rock")]
    private Wall wall;
    private Vector3 lookToWall;
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

    void Update()// ����� ������ ��������� ������ ��� ������������ � ������
    {
        if (!isClimbing && !isPushingRock)
            Walking();
        else if (isClimbing)
            Climbing();
        else  PushingRock();

        Debug.Log(playerVelocity);
        mainCamera.position = new Vector3(transform.position.x,4,transform.position.z-4);
    }
    private void Walking()//������������ ��� ������ ��������� �������� �� ���������
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
    private void Climbing()//������������ ��� ������ �������� ���������� playerVelocity, ������� ����� ������ ����������� ��������� ����� Move
    {
        if (!animator.GetBool("isClimbing"))
        {
            animator.SetBool("isClimbing", true);
            playerVelocity = climbingVector;
            playerVelocity.y = climbingSpeed;
        }
        transform.forward = climbingVector;
        controller.Move(playerVelocity * Time.deltaTime);
    }
/*    private void WallCheck()//�������� ������� ���� �����, ����� �������� ��� ���������� �� ����� ������ ���� �����, ����� ��� ����� ��� �����
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);

        bool newWall = frontWallHit.transform != lastWall || Mathf.Abs(Vector3.Angle(lastWallNormal, frontWallHit.normal)) > minWallNormalAngleChange;

        if ((wallFront && newWall) || pm.grounded)
        {
            climbTimer = maxClimbTime;
            climbJumpsLeft = climbJumps;
        }
    }*/
    private void PushingRock()//���� ��� ��� �������� ��� ��������, �� � �� ������� ���� ���
    {
    }
    public void ChangeRig()
    {
        foreach (var hero in heroes) {
            if (hero.activeSelf)
                animator = hero.GetComponent<Animator>();
        }

    }
    public void ChangeCharacter(int i)//����� ���������� ����������� ����� ������ ��������� � ���������� ����� �������� �� player
    {
        foreach (var hero in heroes) 
            hero.SetActive(false);
        heroes[i].SetActive(true);
        ChangeRig();
    }
    public bool CheckHero(int index)// ��������, ����� �������� �������
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
    private void OnTriggerStay(Collider other)//��������� ���� �����������, ������� ����� ������ �����
    {
        switch (other.gameObject.tag)
        {
            case "Ladder":
                if (CheckHero(1))
                {
                    climbingVector = other.gameObject.transform.forward;
                    isPushingRock = false;
                    isClimbing = true;
                }
                else { break; }
                break;
            case "Creepers":
                if (CheckHero(1))
                {

                }
                else {break; }
                break;
            case "Rock":
                if (CheckHero(0))
                {
                    isPushingRock = true;
                    isClimbing = false;
                }
                else {  break; }
                break;
        }
    }
    private void OnTriggerEnter(Collider other)//��������� ��� �����������, ������� ��� ����������� ����� ��������� 1 ��� ��� ����� � ������
    {
        switch (other.gameObject.tag)
        {
            case "Wall":
                uimanager.DeactivateJoystick();
                wall = other.gameObject.GetComponent<Wall>();
                uimanager.ActivateButton(uimanager.royPunch);
                lookToWall = other.gameObject.transform.position;
                break;
            case "Food":
                gameManager.ChangeEnergy();
                gameManager.SaveData();
                Destroy(other.gameObject);
                break;
        }
    }
    private void OnTriggerExit(Collider other)//��������� ��� �����������, ������� ��� ����������� ����� ��������� 1 ��� ��� ������ �� ������
    {

        switch (other.gameObject.tag)
        {
            case "Wall":
                uimanager.ActivateJoystick();
                uimanager.DeactivateButton(uimanager.royPunch);
                wall.TurnOffColliders();
                break;
            case "Ladder":
                isClimbing = false;
                animator.SetBool("isClimbing", false);
                playerVelocity.y = 0f;
                playerVelocity.z = 0f;
                playerVelocity.x = 0f;
                break;
        }
    }
    public void Explode()//����� ��� ����������� �����
    {
        float z = transform.rotation.z;
        transform.LookAt(lookToWall);
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, z));
        animator.SetBool("IsPunching", true);
        StartCoroutine(PlayAnimation());
    }
    //� ���� �� ���� ��� ������� ����������� ���������� �����, ������� ������ ����� ��������
    private IEnumerator PlayAnimation()//�������� ��� ����������� �����, ������� �������� ������� ��������� ������ �� �������� �������� � ���� ��, �� ���������� �����, �������� �����
    {
        for (int i=0; i < 11; i++)
        {
            if(animator.GetBool("IsPunching")&& animator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.5f)//���� ��������
                wall.Explode();
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)//�������� �� ����� �������� � ���� ��, �� �������� ���������� ��� ��������
                animator.SetBool("IsPunching",false);
            yield return new WaitForSeconds(0.2f);//������� ��� ��������: �� ������� ����� (�� 0,2 �������, ��� ������� �� ���� ������), ������� ���������������� ���� (��� ����� ����� ��� �� ��������)
        }
    }
}
