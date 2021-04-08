using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float fAccel;
    [SerializeField] private float rAccel;
    [SerializeField] private float rotPower;
    [SerializeField] private float g;
    [SerializeField] private float drag;
    private float forceMultiplier = 1, forceIncreaseMultiplier, forceDecreaseMultiplier;
    private float speedInput=0f, turnInput;
    private bool grounded;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundRayLength = 0.5f;
    [SerializeField] private Transform groundRayPoint;
    [SerializeField] private Transform leftFrontWheel, rightFrontWheel;
    [SerializeField] private float maxTurn;
    [SerializeField] private float touchSensitivity;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private float maxrate;
    private float emissionrate;
    
    private AudioSource audioSource;
    private bool playingSound = false;
    private bool previousInput;
    private float lastPlayed=0f;

    private CarSceneManagaer carSceneManagaer;
    //private InputManager inputManager;


    private void Start()
    {
        rb.transform.parent = null;
        fAccel = UIManager.Instance.ForceSlider.value;
        touchSensitivity = UIManager.Instance.Sensitivity.value;
        forceIncreaseMultiplier = UIManager.Instance.ForceIncreaseMultiplier.value;
        forceDecreaseMultiplier = UIManager.Instance.ForceDecreaseMultiplier.value;
        audioSource = GetComponent<AudioSource>();
        carSceneManagaer = FindObjectOfType<CarSceneManagaer>();
        //inputManager = InputManager.Instance;
    }

    private void Update()
    {
        GetInput();

        Turn();

        PlaySound();

        if(grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * rotPower * Time.deltaTime, 0f));
        }

        transform.position = rb.transform.position;

    }

    private void FixedUpdate()
    {
        Move();
        Emit();
    }

    void GetInput()
    {
        /*if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * fAccel * 1000f;
        }
        else
        {
            speedInput = Input.GetAxis("Vertical") * rAccel * 1000f;
        }*/

        speedInput = fAccel * 1000f;

        //turnInput = Input.GetAxis("Horizontal");
        //turnInput = inputManager.touchInput;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Touch touch = Input.touches[0];
            turnInput = touch.deltaPosition.x * touchSensitivity;
        }
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

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }


        emissionrate = maxrate;

        if(grounded)
        {
            
            rb.drag = drag;
            rb.AddForce(forceMultiplier * transform.forward * speedInput);
            //Debug.Log(rb.velocity.magnitude);
            emissionrate = maxrate;
        }
        else
        {
            rb.drag = 0.1f;
            rb.AddForce(Vector3.up * -g * 100f);
        }
    }

    public void changeForceMultiplier(int mul)
    {
        if (mul == 2)
        {
            StartCoroutine(ChangeForce(forceIncreaseMultiplier));
        }
        else
        {
            StartCoroutine(ChangeForce(forceDecreaseMultiplier));
        }

    }

    public IEnumerator ChangeForce(float magni)
    {
        float elapsed = 0f, duration = 1f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            forceMultiplier = magni;
            yield return null;
        }

        forceMultiplier = 1f;
    }

    public void Emit()
    {
        var emission = dust.emission;
        emission.rateOverTime = emissionrate;
    }

    void PlaySound()
    {
        if(!audioSource.isPlaying)
        {
            playingSound = false;
        }
        if (turnInput == 0) 
        {
            previousInput = false;
        }
        else
        {
            if(!playingSound)
            {
                Debug.Log("PlayingSound");
                
                if (previousInput)
                {
                    if ((lastPlayed + 1f) < Time.time)
                    {
                        audioSource.Play();
                        playingSound = true;
                        lastPlayed = Time.time;
                        
                    }
                }
                else
                {
                    audioSource.Play();
                    playingSound = true;
                    lastPlayed = Time.time;
                }
            }
            previousInput = true;
            Debug.Log(previousInput);
        }
    }
}
