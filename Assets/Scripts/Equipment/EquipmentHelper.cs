using Invector.vItemManager;
using Invector.vMelee;
using UnityEngine;
using UnityEngine.Internal.VR;

public class EquipmentHelper : MonoBehaviour
{
    [SerializeField]
    private vItemManager _itemManager;
    [SerializeField]
    private vMeleeManager _meleeManager;

    private vItem _currentWeapon;
    private vItem _lastWeapon;

    private vItem _currentShield;
    private vItem _lastShield;

    //private void Start()
    //{

    //}

    public void AddItem(vItem item)
    {
        if (item == null) return;

        switch (item.type)
        {
            case vItemType.MeleeWeapon:
                _itemManager.AutoEquipItem(item, 0, true, false);
                break;

            case vItemType.Defense:
                SwapShields(item);
                break;

            case vItemType.Money:
                break;
        }
    }

    private void SwapWeapons(vItem newWeapon)
    {
        _currentWeapon = newWeapon;
        _meleeManager.SetRightWeapon(newWeapon.originalObject);
        Debug.Log(_meleeManager.rightWeapon?.ToString());
    }

    private void SwapShields(vItem newShield)
    {
        _currentShield = newShield;
        _meleeManager.SetLeftWeapon(newShield.originalObject);
        Debug.Log(_meleeManager.leftWeapon?.ToString());
    }



    public void DropItem(vItem item, int count)
    {
        switch (item.type)
        {
            case vItemType.MeleeWeapon:
                _lastWeapon = item;
                break;

            case vItemType.Defense:
                _lastShield = item;
                break;
        }
    }

    public void EquipItem(vEquipArea area, vItem item)
    {

    }

    public void UnequipItem(vEquipArea area, vItem item)
    {

    }
}
