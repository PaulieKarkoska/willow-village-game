using Invector.vItemManager;
using Invector.vMelee;
using UnityEngine;

public class EquipmentHelper : MonoBehaviour
{
    [SerializeField]
    private vItemManager _itemManager;
    [SerializeField]
    private vMeleeManager _meleeManager;

    private vItem _currentWeapon;
    private vItem _lastWeapon;
    private EquipPoint _weaponEquipPoint;

    private vItem _currentShield;
    private vItem _lastShield;
    private EquipPoint _shieldEquipPoint;

    private void Start()
    {
        _shieldEquipPoint = _itemManager.equipPoints[0];
        _weaponEquipPoint = _itemManager.equipPoints[1];
    }

    //public void AddItem(vItem item)
    //{
    //    if (item == null) return;

    //    switch (item.type)
    //    {
    //        case vItemType.MeleeWeapon:
    //            //_itemManager.AutoEquipItem(item, 0, true, true);
    //            //DropOldWeapon(item);
    //            break;

    //        case vItemType.Defense:
    //            //DropOldShield(item);
    //            //_itemManager.AutoEquipItem(item, 2, true, true);
    //            break;

    //        case vItemType.Money:
    //            break;
    //    }
    //}

    //private void DropOldWeapon(vItem newWeapon)
    //{
    //    _lastWeapon = _currentWeapon;
    //    _currentWeapon = newWeapon;
    //    //_meleeManager.SetRightWeapon(newWeapon.originalObject);
    //    if (_lastWeapon)
    //        _itemManager.DestroyCurrentEquipedItem(0);

    //    Debug.Log(_meleeManager.rightWeapon?.ToString());
    //}

    //private void DropOldShield(vItem newShield)
    //{
    //    _lastShield = _currentShield;
    //    _currentShield = newShield;
    //    //_meleeManager.SetLeftWeapon(newShield.originalObject);
    //    if (_lastShield)
    //        _itemManager.DestroyCurrentEquipedItem(1);

    //    Debug.Log(_meleeManager.leftWeapon?.ToString());
    //}


    public void DropItemOnUnequip(vEquipArea area, vItem item)
    {
        Transform handle;
        switch (item.type)
        {
            case vItemType.MeleeWeapon:
                handle = _weaponEquipPoint.handler.defaultHandler;
                //_itemManager.items.Remove(item);
                Instantiate(item.dropObject, handle.position + (handle.forward * 0.15f), handle.rotation);
                break;

            case vItemType.Defense:
                handle = _shieldEquipPoint.handler.defaultHandler;
                //_itemManager.items.Remove(item);
                Instantiate(item.dropObject, handle.position + (handle.forward * 0.15f), handle.rotation);
                break;
        }
    }

    public void DestroyItemOnFinishUnequip(vEquipArea area, vItem item)
    {
        switch (item.type)
        {
            case vItemType.MeleeWeapon:
            case vItemType.Defense:
                _itemManager.DestroyItem(item);
                break;
        }
    }

    //public void EquipItem(vEquipArea area, vItem item)
    //{

    //}

    //public void UnequipItem(vEquipArea area, vItem item)
    //{

    //}
}
