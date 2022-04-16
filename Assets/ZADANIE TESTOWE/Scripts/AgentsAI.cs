using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsAI : MonoBehaviour
{
    public SimulationManager simulationManager;
    public GameObject spawnArea; 
    [SerializeField] private float moveSpeed;
    public int livePoints = 3;
    Vector3 randomPos;
    public bool clicked;

    void Start()
    {
        SetRandomPosition();
    }

    void Update()
    {
        if(transform.position != randomPos)
        {
            //Move the agent to random position
            transform.position = Vector3.MoveTowards(transform.position, randomPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            SetRandomPosition();
        }


    }

    //Set new random destination inside given area
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

    //Check collision with other agent
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "agent")
        {
            livePoints--;

            if(livePoints <= 0)
            {
                simulationManager.DestroyAgent(this.gameObject);
            }
        }
    }

}
