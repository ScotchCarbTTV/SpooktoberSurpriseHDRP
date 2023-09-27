using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDebugger : MonoBehaviour
{
    [SerializeField] GameObject HITMARKDEBUG;

    // Start is called before the first frame update
    void Start()
    {
        //subscribe to SPAWNHITMARKDEBUG
        //EventManager.SPAWNDEBUGHITMARKEREVENT += SPAWNDEBUGHITMARK;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SPAWNDEBUGHITMARK(Vector3 position)
    {
        Debug.Log("GOTTA DISABLE THIS BRO");
        Instantiate(HITMARKDEBUG, position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        //EventManager.SPAWNDEBUGHITMARKEREVENT -= SPAWNDEBUGHITMARK;
    }
}
