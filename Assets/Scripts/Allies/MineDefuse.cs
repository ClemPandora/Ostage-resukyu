using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineDefuse : MonoBehaviour
{
    public GameObject difuseUI;
    public Slider difuseBar;
    private float _difuseValue = 0;
    
    private List<GameObject> _mines = new List<GameObject>();

    private void FixedUpdate()
    {
        //If there is a landmine nearby, hold e increase difuse value
        if (Input.GetButton("Use") && _mines.Count > 0)
        {
            _difuseValue += 0.05f;
            if (_difuseValue >= 1)
            {
                //If the difuse value reach 1, destroy all nearby mines
                foreach (var mine in _mines)
                {
                    Destroy(mine);
                }
                _mines.Clear();
                _difuseValue = 0;
                difuseUI.SetActive(false);
            }
        }
        else
        {
            //otherwise, the difuse value decrease.
            if (_difuseValue > 0)
            {
                _difuseValue -= 0.02f;
            }
        }

        difuseBar.value = _difuseValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Add each mine in range to a list
        if (other.GetComponent<LandMine>() != null)
        {
            difuseUI.SetActive(true);
            _mines.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LandMine>() != null)
        {
            //Remove mine from least if they are too far
            _mines.Remove(other.gameObject);
            if (_mines.Count == 0)
            {
                difuseUI.SetActive(false);
                _difuseValue = 0;
            }
        }
    }

    public void Explode(GameObject mine)
    {
        _mines.Remove(mine);
        if (_mines.Count == 0)
        {
            difuseUI.SetActive(false);
            _difuseValue = 0;
        }
    }
}
