### Notes
Every packet has it's own unique ID (ushort)

### Addational Variable Naming

`ClientID`: Unique ID for every user.<br>
`SpriteIndex`: Refers to the sprite's index in a list. Usually used for Sync purposes.<br>
`BuildingType`: Enum<br>
`TileType`: Enum<br>
`Multiple Entry`: The packet doesn't have a fixed length and a single packet can have multiple of these fields.<br>
However the order of these values count.<br>

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

### NewBuilingAdded<br>
Places a new building on the map<br>
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>6</td>
    <td>ushort</td>
    <td>ClientID</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>Guid</td>
    <td>UniqueID for every building</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>BuildingType</td>
    <td></td>
  </tr>
  <tr align="center">
    <td></td>
    <td>Vector3Int</td>
    <td>The new building's position</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>int</td>
    <td>Building's level</td>
  </tr>
</table>
<br>

### PlayerResourceUpdate<br>
Contains information about a player's current resources and resource income per turn<br>
The default server sends them every **0.5** seconds, also the client is not limited to process them only every 0.5 seconds nor has any cooldown<br>
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>7</td>
    <td>int</td>
    <td>List of ResourceType's count</td>
  </tr>
  <tr align="center">
    <td>Multiple Entry</td>
    <td>ResourceType</td>
    <td></td>
  </tr>
  <tr align="center">
    <td>Multiple Entry</td>
    <td>double</td>
    <td>Resource Amount The player have</td>
  </tr>
  <tr align="center">
    <td>Multiple Entry</td>
    <td>double</td>
    <td>This resource income for the player per turn</td>
  </tr>
</table>
<br>

### SyncPlayers<br>
The packet contains every participants' information and used to share it with<br>
a client.<br>
The client use this information for example to determine an another player's name<br>
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>8</td>
    <td>int</td>
    <td>Players count</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>ushort</td>
    <td>The clientID of this packet's reciever</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>string</td>
    <td>The username of this packet's reciever, the client does nothing with it</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>int</td>
    <td>The player's count excluded the reciever.<br>If there are 10 players we should set this to 9<br>It also determines how many entries will be read by the client</td>
  </tr>
  <tr align="center">
    <td>Multiple Entry</td>
    <td>ushort</td>
    <td>ClientID</td>
  </tr>
  <tr align="center">
    <td>Multiple Entry</td>
    <td>string</td>
    <td>Player's name</td>
  </tr>
</table>
<br>

### TurnChange<br>
The server sends this packet to everyone when a next player turn comes<br>
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>9</td>
    <td>ushort</td>
    <td>ClientID of the new player who turns</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>int</td>
    <td>Turn cycle count. One turn cycle means every player has its turn once</td>
  </tr>
</table>
<br>

### UpdateResourceCost<br>
Forces a player's client to calculate and show the new building cost in the Building panel<br>
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>10</td>
    <td>BuildingType</td>
    <td>The BuildingType which should be updated</td>
  </tr>
  <tr align="center">
    <td></td>
    <td>int</td>
    <td>How many times bought this building by this player?</td>
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

### MainGameLoaded<br>
Sent by the server when they loaded the main scene<br>
After this the server will start sending the ChunkInfo packets<br>
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>4</td>
    <td></td>
    <td></td>
  </tr>
</table>
<br>

### TurnEnd<br>
Sent by the client when he is done with their turn<br>
<table>
  <tr align="center">
    <th>ID</th>
    <th>Type</th>
    <th>Note</th>
  </tr>
  <tr align="center">
    <td>5</td>
    <td></td>
    <td></td>
  </tr>
</table>
<br>
