using System;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Day = 0;
    public int Money = 0;
    public int Level = 1;
    public int Xp = 0;
    public int Energy = 0;
}

public static class DataManager
{
    public static GameData Data { get; private set; }

    static DataManager()
    {
        Data = new GameData();
    }

    public static void AddMoney(int amount)
    {
        Data.Money += (int)Math.Round(amount * Data.Energy / 100d);
    }
    public static void SpendMoney(int amount)
    {
        Data.Money -= amount;
    }
    public static void AddXp(int amount)
    {
        amount += Data.Xp;
        while (amount > 20 + 10 * Data.Level)
        {
            Data.Level += 1;
            amount -= 20 + 10 * Data.Level;
        }
        Data.Xp = amount;
    }

    public static void AddDay()
    {
        Data.Day += 1;
        Data.Energy += 100;

    }

    public static void Work()
    {
        AddMoney(10);
        Data.Energy -= 20;   
    }

    public static void EffectEnergy(int amount)
    {
        Data.Energy += (int)Math.Round(amount * Math.Exp(-0.1535 * Data.Level));
    }
}
