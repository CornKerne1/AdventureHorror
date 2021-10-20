using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PapaOverwatch : MonoBehaviour
{

    private GameObject player;
    public NavMeshAgent agent;

    public Vector3 spawnPoint;
    bool spawnPointSet;
    public float spawnPointRange;
    public LayerMask whatIsGround, whatIsPlayer;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.fear > player.fearThreshold)
        {
            TrySpawn();
        }
    }

    private void TrySpawn()
    {
        if (!spawnPointSet) SearchSpawnPoint();

        if (spawnPointSet)
        {
            //Instantiate Papa spawnPoint;
        }

    }

    private void SearchSpawnPoint()
    {
        float randomZ = Random.Range(-spawnPointRange, spawnPointRange);
        float randomX = Random.Range(-spawnPointRange, spawnPointRange);
        spawnPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        Vector3 distFromPlayer = PlayerInstance.instance.player.transform.position - spawnPoint;

        if (Physics.Raycast(spawnPoint, -transform.up, 2f, whatIsGround) && (distFromPlayer.magnitude > 30))
                spawnPointSet = true;

    }
}
