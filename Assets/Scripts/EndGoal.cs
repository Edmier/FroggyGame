using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    public GameObject WinScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
             ThirdPersonMovement p = other.GetComponent<ThirdPersonMovement>();

             if (p != null)
              {
               WinScreen.SetActive(true);
              }
        }
    }
}