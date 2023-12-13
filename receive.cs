using System.Collections.Generic;
using System;
using UnityEngine;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
// 收发消息
public class Connect : MonoBehaviour
{
    //public float[] data_rec;
    public string data_rec_str;
    public GameObject uavs;
    private void Update()
    {
        if (U2P.Instance.isConnected)
        {
            data_rec_str = U2P.Instance.RecData_str();//字符串接收
            if (data_rec_str != null)
            {
                // 将数组转换为字符串并打印  TODO：用newtonsoft.json解析 而不是半自动
                string json = data_rec_str;//合集 还需要分割
                MatchCollection matches = Regex.Matches(json, @"\{.*?\}");//正则表达式
                List<AgentState> agentStates = new List<AgentState>();
                foreach (Match match in matches)
                {
                    try
                    {
                        string matchString = match.Value;
                        print(matchString);
                        string cleanedMatchString = matchString.Replace("\\", "");//必须去除\\ 否则无法转换
                        AgentState agentState = JsonUtility.FromJson<AgentState>(cleanedMatchString);
                        agentStates.Add(agentState);//集合 全部的新状态
                        //Debug.Log($"Type: {agentState.type}, ID:{agentState.id},Position: {string.Join(", ", agentState.position)}, Rotation: {string.Join(", ", agentState.rotation)}, Velocity: {string.Join(", ", agentState.velocity)}, Target: {agentState.target}, Distance to target: {agentState.distance_to_target}");
                    }
                    catch (ArgumentException ex)
                    {
                        Debug.LogError($"Failed to parse JSON: {match.Value}. Error: {ex.Message}");
                    }
                }
                print("接收到指令");
                //更新agent状态，传入控制量
                agent_update(agentStates);

                string msgsend = get_state(agentStates);               
                U2P.Instance.SendStringData(msgsend);//回传
            }
        }
    }
    // 获取状态
    string get_state(List<AgentState> agentStateList)//获取、更新环境状态
    {
        //float[] data_send = new float[]
        Dictionary<string, AgentState> statedict = new Dictionary<string, AgentState>();
        
        
        for (int i = 0; i < agentStateList.Count; i++)
        {
            AgentState agentState = agentStateList[i];
            if (i<2)
            {
                agentState.position[0] = uavs.GetComponent<control_center>().car_localPosition_x[i];
                agentState.position[1] = uavs.GetComponent<control_center>().car_localPosition_y[i];
                agentState.position[2] = uavs.GetComponent<control_center>().car_localPosition_z[i];
                agentState.rotation[0] = uavs.GetComponent<control_center>().car_localRotation_x[i];
                agentState.rotation[1] = uavs.GetComponent<control_center>().car_localRotation_y[i];
                agentState.rotation[2] = uavs.GetComponent<control_center>().car_localRotation_z[i];
                agentState.velocity[0] = uavs.GetComponent<control_center>().car_speed_x[i];
                agentState.velocity[1] = uavs.GetComponent<control_center>().car_speed_y[i];
                agentState.velocity[2] = uavs.GetComponent<control_center>().car_speed_z[i];


            }
            else
            {
                agentState.position[0] = uavs.GetComponent<control_center>().drone_localPosition_x[i-2];
                agentState.position[1] = uavs.GetComponent<control_center>().drone_localPosition_y[i-2];
                agentState.position[2] = uavs.GetComponent<control_center>().drone_localPosition_z[i-2];
                agentState.rotation[0] = uavs.GetComponent<control_center>().drone_localRotation_x[i-2];
                agentState.rotation[1] = uavs.GetComponent<control_center>().drone_localRotation_y[i-2];
                agentState.rotation[2] = uavs.GetComponent<control_center>().drone_localRotation_z[i-2];
                agentState.velocity[0] = uavs.GetComponent<control_center>().drone_speed_x[i-2];
                agentState.velocity[1] = uavs.GetComponent<control_center>().drone_speed_y[i-2];
                agentState.velocity[2] = uavs.GetComponent<control_center>().drone_speed_z[i-2];

            }
            statedict.Add(agentState.id.ToString(), agentState);
        }
        string jsonstr = JsonConvert.SerializeObject(statedict);
        return jsonstr;
    }
    void agent_update(List<AgentState> agentStates)
    {//更新agent状态，传入控制量 后续指定需要更改的状态量
        for (int i = 0; i < agentStates.Count; i++)
        {
            AgentState agentState = agentStates[i];
            if (i<2)
            {
                uavs.GetComponent<control_center>().car_front[i] = agentState.car_manual_control[0];
                uavs.GetComponent<control_center>().car_back[i] = agentState.car_manual_control[1];
                uavs.GetComponent<control_center>().car_left[i] = agentState.car_manual_control[2];
                uavs.GetComponent<control_center>().car_right[i] = agentState.car_manual_control[3];
                uavs.GetComponent<control_center>().car_brake[i] = agentState.car_manual_control[4];
            }
            else
            {
                uavs.GetComponent<control_center>().drone_speed_xcontrol[i-2] = agentState.drone_speed_control[0];
                uavs.GetComponent<control_center>().drone_speed_zcontrol[i-2] = agentState.drone_speed_control[2];
            }

                
        }

    }
}
