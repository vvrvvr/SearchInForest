using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAndFloat : MonoBehaviour
{
    private float index;
    [SerializeField] private float amplitudeY;
    [SerializeField] private float omegaY;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool isOnlyRotate;

    void Start()
    {
        index = 0;
        //amplitudeY = 0.5f;
        //omegaY = 1.0f;
        //rotationSpeed = 100f;
    }

    void Update()
    {
        if (!isOnlyRotate)
        {
            //эффект покачивания объекта в воздухе 
            index += Time.deltaTime;
            float y = Mathf.Abs(amplitudeY * Mathf.Sin(omegaY * index));
            transform.localPosition = new Vector3(0, y, 0);
        }

        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }

}
