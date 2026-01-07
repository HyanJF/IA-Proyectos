using UnityEngine;

public class Animal : MonoBehaviour
{
    public class AnimalBase
    {
        public string Name;
        public int Age = 1;
        public float Speed = 1;
        public bool Vegan = true;
        public bool Domesticated = true;

        public class Builder
        {
            private AnimalBase animal;

            public Builder(string name)
            {
                animal = new AnimalBase();
                animal.Name = name;
            }

            public Builder WithAge(int damage)
            {
                animal.Age = damage;
                return this;
            }

            public Builder BaseSpeed(float damage)
            {
                animal.Speed = damage;
                return this;
            }

            public Builder IsVegan()
            {
                animal.Vegan = false;
                return this;
            }

            public Builder IsDomesticated()
            {
                animal.Domesticated = false;
                return this;
            }

            public AnimalBase Build()
            {
                return animal;
            }
        }
    }

    public AnimalBase Dog = new AnimalBase.Builder("Firulais").Build();

    public AnimalBase Cat = new AnimalBase.Builder("Michi")
        .WithAge(10)
        .BaseSpeed(10)
        .IsVegan()
        .IsDomesticated()
        .Build();

    public enum ItemType
    {
        Dog,
        Cat,
        Shark,
        Rat,
        ColoredDonkey
    }

    public class ItemFactory
    {
        public AnimalBase Create(ItemType type)
        {
            switch (type)
            {
                case ItemType.Dog:
                    return new AnimalBase.Builder("Mike")
                        .WithAge(10)
                        .Build();
                case ItemType.Cat:
                    return new AnimalBase.Builder("Nagatoro")
                        .WithAge(5)
                        .BaseSpeed(10f)
                        .Build();
                case ItemType.Shark:
                    return new AnimalBase.Builder("Ellen Joe")
                        .WithAge(20)
                        .BaseSpeed(10f)
                        .IsDomesticated()
                        .Build();
                case ItemType.Rat:
                    return new AnimalBase.Builder("Jane Joe")
                        .WithAge(20)
                        .BaseSpeed(10f)
                        .IsVegan()
                        .IsDomesticated()
                        .Build();
                case ItemType.ColoredDonkey:
                    return new AnimalBase.Builder("Celestia")
                        .WithAge(1020)
                        .Build();
                default:
                    throw new System.Exception("Shit breken yo!");
            }
        }
    }
}
