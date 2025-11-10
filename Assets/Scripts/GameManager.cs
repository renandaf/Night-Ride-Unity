using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using EZCameraShake;
using OccaSoftware.GaussianBlur.Runtime;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public RoadSpeed RoadController;
    [HideInInspector] public bool IsSudden;
    [HideInInspector] public bool IsDouble;
    [HideInInspector] public bool IsHalf;
    public LifeUI LifeUI;
    public GameObject GameOverPanel;
    public GameObject DeathParticle;
    public AudioManager AudioManager;
    public Player Player;
    public Volume Volume;
    public int maxHealth;
    [HideInInspector] public int PrevHealth;
    [HideInInspector] public int Health;
    private int Score;
    public TMP_Text ScoreText;
    public TMP_Text FinalScoreText;
    [HideInInspector]
    public bool IsGameOver;

    public GameObject hs2;
    public int PickedUpCoin;
    public TMP_Text CoinText;

    void Start()
    {
        var carData = GameData.GetCarData(GameData.SelectedCarId);
        maxHealth = carData.Health;
        Health = maxHealth;
        SetGameOver(false);
        LifeUI.SpawnLives(maxHealth);
        AddCoin(GameData.LoadCoin());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddCoin(int amount)
    {
        PickedUpCoin += amount;
        CoinText.text = "Coin : " + PickedUpCoin;
    }

    public void AddScore(int value)
    {
        if(IsDouble)
        {
            value = value * 2;
        }
        if (IsHalf)
        {
            value = value / 2;
        }
        Score += value;
        ScoreText.text = "Score : " + Score;
    }

    public void SetGameOver(bool isGameOver)
    {
        StartCoroutine(GameOver(isGameOver));
        IsGameOver = isGameOver;
        if (IsGameOver)
        {
            Destroy(Player.gameObject);
            AudioManager.Play("Explosion");
            AudioManager.Stop("Police");
            AudioManager.Stop("Car");
            GameObject particle = Instantiate(DeathParticle, Player.transform.position, Quaternion.identity);
            CameraShaker.Instance.ShakeOnce(12f, 6f, 0.1f, 2f);
            Destroy(particle, 3f);
            int hs = GameData.LoadHighScore();
            bool isNewHS = Score > hs;
            hs2.SetActive(isNewHS);
            if (isNewHS)
            {
                GameData.SaveHighScore(Score);
            }
        }
    }

    IEnumerator GameOver(bool isGameOver)
    {
        yield return new WaitForSeconds(2.5f);
        if (Volume.profile.TryGet(out Vignette v))
        {
            v.intensity.value = 0f;
        }
        CoinText.gameObject.SetActive(!isGameOver);
        ScoreText.gameObject.SetActive(!isGameOver);
        FinalScoreText.text = "Score : " + Score.ToString();
        GameOverPanel.SetActive(isGameOver);
        GameData.SaveCoin(PickedUpCoin);
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DecreaseHealth()
    {
        if (Volume.profile.TryGet(out Vignette v) && Volume.profile.TryGet(out GaussianBlur blur))
        {
            if(maxHealth == 5)
            {
                v.intensity.value += 0.1f;
            }
            else
            {
                v.intensity.value += 0.15f;
            }
           
            blur.radius.value = 12;
            StartCoroutine(ChangeBlur(blur.radius.value, 0, 1, blur));
            Health--;
            LifeUI.UpdateLives(Health);
            if (Health == 0)
            {
                v.center.value = new Vector2(0.5f, 3f);
                SetGameOver(true);
            }
        }
    }

    public void SetHealth(int value)
    {
        Health = value;
        LifeUI.UpdateLives(Health);
    }

    IEnumerator ChangeBlur(int v_start, int v_end, int duration, GaussianBlur blur)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            blur.radius.value = (int)Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        blur.radius.value = v_end;
    }

    public void IncreaseHealth()
    {
        if (Health != maxHealth && !IsSudden)
        {
            if (Volume.profile.TryGet(out Vignette v) && Volume.profile.TryGet(out GaussianBlur blur))
            {
                v.intensity.value -= 0.15f;
            }
            Health++;
            LifeUI.UpdateLives(Health);
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
