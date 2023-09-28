using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //

    // Start is called before the first frame update

    private void OnEnable()
    {
        //LoadBaseScenes();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadBaseScenes()
    {
        SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Interactables", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("HUDandUI", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Environment", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("EnemyAI", LoadSceneMode.Additive);

    }
}
