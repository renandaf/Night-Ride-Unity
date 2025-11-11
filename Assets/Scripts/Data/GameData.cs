using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameData : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";

    public static int SelectedCarId;

    public static string CoinKey = "PickedUpCoin";

    private static string PurchasedCarPrefix = "PurchasedCar_";

    public static bool IsCarPurchased(int carId)
    {
        return PlayerPrefs.HasKey(PurchasedCarPrefix + carId);
    }

    public static void PurchaseCar(int carId)
    {
        PlayerPrefs.SetInt(PurchasedCarPrefix + carId, 1);
    }

    public static int LoadCoin()
    {
        if (PlayerPrefs.HasKey(CoinKey))
        {
            return PlayerPrefs.GetInt(CoinKey);
        }

        return 5;
    }

    public static void SaveCoin(int amount)
    {
        PlayerPrefs.SetInt(CoinKey, amount);
        PlayerPrefs.Save();
    }

    public static UnlockableCarData GetCarData(int carId)
    {
        UnlockableCarData[] cars = LoadCarData();

        foreach (var car in cars)
        {
            if (car.Id == carId)
            {
                return car;
            }
        }

        return null;
    }
    public static int LoadHighScore()
    {
        if (PlayerPrefs.HasKey(HighScoreKey))
        {
            return PlayerPrefs.GetInt(HighScoreKey);
        }

        return 0;
    }

    public static void SaveHighScore(int highScore)
    {
        PlayerPrefs.SetInt(HighScoreKey, highScore);
        PlayerPrefs.Save();
    }

    public static UnlockableCarData[] LoadCarData()
    {
        return Resources.LoadAll<UnlockableCarData>("UnlockableCarData");
    }

    #if UNITY_EDITOR
    [MenuItem("Carithmetic/Clear PlayerPrefs")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    #endif
}
