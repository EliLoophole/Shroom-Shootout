using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Entity sourceEntity;

    public bool hitPlayer = false;
    public bool hitEnemies = false;

    public float speed = 10f;

    void Start()
    {
        if(sourceEntity.isPlayer)
        {
            hitEnemies = true;
        }
        else
        {
            hitPlayer = true;
        }
    }

    void Update()
    {
        
    }
}
