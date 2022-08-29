### This file contains explanation about how some things works
This file serves as documentation.

### Registry and Definitions
- Both the client and server have the same Registry (Scene). The registry contains important static informations (like `Definitions`)
- Definitions are describe a specific `entity or building properties`, so the server doesn't need to send them via packets.
- There are 3 different definitions currently: `TileDefinition`, `BuildingDefinition`, `EntityDefinition`

### Buildings
1. Every building starts from `level 1`, in the BuildingDefinition class you shouldn't use buildings' level as an index.<br>
You need to find the level's value by `List<>.Find();`.
2. Buildings are a bit complex in a way of working. Most of the logic is in the serverside.<br>
   There is an abstract class named `AbstractBuilding`, which makes the logic and they only stored in the server (in ServerPlayer class).
   They represent a physical building on the map. However, there are some cases where server sends a whole AbstractBuilding class, but the<br>
   never stores them.
3. Buildings are rendered above the map, they don't connect to the map.

### Entities
1. Every entity starts from `level 0`, so their level can used as an index for lists. (Ref: Buildings 1.)
2. Entity GameObject is same in server and client too, both keep track of the entities.

### Clients
1. ClientIDs start from 0 and incremented by every joined player. So second who joined will get an `ClientID = 1`
2. ClientID often called `OwnerId` or `PlayerId` in the scripts depending on the class itself.

### Z-Index
- You should pay attention to every object which are not directly connected to the map to have their Z value lesser than the map.<br>
  **Positive Z value means further away the camera, the map is 0 so if you want something to render above the map set the value below 0**
