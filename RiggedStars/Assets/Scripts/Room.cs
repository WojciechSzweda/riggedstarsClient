using UnityEngine;

[System.Serializable]
public struct Room {
    public int ID { get; set; }
    public int ClientsCount { get; set; }
    public int MaxClients { get; set; }
    public string Name { get; set; }
}
