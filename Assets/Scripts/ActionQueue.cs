using UnityEngine;
using System.Collections.Generic;

public class ActionQueue : MonoBehaviour
{

    public List<Card> queuedCards = new List<Card>();
    private Entity entity;

    void Start()
    {
        entity = this.GetComponent<Entity>();
        PlayNext();
    }

    void Update()
    {
        
    }

    public int GetQueueLength()
    {
        return queuedCards.Count;
    }

    public void PlayNext()
    {
        queuedCards[queuedCards.Count - 1].TriggerEffects(entity);
    }
}
