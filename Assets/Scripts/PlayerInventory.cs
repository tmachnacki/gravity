using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    
    public float max_aether;
    public float current_aether;
    ProgressBar aether_bar;

    public float max_fuel;
    public float current_fuel;
    
    ProgressBar fuel_bar;

    // Start is called before the first frame update
    void Start()
    {
        /*
        aether_bar = GameObject.Find("AetherBar").GetComponent<ProgressBar>();
        aether_bar.SetMaxValue(max_aether);
        aether_bar.SetValue(max_aether);
        current_aether = max_aether;
        */
        

        fuel_bar = GameObject.Find("FuelBar").GetComponent<ProgressBar>();
        fuel_bar.SetMaxValue(max_fuel);
        fuel_bar.SetValue(max_fuel);
        current_fuel = max_fuel;
    }


    public void UseAether(float aether_value)
    {
        current_aether -= aether_value * Time.deltaTime;
        if (current_aether < 0)
        {
            current_aether = 0;
        }

        /*
        if (current_aether < 5)
        {
            PlayerState.can_use_black_hole = false;
        }
        */
        aether_bar.SetValue(current_aether);
    }

    public void AddAether(float aether_value)
    {
        current_aether += aether_value;
        if (current_aether > max_aether)
        {
            current_aether = max_aether;
        }

        /*
        if (current_aether > 0)
        {
            PlayerState.can_shoot = true;
        }
        if (current_aether >= 5)
        {
            PlayerState.can_use_black_hole = true;
        }
        */
        aether_bar.SetValue(current_aether);
    }

    public void UseFuel(float fuel_value)
    {
    
        
        current_fuel -= fuel_value * Time.deltaTime;
        
        if (current_fuel <= 0)
        {
            current_fuel = 0;
            PlayerState.can_fly = false;
        }
        fuel_bar.SetValue(current_fuel);
        
    }

    public void AddFuel(float fuel_value)
    {
        current_fuel += fuel_value * Time.deltaTime;
        if (current_fuel >= max_fuel)
        {
            current_fuel = max_fuel;
        }
        if (current_fuel > 0)
        {
            PlayerState.can_fly = true;
        }
        fuel_bar.SetValue(current_fuel);
    }

    public void AddFuelPickUp(float fuel_value)
    {
        current_fuel += fuel_value;
        if (current_fuel >= max_fuel)
        {
            current_fuel = max_fuel;
        }
        if (current_fuel > 0)
        {
            PlayerState.can_fly = true;
        }
        fuel_bar.SetValue(current_fuel);
    }
}
