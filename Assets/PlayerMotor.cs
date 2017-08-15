using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    public float moveSpeed = 10f;
    public float jumpSpeed = 7f;
    public float gravity = 30f;

    public float stoppingSpeed = 10f;
    public AnimationCurve stoppingCurve;

    public bool airControl = true;
    [Range(0,1)]
    public float airControlFactor = 0.75f;
        
    private CharacterController characterController;

    private float inputX = 0;
    private float inputY = 0;
    private bool grounded = false;
    private CollisionFlags collision;

    public float maxVelocity = 10;
    private Vector3 velocity = Vector3.zero;
    private Vector3 move = Vector3.zero;
    private bool isMoving = false;

	// Use this for initialization
	void Start () {
        characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        if(Mathf.Abs(Input.GetAxisRaw("Vertical")) != 0 || Mathf.Abs(Input.GetAxisRaw("Horizontal")) !=0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // If both horizontal and vertical are used simultaneously, limit speed (if allowed), so the total doesn't exceed normal move speed
        float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f) ? .7071f : 1.0f;

        if (!grounded)
        {
            if (airControl)
            {
                move.x = ((inputX * moveSpeed * inputModifyFactor) * airControlFactor) * Time.deltaTime;
                move.z = ((inputY * moveSpeed * inputModifyFactor) * airControlFactor) * Time.deltaTime;
            }
            move.y -= gravity * Time.deltaTime;
        }
        else
        {
            move.x = (inputX * moveSpeed * inputModifyFactor) * Time.deltaTime;
            move.z = (inputY * moveSpeed * inputModifyFactor) * Time.deltaTime;
            move.y = -0.75f * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                move.y = jumpSpeed * Time.deltaTime;
            }
            
        }

        grounded = (characterController.Move(move) & CollisionFlags.Below) !=0;

    }



}
