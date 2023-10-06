using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FiniteStateMachine;

public class EnemyAI : MonoBehaviour
{
    //reference to the navmesh agent
    private NavMeshAgent agent;

    [SerializeField] float moveSpeeed;

    //[SerializeField] float lookCheckDistance;
    [SerializeField] float lookRadius;
    [SerializeField] float viewCone;
    
    [SerializeField] float chaseDistance;

    [SerializeField] float scanRate;

    [SerializeField] private Vector3 lookScan;

    [SerializeField] GameObject scanner;

    [SerializeField] GameObject ragdoll;

    private Vector3 target;

    private WayPointNode currentNode;

    private bool chasing = false;

    public StateMachine StateMachine { get; private set; }


    [SerializeField] int enemyHealth = 40;
    [SerializeField] int enemyHealthMax = 40;


    //DEBUGBOOL

    [SerializeField] bool dead;


    private void Awake()
    {
        //create the statemachine
        StateMachine = new StateMachine();

        if(!TryGetComponent<NavMeshAgent>(out agent))
        {
            Debug.LogError("Where your agent component at dawg?");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //set the initial state
        StateMachine.SetState(new EnemyIdle(this));

        chasing = false;

        Invoke("RegisterSelf", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        ViewCone();

        StateMachine.OnUpdate();

        //DEBUG FOR BEING DEAD
        if (dead)
        { 
            if(StateMachine.GetCurrentStateAsType<EnemyDead>() != StateMachine.CurrentState)
            {
                StateMachine.SetState(new EnemyDead(this));
            }            
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if(enemyHealth <= 0)
        {
            Die();
        }

        //maybe do a little flinching uwu
    }

    public void Die()
    {
        if (StateMachine.GetCurrentStateAsType<EnemyDead>() != StateMachine.CurrentState)
        {
            StateMachine.SetState(new EnemyDead(this));
        }
    }

    private void RegisterSelf()
    {
        EventManager.registerAI(this);
    }

    private bool ViewCone()
    {
        //use an overlapsphere to find if the player is within the radius for being spotted
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, lookRadius);

        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.TryGetComponent<PlayerMove>(out PlayerMove pMove))
            {
                //check if the angle from transform.forward to player position is less than the view angle
                Vector3 targetDir = pMove.transform.position - transform.position;

                float viewAngle = Vector3.Angle(targetDir, transform.forward);
                float viewConeMax = viewCone / 2;
                float viewConeMin = viewConeMax * -1;

                if(viewAngle < viewConeMax && viewAngle > viewConeMin)
                {
                    RaycastHit hit;
                    if(Physics.Raycast(transform.position,targetDir, out hit, lookRadius))
                    {
                        if(hit.collider.TryGetComponent<PlayerMove>(out PlayerMove p))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }                        
                    }                    
                }
            }
        }
        return false;
    }

    public void GetWaypoint()
    {
        currentNode = WayPointManager.Instance.FetchNearestWayPoint(transform.position);
        
    }

    public void Respawn()
    {
        enemyHealth = enemyHealthMax;
        StateMachine.SetState(new EnemyIdle(this));
        
    }

    public abstract class EnemyState : IState
    {
        protected EnemyAI Instance;

        public EnemyState(EnemyAI _instance)
        {
            Instance = _instance;
        }

        public virtual void OnEnter()
        {
           
        }

        public virtual  void OnExit()
        {
           
        }

        public virtual void OnUpdate()
        {
           
        }
    }

    /*AI will:
     * Not move while in idle, with a randomized countdown before going to patrol OR spotting player
     * Move while in patrol at 1/2 maximum speed to a randomly selected waypoint that's connected to the current waypoint until have visited random amount of waypoints
     * Chase player at full speed when in chase
     * Replace self with ragdol when hit by player and teleport to despawn room until respawn
     * Return to idle if the player escapes the view radius
     */

    public class EnemyIdle : EnemyState
    {
        public EnemyIdle(EnemyAI _instance) : base(_instance)
        {

        }

        float timer;

        public override void OnEnter()
        {
            //Debug.Log("Entering Idle");

            Instance.agent.isStopped = true;            

            timer = Random.Range(3, 10);
            timer = Mathf.RoundToInt(timer);
        }

        public override void OnUpdate()
        {
            //tick the timer down
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                Instance.StateMachine.SetState(new EnemyPatrol(Instance));
            }

            //if we spot the player go to the chase state
            if (Instance.ViewCone())
            {
                Instance.StateMachine.SetState(new EnemyChase(Instance));
            }
        }
    }

    public class EnemyChase : EnemyState
    {
        public EnemyChase(EnemyAI _instance) : base(_instance)
        {

        }

         float attackDist = 5;

        
        public override void OnEnter()
        {
            //Debug.Log("Entering Chase");

            //set agent to moving
            Instance.agent.isStopped = false;

            //set movement speed to full
            Instance.agent.speed = Instance.moveSpeeed;
        }

        public override void OnUpdate()
        {
            AttackAction();            

            //check if the player is out of chase range
            if(Vector3.Distance(Instance.transform.position, PlayerMove.Instance.transform.position) > Instance.chaseDistance)
            {
                //go to the idle state
                Instance.StateMachine.SetState(new EnemyIdle(Instance));
            }
        }

        public void AttackAction()
        {
            if (Vector3.Distance(Instance.transform.position, PlayerMove.Instance.transform.position) < attackDist)
            {
                //stop movement
                Instance.agent.isStopped = true;
                //make an attack
                //Debug.Log("Attack!");
                
            }
            else
            {
                //start movement
                Instance.agent.isStopped = false;
                //update desination to the player pos
                Instance.agent.SetDestination(PlayerMove.Instance.transform.position);
            }
        }

    }

    public class EnemyPatrol : EnemyState
    {
        public EnemyPatrol(EnemyAI _instance) : base(_instance)
        {

        }

        int patrolCounter;

        public override void OnEnter()
        {
            //Debug.Log("Entering Patrol");
            //set agent to moving
            Instance.agent.isStopped = false;

            //set movement speed to full
            Instance.agent.speed = Instance.moveSpeeed / 2;

            //randomize the number of points to patrol
            patrolCounter = Random.Range(1, 5);

            //grab nearest waypoint as current waypoint
            Instance.GetWaypoint();

            Instance.agent.SetDestination(Instance.currentNode.transform.position);

        }

        public override void OnUpdate()
        {
            //check if agent is at the current waypoint
            if(Vector3.Distance(Instance.transform.position, Instance.currentNode.transform.position) < Instance.agent.stoppingDistance)
            {
                //if yes
                //check if the number of patrol points remaining is over zero
                if (patrolCounter > 0)
                {
                    Instance.currentNode = Instance.currentNode.GetRandomNeighbour();
                    Instance.agent.SetDestination(Instance.currentNode.transform.position);
                    patrolCounter--;
                }
                else
                {
                    Instance.StateMachine.SetState(new EnemyIdle(Instance));
                }
            }

            //if no, move towards the current waypoint
            
            
            //if yes then pick a new waypoint to go to and reduce number of waypoints remaining by 1
            //if no then go to the idle state


            //if the player is spotted then go to chase
            //if we spot the player go to the chase state
            if (Instance.ViewCone())
            {
                Instance.StateMachine.SetState(new EnemyChase(Instance));
            }

        }
    }

    public class EnemyDead : EnemyState
    {
        public EnemyDead(EnemyAI _instance) : base(_instance)
        {

        }

        float timer = 5f;

        public override void OnEnter()
        {
            Debug.Log("Entering Dead");
            //instantiate the relevant ragdoll prefab at this position
            GameObject corpse = Instantiate(Instance.ragdoll, Instance.transform.position, Instance.transform.rotation);
            if(corpse.TryGetComponent<Rigidbody>(out Rigidbody rig))
            {
                Debug.Log("Found the Rigidbody");
                Vector3 dir = PlayerMove.Instance.transform.position - corpse.transform.position;
                rig.AddForce(-dir * 100);
                
            }
            //turn off renderer for this object

            //set agent to false
            //Instance.agent.enabled = false;

            //teleport to waiting room using 'send to cupboard' event
            EventManager.storeAIEvent(Instance);

            //set && start timer
        }

        public override void OnUpdate()
        {
            //timer tickdown
            timer -= Time.deltaTime;
            //timer complete? Go back to idle state
            if (timer <= 0)
            {
                Instance.StateMachine.SetState(new EnemyIdle(Instance));
            }
        }

        public override void OnExit()
        {
            Debug.Log("Respawning...");
            //teleport to a spawnpoint
            //EventManager.retreiveAIEvent(Instance);
            Instance.dead = false;

            //turn renderer back on

           //Instance.agent.enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
