using System;
using System.Collections.Generic;
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
    public int ID { get; set; }
    public int Ammount { get; set; }
}

public class EndBetStageForm : WsForm {
    public int Pot { get; set; }
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

public class StartRoundForm : WsForm {
    public Dictionary<string, PlayerInfo> Players { get; set; }
    public PlayerInfo ButtonClient { get; set; }
}

public class PlayerInfo {
    public int ID { get; set; }
    public string Name { get; set; }
    public int Stack { get; set; }
}

public class WsChatMessage {
    public string Type { get; set; }
    public string Name { get; set; }
    public string Payload { get; set; }
}

public class EndRoundMessageForm : WsForm {
    public PotWinner[] Winners { get; set; }
}

public class PotWinner {
    public PlayerInfo Winner { get; set; }
    public CardForm[] Cards { get; set; }
    public int Ammount { get; set; }
}



public class WsUserMessage {
    public ClientData Payload { get; set; }
}
