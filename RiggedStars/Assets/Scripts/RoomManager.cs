using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    public Room RoomPrefab;

    public Dictionary<int, Room> Rooms;

    private void Start() {
        Rooms = new Dictionary<int, Room>();
    }

    public void CreateRoom(int roomID) {
        Room room = Instantiate<Room>(RoomPrefab, transform);
        room.JoinRoom(roomID);
        Rooms.Add(roomID, room);
    }

    public void DestroyRoom(int roomID) {
        var room = Rooms[roomID];
        Destroy(room.gameObject);
        Rooms.Remove(roomID);
        FindObjectOfType<UIManager>().ShowHUB();
    }
}
