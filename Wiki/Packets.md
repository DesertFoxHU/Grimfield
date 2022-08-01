### Notes
Every packet has it's own unique ID (ushort)

## Serverside - Server to Client

### SendAlert<br>
| ID |    Type   | Note |
|----|-----------|-------|
| 1  |  String   |  The message you want to display for the client |
<br>

### LoadLobby<br>
Forces the client to load a lobby scene.<br>
The lobby will be totally empty, which can be updated via UpdateLobby packet<br>
| ID |    Type   | Note |
|----|-----------|-------|
| 2  |  -   | - |
<br>

### UpdateLobby<br>
Updates the lobby information<br>
If the client is not in the lobby the packet will simply be disposed<br>
| ID |    Type   | Note |
|----|-----------|-------|
| 3  |  int      | Player's count and also the list size below |
||  int      | The server's max capacity |
|| List of string (Custom Format): "clientID\|playername\|bool" | The value of bool is if the player is ready |
<br>

## Clientside - Client to Server

### JoinLobby<br>
| ID |    Type   | Note |
|----|-----------|-------|
| 1  |  String   | The user's name |
<br>

### ChangeReadyStatus<br>
| ID |    Type   | Note |
|----|-----------|-------|
| 2  |  bool   | Is player ready? |
<br>
