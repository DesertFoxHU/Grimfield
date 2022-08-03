### Notes
Every packet has it's own unique ID (ushort)

### Addational Variable Naming

`ClientID`: Unique ID for every user.
`SpriteIndex`: Refers to the sprite's index in a list. Usually used for Sync purposes.
`BuildingType`: Enum
`TileType`: Enum

## Serverside - Server to Client

### SendAlert<br>
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>1</td>
    <td>string</td>
    <td>The message you want to display for the client</td>
  </tr>
</table>
<br>

### LoadLobby<br>
Forces the client to load a lobby scene.<br>
The lobby will be totally empty, which can be updated via UpdateLobby packet<br>
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>2</td>
    <td>-</td>
    <td>-</td>
  </tr>
</table>
<br>

### UpdateLobby<br>
Updates the lobby information<br>
If the client is not in the lobby the packet will simply be disposed<br>
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>3</td>
    <td>int</td>
    <td>Players count and also the list size below</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>int</td>
    <td>The server's max capacity</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>String list (Custom format): "clientID|playerName|bool"</td>
    <td>The value of bool determines if the player is ready</td>
  </tr>
</table>
<br>

### LoadGameScene<br>
Forces the client to load the Main Game Scene<br>
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>4</td>
    <td>-</td>
    <td>-</td>
  </tr>
</table>
<br>

### ChunkInfo<br>
Updates the chunk and the corresponding tiles<br>
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>5</td>
    <td>int</td>
    <td>Players count and also the list size below</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>int</td>
    <td>ChunkX the chunk's X coordinate</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>int</td>
    <td>ChunkY the chunk's Y coordinate</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>int</td>
    <td>The size of the tiles list in this packet</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>String list (Custom Format): "x|y|TileType|SpriteIndex"</td>
    <td>TileType is an enum and x and y is a real position (not chunk pos)</td>
  </tr>
</table>
<br>

## Clientside - Client to Server

### JoinLobby<br>
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>1</td>
    <td>string</td>
    <td>The user's name</td>
  </tr>
</table>
<br>

### ChangeReadyStatus<br>
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>2</td>
    <td>bool</td>
    <td>Is player ready?</td>
  </tr>
</table>
<br>

### RequestBuild<br>
As the name suggest, this request the server to build a building<br>
The server will check other things, like: is the building placeable there, etc.<br>
But the client checks if placeable there too before sending it
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>3</td>
    <td>Vector3</td>
    <td>The building's position</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>BuildingType</td>
    <td>Enum</td>
  </tr>
</table>
<br>
