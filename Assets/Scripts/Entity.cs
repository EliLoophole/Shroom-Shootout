using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Stats")]
    public int health = 1;  // Most die in 1-2 shots
    public float aimAngle = 0f;
    public bool isPlayer = false;
    public Transform barrel;

    [Header("Actions")]
    public ActionQueue actionQueue;

    public void FireProjectile(GameObject projectilePrefab, int projectileDamage, float spread, float projectileSpeed, float fireAngle, GameObject particlePrefab)
    {
        Projectile projectile = Instantiate(projectilePrefab,barrel.position,Quaternion.Euler(0f,0f,fireAngle + Random.Range(spread, -spread))).GetComponent<Projectile>();
        GameObject particles = Instantiate(particlePrefab,barrel.position,Quaternion.Euler(0f,0f,fireAngle));
        projectile.speed = projectileSpeed;
        projectile.sourceEntity = this;
        projectile.damage = projectileDamage;

        Debug.Log("Fire Krazy Bullets");
    }

    public virtual void TakeDamage(int dmg, int critical) 
    { 
        dmg *= critical;
        health -= dmg; if (health <= 0) Die();
    }
    public virtual void Die() 
    {
        Destroy(this.gameObject);
    }
}