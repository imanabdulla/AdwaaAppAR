using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDraggingBehavior : MonoBehaviour
{
    public Sprite sprite;
    protected static short correctAnswers = 0;
    protected static short maxCorrectAnswers = 3;

    private void Update()
    {
        EndGame();
    }
    private void EndGame()
    {
        if (correctAnswers == maxCorrectAnswers)
        {
            this.GetComponent<SpriteRenderer>().sprite = this.sprite;
            this.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }
}
