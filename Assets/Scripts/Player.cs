using UnityEngine;

public class Player : MonoBehaviour
{
    public int Level = 10;
    public string gender = "Male";
    void Start()
    {
        Debug.Log("Player Started!");
        Debug.Log("Level: " + Level);
        Debug.Log("Gender: " + gender);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}