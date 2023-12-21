using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel1 :MonoBehaviour
{
    public Vector3 com;
    public Rigidbody rb;

    public float moveSpeed = 1500;
    public float maxAngle = 45;
    public float angleSpeed = 50;
    public float breakMove = 5000f;
    
    //注意这八个变量，四个是获取车轮碰撞器的，四个是获取车轮模型的
    public WheelCollider leftF;
    public WheelCollider leftB;
    public WheelCollider rightF;
    public WheelCollider rightB;
    public Transform model_leftF;
    public Transform model_leftB;
    public Transform model_rightF;
    public Transform model_rightB;

    public bool left;
    public bool right;
    public bool front;
    public bool back;
    public bool isBraking;
    public float local_Position_x;
    public float local_Position_y;
    public float local_Position_z;
    public float local_Rotation_x;
    public float local_Rotation_y;
    public float local_Rotation_z;
    public float car_speed_x;
    public float car_speed_y;
    public float car_speed_z;
    float target_x;
    float target_y;
    float target_z;
    float distance_car1;
    bool car1_found;
    int car_id =0;
    public GameObject uavs;
    void Update()
    {
        get_self_information();
        get_center_of_mass();
        information_update();
        WheelsControl_Update();
        WheelRot();
    }
    void information_update()
    {
        left = uavs.GetComponent<control_center>().car_left[car_id];
        right = uavs.GetComponent<control_center>().car_right[car_id];
        front = uavs.GetComponent<control_center>().car_front[car_id];
        back = uavs.GetComponent<control_center>().car_back[car_id];
        isBraking = uavs.GetComponent<control_center>().car_brake[car_id];
        target_x = uavs.GetComponent<control_center>().target_x;
        target_y = uavs.GetComponent<control_center>().target_y;
        target_z = uavs.GetComponent<control_center>().target_z;
        local_Position_x = transform.localPosition.x;
        local_Position_y = transform.localPosition.y;
        local_Position_z = transform.localPosition.z;
        distance_car1 = math_distance(local_Position_x, local_Position_y, local_Position_z, target_x, target_y, target_z);
        car1_found = found(distance_car1);
        uavs.GetComponent<control_center>().car1_found = car1_found;
        uavs.GetComponent<control_center>().distance_car1_target = distance_car1;
    }
    float math_distance(float x1, float y1, float z1, float x2, float y2, float z2)
    {
        float distance;
        distance = Mathf.Sqrt((x1 - x2) * (x1 - x2) + (z1-z2)*(z1-z2));
        return distance;
    }
    bool found(float distance)
    {
        if(distance<30f)
            return true;
        else
            return false;
    }

    void get_self_information()
    {
        local_Position_x = transform.localPosition.x;
        uavs.GetComponent<control_center>().car_localPosition_x[car_id] = local_Position_x;
        local_Position_y = transform.localPosition.y;
        uavs.GetComponent<control_center>().car_localPosition_y[car_id] = local_Position_y;
        local_Position_z = transform.localPosition.z;
        uavs.GetComponent<control_center>().car_localPosition_z[car_id] = local_Position_z;
        local_Rotation_x = transform.localRotation.eulerAngles.x;
        uavs.GetComponent<control_center>().car_localRotation_x[car_id] = local_Rotation_x;
        local_Rotation_y = transform.localRotation.eulerAngles.y;
        uavs.GetComponent<control_center>().car_localRotation_y[car_id] = local_Rotation_y;
        local_Rotation_z = transform.localRotation.eulerAngles.z;
        uavs.GetComponent<control_center>().car_localRotation_z[car_id] = local_Rotation_z;
        car_speed_x = rb.velocity.x;
        uavs.GetComponent<control_center>().car_speed_x[car_id] = car_speed_x;
        car_speed_y = rb.velocity.y;
        uavs.GetComponent<control_center>().car_speed_y[car_id] = car_speed_y;
        car_speed_z = rb.velocity.z;
        uavs.GetComponent<control_center>().car_speed_z[car_id] = car_speed_z;

    }
    void get_center_of_mass()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
    }
   

    void WheelsControl_Update()
    {
        // float h = Input.GetAxisRaw("Horizontal");
        // float v = Input.GetAxisRaw("Vertical");
        if (front)
        {
            leftF.motorTorque = moveSpeed;
            leftB.motorTorque = moveSpeed;
            rightF.motorTorque = moveSpeed;
            rightB.motorTorque = moveSpeed;
        }
        else if (back)
        {
            leftF.motorTorque = -moveSpeed;
            leftB.motorTorque = -moveSpeed;
            rightF.motorTorque = -moveSpeed;
            rightB.motorTorque = -moveSpeed;
        }
        else
        {
            leftF.motorTorque = 0;
            leftB.motorTorque = 0;
            rightF.motorTorque = 0;
            rightB.motorTorque = 0;
        }
        // leftB.motorTorque = v * moveSpeed;
        // rightB.motorTorque = v * moveSpeed;
        // leftF.motorTorque = v * moveSpeed;
        // rightF.motorTorque = v * moveSpeed;
        WheelsModel_Update(model_leftF,leftF);
        WheelsModel_Update(model_leftB,leftB);
        WheelsModel_Update(model_rightF,rightF);
        WheelsModel_Update(model_rightB,rightB);
    }
    void WheelsModel_Update(Transform t, WheelCollider wheel)
    {
        Vector3 pos = t.position;
        Quaternion rot = t.rotation * Quaternion.Euler(0, 90, 0);
        wheel.GetWorldPose(out pos,out rot);
        t.position = pos;
        t.rotation = rot * Quaternion.Euler(0, 90, 0);
    }
    void WheelRot()
    {
        float v = Input.GetAxisRaw("Vertical");
        if (left)
        {
            leftF.steerAngle -= Time.deltaTime * angleSpeed;
            rightF.steerAngle -= Time.deltaTime * angleSpeed;
            if (leftF.steerAngle < (0-maxAngle) || rightF.steerAngle < (0-maxAngle))
            {
                //到最大值后就不能继续加角度了
                leftF.steerAngle = (0 - maxAngle);
                rightF.steerAngle = (0 - maxAngle);
            }
            leftF.motorTorque = v * moveSpeed * 0.85f;
            leftB.motorTorque = v * moveSpeed * 0.85f;

        }
         //右转向
        else if (right)
        {
            leftF.steerAngle += Time.deltaTime * angleSpeed;
            rightF.steerAngle += Time.deltaTime * angleSpeed;
            if (leftF.steerAngle > maxAngle || rightF.steerAngle > maxAngle)
            {
                leftF.steerAngle = maxAngle;
                rightF.steerAngle = maxAngle;
            }
            rightF.motorTorque = v * moveSpeed * 0.85f;
            rightB.motorTorque = v * moveSpeed * 0.85f;

        }
        //松开转向后，方向打回
        else 
        {
            if (leftF.steerAngle < 0)
            {
                leftF.steerAngle += Time.deltaTime * 2 * angleSpeed;
                rightF.steerAngle += Time.deltaTime * 2 * angleSpeed;  
            }
            if (leftF.steerAngle > 0)
            {
                leftF.steerAngle -= Time.deltaTime * angleSpeed;
                rightF.steerAngle -= Time.deltaTime * angleSpeed;   
            }
        }
            
        // bool isBraking = Input.GetKey(KeyCode.Space);
        leftB.brakeTorque = isBraking ? breakMove : 0f;
        rightB.brakeTorque = isBraking ? breakMove : 0f;
        leftF.brakeTorque = isBraking ? breakMove : 0f;
        rightF.brakeTorque = isBraking ? breakMove : 0f;
    }    
}
    