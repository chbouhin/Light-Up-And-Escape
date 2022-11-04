using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsCount : MonoBehaviour
{
    [SerializeField] private List<Image> stars;
    private int addStarsCount = 0;
    private int posStarsCount = -1;

    public void AddNewStar()
    {
        stars[addStarsCount].color = Color.white;
        addStarsCount++;
    }

    public Vector2 GetPosNewStar()
    {
        posStarsCount++;
        return stars[posStarsCount].transform.position;
    }

    public int GetNbStars()
    {
        return posStarsCount + 1;
    }
}
