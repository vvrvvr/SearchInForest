using System;
using UnityEngine;
using DG.Tweening;  // Import DOTween

public class PlayerController : MonoBehaviour
{
    public GameObject center1;
    public GameObject center2;
    public GameObject body;
    public GameObject red;
    public GameObject blue;
    public Rigidbody rb1;
    public Rigidbody rb2;
    public GameObject effect;
    public GameObject testMesh;

    private Transform position1;
    private Transform position2;
    private Rigidbody currentRb;
    public GameObject currentCenter;
    private bool isRotatingClockwise = true;
    private bool isChangeCenter = false;
    public bool hasControl;

    private float normalRotationSpeed;
    public float currentRotationSpeed;
    private float accelerationTimer;

    public float rotationSpeed = 100.0f;
    public float rotationSpeedMax = 200.0f;
    public float accelerationTime = 1.0f;
    public float deaccelerationTime = 0.5f;
    private float interactionDistance = 2f;

    public float meshRotationTime = 1.0f; // Time in seconds for mesh rotation

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
                    GameObject singletonObject = new GameObject("PlayerControllerSingleton");
                    _instance = singletonObject.AddComponent<PlayerController>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        // Set initial rotation of testMesh
        if (testMesh != null)
        {
            testMesh.transform.localRotation = Quaternion.Euler(0f, -90f, 90f);
        }
    }

    private void Update()
    {
        if (!hasControl) return;

        currentRb.velocity = Vector3.zero;
        currentRb.transform.rotation = Quaternion.Euler(0f, currentRb.transform.rotation.eulerAngles.y, 0f);

        HandleInput();
        HandleAcceleration();
    }

    private void FixedUpdate()
    {
        if (!hasControl) return;

        HandleRotation();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRotatingClockwise = !isRotatingClockwise;
            isChangeCenter = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isChangeCenter)
            {
                SwitchCenter();
            }
            InteractWithFloor();
        }

        if (!Input.GetKey(KeyCode.Space))
        {
            currentRb.angularVelocity = Vector3.zero;
        }
    }

    private void HandleAcceleration()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (accelerationTimer < accelerationTime)
            {
                accelerationTimer += Time.deltaTime;
                currentRotationSpeed = Mathf.Lerp(normalRotationSpeed, rotationSpeedMax, accelerationTimer / accelerationTime);
            }
        }
        else
        {
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, normalRotationSpeed, Time.deltaTime / deaccelerationTime);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentRotationSpeed = normalRotationSpeed;
            accelerationTimer = 0f;
        }
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentRb.angularVelocity = (isRotatingClockwise ? Vector3.up : -Vector3.up) * currentRotationSpeed * Time.fixedDeltaTime;
        }
    }

    public void Death()
    {
        Instantiate(effect, body.transform.position, Quaternion.identity);
        Debug.Log("death");
        DisablePlayer();
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
        if (currentCenter == null) return;

        Ray ray = new Ray(currentCenter.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            interactable?.Interact();
        }
    }

    private void SwitchCenter()
    {
        isChangeCenter = false;

        currentCenter = currentCenter == center1 ? center2 : center1;

        if (currentCenter == center1)
        {
            SetCenterTransform(center1, position1);
            SetCenterTransform(center2, null);
            currentRb = rb1;
        }
        else
        {
            SetCenterTransform(center2, position2);
            SetCenterTransform(center1, null);
            currentRb = rb2;
        }

        currentRotationSpeed = normalRotationSpeed;
        accelerationTimer = 0f;
    }

    private void SetCenterTransform(GameObject center, Transform position)
    {
        if (position != null)
        {
            center.transform.position = new Vector3(position.position.x, 0f, position.position.z);
            center.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            body.transform.SetParent(center.transform);
        }
        else
        {
            center.transform.SetParent(null);
        }
    }

    public void ChangeRotation()
    {
        isRotatingClockwise = !isRotatingClockwise;

        if (testMesh != null)
        {
            // Check the current local rotation of testMesh on the Z axis
            float currentZRotation = testMesh.transform.localEulerAngles.z;
            // Determine the target rotation
            float targetZRotation = Mathf.Approximately(currentZRotation, 90f) ? -90f : 90f;
            // Use DOTween to rotate the mesh over meshRotationTime seconds
            testMesh.transform.DOLocalRotate(new Vector3(testMesh.transform.localEulerAngles.x, testMesh.transform.localEulerAngles.y, targetZRotation), meshRotationTime);
        }
    }

    public void SpawnPlayer(Vector3 position)
    {
        if (currentCenter != null)
        {
            currentCenter.SetActive(true);
        }

        hasControl = true;
        position1 = red.transform;
        position2 = blue.transform;
        normalRotationSpeed = rotationSpeed;
        currentRb = rb1;
        SetCenterTransform(center1, position1);
        SetCenterTransform(center2, position2);
        currentRotationSpeed = normalRotationSpeed;

        currentCenter = center1;
        currentCenter.transform.position = new Vector3(position.x, 0f, position.z);
        currentCenter.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
