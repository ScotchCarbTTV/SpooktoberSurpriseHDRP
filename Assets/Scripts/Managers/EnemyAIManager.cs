using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIManager : MonoBehaviour
{
    [SerializeField] List<WaitingRoom> rooms = new List<WaitingRoom>();

    [SerializeField] List<EnemyAI> enemies = new List<EnemyAI>();

    //[SerializeField] List<bool> checkOccupied = new List<bool>();
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    private Vector3 storageSpot;

    [SerializeField] float AISpawnerTickRate;
    [SerializeField] float timer;

    private void OnEnable()
    {
        storageSpot = new Vector3(-500, -500, -500);
    }

    private void Awake()
    {
        //subscribe to the 'register AI' event
        EventManager.registerAI += RegisterAI;

        //subscribe to the 'store an AI' event
        EventManager.storeAIEvent += StoreAI;

        //subscribe to 'retrieve an AI' event
        //EventManager.retreiveAIEvent += RespawnAI;
    }

    // Start is called before the first frame update
    void Start()
    {

        timer = AISpawnerTickRate;
        
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = AISpawnerTickRate;
            RespawnAI();
        }
    }

    private void RegisterAI(EnemyAI enemy)
    {
        enemies.Add(enemy);
        WaitingRoom room = new WaitingRoom(enemy, false, storageSpot);
        rooms.Add(room);

        storageSpot -= new Vector3(10, 10, 10);
    }

    //store an AI method
    private void StoreAI(EnemyAI enemy)
    {
        for(int i = 0; i < rooms.Count; i++)
        {
            if(enemy == rooms[i].enemy)
            {
                rooms[i].occupied = true;
                //enemy.transform.position = rooms[i].roomPos;
                enemy.gameObject.SetActive(false);
            }
        }
    }

    //send an AI back method
    private void RespawnAI()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].occupied)
            {
                rooms[i].occupied = false;
                int spawn = Random.Range(0, spawnPoints.Count);
                rooms[i].enemy.gameObject.SetActive(true);
                rooms[i].enemy.transform.position = spawnPoints[spawn].position;
                rooms[i].enemy.Respawn();
                break;
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.registerAI -= RegisterAI;

        //unsub from 'store an AI' event
        EventManager.storeAIEvent -= StoreAI;

        //unsub from 'retrieve an AI' event
        //EventManager.registerAI -= RegisterAI;
    }

    public class WaitingRoom
    {
        public EnemyAI enemy;
        public bool occupied;
        public Vector3 roomPos;

        public WaitingRoom(EnemyAI _enemy, bool _occupied, Vector3 _position)
        {
            enemy = _enemy;
            occupied = _occupied;
            roomPos = _position;
        }
    }

}
