using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if(numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
    
}
