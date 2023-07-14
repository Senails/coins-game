using ItemSystemTypes;

public static class ItemSystemUtils{
    public static int FindCountItemsInConteiner(ItemListConteiner conteiner,int itemId){
        int count = 0;

        foreach(ItemOnInventoryR elem in conteiner.ItemArray){
            if (elem.item==null || elem.count==0) continue;
            if (elem.item.id==itemId){
                count+=elem.count;
            }
        }
        
        return count;
    }
}