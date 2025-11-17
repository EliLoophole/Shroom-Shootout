using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Entity sourceEntity;

    public bool hitPlayer = false;
    public bool hitEnemies = false;
    public int damage = 1;

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
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Entity entity = other.GetComponent<Entity>();
        Projectile proj = other.GetComponent<Projectile>();

        if(entity != null)
        {
            if((entity.GetComponent<Enemy>()!=null && hitEnemies) || (entity.GetComponent<Player>()!=null && hitPlayer))
            {
                int value = Mathf.Min(entity.health,damage);
                int multiplier = 1;

                if(other.GetComponent<Weakpoint>() != null)
                {
                    multiplier = other.GetComponent<Weakpoint>().multiplier;
                    Debug.Log("crit");
                }

                entity.TakeDamage(value, multiplier);

                this.damage -= value;
            }
        }
        else if(proj != null)
        {
            if(proj != null && proj.hitPlayer == hitEnemies)
            {
                damage -= proj.damage;
            }
        }
        else
        {
            damage = 0;
        }
        

        if(damage <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
