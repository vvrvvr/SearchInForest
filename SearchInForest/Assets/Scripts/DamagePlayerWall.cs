using UnityEngine;

public class DamagePlayerWall : MonoBehaviour
{
    [SerializeField] private float bounceSpeedMultiplier = 1f;
    [SerializeField] private float impulsePower = 1;
    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, если объект с тэгом "Player" входит в триггер
        if (collision.gameObject.CompareTag("Player" ))
        {
            GameManager.Instance.ReduceLives(1,impulsePower);
            PlayerController.Instance.currentRotationSpeed *= bounceSpeedMultiplier;
        }
    }
}
