using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsAnimationManager : MonoBehaviour
{
    [SerializeField] private List<CoinAnimation> coins;
    private List<Transform> destinations = new List<Transform>();
    private int nbCoinsGet = 0;
    private int count = 0;
    private float timeBetweenCoinsAnim = 0.5f;
    private float timer = 0.5f;

    private void Update()
    {
        if (nbCoinsGet == count)
            return;
        timer += Time.deltaTime;
        if (timer > timeBetweenCoinsAnim) {
            timer -= timeBetweenCoinsAnim;
            coins[count].gameObject.SetActive(true);
            count++;
        }
    }

    public void StartAnimation(Transform coins, int nbCoinsGet)
    {
        this.nbCoinsGet = nbCoinsGet;
        for (int i = 0; i < nbCoinsGet; i++) {
            destinations.Add(coins.GetChild(i));
            this.coins[i].SetDestination(destinations[i]);
        }
    }
}
