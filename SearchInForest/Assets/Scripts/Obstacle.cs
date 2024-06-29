using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // private void OnTriggerEnter(Collider other)
    // {
    //     // Проверяем, если объект с тэгом "Player" входит в триггер
    //     if (other.CompareTag("Player" ))
    //     {
    //         Rotation.Instance.isRotatingClockwise = !Rotation.Instance.isRotatingClockwise;
    //         Debug.Log("trigger да");
    //         
    //     }
    // }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, если объект с тэгом "Player" входит в триггер
        if (collision.gameObject.CompareTag("Player" ))
        {
            PlayerController.Instance.ChangeRotation();
           // Debug.Log("collision да");
            
        }
    }

    // private void OnCollisionStay(Collision collisionInfo)
    // {
    //     // Проверяем, если объект с тэгом "Player" входит в триггер
    //     if (collisionInfo.gameObject.CompareTag("Player" ))
    //     {
    //         Rotation.Instance.isRotatingClockwise = !Rotation.Instance.isRotatingClockwise;
    //         Debug.Log("collision да");
    //         
    //     }
    // }
}
