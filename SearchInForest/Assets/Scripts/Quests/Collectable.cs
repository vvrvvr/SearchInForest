using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Quest
{
    [SerializeField] private GameObject effect;
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, если объект с тэгом "Player" входит в триггер
        if (other.CompareTag("Player" ))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Debug.Log("trigger да");
            ConditionComplete();

        }
    }
}
