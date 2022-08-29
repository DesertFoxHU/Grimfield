using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Entity : MonoBehaviour
{
    public EntityDefinition Definition { get; private set; }
    public EntityType Type
    {
        get => Definition.Type;
    }
    public bool IsInitialized { get; private set; } = false;
    public Vector3Int Position { get; set; }
    public ushort OwnerId { get; private set; }
    public double health;
    public double damage;
    public double speed;

    private List<Vector3Int> lastDrawn = new List<Vector3Int>();
    private HashSet<Vector3Int> lastCanMove = new HashSet<Vector3Int>();

    private MaterialInstancing secondaryTexture;

    public void Awake()
    {
        secondaryTexture = GetComponent<MaterialInstancing>();
    }

    public void Initialize(Vector3Int Position, EntityDefinition definition)
    {
        Definition = definition;
        this.Position = Position;
        health = definition.Health[0];
        damage = definition.Damage[0];
        speed = definition.Speed[0];
        IsInitialized = true;
    }

    public void SetOwner(ushort clientID)
    {
        this.OwnerId = clientID;
    }

    public void SetColor(Color color)
    {
        secondaryTexture.input = color;
        secondaryTexture.ChangePropertyBlock();
    }

    public void CalculateMovementRange()
    {
        WeightGraph graph = new WeightGraph(this);
        lastCanMove = graph.GetMovementRange(this);
    }

    public void DrawNavigation()
    {
        Tilemap map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();
        CalculateMovementRange();

        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 botLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        topRight.Set(topRight.x + 3f, topRight.y + 3f, topRight.z);
        botLeft.Set(botLeft.x - 3f, botLeft.y - 3f, botLeft.z);

        Vector3Int topRightInt = map.ToVector3Int(topRight);
        Vector3Int botLeftInt = map.ToVector3Int(botLeft);

        Color outside = new Color(87 / 255f, 87 / 255f, 87 / 255f, 235 / 255f);

        for(int x = botLeftInt.x; x < topRightInt.x; x++)
        {
            for (int y = botLeftInt.y; y < topRightInt.y; y++)
            {
                Vector3Int v3 = new Vector3Int(x, y, botLeftInt.z);
                if (lastCanMove.Contains(v3)) continue;

                map.SetColor(v3, outside);
                lastDrawn.Add(v3);
            }
        }
        map.RefreshAllTiles();
    }

    public void ClearDraw()
    {
        Tilemap map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();
        foreach (Vector3Int v3 in lastDrawn)
        {
            map.SetColor(v3, Color.white);
        }
        lastDrawn.Clear();
    }

    public void ClientMoveToRequest(Vector3Int to)
    {
        CalculateMovementRange();
        if (!lastCanMove.Contains(to))
        {
            FindObjectOfType<MessageDisplayer>().SetMessage($"This unit can't move there.");
            return;
        }

        Message message = Message.Create(MessageSendMode.reliable, ClientToServerPacket.MoveEntity);
        message.Add(Position);
        message.Add(to);
        NetworkManager.Instance.Client.Send(message);
    }
}
