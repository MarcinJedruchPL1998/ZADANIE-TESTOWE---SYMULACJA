using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] GameObject transition;
    void Start()
    {
        StartCoroutine(LoadSimulationScene());
    }

    IEnumerator LoadSimulationScene()
    {
        yield return new WaitForSeconds(1);
        transition.SetActive(true);
        transition.GetComponent<Animator>().Play("transition_in");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
}
