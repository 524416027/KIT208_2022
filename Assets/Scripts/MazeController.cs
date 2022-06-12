using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
    //[SerializeField] private Transform startPoint = null;
    [SerializeField] private List<GameObject> checkPoints = null;
    [SerializeField] private int checkPointPassCount = 0;

    [SerializeField] private GameObject successParticlePrefab = null;
    [SerializeField] private GameObject failParticalPrefab = null;

    [SerializeField] private GameObject audioPlayerPrefab = null;
    [SerializeField] private AudioClip[] audioClips = null;

    private void Start()
    {
        RecordeCheckPoints();
    }

    private void OnTriggerEnter(Collider other)
    {
        //ring collide with the maze
        if (other.gameObject.CompareTag("RingCollision"))
        {
            if (other != null)
            {
                //create fail particle effect
                Instantiate(failParticalPrefab, other.gameObject.transform.position, Quaternion.identity);

                //create audio player and play fail audio
                GameObject audioPlayer = Instantiate(audioPlayerPrefab, other.gameObject.transform.position, Quaternion.identity) as GameObject;
                AudioSource audioSource = audioPlayer.GetComponentInChildren<AudioSource>();
                //apply fail sound and play
                audioSource.clip = audioClips[0];
                audioSource.Play();

                //reset ring
                other.gameObject.transform.root.GetComponentInChildren<RingController>().ResetRound();
                //reset maze
                ResetRound();
                //reset score
                GameManager.instance.Scores = 0;
            }
        }
    }

    private void RecordeCheckPoints()
    {
        checkPoints.Clear();

        foreach (Transform checkPoint in this.gameObject.transform.GetChild(GameManager.instance.MazeShapeIndex).GetChild(0))
        {
            checkPoints.Add(checkPoint.gameObject);
        }
    }

    public void ResetRound()
    {
        //enable back all check point
        foreach (GameObject checkPoint in checkPoints)
        {
            checkPoint.SetActive(true);
        }
        //re count checkpoint pass
        checkPointPassCount = 0;
        RecordeCheckPoints();
    }

    public void CheckPointPerform()
    {
        checkPointPassCount++;
        CheckEnd();
    }

    private void CheckEnd()
    {
        if (checkPointPassCount >= checkPoints.Count)
        {
            //round end here
            GameManager.instance.Scores += ((int)GameManager.instance.DifficultyLevel + 1) * 10;

            //create success particle effect
            Instantiate(successParticlePrefab, this.gameObject.transform.position, Quaternion.identity);

            //create fail particle effect
            Instantiate(failParticalPrefab, this.gameObject.transform.position, Quaternion.identity);
            //create audio player and play fail audio
            GameObject audioPlayer = Instantiate(audioPlayerPrefab, this.gameObject.transform.position, Quaternion.identity) as GameObject;
            AudioSource audioSource = audioPlayer.GetComponentInChildren<AudioSource>();
            //apply fail sound and play
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
    }
}
