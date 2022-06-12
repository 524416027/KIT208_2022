using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MazeCheckPoint : MonoBehaviour
{
    //[SerializeField] private MazeController mazeController = null;
    [SerializeField] private GameObject scoreTextPrefab = null;

    private void OnTriggerEnter(Collider other)
    {
        //print("in maze check point: " + other.gameObject.name);
        GameManager.instance.IncreaseScore((int)GameManager.instance.DifficultyLevel + 1);

        GameObject scoreTextObj = Instantiate(scoreTextPrefab, this.gameObject.transform.position, Quaternion.identity) as GameObject;
        scoreTextObj.GetComponentInChildren<TextMeshPro>().text = "+" + ((int)GameManager.instance.DifficultyLevel + 1);

        this.gameObject.SetActive(false);
    }
}
