using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsAI : MonoBehaviour
{
    public GameObject spawnArea;
    [SerializeField] private float moveSpeed;
    Vector3 randomPos;

    void Start()
    {
        SetRandomPosition();
    }

    void Update()
    {
        if(transform.position != randomPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, randomPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            SetRandomPosition();
        }
    }

    public Vector3 SetRandomPosition()
    {
        float x = spawnArea.transform.localScale.x;
        float z = spawnArea.transform.localScale.z;
        float randomPosX = Random.Range(-x / 2, x / 2);
        float randomPosZ = Random.Range(-z / 2, z / 2);
        randomPos = new Vector3(randomPosX, spawnArea.transform.position.y, randomPosZ);
        transform.LookAt(randomPos);

        return randomPos;
    }
}
