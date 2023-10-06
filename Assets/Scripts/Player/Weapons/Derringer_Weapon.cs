using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Derringer_Weapon : Weapon
{
    public Derringer_Weapon(int _ammoMax, int _magazineMax, int _weaponDamage)
    {
        ammoMax = _ammoMax;
        magazineMax = _magazineMax;
        weaponDamage = _weaponDamage;

        magazineCurrent = magazineMax;
        ammoCurrent = ammoMax;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Reload()
    {
        //i need more boolets

        //reload this weapon
        //check if there's any ammo available
        if (ammoCurrent > 0)
        {
            //check if the ammo available is greater than what we need
            //work out how much you need and top the magazine up accordingly
            int magDiff = magazineMax - magazineCurrent;
            if (ammoCurrent >= magDiff)
            {
                //play the reload animation


                //add magDiff to magazineCurrent
                magazineCurrent += magDiff;
                //subtract magDiff from ammoCurrent
                ammoCurrent -= magDiff;
            }
            else
            {
                //we have bullets but not enough to fill the magazine
                //just chuck the remainder into the magazine in that case
                magazineCurrent += ammoCurrent;
                ammoCurrent -= ammoCurrent;
            }
        }
    }
}
