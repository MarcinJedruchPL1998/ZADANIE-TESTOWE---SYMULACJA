using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnArea;
    [SerializeField] private GameObject agentPrefab;

    [SerializeField] private int minWaitingSeconds;
    [SerializeField] private int maxWaitingSeconds;
    [SerializeField] private int maxAgents;

    [SerializeField] private List<GameObject> agents;

    void Start()
    {
        StartCoroutine(SpawnNewAgent());
    }

    IEnumerator SpawnNewAgent()
    {
        if(agents.Count < maxAgents)
        {
            float seconds = Random.Range(minWaitingSeconds, maxWaitingSeconds + 1);

            yield return new WaitForSeconds(seconds);

            float x = spawnArea.transform.localScale.x;
            float z = spawnArea.transform.localScale.z;
            float randX = Random.Range(-x / 2, x / 2);
            float randZ = Random.Range(-z / 2, z / 2);
            Vector3 spawnPos = new Vector3(randX, spawnArea.transform.position.y, randZ);
            GameObject spawnedAgent = Instantiate(agentPrefab, spawnPos, Quaternion.identity, transform);
            spawnedAgent.GetComponent<AgentsAI>().spawnArea = spawnArea;
            agents.Add(spawnedAgent);

            StartCoroutine(SpawnNewAgent());
        }
    }

}
