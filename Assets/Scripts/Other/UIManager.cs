using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Missing UIManager");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] private TextMeshProUGUI[] _itemNames;
    [SerializeField] private TextMeshProUGUI[] _itemPrices;
    [SerializeField] private Button[] _itemButton;
    [SerializeField] private TextMeshProUGUI _purse;
    private int? _selection = null;

    public void PopulateShop(List<ShopItem> items)
    {
        for (int i = 0; i < 3; i++)
        {
            _itemNames[i].text = items[i].name;
            if (!items[i].onSale)
            {
                _itemNames[i].color = Color.grey;
                _itemButton[i].interactable = false;
            }
            _itemPrices[i].text = items[i].price + "D";
        }
    }

    public void PopulatePlayerPurse(int amount)
    {
        _purse.text = amount + "D";
    }

    public void SelectItem(int i)
    {
        foreach (var b in _itemButton)
        {
            b.GetComponent<Image>().enabled = false;
        }

        if (i >= 0 && i < _itemButton.Length)
        {
            _itemButton[i].GetComponent<Image>().enabled = true;
            _selection = i;
        }
    }

    public void BuyItem(ShopKeeper sk)
    {
        if (_selection != null)
            sk.BuyItem(_selection.Value);  
    }
}