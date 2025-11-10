using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CarSelectionPanel : MonoBehaviour
{
    private UnlockableCarData[] _cars;
    public GameObject CarPanel;
    private int _selectionIdx;
    public TMP_Text CarNameText;
    private GameObject _spawnedCar;
    public TMP_Text CoinText;
    public Button BuyButton;
    public Button SelectButton;
    public TMP_Text BuyButtonText;


    void Start()
    {
        _cars = GameData.LoadCarData();
        DrawSelection(0);
    }
    public void DrawSelection(int idx)
    {
        if (_spawnedCar != null)
        {
            Destroy(_spawnedCar);
        }

        _spawnedCar = Instantiate(_cars[idx].Model, CarPanel.transform);
        _spawnedCar.transform.eulerAngles = new Vector3(0, 240, 0);
        _spawnedCar.transform.localScale = new Vector3(3, 3, 3);
        CarNameText.text = _cars[idx].Name;

        SelectButton.interactable = GameData.IsCarPurchased(_cars[idx].Id);
        BuyButton.interactable = !GameData.IsCarPurchased(_cars[idx].Id) && GameData.LoadCoin() >= _cars[idx].Price;
        BuyButtonText.text = GameData.IsCarPurchased(_cars[idx].Id) ? "Bought" : $"Buy ({_cars[idx].Price})";
    }

    private void OnEnable()
    {
        CoinText.text = "COIN: " + GameData.LoadCoin();
    }

    public void OnPurchaseButtonClicked()
    {
        int coin = GameData.LoadCoin();
        if (coin >= _cars[_selectionIdx].Price)
        {
            coin -= _cars[_selectionIdx].Price;
            GameData.PurchaseCar(_cars[_selectionIdx].Id);
            GameData.SaveCoin(coin);

            DrawSelection(_selectionIdx);
            OnEnable();
        }
    }

    public void NextSelection()
    {
        _selectionIdx++;
        if (_selectionIdx >= _cars.Length)
        {
            _selectionIdx = 0;
        }
        DrawSelection(_selectionIdx);
    }

    public void PrevSelection()
    {
        _selectionIdx--;
        if (_selectionIdx < 0)
        {
            _selectionIdx = _cars.Length - 1;
        }
        DrawSelection(_selectionIdx);
    }

    public void OnSelectButtonClicked()
    {
        GameData.SelectedCarId = _cars[_selectionIdx].Id;
        SceneManager.LoadScene(1);
    }
}
