using TMPro;
using UnityEngine;

public class Bomb : MonoBehaviour
{
   //[SerializeField] private MeshRenderer fieldMeshRenderer;
   [SerializeField] private GameObject parent;
   [SerializeField] private GameObject explosion;
   public TextMeshProUGUI timerText;

   [Space(15)]
   [HideInInspector] public float bombShakePower;
   [HideInInspector] public int bombTimer; 
    // Время таймера в секундах
    private float timeRemaining;    // Оставшееся время таймера
    private bool isTimerRunning;    // Флаг для определения, идет ли таймер
    
    void Start()
    {
        //fieldMeshRenderer.enabled = true;
        timeRemaining = bombTimer;
        isTimerRunning = true;
        UpdateTimerText();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                isTimerRunning = false;
                OnTimerComplete();
            }

            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
        }
    }

    private void OnTimerComplete()
    {
        Explode();
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        GameManager.Instance.CameraShake(bombShakePower);
        ExplodeObjectsInsideCollider();
        Destroy(parent);
    }
    

    public void ReduceTime(int seconds)
    {
        if (isTimerRunning)
        {
            timeRemaining -= seconds;
            if (timeRemaining < 0f)
            {
                timeRemaining = 0.1f;
            }
            UpdateTimerText();
        }
    }
    
    private void ExplodeObjectsInsideCollider()
    {
        float explosionDistance = GetComponent<SphereCollider>().radius * transform.localScale.x;
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionDistance);
        
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != parent) // Исключаем сам объект
            {
                IExplodable interactable = collider.gameObject.GetComponent<IExplodable>();
                if (interactable != null)
                {
                    interactable.BombExplode();
                }
            }
        }
    }
}
