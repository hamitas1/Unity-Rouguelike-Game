using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(GameManager.instance == null)
            Instantiate(gameManager);
    }

  
}
