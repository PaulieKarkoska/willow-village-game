using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private UIManager uiManager;

    [Header("Audio")]
    [SerializeField]
    private AudioClip[] moneyAudioClips;

    private int _totalMoney = 0;
    public int totalMoney
    {
        get
        {
            return _totalMoney;
        }
        private set
        {
            _totalMoney = value;
            PlaySound();
            uiManager.UpdateGoldCountText(value);
        }
    }

    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("GameUI").GetComponent<UIManager>();
    }

    public int AddMoney(int moneyToAdd)
    {
        totalMoney += moneyToAdd;
        return totalMoney;
    }

    public int RemoveMoney(int moneyToRemove)
    {
        totalMoney -= moneyToRemove;
        return totalMoney;
    }

    public bool HasEnough(int cost)
    {
        return cost <= totalMoney;
    }

    #region Audio
    private void PlaySound()
    {
        if (moneyAudioClips.Length > 0)
            AudioSource.PlayClipAtPoint( moneyAudioClips[UnityEngine.Random.Range(0, moneyAudioClips.Length - 1)], transform.position);
    }
    #endregion

}