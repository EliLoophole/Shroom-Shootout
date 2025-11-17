using UnityEngine;
using System.Collections.Generic;

public class ActionQueue : MonoBehaviour
{

    public List<Card> queuedCards = new List<Card>();
    private Entity entity;
    public bool testAction = false;

    void Start()
    {
        entity = this.GetComponent<Entity>();
        PlayNext();
    }

    void Update()
    {
        if(testAction)
        {
            PlayNext();
            testAction = false;
        }
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
