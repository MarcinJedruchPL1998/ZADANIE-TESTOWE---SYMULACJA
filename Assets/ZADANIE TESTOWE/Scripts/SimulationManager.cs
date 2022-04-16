using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnArea;
    [SerializeField] private GameObject agentPrefab;

    [SerializeField] private int minWaitingSeconds = 2;
    [SerializeField] private int maxWaitingSeconds = 10;
    [SerializeField] private int maxAgents = 30;

    [SerializeField] private List<GameObject> agents;

    [SerializeField] private Material unclickedMaterial, clickedMaterial;

    [SerializeField] private GameObject clickedObjectPanel;
    [SerializeField] private Text name;
    [SerializeField] private Text lives;

    void Start()
    {
        StartCoroutine(SpawnNewAgent());
    }

    IEnumerator SpawnNewAgent()
    {
        if(agents.Count < maxAgents) //Spawn new agent only if agents count is less than max agents value
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
            spawnedAgent.GetComponent<AgentsAI>().simulationManager = this;
            spawnedAgent.name = "Agent_" + agents.Count;
            agents.Add(spawnedAgent);

            StartCoroutine(SpawnNewAgent());
        }
    }

   

    private void Update()
    {
        //Select agent and show it's name and live points on UI
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayhit;

            if(Physics.Raycast(ray, out rayhit) && rayhit.collider.tag == "agent")
            {
                GameObject click = rayhit.collider.gameObject;
                name.text = click.name;
                lives.text = click.GetComponent<AgentsAI>().livePoints.ToString();

                for(int i = 0; i < agents.Count; i++)
                {
                    if (agents[i] == click) //If you click any agent
                    {
                        if(!agents[i].GetComponent<AgentsAI>().clicked) //If clicked agent is unclicked
                        {
                            agents[i].GetComponent<AgentsAI>().clicked = true;
                            agents[i].GetComponent<MeshRenderer>().material = clickedMaterial;
                            agents[i].transform.GetChild(0).GetComponent<MeshRenderer>().material = clickedMaterial;
                            clickedObjectPanel.SetActive(true);
                        }
                        
                        else //If clicked agent is clicked previously
                        {
                            agents[i].GetComponent<AgentsAI>().clicked = false;
                            agents[i].GetComponent<MeshRenderer>().material = unclickedMaterial;
                            agents[i].transform.GetChild(0).GetComponent<MeshRenderer>().material = unclickedMaterial;
                            clickedObjectPanel.SetActive(false);
                        }
                    }

                    else //If you click other agent, your previously clicked agent becomes unclicked
                    {
                        agents[i].GetComponent<AgentsAI>().clicked = false;
                        agents[i].GetComponent<MeshRenderer>().material = unclickedMaterial;
                        agents[i].transform.GetChild(0).GetComponent<MeshRenderer>().material = unclickedMaterial;
                        
                    }
                }

                
            }
        }
    }


    public void DestroyAgent(GameObject agent)
    {
        agents.Remove(agent);
        Destroy(agent);
    }

}
