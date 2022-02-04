using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private GameObject _shopkeeperUI;
    [SerializeField] private List<ShopItem> _items;
    private PlayerInventory _inventory;
    public List<ShopItem> Items { get => _items; }

    private void Awake()
    {
        UIManager.Instance.PopulateShop(_items);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _shopkeeperUI.SetActive(true);

            if (collision.TryGetComponent<PlayerInventory>(out _inventory))
                UIManager.Instance.PopulatePlayerPurse(_inventory.Diamonds);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _shopkeeperUI.SetActive(false);
            _inventory = null;
        }

    }

    public void BuyItem(int i)
    {
        if (_inventory != null && _items[i].onSale && _items[i].price <= _inventory.Diamonds)
        {
            _inventory.Inventory.Add(new Item() { name = _items[i].name });
            _items[i].onSale = false;
            _inventory.AddDiamonds(-_items[i].price);
            UIManager.Instance.PopulateShop(_items);
            UIManager.Instance.PopulatePlayerPurse(_inventory.Diamonds);
            UIManager.Instance.SelectItem(-1);
        }
    }
}
