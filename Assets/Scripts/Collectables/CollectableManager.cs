using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private UIManager uiManager;

    [Header("Audio")]
    [SerializeField]
    private AudioClip[] moneyAudioClips;
    [SerializeField]
    private AudioClip[] seedAudioClips;

    private int _totalSeeds = 0;
    public int totalSeeds
    {
        get { return _totalSeeds; }
        set
        {
            _totalSeeds = value;
            PlaySound("seed");
            uiManager.UpdateSeedCountText(value);
        }
    }

    private int _totalMoney = 0;
    public int totalMoney
    {
        get { return _totalMoney; }
        private set
        {
            _totalMoney = value;
            PlaySound("money");
            uiManager.UpdateGoldCountText(value);
        }
    }

    private void Start()
    {
        if (!uiManager)
            uiManager = GameObject.FindGameObjectWithTag("GameUI").GetComponent<UIManager>();

        if (!uiManager)
            Debug.Log("There is no UiManager for the player to use");
    }

    public int AddSeeds(int seedsToAdd)
    {
        totalSeeds += seedsToAdd;
        return totalSeeds;
    }
    public int RemoveSeeds(int seedsToRemove)
    {
        totalSeeds -= seedsToRemove;
        return totalSeeds;
    }
    public bool HasEnoughSeeds(int cost)
    {
        return cost <= totalSeeds;
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

    public bool HasEnoughMoney(int cost)
    {
        return cost <= totalMoney;
    }

    #region Audio
    private void PlaySound(string type)
    {
        AudioClip[] clips;
        switch (type)
        {
            case "money":
                clips = moneyAudioClips;
                break;
            case "seed":
                clips = seedAudioClips;
                break;
            default:
                return;
        }

        if (clips.Length > 0)
            AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Length - 1)], transform.position);
    }
    #endregion

}