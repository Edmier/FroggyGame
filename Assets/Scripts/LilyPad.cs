using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPad : MonoBehaviour
{

    private float startY;
    private bool playerPresent = false;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    private void Update()
    {
        if (playerPresent)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f * Time.deltaTime, transform.position.z);
        }
        else if (startY != transform.position.y)
        {
            if (Mathf.Abs(startY - transform.position.y) < 0.1f)
            {
                transform.position = new Vector3(transform.position.x, startY, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f * Time.deltaTime, transform.position.z);
            }
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerPresent = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerPresent = false;
    }
}

