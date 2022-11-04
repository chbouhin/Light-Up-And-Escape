using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsAnimationManager : MonoBehaviour
{
    [SerializeField] private List<StarAnimation> stars;
    private List<Transform> destinations = new List<Transform>();
    private int nbStarsGet = 0;
    private int count = 0;
    private float timeBetweenStarsAnim = 0.5f;
    private float timer = 0.5f;

    private void Update()
    {
        if (nbStarsGet == count)
            return;
        timer += Time.deltaTime;
        if (timer > timeBetweenStarsAnim) {
            timer -= timeBetweenStarsAnim;
            stars[count].gameObject.SetActive(true);
            count++;
        }
    }

    public void StartAnimation(Transform stars, int nbStarsGet)
    {
        this.nbStarsGet = nbStarsGet;
        for (int i = 0; i < nbStarsGet; i++) {
            destinations.Add(stars.GetChild(i));
            this.stars[i].SetDestination(destinations[i]);
        }
    }
}
