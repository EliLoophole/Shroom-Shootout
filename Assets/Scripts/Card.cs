using UnityEngine;
using System.Collections.Generic;
using System;


public enum CardType { Attack, Maneuver, Trick }
public enum Keyword { Exhaust, Unplayable, QueueBack, QueueRandom, Prefire }

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card")]
public class Card : ScriptableObject
{
    [Header("Display Data")]
    public string name;
    public string description;
    public Sprite image;

    [Header("Gameplay Data")]
    public CardType type;
    public Keyword[] keywords;

    [SerializeReference]
    public List<CardEffect> effects = new List<CardEffect>();
    public int Time = 1;


    public void TriggerEffects(Entity sourceEntity)
    {
        Debug.Log("Generic card effects played");
        foreach(CardEffect effect in effects)
        {
            effect.TriggerEffect(sourceEntity);
        }
    }
}

[System.Serializable]
public abstract class CardEffect
{
    public abstract void TriggerEffect(Entity sourceEntity);
}

[System.Serializable]
public class ProjectileEffect : CardEffect
{
    public int projectileDamage = 1;
    public float projectileSpeed = 10f;
    public GameObject projectilePrefab;
    public GameObject particles;


    public int projectileCount = 1;

    public int projectileSeparation = 8;
    public int projectileSpread = 0;

    public override void TriggerEffect(Entity sourceEntity)
    {
        float fireAngle = sourceEntity.aimAngle - (projectileSeparation * (projectileCount - 1)) / 2;

        for(int i = 0; i < projectileCount; i++)
        {
            sourceEntity.FireProjectile(projectilePrefab, projectileDamage, projectileSpread, projectileSpeed, fireAngle, particles);
            fireAngle += projectileSeparation;
            
        }
    }   

 
    
}

[System.Serializable]
public class WalkEffect : CardEffect
{
    public float paces = 1;

    public override void TriggerEffect(Entity sourceEntity)
    {
        //sourceEntity.Move(paces);
    }   

 
    
}