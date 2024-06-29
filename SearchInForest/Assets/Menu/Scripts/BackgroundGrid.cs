using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGrid : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private float repeatWidth;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < repeatWidth)
        {
            transform.position = startPos;
        }
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
