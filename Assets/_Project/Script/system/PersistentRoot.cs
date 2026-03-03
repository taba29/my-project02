using UnityEngine;

public class PersistentRoot : MonoBehaviour
{
    static PersistentRoot I;

    void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        
    }
}