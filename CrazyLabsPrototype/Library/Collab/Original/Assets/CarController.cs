using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float fAccel;
    [SerializeField] private float rAccel;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotPower;
    [SerializeField] private float g;
    [SerializeField] private float drag;
    private float speedInput=0f, turnInput;
    private bool grounded;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundRayLength = 0.5f;
    [SerializeField] private Transform groundRayPoint;
    [SerializeField] private Transform leftFrontWheel, rightFrontWheel;
    [SerializeField] private float maxTurn;


    private void Start()
    {
        rb.transform.parent = null;
    }

    private void Update()
    {
        GetInput();

        Turn();

        if(grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * rotPower * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        transform.position = rb.transform.position;

    }

    private void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * fAccel * 1000f;
        }
        else
        {
            speedInput = Input.GetAxis("Vertical") * rAccel * 1000f;
        }

        turnInput = Input.GetAxis("Horizontal");
    }

    void Turn()
    {
        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, (turnInput * maxTurn), rightFrontWheel.localRotation.eulerAngles.z);
    }

    void Move()
    {
        grounded = false;
        RaycastHit hit;

        if(Physics.Raycast(groundRayPoint.position,-transform.up,out hit, groundRayLength, whatIsGround))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal)*transform.rotation;
        }

        if(grounded)
        {
            rb.drag = drag;
            Debug.Log(transform.forward);
            rb.AddForce(transform.forward * speedInput);
        }
        else
        {
            rb.drag = 0.1f;
            rb.AddForce(Vector3.up * -g * 100f);
        }
    }
}
