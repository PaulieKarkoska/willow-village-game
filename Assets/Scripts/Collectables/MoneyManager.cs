using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int totalMoney;

    public int AddMoney(int money)
    {
        totalMoney += money;
        return totalMoney;
    }

    public int TakeMoney(int money)
    {
        totalMoney -= money;
        return totalMoney;
    }

    public bool HasEnough(int cost)
    {
        return cost <= totalMoney;
    }

}