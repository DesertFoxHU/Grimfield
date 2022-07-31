### Notes
Every packet has it's own unique ID (ushort)

## Serverside - Server to Client

**SendAlert**<br>
| ID |    Type   | Note |
|----|-----------|-------|
| 1  |  String   |  The message you want to display for the client |
<br>

**LoadLobby**<br>
Forces the client to load a lobby scene<br>

| ID |    Type   | Note |
|----|-----------|-------|
| 2  |  -   | - |
<br>

## Clientside - Client to Server

**JoinLobby**<br>
| ID |    Type   | Note |
|----|-----------|-------|
| 1  |  String   | The user's name |
<br>
