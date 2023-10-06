using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunManager : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] GameObject barrelEnd;

    [SerializeField] Transform cameraDir;

    [SerializeField] GameObject prefabBlood;

    Vector3 aimSpot = new Vector3();

    [SerializeField] List<Weapon> weaponBelt = new List<Weapon>();

    private int equippedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        weaponBelt.Add(new Automag_Weapon(25, 10, 10, "ammo_automag"));
        weaponBelt.Add(new Derringer_Weapon(10, 4, 20, "ammo_derringer"));
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
            //does the gun have any ammo?
            if(weaponBelt[equippedWeapon].magazineCurrent > 0)
            {
                //Debug.Log("You have enough bullets");
                //shoot a bullet
                RaycastHit hit;
                
                if(Physics.SphereCast(Camera.main.transform.position, 0.3f, Camera.main.transform.forward, out hit, 30f))
                {
                    //Debug.Log($"Hit {hit.collider.name}");
                    if (hit.collider.TryGetComponent<EnemyAI>(out EnemyAI enemy))
                    {
                        //instantiate a particle effect of blood where the hit was
                      

                        Instantiate(prefabBlood, hit.point, Quaternion.Euler(-transform.forward));

                        //do damage to the enemy
                        enemy.TakeDamage(weaponBelt[equippedWeapon].weaponDamage);
                        //Debug.Log($"Hit {hit.collider.name} for {weaponBelt[equippedWeapon].weaponDamage}");
                    }
                }

                /*
                GameObject newBullet = Instantiate(bulletPrefab, barrelEnd.transform.position, barrelEnd.transform.rotation);
                if (newBullet.TryGetComponent<Bullet>(out Bullet bullet))
                {
                    bullet.SetDir(barrelEnd.transform.forward);
                    bullet.Damage = weaponBelt[equippedWeapon].Shoot();
                }*/
            }
            else
            {
                //i need more boolet
                //Debug.Log("i need more boolet");

            }                      
        }
    }

    private void FindAimSpot()
    {
        RaycastHit hit;

        if(Physics.Raycast(cameraDir.position, cameraDir.forward, out hit, 20f))
        {
            aimSpot = hit.point;
            transform.LookAt(aimSpot);
        }
        
    }
}

public abstract class Weapon : MonoBehaviour
{
    public int ammoMax;

    public int ammoCurrent;

    public int magazineMax;

    public int magazineCurrent;

    public int weaponDamage;

    public string ammoID;

    public virtual int Shoot()
    {
        //play bang bang sound

        //substract ammo from magazineCurrent
        magazineCurrent--;

        return weaponDamage;
    }
    public virtual void Reload() { }
}
