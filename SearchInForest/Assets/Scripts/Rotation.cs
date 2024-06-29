using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed = 60.0f; 

    void FixedUpdate()
    {
        // Получаем текущий угол вокруг локальной оси Y
        Vector3 currentEulerAngles = transform.localEulerAngles;

        // Вычисляем новый угол вокруг локальной оси Y
        float newRotation = currentEulerAngles.y + (speed * Time.fixedDeltaTime);

        // Устанавливаем новый угол вокруг локальной оси Y
        transform.localEulerAngles = new Vector3(currentEulerAngles.x, newRotation, currentEulerAngles.z);

    }
}
