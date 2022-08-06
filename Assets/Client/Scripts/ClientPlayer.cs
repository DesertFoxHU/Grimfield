using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ClientPlayer
{
    public int ClientID { get; private set; }
    public string Name { get; private set; }

    public ClientPlayer(int clientID, string name)
    {
        ClientID = clientID;
        Name = name;
    }
}
