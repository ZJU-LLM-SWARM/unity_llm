import numpy as np
import math
import time
import drone_formation.formation as fm

d_v = 100

def delete_first_line(filename):
    try:
        with open(filename, 'r') as f:
            lines = f.readlines()
        with open(filename, 'w') as f:
            f.writelines(lines[1:])
    except FileNotFoundError:
        print(f"File '{filename}' not found.")

def get_txt_lines(filename):
    with open(filename, 'r') as f:
        lines = f.readlines()
    line_number = len(lines)
    return line_number

def get_car_target(state_x,state_y,file_path):
    target_positions = np.loadtxt(file_path)
    target_positions = np.array(target_positions)
    line_number = get_txt_lines(file_path)
    if line_number == 0:
        return None
    elif line_number == 1:
        x = target_positions[1] - state_y
        y = target_positions[0] - state_x
        distance = math.sqrt(x**2 + y**2)
        if distance < 5:
            #移除第一个元素
            delete_first_line(file_path)
            # time.sleep(2)
        target = target_positions
        return target
    else:
        x = target_positions[0][1] - state_y
        y = target_positions[0][0] - state_x
        distance = math.sqrt(x**2 + y**2)
        if distance < 5:
            #移除第一个元素
            delete_first_line(file_path)
        target = target_positions[0]
        return target
    
class VehicleState:
    def __init__(self, x=0.0, y=0.0, yaw=0.0, v=0.0):
        self.x = x
        self.y = y
        self.yaw = yaw
        self.v = v

class DroneState:
    def __init__(self, x=0.0, y=0.0, z=0.0, yaw=0.0):
        self.x = x
        self.y = y
        self.z = z
        self.yaw = yaw

def PControl(target_speed,state):
    if target_speed > state.v:
        a = 1
    elif target_speed < state.v:
        a = -1
    if target_speed == 0:
        a = 0
    return a

def angle_control(state, target_position):
    tx = target_position[0]
    ty = target_position[1]
    rotation_target = math.atan2(tx - state.x, ty - state.y)
    rotation_target = rotation_target * 180 / math.pi
    self_angle = state.yaw
    if self_angle > 180:
        self_angle -= 360
    alpha = rotation_target - self_angle
    if alpha > 180:
        alpha -= 360
    if alpha < -180:
        alpha += 360
    if alpha > 5:
        alpha = -1
    elif alpha < -5:
        alpha = 1
    else:
        alpha = 0
    return alpha

def car1_control(rev):

    # 设置车辆的出事状态
    car1_state = VehicleState(x=rev[0], y=rev[2], yaw=rev[4], v=rev[6])
    data = np.zeros(3)
    target_position = get_car_target(car1_state.x,car1_state.y,'D:/LLM_unity/001.txt')
    if target_position is None:
        car1_target_speed = 0
        data[0] = 0
        data[1] = 0
        data[2] = 1
    else :
        car_tar_dis = math.sqrt((target_position[0] - car1_state.x)**2 + (target_position[1] - car1_state.y)**2)
        if car_tar_dis < 10:
            car1_target_speed = 5
        elif car_tar_dis < 30:
            car1_target_speed = 10
        else:
            car1_target_speed = 20   
        di = angle_control(car1_state, target_position)
        if di is not 0:
            car1_target_speed = 5
        ai = PControl(car1_target_speed, car1_state)
        data[0] = ai
        data[1] = di
        data[2] = 0
    return data

def car2_control(rev):
    car2_state = VehicleState(x=rev[9], y=rev[11], yaw=rev[13], v=rev[15])
    data = np.zeros(3)
    target_position = get_car_target(car2_state.x,car2_state.y,'D:/LLM_unity/002.txt')
    if target_position is None:
        car2_target_speed = 0
        data[0] = 0
        data[1] = 0
        data[2] = 1
    else :
        car2_target_speed = 5    
        ai = PControl(car2_target_speed, car2_state)
        di = angle_control(car2_state, target_position)
        data[0] = ai
        data[1] = di
        data[2] = 0
    return data

n = 4
ob_n = 2
distance = np.zeros((n+1, n+1))
velocity = np.zeros((n+1, 2))
position = np.zeros((n+1, 2))
ob_position = np.zeros((ob_n, 2))
target_distance = np.zeros((n+1, n+1))
clockwise = np.zeros(n+1)
virtual_drone_position = np.zeros((2,1))
alpha_force = 1000000
F_d = np.array([0,0])


def set_obstacle():
    ob_position[0,0] = -200
    ob_position[0,1] = -425
    ob_position[1,0] = -425
    ob_position[1,1] = -200

# force of obstacle
def get_avoid_force(x,y):
    F_d[0] = 0
    F_d[1] = 0
    for j in range(0,ob_n):
        q = 100
        d = math.sqrt((x - ob_position[j,0])**2 + (y - ob_position[j,1])**2)
        if d < q:
            F_d[0] = F_d[0] + alpha_force * (1/d - 1/100) * (1/d) * (x - ob_position[j,0])
            F_d[1] = F_d[1] + alpha_force * (1/d - 1/100) * (1/d) * (y - ob_position[j,1])

def auto_avoid_obstacle():
    set_obstacle()
    for i in range (2,n+1):
        get_avoid_force(position[i,0],position[i,1])
        print(F_d)
        print(velocity[i,:])
        velocity[i,0] = velocity[i,0] + F_d[0]
        velocity[i,1] = velocity[i,1] + F_d[1]

def target_distance_init():
    for i in range(0,n):
        for j in range(0,n):
            target_distance[i,j] = -1
    for i in range(1,n):
        target_distance[i,i] = 0

    target_distance[2,0] = 0
    target_distance[3,1] = 141.4
    target_distance[3,2] = 200
    target_distance[4,1] = 141.4
    target_distance[4,2] = 200

def clockwise_init():
    clockwise[1] = 0
    clockwise[2] = 0
    clockwise[3] = -1
    clockwise[4] = 1

def update(rev):
    car1_position = np.array(rev[:3])
    car1_rotation = np.array(rev[3:6])
    car1_speed = rev[6:9]
    car2_position = np.array(rev[11:14])
    car2_rotation = np.array(rev[14:17])
    car2_speed = rev[17:20]
    drone1_position = np.array(rev[22:25])
    drone1_rotation = np.array(rev[25:28])
    drone1_speed = rev[28:31]
    drone2_position = np.array(rev[33:36])
    drone2_rotation = np.array(rev[36:39])
    drone2_speed = rev[39:42]
    drone3_position = np.array(rev[44:47])
    drone3_rotation = np.array(rev[47:50])
    drone3_speed = rev[50:53]
    time = rev[58]
    velocity[1,0] = car1_speed[0]
    velocity[1,1] = car1_speed[2]
    velocity[2,0] = drone1_speed[0]
    velocity[2,1] = drone1_speed[2]
    velocity[3,0] = drone2_speed[0]
    velocity[3,1] = drone2_speed[2]
    velocity[4,0] = drone3_speed[0]
    velocity[4,1] = drone3_speed[2]

    position[0,0] = car1_position[0] + d_v
    position[0,1] = car1_position[2] + d_v
    position[1,0] = car1_position[0]
    position[1,1] = car1_position[2]
    position[2,0] = drone1_position[0]
    position[2,1] = drone1_position[2]
    position[3,0] = drone2_position[0]
    position[3,1] = drone2_position[2]
    position[4,0] = drone3_position[0]
    position[4,1] = drone3_position[2]
    for k in range(0, n+1):
        for j in range(0, n+1):
            distance[k,j] = math.sqrt((position[k,0] - position[j,0])**2 + (position[k,1] - position[j,1])**2)

def get_first_follower_v():
    y1, y2, d1, d2 = fm.transforming(target_distance[2,0], target_distance[2,2], position[2,:], position[0,:])
    s = fm.cal_s(y1, y2, d1, d2)
    Rs = np.array([[s, clockwise[2] * math.sqrt(1 - s*s)],[-clockwise[2] * math.sqrt(1 - s*s), s]])
    velocity[2,:] = -distance[2,0] ** 2 * (position[2,:] - y1).T - np.dot(d1 * distance[2,0] * Rs, np.array((y1 - y2).T))    

def control():
    get_first_follower_v()
    for k in range(3,n+1):
        y1, y2, d1, d2 = fm.transforming(target_distance[k,1], target_distance[k,2], position[1,:], position[2,:])
        s = fm.cal_s(y1, y2, d1, d2)
        Rs = np.array([[s, clockwise[k] * math.sqrt(1 - s*s)],[-clockwise[k] * math.sqrt(1 - s*s), s]])
        velocity[k,:] = -distance[2,1] ** 2 * (position[k,:] - y1).T - np.dot(d1 * distance[2,1] * Rs, np.array((y1 - y2).T))

def check():
    for i in range(1,n+1):
        if velocity[i,0] > 100:
            velocity[i,0] = 100
        if velocity[i,0] < -100:
            velocity[i,0] = -100
        if velocity[i,1] > 100:
            velocity[i,1] = 100
        if velocity[i,1] < -100:
            velocity[i,1] = -100
        

def drone_control(rev):
    update(rev)
    control()
    check()
    auto_avoid_obstacle()
    check()
    return velocity[2,:],velocity[3,:],velocity[4,:]

def main_control(rev):
    data_send = np.zeros(13)
    data_send[0:3] = car1_control(rev)
    data_send[3:6] = car2_control(rev)
    drone_control(rev)
    data_send[6:8] = velocity[2,:]
    data_send[8:10] = velocity[3,:]
    data_send[10:12] = velocity[4,:]
    data_send[12] = 9
    # data_send[3:6] = car2_control(rev)
    # data_send[6:10] = drone1_control(rev)
    # data_send[10:] = drone2_control(rev)
    return data_send
    
# if __name__ == '__main__':
#     main_control()

