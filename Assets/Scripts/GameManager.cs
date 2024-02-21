using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
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


    private void OnCollectItem(string item_name)
    {
        Debug.Log(item_name);
        _itemsCollected++;
        Debug.Log(_itemsCollected);
    }


}
