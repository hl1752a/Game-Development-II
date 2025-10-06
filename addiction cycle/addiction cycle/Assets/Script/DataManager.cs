using System;

[System.Serializable]
public class GameData
{
    public int Day = 0;
    public int Money = 0;
    public int Level = 1;
    public int Xp = 0;
    public int Energy = 0;
    public int Efficiency = 100;
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
        Data.Money += (int)Math.Round(amount * Data.Efficiency / 100d);
    }

    public static void AddXp(int amount)
    {
        Data.Xp += amount;
    }

    public static void AddDay()
    {
        Data.Day += 1;
        AddEnergy();
        AddEfficiency();

    }

    private static void AddEfficiency()
    {
        Data.Efficiency += Data.Energy;
    }

    private static void AddEnergy()
    {
        Data.Efficiency += 100;
    }
}
