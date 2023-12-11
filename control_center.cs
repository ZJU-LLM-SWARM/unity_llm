using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_center : MonoBehaviour
{
    // public int car1[4];
    // public int car2[4];
    // public int drone1[8];
    // public int drone2[8];

    public bool car1_front;
    public bool car1_back;
    public bool car1_left;
    public bool car1_right;
    public bool car1_brake;
    public float car1_localPosition_x;
    public float car1_localPosition_y;  
    public float car1_localPosition_z;
    public float car1_localRotation_x;
    public float car1_localRotation_y;
    public float car1_localRotation_z;
    public float car1_speed_x;
    public float car1_speed_y;
    public float car1_speed_z;

    public bool car2_front;
    public bool car2_back;
    public bool car2_left;
    public bool car2_right;
    public bool car2_brake;
    public float car2_localPosition_x;
    public float car2_localPosition_y;
    public float car2_localPosition_z;
    public float car2_localRotation_x;
    public float car2_localRotation_y;
    public float car2_localRotation_z;
    public float car2_speed_x;
    public float car2_speed_y;
    public float car2_speed_z;

    public bool drone1_front;
    public bool drone1_back;
    public bool drone1_left;
    public bool drone1_right;
    public bool drone1_up;
    public bool drone1_down;
    public bool drone1_left_turn;
    public bool drone1_right_turn;
    public float drone1_localPosition_x;
    public float drone1_localPosition_y;
    public float drone1_localPosition_z;
    public float drone1_localRotation_x;
    public float drone1_localRotation_y;
    public float drone1_localRotation_z;
    public float drone1_speed_x;
    public float drone1_speed_y;
    public float drone1_speed_z;

    public bool drone2_front;
    public bool drone2_back;
    public bool drone2_left;
    public bool drone2_right;
    public bool drone2_up;
    public bool drone2_down;
    public bool drone2_left_turn;
    public bool drone2_right_turn;
    public float drone2_localPosition_x;
    public float drone2_localPosition_y;    
    public float drone2_localPosition_z;
    public float drone2_localRotation_x;
    public float drone2_localRotation_y;
    public float drone2_localRotation_z;
    public float drone2_speed_x;
    public float drone2_speed_y;
    public float drone2_speed_z;



    public bool drone3_front;
    public bool drone3_back;
    public bool drone3_left;
    public bool drone3_right;
    public bool drone3_up;
    public bool drone3_down;
    public bool drone3_left_turn;
    public bool drone3_right_turn;
    public float drone3_localPosition_x;
    public float drone3_localPosition_y;
    public float drone3_localPosition_z;
    public float drone3_localRotation_x;
    public float drone3_localRotation_y;
    public float drone3_localRotation_z;
    public float drone3_speed_x;
    public float drone3_speed_y;
    public float drone3_speed_z;



    public float target_x;
    public float target_y;
    public float target_z;
    public bool drone1_found;
    public bool drone2_found;
    public bool drone3_found;
    public bool car1_found;
    public bool car2_found;
    public float distance_car1_target;
    public float distance_car2_target;
    public float distance_drone1_target;
    public float distance_drone2_target;    
    public float distance_drone3_target;
    public float drone1_speed_xcontrol;
    public float drone1_speed_zcontrol;
    public float drone2_speed_xcontrol;
    public float drone2_speed_zcontrol;
    public float drone3_speed_xcontrol;
    public float drone3_speed_zcontrol;



    // Start is called before the first frame update
    void Start()
    {

    }
    void update()
    {
        
    }

}
