using UnityEngine;

public class Player : MonoBehaviour
{
    private Entity entity;

    void Start() { entity = GetComponent<Entity>(); }

    // Player-specific: Input, animations
}

