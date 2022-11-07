using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCount : MonoBehaviour
{
    [SerializeField] private List<Image> coins;
    private int addCoinsCount = 0;
    private int posCoinsCount = -1;

    public void AddNewCoin()
    {
        coins[addCoinsCount].color = Color.white;
        addCoinsCount++;
    }

    public Vector2 GetPosNewCoin()
    {
        posCoinsCount++;
        return coins[posCoinsCount].transform.position;
    }

    public int GetNbCoins()
    {
        return posCoinsCount + 1;
    }
}
