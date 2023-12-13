import numpy as np
import socket
import os

# import send_data as sd
import path_control as pc
import path_control_formation as pcf
import AgentState
import json
import sys

# 1:formation 2:path
mode = 1

def connect_unity(host, port):
    global sock
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    # sock = socket.socket()
    sock.connect((host, port))
    print("连接已建立")


def rec_json_from_unity():  # 接收unity的json字符串数据 并转为状态量字典

    data = sock.recv(1600)
    # data_size = sys.getsizeof(data)
    # print(f"The size of the data is {data_size} bytes.")
    data = str(data, encoding="utf-8")
    # 将 JSON 字符串转换为字典
    data_dict = json.loads(data)

    return data_dict


def agent_state_update(agents, state_dict):
    for idx, _ in enumerate(state_dict):
        agentstate = state_dict[str(idx)]
        agents[idx].position = agentstate["position"]
        agents[idx].rotation = agentstate["rotation"]
        agents[idx].velocity = agentstate["velocity"]
        agents[idx].target = agentstate["target"]
        agents[idx].distance_to_target = agentstate["distance_to_target"]


def write_log(rev):  # TODO log形式
    with open("situation.txt", "w", encoding="utf-8") as f:
        f.write()


def agent_init():
    agents = AgentState.get_agent_list()  # 智能体初始化
    init_data = []
    for agent in agents:
        init_data.append(agent.encode_state_json())
    init_data_str = json.dumps(init_data)
    sock.sendall(init_data_str.encode("utf-8"))  # 发送json信息
    return agents

def socket_main():
    host = "127.0.0.1"
    port = 5005  # 5005
    # import path_create as create
    # create.create_path()
    mode = 1

    connect_unity(host, port)
    agents = agent_init()  # 根据agentsettings初始化智能体
    # 1阶控制器
    if mode == 1:
        pcf.target_distance_init()
        pcf.clockwise_init()
        while True:
            dic = rec_json_from_unity()  # 回传的agent状态
            agent_state_update(agents, dic)  # 更新部分最新状态
            agents = pcf.main_control(agents)
            data_to_send = []
            for agent in agents:
                data_to_send.append(agent.encode_state_json())
            data_str = json.dumps(data_to_send)
            sock.sendall(data_str.encode("utf-8"))  # 发送json信息
    # 2阶控制器




        # send_to_unity(data_test)
        # if mode == 1:
        #     pcf.target_distance_init()
        #     pcf.clockwise_init()
        #     data_test = pcf.main_control(rev)
        # elif mode == 2:
        #     data_test = pc.main_control(rev)
socket_main()
