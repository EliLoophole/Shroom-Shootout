using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Stats")]
    public int health = 1;  // Most die in 1-2 shots
    public float aimAngle = 0f;
    public bool isPlayer = false;

    [Header("Actions")]
    public ActionQueue actionQueue;

    public void FireProjectile(GameObject projectilePrefab, int projectileDamage, float spread, float projectileSpeed, float fireAngle)
    {
        Projectile projectile = Instantiate(projectilePrefab,transform.position,Quaternion.Euler(0f,0f,fireAngle + Random.Range(spread, -spread))).GetComponent<Projectile>();
        projectile.speed = projectileSpeed;
        projectile.sourceEntity = this;

        Debug.Log("Fire Krazy Bullets");
    }

    public virtual void TakeDamage(int dmg) 
    { 
        health -= dmg; if (health <= 0) Die();
    }
    public virtual void Die() 
    {
        Destroy(this.gameObject);
    }
}