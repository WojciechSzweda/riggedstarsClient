using UnityEngine;

[System.Serializable]
public class RoomForm {
    public int ID { get; set; }
    public int ClientsCount { get; set; }
    public int MaxClients { get; set; }
    public string Name { get; set; }
}

public class UserForm {
    public string Name;
    public string Password;
}

public class CreateRoomForm {
    public string Name;
    public int MaxClients;
}

public class UserResponseForm {
    public ClientData Data;
    public string Token;
    public int Status;
}

public class ClientData {
    public int ID { get; set; }
    public string CreatedAt { get; set; }
    public string Name { get; set; }
    public long Stack { get; set; }
}

public class CardForm {
    public int Value { get; set; }
    public string Suit { get; set; }
}

public class WsCardsForm : WsForm {
    public CardForm[] Payload;
}

public class BetForm : WsForm{
    public int Payload { get; set; }
}

public class WsForm {
    public string Type { get; set; }
}

public class ActivePlayerForm : WsForm{
    public string Name { get; set; }
    public int ID { get; set; }
    public int MinBet { get; set; }
}

public class TableCardsForm : WsForm {
    public CardForm[] Payload { get; set; }
}

