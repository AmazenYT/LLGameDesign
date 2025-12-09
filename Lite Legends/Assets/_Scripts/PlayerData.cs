using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int health;
    public float speed;
    public string name;


    // Start 
    void Start()
    {
        health = 100;
        speed = 20;
        name = "player";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
