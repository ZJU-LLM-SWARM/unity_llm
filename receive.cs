using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEditor;
 

// 收发消息
public class Connect : MonoBehaviour
{
    public float[] data_rec;
    public GameObject uavs;
    private void Update()
    {
        if (U2P.Instance.isConnected)
        {
            data_rec = U2P.Instance.RecData();
            if (data_rec != null)
            {
                Debug.Log(data_rec);
                print("接收到指令");
                action();
                // get_state();
                // 返回0，表示接收到数据
                List<float> list_send = new List<float>(get_state());
                U2P.Instance.SendData(list_send);
                // TODO: 处理数据
            }
        }
    }
// 获取状态
    float[] get_state()
    {
        float[] data_send = new float[]
        {
            uavs.GetComponent<control_center>().car1_localPosition_x,
            uavs.GetComponent<control_center>().car1_localPosition_y,
            uavs.GetComponent<control_center>().car1_localPosition_z,
            uavs.GetComponent<control_center>().car1_localRotation_x,
            uavs.GetComponent<control_center>().car1_localRotation_y,
            uavs.GetComponent<control_center>().car1_localRotation_z,
            uavs.GetComponent<control_center>().car1_speed_x,
            uavs.GetComponent<control_center>().car1_speed_y,
            uavs.GetComponent<control_center>().car1_speed_z,
            uavs.GetComponent<control_center>().car1_found?1:0,
            uavs.GetComponent<control_center>().distance_car1_target,

            uavs.GetComponent<control_center>().car2_localPosition_x,
            uavs.GetComponent<control_center>().car2_localPosition_y,
            uavs.GetComponent<control_center>().car2_localPosition_z,
            uavs.GetComponent<control_center>().car2_localRotation_x,
            uavs.GetComponent<control_center>().car2_localRotation_y,
            uavs.GetComponent<control_center>().car2_localRotation_z,
            uavs.GetComponent<control_center>().car2_speed_x,
            uavs.GetComponent<control_center>().car2_speed_y,
            uavs.GetComponent<control_center>().car2_speed_z,
            uavs.GetComponent<control_center>().car2_found?1:0,
            uavs.GetComponent<control_center>().distance_car2_target,

            uavs.GetComponent<control_center>().drone1_localPosition_x,
            uavs.GetComponent<control_center>().drone1_localPosition_y,
            uavs.GetComponent<control_center>().drone1_localPosition_z,
            uavs.GetComponent<control_center>().drone1_localRotation_x,
            uavs.GetComponent<control_center>().drone1_localRotation_y,
            uavs.GetComponent<control_center>().drone1_localRotation_z,
            uavs.GetComponent<control_center>().drone1_speed_x,
            uavs.GetComponent<control_center>().drone1_speed_y,
            uavs.GetComponent<control_center>().drone1_speed_z,
            uavs.GetComponent<control_center>().drone1_found?1:0,
            uavs.GetComponent<control_center>().distance_drone1_target,

            uavs.GetComponent<control_center>().drone2_localPosition_x,
            uavs.GetComponent<control_center>().drone2_localPosition_y,
            uavs.GetComponent<control_center>().drone2_localPosition_z,
            uavs.GetComponent<control_center>().drone2_localRotation_x,
            uavs.GetComponent<control_center>().drone2_localRotation_y,
            uavs.GetComponent<control_center>().drone2_localRotation_z,
            uavs.GetComponent<control_center>().drone2_speed_x,
            uavs.GetComponent<control_center>().drone2_speed_y,
            uavs.GetComponent<control_center>().drone2_speed_z,
            uavs.GetComponent<control_center>().drone2_found?1:0,
            uavs.GetComponent<control_center>().distance_drone2_target,
                        
            uavs.GetComponent<control_center>().drone3_localPosition_x,
            uavs.GetComponent<control_center>().drone3_localPosition_y,
            uavs.GetComponent<control_center>().drone3_localPosition_z,
            uavs.GetComponent<control_center>().drone3_localRotation_x,
            uavs.GetComponent<control_center>().drone3_localRotation_y,
            uavs.GetComponent<control_center>().drone3_localRotation_z,
            uavs.GetComponent<control_center>().drone3_speed_x,
            uavs.GetComponent<control_center>().drone3_speed_y,
            uavs.GetComponent<control_center>().drone3_speed_z,
            uavs.GetComponent<control_center>().drone3_found?1:0,
            uavs.GetComponent<control_center>().distance_drone3_target,
            
            uavs.GetComponent<control_center>().target_x,
            uavs.GetComponent<control_center>().target_y,
            uavs.GetComponent<control_center>().target_z,     

            Time.time
        };
        return data_send;
    }
    // 传入长度为26的整形数组的函数，行动
    void action()
    {   
        if (data_rec[12] != 9)
        {
            Debug.Log(data_rec);
            if (data_rec[0] == 1)
                uavs.GetComponent<control_center>().car1_front = true;
            else if (data_rec[0] == -1)
            {
                uavs.GetComponent<control_center>().car1_back = true;
                uavs.GetComponent<control_center>().car1_front = false;
            }
            else if (data_rec[0] == 0)
            {
                uavs.GetComponent<control_center>().car1_front = false;
                uavs.GetComponent<control_center>().car1_back = false;
            }
            if (data_rec[1] == 1)
            {
                uavs.GetComponent<control_center>().car1_left = true;
                uavs.GetComponent<control_center>().car1_right = false;
            }
            else if (data_rec[1] == -1)
            {
                uavs.GetComponent<control_center>().car1_left = false;
                uavs.GetComponent<control_center>().car1_right = true;
            }
            else if (data_rec[1] == 0)
            {
                uavs.GetComponent<control_center>().car1_left = false;
                uavs.GetComponent<control_center>().car1_right = false;
            }
            if (data_rec[2] == 1)
                uavs.GetComponent<control_center>().car1_brake = true;
            else if (data_rec[2] == 0)
                uavs.GetComponent<control_center>().car1_brake = false;
            if (data_rec[3] == 1)
            {
                uavs.GetComponent<control_center>().car2_front = true;
                uavs.GetComponent<control_center>().car2_back = false;
            }
            else if (data_rec[3] == -1)
            {
                uavs.GetComponent<control_center>().car2_front = false;
                uavs.GetComponent<control_center>().car2_back = true;
            }
            else if (data_rec[3] == 0)
            {
                uavs.GetComponent<control_center>().car2_front = false;
                uavs.GetComponent<control_center>().car2_back = false;
            }
            if (data_rec[4] == 1)
            {
                uavs.GetComponent<control_center>().car2_left = true;
                uavs.GetComponent<control_center>().car2_right = false;
            }
            else if (data_rec[4] == -1)
            {
                uavs.GetComponent<control_center>().car2_left = false;
                uavs.GetComponent<control_center>().car2_right = true;
            }
            else if (data_rec[4] == 0)
            {
                uavs.GetComponent<control_center>().car2_left = false;
                uavs.GetComponent<control_center>().car2_right = false;
            }
            if (data_rec[5] == 1)
                uavs.GetComponent<control_center>().car2_brake = true;
            else if (data_rec[5] == 0)
                uavs.GetComponent<control_center>().car2_brake = false;
            if (data_rec[6] == 1)
            {
                uavs.GetComponent<control_center>().drone1_front = true;
                uavs.GetComponent<control_center>().drone1_back = false;
            }
            else if (data_rec[6] == -1)
            {
                uavs.GetComponent<control_center>().drone1_front = false;
                uavs.GetComponent<control_center>().drone1_back = true;
            }
            else if (data_rec[6] == 0)
            {
                uavs.GetComponent<control_center>().drone1_front = false;
                uavs.GetComponent<control_center>().drone1_back = false;
            }
            if (data_rec[7] == 1)
            {
                uavs.GetComponent<control_center>().drone1_left = false;
                uavs.GetComponent<control_center>().drone1_right = true;
            }
            else if (data_rec[7] == -1)
            {
                uavs.GetComponent<control_center>().drone1_left = true;
                uavs.GetComponent<control_center>().drone1_right = false;
            }
            else if (data_rec[7] == 0)
            {
                uavs.GetComponent<control_center>().drone1_left = false;
                uavs.GetComponent<control_center>().drone1_right = false;
            }
            if (data_rec[8] == 1)
            {
                uavs.GetComponent<control_center>().drone1_up = true;
                uavs.GetComponent<control_center>().drone1_down = false;
            }
            else if (data_rec[8] == -1)
            {
                uavs.GetComponent<control_center>().drone1_up = false;
                uavs.GetComponent<control_center>().drone1_down = true;
            }
            else if (data_rec[8] == 0)
            {
                uavs.GetComponent<control_center>().drone1_up = false;
                uavs.GetComponent<control_center>().drone1_down = false;
            }
            if (data_rec[9] == 1)
            {
                uavs.GetComponent<control_center>().drone1_left_turn = true;
                uavs.GetComponent<control_center>().drone1_right_turn = false;
            }
            else if (data_rec[9] == -1)
            {
                uavs.GetComponent<control_center>().drone1_left_turn = false;
                uavs.GetComponent<control_center>().drone1_right_turn = true;
            }
            else if (data_rec[9] == 0)
            {
                uavs.GetComponent<control_center>().drone1_left_turn = false;
                uavs.GetComponent<control_center>().drone1_right_turn = false;
            }
            if (data_rec[10] == 1)
            {
                uavs.GetComponent<control_center>().drone2_front = true;
                uavs.GetComponent<control_center>().drone2_back = false;
            }
            else if (data_rec[10] == -1)
            {
                uavs.GetComponent<control_center>().drone2_front = false;
                uavs.GetComponent<control_center>().drone2_back = true;
            }
            else if (data_rec[10] == 0)
            {
                uavs.GetComponent<control_center>().drone2_front = false;
                uavs.GetComponent<control_center>().drone2_back = false;
            }
            if (data_rec[11] == 1)
            {
                uavs.GetComponent<control_center>().drone2_left = false;
                uavs.GetComponent<control_center>().drone2_right = true;
            }
            else if (data_rec[11] == -1)
            {
                uavs.GetComponent<control_center>().drone2_left = true;
                uavs.GetComponent<control_center>().drone2_right = false;
            }
            else if (data_rec[11] == 0)
            {
                uavs.GetComponent<control_center>().drone2_left = false;
                uavs.GetComponent<control_center>().drone2_right = false;
            }
            if (data_rec[12] == 1)
            {
                uavs.GetComponent<control_center>().drone2_up = true;
                uavs.GetComponent<control_center>().drone2_down = false;
            }
            else if (data_rec[12] == -1)
            {
                uavs.GetComponent<control_center>().drone2_up = false;
                uavs.GetComponent<control_center>().drone2_down = true;
            }
            else if (data_rec[12] == 0)
            {
                uavs.GetComponent<control_center>().drone2_up = false;
                uavs.GetComponent<control_center>().drone2_down = false;
            }
            if (data_rec[13] == 1)
            {
                uavs.GetComponent<control_center>().drone2_left_turn = true;
                uavs.GetComponent<control_center>().drone2_right_turn = false;
            }
            else if (data_rec[13] == -1)
            {
                uavs.GetComponent<control_center>().drone2_left_turn = false;
                uavs.GetComponent<control_center>().drone2_right_turn = true;
            }
            else if (data_rec[13] == 0)
            {
                uavs.GetComponent<control_center>().drone2_left_turn = false;
                uavs.GetComponent<control_center>().drone2_right_turn = false;
            }
        }
        else
        {
            Debug.Log(data_rec);
            if (data_rec[0] == 1)
                uavs.GetComponent<control_center>().car1_front = true;
            else if (data_rec[0] == -1)
            {
                uavs.GetComponent<control_center>().car1_back = true;
                uavs.GetComponent<control_center>().car1_front = false;
            }
            else if (data_rec[0] == 0)
            {
                uavs.GetComponent<control_center>().car1_front = false;
                uavs.GetComponent<control_center>().car1_back = false;
            }
            if (data_rec[1] == 1)
            {
                uavs.GetComponent<control_center>().car1_left = true;
                uavs.GetComponent<control_center>().car1_right = false;
            }
            else if (data_rec[1] == -1)
            {
                uavs.GetComponent<control_center>().car1_left = false;
                uavs.GetComponent<control_center>().car1_right = true;
            }
            else if (data_rec[1] == 0)
            {
                uavs.GetComponent<control_center>().car1_left = false;
                uavs.GetComponent<control_center>().car1_right = false;
            }
            if (data_rec[2] == 1)
                uavs.GetComponent<control_center>().car1_brake = true;
            else if (data_rec[2] == 0)
                uavs.GetComponent<control_center>().car1_brake = false;
            if (data_rec[3] == 1)
            {
                uavs.GetComponent<control_center>().car2_front = true;
                uavs.GetComponent<control_center>().car2_back = false;
            }
            else if (data_rec[3] == -1)
            {
                uavs.GetComponent<control_center>().car2_front = false;
                uavs.GetComponent<control_center>().car2_back = true;
            }
            else if (data_rec[3] == 0)
            {
                uavs.GetComponent<control_center>().car2_front = false;
                uavs.GetComponent<control_center>().car2_back = false;
            }
            if (data_rec[4] == 1)
            {
                uavs.GetComponent<control_center>().car2_left = true;
                uavs.GetComponent<control_center>().car2_right = false;
            }
            else if (data_rec[4] == -1)
            {
                uavs.GetComponent<control_center>().car2_left = false;
                uavs.GetComponent<control_center>().car2_right = true;
            }
            else if (data_rec[4] == 0)
            {
                uavs.GetComponent<control_center>().car2_left = false;
                uavs.GetComponent<control_center>().car2_right = false;
            }
            if (data_rec[5] == 1)
                uavs.GetComponent<control_center>().car2_brake = true;
            else if (data_rec[5] == 0)
                uavs.GetComponent<control_center>().car2_brake = false;
            uavs.GetComponent<control_center>().drone1_speed_xcontrol = data_rec[6];
            uavs.GetComponent<control_center>().drone1_speed_zcontrol = data_rec[7];
            uavs.GetComponent<control_center>().drone2_speed_xcontrol = data_rec[8];
            uavs.GetComponent<control_center>().drone2_speed_zcontrol = data_rec[9];
            uavs.GetComponent<control_center>().drone3_speed_xcontrol = data_rec[10];
            uavs.GetComponent<control_center>().drone3_speed_zcontrol = data_rec[11];

        }
    }
}
