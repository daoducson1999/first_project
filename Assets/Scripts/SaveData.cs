using System;

[Serializable]
public class SaveData
{
    public float playerX;
    public float playerY;
    public int health;
    public int score;
    public ItemSaveData[] items;
    public int coinCount;
    public string sceneName;
}