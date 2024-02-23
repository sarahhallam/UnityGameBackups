using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timerText;

    [SerializeField]
    private int _gamePlayTime;

    public static GameManager instance;

    private int _itemsCollected;

    void Awake()
    {
        MakeSingleton();
    }

    void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Tried to create another GameManager.");
            Destroy(gameObject);
        }
    }
    void OnEnable()
    {
        ThirdPersonController.onCollectItem += OnCollectItem;
    }

    void OnDisable()
    {
        ThirdPersonController.onCollectItem -= OnCollectItem;
    }

    void Update()
    {
        float time = (_gamePlayTime * 60) - Time.timeSinceLevelLoad;
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time - minutes * 60);

        string displayTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        _timerText.text = "Timer: " + displayTime;
    }


    private void OnCollectItem(string item_name)
    {
        Debug.Log(item_name);
        _itemsCollected++;
        Debug.Log(_itemsCollected);
    }


}
