using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_control_center : MonoBehaviour
{
    public List<bool> activate_drones = new List<bool>();
    public GameObject[] drones = new GameObject[10];
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            if (activate_drones[i])
            {
                drones[i].SetActive(true);
            }
            else
            {
                drones[i].SetActive(false);
            }
        }
    }

}
