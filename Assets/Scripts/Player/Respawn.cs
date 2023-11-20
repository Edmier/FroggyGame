using System;
using UnityEngine;

public class Respawn : MonoBehaviour {
    
    [SerializeField] private Vector3 spawnPoint = Vector3.zero;

    // Start is called before the first frame update
    private void Start() {
        if (spawnPoint == Vector3.zero) {
            spawnPoint = transform.position;
        }
    }

    public void SetRespawnPoint(Vector3 position) {
        spawnPoint = position;
    }

    public void RespawnPlayer() {
        transform.position = spawnPoint;
    }

    private void FixedUpdate() {
        if (transform.position.y < 0) {
            RespawnPlayer();
        }
    }
}
