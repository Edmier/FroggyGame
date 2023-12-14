using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var respawn = other.GetComponent<Respawn>();
            if (respawn != null) respawn.RespawnPlayer();
        }
    }
}
