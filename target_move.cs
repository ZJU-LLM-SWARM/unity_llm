using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_move : MonoBehaviour
{
    public float speed = 2f; // 移动速度
    public float range = 5f; // 移动范围
    public float map_range = 450f; // 地图边界
    private Vector3 targetPosition; // 目标位置
    public float target_localPosition_x;
    public float target_localPosition_y;
    public float target_localPosition_z;
    public GameObject uavs;
    void Start () {
        // 初始化目标位置
        targetPosition = GetRandomPosition();
    }
    void Update () {
        // 计算当前位置到目标位置的距离
        float distance = Vector3.Distance(transform.position, targetPosition);
        uavs.GetComponent<control_center>().target_x = transform.position.x;
        uavs.GetComponent<control_center>().target_y = transform.position.y;
        uavs.GetComponent<control_center>().target_z = transform.position.z;


        // 如果距离小于0.1则重新获取目标位置
        if (distance < 0.1f) {
            targetPosition = GetRandomPosition();
        }
        // 如果超出地图边界，则重新获取目标位置
        if (transform.position.x < -map_range || transform.position.x > map_range ||
            transform.position.y < -map_range || transform.position.y > map_range ||
            transform.position.z < -map_range || transform.position.z > map_range) {
            targetPosition = GetRandomPosition();
        }

        // 计算移动方向和距离
        Vector3 direction = (targetPosition - transform.position).normalized;
        float moveDistance = Mathf.Min(speed * Time.deltaTime, distance);

        // 移动物体
        transform.position += direction * moveDistance;
    }

    // 获取一个随机位置
    Vector3 GetRandomPosition () {
        float x = Random.Range(-range, range);
        float y = 0;
        float z = Random.Range(-range, range);

        return new Vector3(x, y, z);
    }
}
