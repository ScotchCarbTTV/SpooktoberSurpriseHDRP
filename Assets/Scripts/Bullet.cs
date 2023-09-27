using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;

    private Rigidbody rbody;

    private Vector3 travelDir;

    [SerializeField] MeshRenderer mRenderer;

    public void SetDir(Vector3 trav)
    {
        travelDir = trav;
    }

    private void Awake()
    {
        if(!TryGetComponent<Rigidbody>(out rbody))
        {
            Debug.LogError("Kill yourself");
        }
    }

    private void Start()
    {
        rbody.useGravity = false;
        rbody.velocity = travelDir * bulletSpeed;
        
    }

    private void RendererON()
    {
        mRenderer.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<EnemyAI>(out EnemyAI enemy))
        {
            enemy.Die();
        }
        else
        {
            TESTHITMARKER();
            Destroy(gameObject);
            
        }
    }

    private void TESTHITMARKER()
    {
        //call 'spawn hitmarker' event
        //EventManager.SPAWNDEBUGHITMARKEREVENT(transform.position);

    }

}
