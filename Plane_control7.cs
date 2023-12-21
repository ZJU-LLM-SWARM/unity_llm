using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class Plane_Control7 : MonoBehaviour
{
    public Transform drone;
    public float speed = 30f;
    public Rigidbody rb;
    public float angle_speed = 1f;
    public float ad_Angel = 0f;
    public float ws_Angel = 0f;
    public bool left;
    public bool right;
    public bool front;
    public bool back;
    public bool Up;
    public bool Down;

    public bool left_turn;
    public bool right_turn;
    public float local_Position_x;
    public float local_Position_y;
    public float local_Position_z;
    public float local_Rotation_x;
    public float local_Rotation_y;
    public float local_Rotation_z;
    public float drone_speed_x;
    public float drone_speed_y;
    public float drone_speed_z;
    public float target_x ;
    public float target_y ;
    public float target_z ;
    public float distance_drone7;
    public bool drone7_found;
    float left_right_move;
    float front_back_move;
    public GameObject uavs;
    private int drone_id = 6;
    void Start()
    {
        drone = gameObject.GetComponent<Transform>();//获取组件
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {   
        get_self_information();
        information_update();
        control();
    }
    void information_update()
    {
        left = uavs.GetComponent<control_center>().drone_left[drone_id];
        right = uavs.GetComponent<control_center>().drone_right[drone_id];
        front = uavs.GetComponent<control_center>().drone_front[drone_id];
        back = uavs.GetComponent<control_center>().drone_back[drone_id];
        Up = uavs.GetComponent<control_center>().drone_up[drone_id];
        Down = uavs.GetComponent<control_center>().drone_down[drone_id];
        left_turn = uavs.GetComponent<control_center>().drone_left_turn[drone_id];
        right_turn = uavs.GetComponent<control_center>().drone_right_turn[drone_id];
        target_x = uavs.GetComponent<control_center>().target_x;
        target_y = uavs.GetComponent<control_center>().target_y;
        target_z = uavs.GetComponent<control_center>().target_z;
        local_Position_x = transform.localPosition.x;
        local_Position_y = transform.localPosition.y;
        local_Position_z = transform.localPosition.z;
        distance_drone7 = math_distance(local_Position_x, local_Position_y, local_Position_z, target_x, target_y, target_z);
        drone7_found = found(distance_drone7);
        uavs.GetComponent<control_center>().drone7_found = drone7_found;
        uavs.GetComponent<control_center>().distance_drone7_target = distance_drone7;
        left_right_move = uavs.GetComponent<control_center>().drone_speed_xcontrol[drone_id];
        front_back_move = uavs.GetComponent<control_center>().drone_speed_zcontrol[drone_id];
    }
    float math_distance(float x1, float y1, float z1, float x2, float y2, float z2)
    {
        float distance;
        distance = Mathf.Sqrt((x1 - x2) * (x1 - x2) + (z1-z2)*(z1-z2));
        return distance;
    }
    bool found(float distance)
    {
        if(distance<100f)
            return true;
        else
            return false;
    }
    void get_self_information()
    {
        uavs.GetComponent<control_center>().drone_localPosition_x[drone_id] = local_Position_x;
        uavs.GetComponent<control_center>().drone_localPosition_y[drone_id] = local_Position_y;
        uavs.GetComponent<control_center>().drone_localPosition_z[drone_id] = local_Position_z;
        local_Rotation_x = transform.localRotation.eulerAngles.x;
        uavs.GetComponent<control_center>().drone_localRotation_x[drone_id] = local_Rotation_x;
        local_Rotation_y = transform.localRotation.eulerAngles.y;
        uavs.GetComponent<control_center>().drone_localRotation_y[drone_id] = local_Rotation_y;
        local_Rotation_z = transform.localRotation.eulerAngles.z;
        uavs.GetComponent<control_center>().drone_localRotation_z[drone_id] = local_Rotation_z;
        drone_speed_x = rb.velocity.x;
        uavs.GetComponent<control_center>().drone_speed_x[drone_id] = drone_speed_x;
        drone_speed_y = rb.velocity.y;
        uavs.GetComponent<control_center>().drone_speed_y[drone_id] = drone_speed_y;
        drone_speed_z = rb.velocity.z;
        uavs.GetComponent<control_center>().drone_speed_z[drone_id] = drone_speed_z;
    }
    void control()
    {
        rb.velocity = new Vector3(left_right_move, 0, front_back_move);        
        // rb.AddForce(0, 5f*9.8f, 0);
        if (left)
        {
            transform.Translate(-speed * Time.deltaTime * Mathf.Cos(ad_Angel /10 * Mathf.Deg2Rad), speed*Time.deltaTime * Mathf.Sin(ad_Angel / 10 * Mathf.Deg2Rad), 0);//Mathf.Cos(Angel * Mathf.Deg2Rad)是Angel°的余弦值，Mathf.Sin(Angel * Mathf.Deg2Rad)是Angel°的正弦值，下同
            if (ad_Angel < 100f)
            {
                this.transform.Rotate(0, 0, 0.1f);
                ad_Angel++;
            }
        }
        if (right)
        {
            transform.Translate(speed*Time.deltaTime * Mathf.Cos(ad_Angel /10 * Mathf.Deg2Rad), speed*Time.deltaTime * Mathf.Sin(- ad_Angel /10 * Mathf.Deg2Rad), 0);
            if (ad_Angel > -100f)
            {
                this.transform.Rotate(0, 0, -0.1f);
                ad_Angel--;
            }
        }
        if (!left && !right)
        {
            if(ad_Angel>0)
            {
                this.transform.Rotate(0, 0, -0.1f);
                ad_Angel--;
            }
            if(ad_Angel<0)
            {
                this.transform.Rotate(0, 0, 0.1f);
                ad_Angel++;
            }
        }

        if (front)
        {
            transform.Translate(0, speed*Time.deltaTime * Mathf.Sin(ws_Angel /10 * Mathf.Deg2Rad), speed*Time.deltaTime * Mathf.Cos(ws_Angel/10 * Mathf.Deg2Rad));
            if (ws_Angel < 100f)
            {
                this.transform.Rotate(0.1f, 0, 0);
                ws_Angel++;
            }
        }
        if (back)
        {
            transform.Translate(0, speed*Time.deltaTime * Mathf.Sin(-ws_Angel /10 * Mathf.Deg2Rad), -speed*Time.deltaTime * Mathf.Cos(ws_Angel /10 * Mathf.Deg2Rad));
        if (ws_Angel > -100f)
            {
                this.transform.Rotate(-0.1f, 0, 0);
                ws_Angel--;
            }
        }
        if (!front && !back)
        {
            if(ws_Angel>0)
            {
                this.transform.Rotate(-0.1f, 0, 0);
                ws_Angel--;
            }
            if(ws_Angel<0)
            {
                this.transform.Rotate(0.1f, 0, 0);
                ws_Angel++;
            }
        }   
        if (left_turn)
        {
            this.transform.Rotate(0, -angle_speed, 0, Space.World);
        }
        if (right_turn)
        {
            this.transform.Rotate(0, angle_speed, 0, Space.World);
        }
        if (Up)
            transform.Translate(0, speed*Time.deltaTime, 0);
        if (Down)
        {
            if(transform.position.y>0.5f)
                transform.Translate(0, -speed*Time.deltaTime, 0);
        }
    }
}
