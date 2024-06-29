using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject center1;
    public GameObject center2;
    public GameObject body;
    public GameObject red;
    public GameObject blue;
    private Transform position1;
    private Transform position2;
    public Rigidbody rb1;
    public Rigidbody rb2;
    public GameObject effect;
    //private AudioSource audio;
    
    private Rigidbody rb;
    public bool isRotatingClockwise = true;
    public GameObject currentCenter = null;
    [Space(50)] 
    private float normalRotationSpeed; // Нормальная скорость вращения
    public float currentRotationSpeed; // Текущая скорость вращения
    private float accelerationTimer; // Таймер ускорения

    private Rigidbody currentRb;
    
    
    public float rotationSpeed = 100.0f;
    public float rotationSpeedMax = 200.0f; // Максимальная скорость вращения
    public float accelerationTime = 1.0f;
    public float deaccelerationTime = 0.5f; // Время ускорения до максимальной скорости

    //public GameObject currentCenter = null;
    private float interactionDistance = 2f;
    [HideInInspector] public bool hasControl;

    
    [Space(10)] 
    [SerializeField] private bool iFramesAfterDamage = false;
    [SerializeField] private float iFramseDuration = 0.3f;

    private bool isChangeCenter = false;
    

    private static PlayerController _instance;

    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerController>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("YourScriptSingleton");
                    _instance = singletonObject.AddComponent<PlayerController>();
                }
            }

            return _instance;
        }
    }

    private void Start()
    {
       
       
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (!hasControl)
            return;
        
        currentRb.velocity = Vector3.zero;
        currentRb.transform.rotation = Quaternion.Euler(0f, currentRb.transform.rotation.eulerAngles.y, 0f);
        
        HandleInput();
        HandleAcceleration();
    }

    private void FixedUpdate()
    {
        if (!hasControl)
        {
            return;
        }
        
        HandleRotation();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            isRotatingClockwise = true;
            isChangeCenter = true;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isRotatingClockwise = false;
            isChangeCenter = true;
        }
        
        if (isChangeCenter)
        {
            isChangeCenter = false;
            //isRotatingClockwise = !isRotatingClockwise;
           
                
            
            if (currentCenter == center1)
                currentCenter = center2;
            else
                currentCenter = center1;

            if (currentCenter == center1)
            {
                center1.transform.position = new Vector3(position1.position.x, 0f, position1.position.z);
                center1.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                body.transform.SetParent(center1.transform);
                center2.transform.SetParent(null);
                currentRb = rb1;
            }
            else
            {
                center2.transform.position = new Vector3(position2.position.x, 0f, position2.position.z);
                center2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                body.transform.SetParent(center2.transform);
                center1.transform.SetParent(null);
                currentRb = rb2;
            }

            currentRotationSpeed = normalRotationSpeed;
            accelerationTimer = 0f;

            InteractWithFloor();
        }
    }

    private void HandleAcceleration()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (accelerationTimer < accelerationTime)
            {
                accelerationTimer += Time.deltaTime;
                currentRotationSpeed = Mathf.Lerp(normalRotationSpeed, rotationSpeedMax,
                    accelerationTimer / accelerationTime);
            }
        }
        else
        {
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, normalRotationSpeed,
                Time.deltaTime / deaccelerationTime);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentRotationSpeed = normalRotationSpeed;
            accelerationTimer = 0f;
        }
    }

    private void HandleRotation()
    {
        if (isRotatingClockwise)
        {
            currentRb.angularVelocity = Vector3.up * currentRotationSpeed * Time.fixedDeltaTime;
        }
        else
        {
            currentRb.angularVelocity = -Vector3.up * currentRotationSpeed * Time.fixedDeltaTime;
        }
    }

    public void Death()
    {
        Instantiate(effect, body.transform.position, Quaternion.identity);
        Debug.Log("death");
        currentRb.angularVelocity = Vector3.zero;
        currentRb.velocity = Vector3.zero;
        hasControl = false;
        currentCenter.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        currentCenter.SetActive(false);
    }

    public void DisablePlayer()
    {
        currentRb.angularVelocity = Vector3.zero;
        currentRb.velocity = Vector3.zero;
        hasControl = false;
        currentCenter.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        currentCenter.SetActive(false);
    }

   

    private void InteractWithFloor()
    {
        if (currentCenter != null)
        {
            RaycastHit hit;
            Ray ray = new Ray(currentCenter.transform.position, Vector3.down);
            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                if ( interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }

    public void ChangeRotation()
    {
        isRotatingClockwise = !isRotatingClockwise;
    }

    public void SpawnPlayer(Vector3 position)
    {
        if(currentCenter !=null)
            currentCenter.SetActive(true);
        hasControl = true;
        position1 = red.transform;
        position2 = blue.transform;
        normalRotationSpeed = rotationSpeed;
        currentRb = rb1;
        center1.transform.SetParent(null);
        center1.transform.position = new Vector3(position1.position.x, 0f, position1.position.z);
        center1.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        body.transform.SetParent(center1.transform);
        center2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        center2.transform.position = new Vector3(position2.position.x, 0f, position2.position.z);
        center2.transform.SetParent(null);
        currentRotationSpeed = normalRotationSpeed;
        
        currentCenter = center1;
        currentCenter.transform.position = new Vector3(position.x, 0f, position.z);
        currentCenter.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    
}
