using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    [SerializeField] public float height = 2f; 
    
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;

        var respawn = other.GetComponent<Respawn>();
        respawn.SetRespawnPoint(transform.position + new Vector3(0, height, 0));
    }
}