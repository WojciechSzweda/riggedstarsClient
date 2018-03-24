using UnityEngine;

public static class ServerConfig {
    public static string URL = "127.0.0.1";
    public static string PORT = "3000";

    public static string getServerURL() {
        return URL + ":" + PORT;
    }

}
