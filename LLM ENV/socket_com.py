import numpy as np
import socket
import os
# import send_data as sd
import path_control as pc
import path_control_formation as pcf

# 1:formation 2:path
mode = 1

def connect_unity(host,port):
    global sock
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    # sock = socket.socket()
    sock.connect((host, port))
    print('连接已建立')
 
def send_to_unity(arr):
    arr_list = arr.flatten().tolist() # numpy数组转换为list类型
    data = '' + ','.join([str(elem) for elem in arr_list]) + ''  # 每个float用,分割
    sock.sendall(bytes(data, encoding="utf-8"))  # 发送数据
    # print("向unity发送：", arr_list)
 
def rec_from_unity():
    data = sock.recv(1024)
    data = str(data, encoding='utf-8')
    # print(data)
    data = data.split(',')
    new_data = []
    for d in data:
        new_data.append(float(d))
    # print('从环境接收：\n小车1状态：',new_data[0:7])
    # print('\n小车2状态：',new_data[7:14])
    # print('\n无人机1状态：',new_data[14:20])
    # print('\n无人机2状态：',new_data[20:])
    return new_data
 
# # 打开并读取文件
# with open('my_file.txt', 'r', encoding='utf-8') as f:
#     content = f.readlines()

# # 提取特定内容后面的字符串
# substring = ["car1","car2","drone1","drone2"]
# text = []
# for x in substring:
#     for line in content:
#         if x in line:
#             parts = line.split(x)
#             if len(parts) > 1:
#                 text.append(parts[1]) 
#                 # print(text)  # parts[1] 是指定字符串后面的内容
#             else:
#                 print("指定字符串后面没有内容.")
#             break
# # print(text)
# numbers = re.findall(r"-?\d+", str(text))
# data = np.array(numbers)
def write_situation(rev):
    with open('situation.txt', 'w', encoding='utf-8') as f:
        f.write('car1:' +'\n' + 'position:' + str(rev[0:3]) + '  rotation:' + str(rev[3:6]) + '  speed:' + str(rev[6]) + '  target found!' + str(bool(int(rev[7]))) +'  distance'+ str(rev[8]) +'\n')
        f.write('car2:' +'\n' + 'position:' + str(rev[9:12]) + '  rotation:' + str(rev[12:15]) + '  speed:' + str(rev[15]) + 'target found!'+ str(bool(int(rev[16]))) + '  distance' + str(rev[17]) + '\n')
        f.write('drone1:' + '\n' + 'position' + str(rev[18:21]) + '  rotation:' + str(rev[21:24]) + '  target found!'+ str(bool(int(rev[24]))) + '  distance' + str(rev[25])+ '\n')
        f.write('drone2:' + '\n' + 'position' + str(rev[26:29]) + '  rotation:' + str(rev[29:32]) + '  target found!'+ str(bool(int(rev[32]))) + '  distance' + str(rev[33]) +'\n')
        if rev[7] or rev[16] or rev[24] or rev[32]:
            f.write('target:' + '\n' + 'position' + str(rev[34:37]) + '\n')
        else :
            f.write('target:' + '\n' + 'position' + 'not found' + '\n')
        f.close()

def socket_main():
    host = '127.0.0.1'
    port = 5005   
    # import path_create as create
    # create.create_path()
    connect_unity(host,port)
    
    # 输出非 1 0 -1 时，unity会保持上一次的状态
    data_test = np.ones(14) * 2


    while True:
        # os.system('cls')  # 清屏
        send_to_unity(data_test)
        rev = rec_from_unity()  # Unity接收数据后再发送下一个
        write_situation(rev)
        if mode == 1:
            pcf.target_distance_init()
            pcf.clockwise_init()
            data_test = pcf.main_control(rev)
        elif mode == 2:
            data_test = pc.main_control(rev)

socket_main()