using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ServerToClientPacket : ushort
{
    SendAlert = 1,
    LoadLobby,
    UpdateLobby,
    LoadGameScene,
    ChunkInfo,
    NewBuildingAdded,
    PlayerResourceUpdate
}

public enum ClientToServerPacket : ushort
{
    JoinLobby = 1,
    ChangeReadyStatus,
    RequestBuild,
    MainGameLoaded,
    NextTurn,
}
