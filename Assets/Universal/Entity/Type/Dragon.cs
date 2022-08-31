using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Schema for Entities
public class Dragon : Entity
{
    public override void OnDamaged()
    {
        base.OnDamaged();
    }

    public override void OnUpkeepFailedToPay()
    {
        base.OnUpkeepFailedToPay();
    }
}
