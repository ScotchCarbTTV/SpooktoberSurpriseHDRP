using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Derringer_Weapon : Weapon
{
    public Derringer_Weapon(int _ammoMax, int _magazineMax, int _weaponDamage, string _ammoID, string _name)
    {
        ammoMax = _ammoMax;
        magazineMax = _magazineMax;
        weaponDamage = _weaponDamage;
        ammoID = _ammoID;

        magazineCurrent = magazineMax;
        ammoCurrent = ammoMax;

        gunName = _name;

        EventManager.addAmmoEvent += AddAmmo;
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

    private void AddAmmo(string _ammoID)
    {
        if (_ammoID == ammoID)
        {
            Debug.Log($"Received more boolet for {ammoID}");
            ammoCurrent += ammoMax / 4;
            if (ammoCurrent > ammoMax)
            {
                ammoCurrent = ammoMax;
            }
        }
    }
    public override void Unsubscribe()
    {
        EventManager.addAmmoEvent -= AddAmmo;
    }
}
