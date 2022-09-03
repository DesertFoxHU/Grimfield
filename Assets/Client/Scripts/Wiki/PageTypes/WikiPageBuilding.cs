using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class WikiPageBuilding : WikiPage
{
    public BuildingDefinition definition;

    public TextMeshProUGUI description;
    public ResourceCostDisplayer firstCost;
    public ResourceCostDisplayer costPerBuild;
    public ResourceCostDisplayer produces;
    public ResourceCostDisplayer storage;
    public TextMeshProUGUI territoryClaim;
    public ResourceCostDisplayer exchangeFrom;
    public ResourceCostDisplayer exchangeTo;

    public override void Initialize()
    {
        base.Initialize();
        description.text = definition.description;
        firstCost.Render(definition.GetBuildingCost(0));
        costPerBuild.Render(definition.IncreasePerBuy.ToDictionary(x => x.type, y => y.Value));

        if (definition.isProducer)
        {
            Dictionary<ResourceType, double> produce = new Dictionary<ResourceType, double>();
            produce.Add(definition.produceType, definition.ProduceLevel.Find(x => x.level == 1).value);
            produces.Render(produce);
        }

        if (definition.hasProductStorage)
        {
            storage.Render(definition.StorageCapacity.ToDictionary(x => x.type, y => y.Value));
        }

        if (definition.canClaimTerritory)
        {
            territoryClaim.text = $"Territory claim range: {definition.territoryClaimRange}";
        }

        if (definition.isExchanger)
        {
            exchangeFrom.Render(definition.ExchangeFrom.ToDictionary(x => x.type, y => y.amount));
            exchangeTo.Render(definition.ExchangeTo.ToDictionary(x => x.type, y => y.amount));
        }
    }
}
