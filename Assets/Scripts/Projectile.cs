using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    public Entity sourceEntity;

    public bool hitPlayer = false;
    public bool hitEnemies = false;
    public int pierce = 1;
    public List<Entity> hitEntities = new List<Entity>();
    public int damage = 1;

    public float speed = 10f;

    public GameObject impactEffect;
    public GameObject critEffect;

    void Start()
    {
        if(sourceEntity.GetComponent<Player>() != null)
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
        if(damage <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        Entity entity = other.GetComponentInParent<Entity>();
        Projectile proj = other.GetComponent<Projectile>();

        Debug.Log(entity);

        if(entity != null && NotHit(entity))
        {
            if((entity.GetComponent<Enemy>()!=null && hitEnemies) || (entity.GetComponent<Player>()!=null && hitPlayer))
            {
                int value = Mathf.Min(entity.health,damage);
                int multiplier = 1;

                if(other.GetComponent<Weakpoint>() != null)
                {
                    multiplier = other.GetComponent<Weakpoint>().multiplier;
                    Instantiate(critEffect,transform.position,Quaternion.identity);
                }

                entity.TakeDamage(value, multiplier);

                this.damage -= value;
                
            }
        }
        else if(proj != null)
        {
            if(proj != null && proj.hitPlayer == hitEnemies)
            {
                proj.pierce = 0;
                pierce = 0;
                proj.Impact();
                
            }
        }
        else
        {
            pierce = 0;
        }
        
        Impact();
    }

    public void Impact()
    {
        pierce--;
        Instantiate(impactEffect,transform.position,Quaternion.identity);
        if(pierce < 1) Destroy(this.gameObject);
    }

    private bool NotHit(Entity testEntity)
    {
        foreach(Entity hitEntity in hitEntities)
        {
            if(hitEntity == testEntity)
            {
                return false;
            }
        }
        return true;
    }

}
