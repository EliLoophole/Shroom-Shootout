using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Entity entity;
    [Header("AI")]
    public Card[] aiDeck;  // Predefined "cards" (SOs) for patterns

    void StartDuelPlanning()
    {
        entity = GetComponent<Entity>();
        QueueAIActions();  // AI logic
    }

    void QueueAIActions()
    {
        // Simple AI: Random/priority from aiDeck
    }
}