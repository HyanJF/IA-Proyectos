using UnityEngine;

public class Builder : MonoBehaviour
{
    public class Item
    {
        public string Name;
        public int Damage = 5;
        public bool IsMagical = false;
        public bool IsRare = false;

        public class Builder
        {
            private Item item;

            public Builder(string name)
            {
                item = new Item();
                item.Name = name;
            }

            public Builder WithDamage(int damage)
            {
                item.Damage = damage;
                return this;
            }

            public Builder MakeMagical()
            {
                item.IsMagical = true;
                return this;
            }

            public Builder MakeRare()
            {
                item.IsRare = true;
                return this;
            }

            public Item Build()
            {
                return item;
            }
        }
    }

    //Basic Item
    //public Item sword = new Item.Builder("La Sword").Build();

    //Asignar Valores
    public Item sword = new Item.Builder("La Sword")
        .WithDamage(20)
        .MakeRare()
        .Build();

    public enum ItemType
    {
        Sword,
        Staff,
        Axe
    }

    public class ItemFactory
    {
        public Item Create(ItemType type)
        {
            switch ( type)
            {
                case ItemType.Sword:
                    return new Item.Builder("Sword")
                        .WithDamage(20)
                        .Build();
                case ItemType.Staff:
                    return new Item.Builder("Staff")
                        .WithDamage(30)
                        .MakeMagical()
                        .Build();
                case ItemType.Axe:
                    return new Item.Builder("Axe")
                        .WithDamage(50)
                        .MakeRare()
                        .Build();
                default:
                    throw new System.Exception("Shit breken yo!");
            }
        }
    }
}
