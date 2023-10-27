using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextScene2());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator NextScene2()
    {
        yield return new WaitForSeconds(32f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

  /*  public void Retry()
    {
        SceneManager.LoadScene("Outside");
    }

    public void Restart()
    {
        SceneManager.LoadScene("StartingRoom");
    } */

} 
