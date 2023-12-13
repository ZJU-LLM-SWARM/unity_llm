using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Linq;

[Serializable]
public class AgentState//新增
{
    public string type;
    public int id;
    public float[] position;
    public float[] rotation;
    public float[] velocity;
    public bool target;
    public float distance_to_target;

    public bool[] car_manual_control= new bool[5];
    public float[] drone_speed_control = new float[3];

}
public class U2P
{
    private static U2P instance;
    private Socket serverSocket;
    private Socket clientSocket;
    public bool isConnected;

    public static U2P Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new U2P();
            }
            return instance;
        }
    }
    private U2P()
    {
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        // IPAddress ipAddress = IPAddress.Parse("0.0.0.0");
        //5005
        serverSocket.Bind(new IPEndPoint(ipAddress, 5005));
        serverSocket.Listen(5);
        StartServer();
    }
    private void StartServer()
    {
        serverSocket.BeginAccept(AcceptCallback, null);
    }
    private void AcceptCallback(IAsyncResult ar)
    {
        Socket sc = serverSocket.EndAccept(ar);
        if (clientSocket == null)
        {
            clientSocket = sc;
            isConnected = true;
            Debug.Log("连接成功");
        }
    }
    public float[] RecData()
    {
        try
        {
            if (clientSocket != null)
            {
                int canRead = clientSocket.Available;
                byte[] buff = new byte[canRead];
                clientSocket.Receive(buff);
                string str = Encoding.UTF8.GetString(buff);
                //Debug.Log(str);
                if (str == "")
                {
                    return null;
                }
                //str = str.Replace("[","").Replace("]","").Replace("\n")Replace("Replace(
                //Debug.Log(“接受消息:”+ str);
                string[] strData = str.Split(',');
                float[] data = new float[strData.Length];
                for (int i = 0; i < strData.Length; i++)
                {
                    data[i] = float.Parse(strData[i]);
                }
                return data;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("发生错误:" + ex);
            if (clientSocket != null)
            {
                clientSocket.Close();
                clientSocket = null;
                isConnected = false;
                StartServer();
            }
        }
        return null;
    }
    public string RecData_str()//新增
    {
        try
        {
            if (clientSocket != null)
            {
                int canRead = clientSocket.Available;
                byte[] buff = new byte[canRead];
                clientSocket.Receive(buff);
                string str = Encoding.UTF8.GetString(buff);
                // Debug.Log(str);
                if (str == "")
                {
                    return null;
                }
                Debug.Log("接受消息:" + str);
                return str;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("发生错误:" + ex);
            if (clientSocket != null)
            {
                clientSocket.Close();
                clientSocket = null;
                isConnected = false;
                StartServer();
            }
        }
        return null;
    }
    public void SendStringData(string strData)//新增
    {
        try
        {
            if (clientSocket != null)
            {
                // 使用clientSocket发送strData到服务器的代码
                byte[] dataBytes = Encoding.UTF8.GetBytes(strData);
                clientSocket.Send(dataBytes);
            }
        }
        catch (Exception ex)
        {
            // 处理异常的代码
            Debug.Log("Error when sending string data: " + ex.Message);
        }
    }
    public void SendData(List<float> data)//之前的浮点数据发送
    {
        try
        {
            if (clientSocket != null)
            {
                string strData = "";
                for (int i = 0; i < data.Count; i++)
                {
                    strData += data[i];
                    if (i != data.Count - 1)
                    {
                        strData += ",";
                    }
                }
                // 使用clientSocket发送strData到服务器的代码
                byte[] dataBytes = Encoding.UTF8.GetBytes(strData);
                clientSocket.Send(dataBytes);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("发生错误:" + ex);
            if (clientSocket != null)
            {
                clientSocket.Close();
                clientSocket = null;
                isConnected = false;
                StartServer();
            }
        }
    }
}
