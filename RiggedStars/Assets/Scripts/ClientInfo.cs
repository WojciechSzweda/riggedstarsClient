using UnityEngine;

public static class ClientInfo {

    public static string Name;
    public static int ID;
    public static string Token;
    public static string CreatedAt;
    public static long Stack;


    public static void SetClientInfo(ClientData data, string token) {
        Name = data.Name;
        ID = data.ID;
        CreatedAt = data.CreatedAt;
        Stack = data.Stack;
        Token = token;
    }

}

public class ClientData {
    public int ID { get; set; }
    public string CreatedAt { get; set; }
    public string Name { get; set; }
    public long Stack { get; set; }
}
