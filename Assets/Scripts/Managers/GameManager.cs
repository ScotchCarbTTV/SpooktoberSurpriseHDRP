using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager Instance { get; private set; }

    private void OnEnable()
    {
        //LoadBaseScenes();

        if (Instance == null)
        {
            Instance = this;
        }
        else Debug.LogError("Kill yourslef, you have too many game manager scriupts in the scene idiot");
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
