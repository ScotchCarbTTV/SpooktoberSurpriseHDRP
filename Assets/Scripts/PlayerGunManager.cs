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


    int lMask = 1 << 32;

    // Start is called before the first frame update
    void Start()
    {      
        weaponBelt.Add(new Automag_Weapon(25, 10, 10, "ammo_automag", "Automag"));
        //weaponBelt[0].name = "Automag";
        weaponBelt.Add(new Derringer_Weapon(10, 4, 20, "ammo_derringer", "Derringer"));
        //weaponBelt[1].name = "Derringer";
        //InvokeRepeating("FindAimSpot", 0.3f, 0.3f);
    }

    // Update is called once per frames
    void Update()
    {
        FindAimSpot();
        Shoot();
        SwitchWeapon();
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
                
                if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 30f, lMask, QueryTriggerInteraction.Ignore))
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

    private void SwitchWeapon()
    {
        if (Input.GetButtonDown("weapon1"))
        {
            
            equippedWeapon = 0;
            Debug.Log($"Equipped weapon is now {weaponBelt[equippedWeapon].gunName}");
        }
        else if (Input.GetButtonDown("weapon2"))
        {
            
            equippedWeapon = 1;
            Debug.Log($"Equipped weapon is now {weaponBelt[equippedWeapon].gunName}");
        }
    }

    private void OnDestroy()
    {
        foreach(Weapon wepon in weaponBelt)
        {
            wepon.Unsubscribe();
        }
    }

}

public abstract class Weapon
{
    public int ammoMax;

    public int ammoCurrent;

    public int magazineMax;

    public int magazineCurrent;

    public int weaponDamage;

    public string ammoID;

    public string gunName;

    public virtual int Shoot()
    {
        //play bang bang sound

        //substract ammo from magazineCurrent
        magazineCurrent--;

        return weaponDamage;
    }
    public virtual void Reload() { }

    public virtual void Unsubscribe() { }
}
