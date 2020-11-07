using Invector.vMelee;
using UnityEngine;

public class EnemyNpc : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private vMeleeManager meleeManager;

    private void Start()
    {
        var wc = WaveController.instance;
        if (wc != null && wc.Waves != null && wc.Waves.TryGetValue(wc.currentWave, out WaveInfo wave))
        {
            if (!wave.enemiesHaveWeapon)
            {
                meleeManager.rightWeapon = null;
                Destroy(weapon);
            }
            else
                weapon.SetActive(wave.enemiesHaveWeapon);

            if (!wave.enemiesHaveShield)
            {
                meleeManager.leftWeapon = null;
                Destroy(shield);
            }
            else
                shield.SetActive(wave.enemiesHaveWeapon);
        }
        else
        {
            Destroy(weapon);
            Destroy(shield);
        }
    }
}