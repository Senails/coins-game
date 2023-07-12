using System;

using ItemSystemTypes;
using static AsyncLib;
using static ItemSystemUtils;


public class PotionGenerator 
{   
    private ChestR _chest;
    private Action<Action> _trotlingAttack = CreateTrotlingFunc(5000);

    
    public PotionGenerator(ChestR chest)
    {
        _chest= chest;
        _chest.OnOpenChest = OnOpenChest;
    }

    void OnOpenChest()
    {
        ItemOnInventoryR Item = new ItemOnInventoryR{
            count=1,
            item = ItemDB.Self.itemListDB[2]
        };

        if (_chest.HowManyCanAddItem(Item)<1) return;

        int count = FindCountItemsInConteiner(_chest,2);
        if (count>2) return;

        _trotlingAttack.Invoke(()=>{
            _chest.AddItem(Item);
        });
    }
}
