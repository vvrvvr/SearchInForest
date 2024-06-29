using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform direction;
    [SerializeField] private float cooldownTime = 5.0f;
    [SerializeField] private float bulletSpeed = 1f;
    private float offset = 0.3f;
    private bool canFire = true;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(ShootRoutine());
        
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (canFire)
            {
                Fire();
                canFire = false;
                yield return new WaitForSeconds(cooldownTime);
                canFire = true;
            }
            yield return null;
        }
    }

    private void Fire()
    {
        audio.Play();
        // Рассчитываем направление стрельбы от текущей позиции к позиции direction без учета Y
        Vector3 targetPosition = direction.position;
        targetPosition.y = transform.position.y + offset;
        var correctedTransformposition = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        Vector3 fireDirection = (targetPosition - correctedTransformposition).normalized;

        // Создаем пулю и передаем ей рассчитанное направление
        Bullet newBullet = Instantiate(bulletPrefab, correctedTransformposition, Quaternion.identity);
        newBullet.SetDirection(fireDirection,bulletSpeed );
    }
}