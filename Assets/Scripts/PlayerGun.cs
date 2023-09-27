using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] GameObject barrelEnd;

    [SerializeField] Transform cameraDir;

    Vector3 aimSpot = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("FindAimSpot", 0.3f, 0.3f);
    }

    // Update is called once per frames
    void Update()
    {
        FindAimSpot();
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //shoot a bullet
            GameObject newBullet = Instantiate(bulletPrefab, barrelEnd.transform.position, barrelEnd.transform.rotation);
            if(newBullet.TryGetComponent<Bullet>(out Bullet bullet))
            {
                bullet.SetDir(barrelEnd.transform.forward);
            }
            
        }
    }

    private void FindAimSpot()
    {
        RaycastHit hit;

        if(Physics.Raycast(cameraDir.position, cameraDir.forward, out hit, 50f))
        {
            aimSpot = hit.point;
            transform.LookAt(aimSpot);
        }
        
    }
}
