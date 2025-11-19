using UnityEngine;
using System.Collections;

public class Cleanup : MonoBehaviour
{

    public float lifetime = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(cleanup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator cleanup()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
