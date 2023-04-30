using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public static class TestNetwork
{
    static string url = "localhost";
    static int port = 4000;

    static Socket socket = null;

    static bool conected = false;

    static TestNetwork(){
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    static public async void connect(){
        if (conected) return;

        bool flag = true;
        try{
            await socket.ConnectAsync(url, port);
        }catch{
            flag = false;
            Debug.Log("Оштбка при подключении к серверу");
        }

        if(flag){
            Debug.Log("Подключение было успешным");
            conected = true;
        }
    }

    static public async void senMessage(string reqMessage){
        byte[] bufferReq = Encoding.UTF8.GetBytes(reqMessage);
        await socket.SendAsync(bufferReq);
        Debug.Log($"посылаю запрос : {reqMessage}");

        byte[] buffer = new byte[512];
        int count = await socket.ReceiveAsync(buffer);
        string resText = Encoding.UTF8.GetString(buffer);

        Debug.Log($"получил ответ : {resText}");
    }

}
