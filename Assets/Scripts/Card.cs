using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.Events;

// Updated Enums
public enum CardType { Attack, Maneuver, Trick }
public enum Keyword { Exhaust, Unplayable, QueueBack, QueueRandom, Prefire }

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card")]
public abstract class Card : ScriptableObject
{
    [Header("Display Data")]
    public string name;
    public string description;
    public Sprite image;

    [Header("Gameplay Data")]
    public CardType type;
    public Keyword[] keywords;
    public int Time = 1;


    public abstract void TriggerEffects(Entity sourceEntity);

    public void TriggerGenericEffects(Entity sourceEntity)
    {
        Debug.Log("Generic card effects played");
    }
}

[CreateAssetMenu(fileName = "Projectile Card", menuName = "Cards/ProjectileCard")]
public class ProjectileCard : Card
{
    public int projectileDamage = 1;
    public float projectileSpeed = 10f;
    public GameObject projectilePrefab;
    public GameObject particles;


    public int projectileCount = 1;

    private int projectileSeparation = 15;
    public int projectileSpread = 0;

    public override void TriggerEffects(Entity sourceEntity)
    {
        float fireAngle = sourceEntity.aimAngle - (projectileSeparation * (projectileCount - 1)) / 2;

        for(int i = 0; i < projectileCount; i++)
        {
            sourceEntity.FireProjectile(projectilePrefab, projectileDamage, projectileSpread, projectileSpeed, fireAngle, particles);
            fireAngle += projectileSeparation;
            
        }
    }   

 
    
}