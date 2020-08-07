using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    // Here we setup the other components we need;
    [Header("References")]
    [SerializeField] CharacterController controller;
    public GameObject GFX;
    public Animator anim;

    // Here we define the values for how the character moves;
    [Header("Character Attributes")]
    public float runSpeed;
    public float turnSpeed, baseSpeed;

    // Here we set variables needed for the controller;
    [Header("Controller Configuration")]
    [SerializeField] float groundLevel;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] string[] playerInput;

    [Header("Stats")]
    public Vector3 move;
    public bool isGrounded, canMove, isMoving;
    float x, z, distToGround, dir;
    Vector3 addedForce, nextPos;

    // Here we check if the controller's bottom is touching valid Ground;
    void GroundCheck()
    {
        //Left Ground
        if (!Physics.Raycast(transform.position, -transform.up, groundLevel, groundLayer) && isGrounded)
        {

        }        

        //Touched Ground
        if (Physics.Raycast(transform.position, -transform.up, groundLevel, groundLayer) && !isGrounded)
            move.y = 0;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundLevel, groundLayer))
            isGrounded = true;
        else
            isGrounded = false;

        anim.SetBool("IsGrounded", isGrounded);
    }

    // Here we make the character always face the direction of its movement;
    void Orientation()
    {
        nextPos = new Vector3(x, 0, z);

        if (canMove)
        {
            if (x == 0 && z == 0)
                return;
            else
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(nextPos), turnSpeed * Time.deltaTime);
        }
    }

    // Here we pass horizontal input and constant speed to the controller's movement, as well as adding fake gravity;
    void Movement()
    {
        anim.SetBool("IsMoving", isMoving);

        if (canMove)
        {
            if (z != 0 && x != 0)
                move = new Vector3((x * runSpeed * .6f) + addedForce.x, move.y, (z * runSpeed * .6f) + addedForce.z);
            else
                move = new Vector3((x * runSpeed) + addedForce.x, move.y, (z * runSpeed) + addedForce.z);
        }
        else
        {
            x = 0;
            z = 0;
        }

        if (!isGrounded)
            move.y = (Mathf.Lerp(move.y, -30, 3* Time.deltaTime));

        controller.Move(move * Time.deltaTime);

        if ((x != 0 || z != 0) && canMove)
            isMoving = true;
        else
            isMoving = false;
    }

    // Here we fake physics forces;
    public void ApplyForce(Vector3 dir)
    {
        addedForce = dir;
        move.y = addedForce.y;
    }

    // Here we gradually damp the forces inside the fake force vector to 0;
    void ForceDamp()
    {
        if (addedForce.x != 0)
        {
            if (addedForce.x < 0)
                addedForce.x = Mathf.Lerp(addedForce.x, 0.0001f, 5 * Time.deltaTime);
            else if (addedForce.x > 0)
                addedForce.x = Mathf.Lerp(addedForce.x, -0.0001f, 5 * Time.deltaTime);
        }

        if (addedForce.y != 0)
        {
            if (addedForce.y < 0)
                addedForce.y = Mathf.Lerp(addedForce.y, 0.0001f, 5 * Time.deltaTime);
            else if (addedForce.y > 0)
                addedForce.y = Mathf.Lerp(addedForce.y, -0.0001f, 5 * Time.deltaTime);
        }
    }

    // Here we define what stops movement;
    void MovementAllower()
    {
       /* if (GameManager.instance.currentGameState != 3)
            canMove = false;
        else if (combat.isStrongAttacking)
            canMove = false;
        else if (combat.isDead)
            canMove = false;
        else
            canMove = true;
       */

        if (canMove)
        {
            if (runSpeed < baseSpeed)
                runSpeed += 10 * Time.deltaTime;
            else if (runSpeed > baseSpeed)
                runSpeed = baseSpeed;
        }
        else
        {
            if (runSpeed > 0)
                runSpeed -= 10 * Time.deltaTime;
            else if (runSpeed < 0)
                runSpeed = 0;
        }
    }

    // Here we check for input;
    void InputCheck()
    {
        x = Input.GetAxis(playerInput[0]);
        z = Input.GetAxis(playerInput[1]); 
    }

    private void OnEnable()
    {
        if (anim == null)
            anim = transform.Find("GFX").GetComponent<Animator>();
    }

    private void Update()
    {
        MovementAllower();

        InputCheck();
        GroundCheck();

        Movement();
        Orientation();
        ForceDamp();
    }    
}
