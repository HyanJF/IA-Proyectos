using UnityEngine;

public class Factory : MonoBehaviour
{
    Enemy elCochiloco = new EnemyFactory().Create(EnemyType.PDiddy);
}
    public abstract class Enemy
    {
        public int Health;
        public abstract void Attack();
    }

    public class Zombie: Enemy
    {
        public Zombie()
        {
            Health = 50;
        }

        public override void Attack()
        {
            Debug.Log("Zombie bite!");
        }
    }

    public class SpukiSkeleton : Enemy
    {
        public SpukiSkeleton()
        {
            Health = int.MaxValue;
        }

        public override void Attack()
        {
            Debug.Log("TRAE ESE CUL*!");
        }
    }

    public class PDiddy : Enemy
    {
        public PDiddy()
        {
            Health = 69;
        }

        public override void Attack()
        {
            Debug.Log("MAMAAAAAA!");
        }
    }

    public enum EnemyType
    {
        Zombie,
        SpukiSkeleton,
        PDiddy
    }

    public class EnemyFactory
    {
        public Enemy Create(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Zombie:
                    return new Zombie();
                case EnemyType.SpukiSkeleton:
                    return new SpukiSkeleton();
                case EnemyType.PDiddy:
                    return new PDiddy();
                default:
                    Debug.Log("Ese no es del barrio");
                    return null;
            }
        }
    }
