using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class control_center : MonoBehaviour
{
    public bool[] car_front = new bool[2];
    public bool[] car_back = new bool[2];
    public bool[] car_left = new bool[2];
    public bool[] car_right = new bool[2];
    public bool[] car_brake = new bool[2];
    public float[] car_localPosition_x = new float[2];
    public float[] car_localPosition_y = new float[2];
    public float[] car_localPosition_z = new float[2];
    public float[] car_localRotation_x = new float[2];
    public float[] car_localRotation_y = new float[2];
    public float[] car_localRotation_z = new float[2];
    public float[] car_speed_x = new float[2];
    public float[] car_speed_y = new float[2];
    public float[] car_speed_z = new float[2];
    public bool[] drone_front = new bool[10];
    public bool[] drone_back = new bool[10];
    public bool[] drone_left = new bool[10];
    public bool[] drone_right = new bool[10];
    public bool[] drone_up = new bool[10];
    public bool[] drone_down = new bool[10];
    public bool[] drone_left_turn = new bool[10];
    public bool[] drone_right_turn = new bool[10];
    public float[] drone_localPosition_x = new float[10];
    public float[] drone_localPosition_y = new float[10];
    public float[] drone_localPosition_z = new float[10];
    public float[] drone_localRotation_x = new float[10];
    public float[] drone_localRotation_y = new float[10];
    public float[] drone_localRotation_z = new float[10];
    // unity 负责控制的速度
    public float[] drone_speed_x = new float[10];
    public float[] drone_speed_y = new float[10];
    public float[] drone_speed_z = new float[10];
    // python 控制的量
    public float[] drone_speed_xcontrol = new float[10];
    public float[] drone_speed_zcontrol = new float[10];
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
    public float drone1_speed_xcontrol;
    public float drone1_speed_zcontrol;

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
    public float drone2_speed_xcontrol;
    public float drone2_speed_zcontrol;



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
    public float drone3_speed_xcontrol;
    public float drone3_speed_zcontrol;

    public bool drone4_front;
    public bool drone4_back;
    public bool drone4_left;
    public bool drone4_right;
    public bool drone4_up;
    public bool drone4_down;
    public bool drone4_left_turn;
    public bool drone4_right_turn;
    public float drone4_localPosition_x;
    public float drone4_localPosition_y;
    public float drone4_localPosition_z;
    public float drone4_localRotation_x;
    public float drone4_localRotation_y;
    public float drone4_localRotation_z;
    public float drone4_speed_x;
    public float drone4_speed_y;
    public float drone4_speed_z;
    public float drone4_speed_xcontrol;
    public float drone4_speed_zcontrol;

    public bool drone5_front;
    public bool drone5_back;
    public bool drone5_left;
    public bool drone5_right;
    public bool drone5_up;
    public bool drone5_down;
    public bool drone5_left_turn;
    public bool drone5_right_turn;
    public float drone5_localPosition_x;
    public float drone5_localPosition_y;
    public float drone5_localPosition_z;
    public float drone5_localRotation_x;
    public float drone5_localRotation_y;
    public float drone5_localRotation_z;
    public float drone5_speed_x;
    public float drone5_speed_y;
    public float drone5_speed_z;
    public float drone5_speed_xcontrol;
    public float drone5_speed_zcontrol;

    public bool drone6_front;
    public bool drone6_back;
    public bool drone6_left;
    public bool drone6_right;
    public bool drone6_up;
    public bool drone6_down;
    public bool drone6_left_turn;
    public bool drone6_right_turn;
    public float drone6_localPosition_x;
    public float drone6_localPosition_y;
    public float drone6_localPosition_z;
    public float drone6_localRotation_x;
    public float drone6_localRotation_y;
    public float drone6_localRotation_z;
    public float drone6_speed_x;
    public float drone6_speed_y;
    public float drone6_speed_z;
    public float drone6_speed_xcontrol;
    public float drone6_speed_zcontrol;

    public bool drone7_front;
    public bool drone7_back;
    public bool drone7_left;
    public bool drone7_right;
    public bool drone7_up;
    public bool drone7_down;
    public bool drone7_left_turn;
    public bool drone7_right_turn;
    public float drone7_localPosition_x;
    public float drone7_localPosition_y;
    public float drone7_localPosition_z;
    public float drone7_localRotation_x;
    public float drone7_localRotation_y;
    public float drone7_localRotation_z;
    public float drone7_speed_x;
    public float drone7_speed_y;
    public float drone7_speed_z;
    public float drone7_speed_xcontrol;
    public float drone7_speed_zcontrol;
    
    public bool drone8_front;
    public bool drone8_back;
    public bool drone8_left;
    public bool drone8_right;
    public bool drone8_up;
    public bool drone8_down;
    public bool drone8_left_turn;
    public bool drone8_right_turn;
    public float drone8_localPosition_x;
    public float drone8_localPosition_y;
    public float drone8_localPosition_z;
    public float drone8_localRotation_x;
    public float drone8_localRotation_y;
    public float drone8_localRotation_z;
    public float drone8_speed_x;
    public float drone8_speed_y;
    public float drone8_speed_z;
    public float drone8_speed_xcontrol;
    public float drone8_speed_zcontrol;

    public bool drone9_front;
    public bool drone9_back;
    public bool drone9_left;
    public bool drone9_right;
    public bool drone9_up;
    public bool drone9_down;
    public bool drone9_left_turn;
    public bool drone9_right_turn;
    public float drone9_localPosition_x;
    public float drone9_localPosition_y;
    public float drone9_localPosition_z;
    public float drone9_localRotation_x;
    public float drone9_localRotation_y;
    public float drone9_localRotation_z;
    public float drone9_speed_x;
    public float drone9_speed_y;
    public float drone9_speed_z;
    public float drone9_speed_xcontrol;
    public float drone9_speed_zcontrol;

    public bool drone10_front;
    public bool drone10_back;
    public bool drone10_left;
    public bool drone10_right;
    public bool drone10_up;
    public bool drone10_down;
    public bool drone10_left_turn;
    public bool drone10_right_turn;
    public float drone10_localPosition_x;
    public float drone10_localPosition_y;
    public float drone10_localPosition_z;
    public float drone10_localRotation_x;
    public float drone10_localRotation_y;
    public float drone10_localRotation_z;
    public float drone10_speed_x;
    public float drone10_speed_y;
    public float drone10_speed_z;
    public float drone10_speed_xcontrol;
    public float drone10_speed_zcontrol;

    public float target_x;
    public float target_y;
    public float target_z;
    public bool drone1_found;
    public bool drone2_found;
    public bool drone3_found;
    public bool drone4_found;
    public bool drone5_found;
    public bool drone6_found;
    public bool drone7_found;
    public bool drone8_found;
    public bool drone9_found;
    public bool drone10_found;

    public bool car1_found;
    public bool car2_found;
    public float distance_car1_target;
    public float distance_car2_target;
    public float distance_drone1_target;
    public float distance_drone2_target;    
    public float distance_drone3_target;
    public float distance_drone4_target;
    public float distance_drone5_target;
    public float distance_drone6_target;
    public float distance_drone7_target;
    public float distance_drone8_target;
    public float distance_drone9_target;
    public float distance_drone10_target;


    public float activated_agent_number;



    // Start is called before the first frame update
    void Start()
    {
        
    }
    void update()
    {
        
    }

}
