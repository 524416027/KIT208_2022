using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameDifficulty { easy, normal }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    [SerializeField] private InGameMenuController inGameMenuController = null;
    [SerializeField] private GameObject UIRingDisplayGroup = null;
    [SerializeField] private GameObject UIMazeDisplayGroup = null;
    [SerializeField] private GameObject UIScoreTotal = null;

    [Header("Maze")]
    [SerializeField] private MazeController mazeController = null;
    [SerializeField] private Transform mazeCollection = null;
    [SerializeField] private float mazeMoveStepDistance = 0.1f;
    [SerializeField] private int _mazeShapeIndex = 0;
    public int MazeShapeIndex
    {
        get { return _mazeShapeIndex; }
        set
        {
            _mazeShapeIndex = value;

            ChangeMazeShape();
            mazeController.ResetRound();
        }
    }

    [Header("Rings")]
    [SerializeField] private GameObject[] rings = null;
    [SerializeField] private int _ringShapeIndex = 0;
    public int RingShapeIndex
    {
        get { return _ringShapeIndex; }
        set
        {
            _ringShapeIndex = value;

            ChangeRingShape();
        }
    }

    [Header("Game Difficulty")]
    [SerializeField] private TextMeshProUGUI difficultyText = null;
    [SerializeField] private GameDifficulty _difficultyLevel = GameDifficulty.easy;
    public GameDifficulty DifficultyLevel
    {
        get { return _difficultyLevel; }
        set
        {
            _difficultyLevel = value;

            if (_difficultyLevel == GameDifficulty.easy)
            {
                //make all ring collider type to collision(not trigger) for easy mode
                ChangeAllRingColliderType(false);

                difficultyText.text = "Difficulty: Easy";
            }
            else
            {
                //make all ring collider type to trigger for normal mode
                ChangeAllRingColliderType(true);

                difficultyText.text = "Difficulty: Normal";
            }
        }
    }

    [Header("Ring Spin")]
    [SerializeField] private TextMeshProUGUI ringSpinText = null;
    [SerializeField] private int _ringSpinSpeed = 0;
    [SerializeField] private int ringSpinLevelCount = 4;
    public int RingSpinSpeed
    {
        get { return _ringSpinSpeed; }
        set
        {
            _ringSpinSpeed = value;

            //update UI text about ring spin
            switch (_ringSpinSpeed)
            {
                case 0:
                    ringSpinText.text = "Ring Spin: None";
                    break;
                case 1:
                    ringSpinText.text = "Ring Spin: Slow";
                    break;
                case 2:
                    ringSpinText.text = "Ring Spin: Normal";
                    break;
                case 3:
                    ringSpinText.text = "Ring Spin: Fast";
                    break;
            }

            ChangeAllRingSpinSpeed();
        }
    }

    [Header("Scores")]
    [SerializeField] private int _scores = 0;
    public int Scores
    {
        get { return _scores; }
        set
        {
            _scores = value;

            UIScoreTotal.GetComponent<TextMeshPro>().text = "Score: " + _scores;
        }
    }

    //================
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        DifficultyLevel = GameDifficulty.easy;
        RingSpinSpeed = 0;
        RingShapeIndex = 0;
    }

    private void Update()
    {
        //UI display mesh spinning
        UIRingDisplayGroup.transform.Rotate(Vector3.up * 20f * Time.deltaTime, Space.Self);
        UIMazeDisplayGroup.transform.Rotate(Vector3.up * 15f * Time.deltaTime, Space.Self);
    }

    //================
    public void IncreaseScore(int value)
    {
        Scores += value;

        //record check point perform count
        mazeController.CheckPointPerform();
    }

    public void ResetScore()
    {
        Scores = 0;
        
        //reset maze
        mazeController.ResetRound();

        inGameMenuController.HideMenu();
    }

    //================
    private void ChangeAllRingColliderType(bool isTrigger)
    {
        //change all ring collision type
        foreach (GameObject ring in rings)
        {
            ring.GetComponent<RingController>().ChangeColliderType(isTrigger);
        }
    }

    private void ChangeAllRingSpinSpeed()
    {
        foreach (GameObject ring in rings)
        {
            ring.GetComponent<RingController>().ChangeSpinSpeed(_ringSpinSpeed);
        }
    }

    private void ChangeRingShape()
    {
        for (int i = 0; i < rings.Length; i++)
        {
            if (i == _ringShapeIndex)
            {
                //game play ring shape
                rings[i].SetActive(true);
                //UI display ring
                UIRingDisplayGroup.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                //game play ring shape
                rings[i].SetActive(false);
                //UI display ring
                UIRingDisplayGroup.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    //================
    public void DifficultyLevelNext()
    {
        GameDifficulty newDifficulty;

        //current is last game difficulty enum index
        if ((int)_difficultyLevel + 1 >= Enum.GetNames(typeof(GameDifficulty)).Length)
        {
            newDifficulty = (GameDifficulty)0;
        }
        else
        {
            newDifficulty = (GameDifficulty)((int)_difficultyLevel + 1);
        }

        DifficultyLevel = newDifficulty;
    }

    public void DifficultyLevelPrev()
    {
        GameDifficulty newDifficulty;

        //current is last game difficulty enum index
        if ((int)_difficultyLevel - 1 < 0)
        {
            newDifficulty = (GameDifficulty)(Enum.GetNames(typeof(GameDifficulty)).Length - 1);
        }
        else
        {
            newDifficulty = (GameDifficulty)((int)_difficultyLevel - 1);
        }

        DifficultyLevel = newDifficulty;
    }

    //================
    public void RingSpinSpeedNext()
    {
        if (_ringSpinSpeed + 1 >= ringSpinLevelCount)
        {
            RingSpinSpeed = 0;
        }
        else
        {
            RingSpinSpeed += 1;
        }
    }

    public void RingSpinSpeedPrev()
    {
        if (_ringSpinSpeed - 1 < 0)
        {
            RingSpinSpeed = ringSpinLevelCount - 1;
        }
        else
        {
            RingSpinSpeed -= 1;
        }
    }

    //================
    public void RingShapeNext()
    {
        if (_ringShapeIndex + 1 >= rings.Length)
        {
            RingShapeIndex = 0;
        }
        else
        {
            RingShapeIndex += 1;
        }
    }

    public void RingShapePrev()
    {
        if (_ringShapeIndex - 1 < 0)
        {
            RingShapeIndex = rings.Length - 1;
        }
        else
        {
            RingShapeIndex -= 1;
        }
    }

    //================
    private void ChangeMazeShape()
    {
        //loop through mazeCollection but exclude none maze gameObjects
        for (int i = 0; i < mazeCollection.childCount - 2; i++)
        {
            if (_mazeShapeIndex == i)
            {
                //active maze
                mazeCollection.GetChild(i).gameObject.SetActive(true);
                //active UI maze display
                UIMazeDisplayGroup.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                //deactivate maze
                mazeCollection.GetChild(i).gameObject.SetActive(false);
                //deactivate UI maze display
                UIMazeDisplayGroup.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    //================
    public void MazeMoveUp()
    {
        Vector3 newPos = new Vector3(mazeCollection.position.x, mazeCollection.position.y + mazeMoveStepDistance, mazeCollection.position.z);
        //apply new maze collection position
        mazeCollection.position = newPos;
    }

    public void MazeMoveDown()
    {
        Vector3 newPos = new Vector3(mazeCollection.position.x, mazeCollection.position.y - mazeMoveStepDistance, mazeCollection.position.z);
        //apply new maze collection position
        mazeCollection.position = newPos;
    }

    //================
    public void MazeShapeNext()
    {
        if (_mazeShapeIndex + 1 >= mazeCollection.childCount - 2)
        {
            MazeShapeIndex = 0;
        }
        else
        {
            MazeShapeIndex += 1;
        }
    }

    public void MazeShapePrev()
    {
        if (_mazeShapeIndex - 1 < 0)
        {
            MazeShapeIndex = mazeCollection.childCount - 2 - 1;
        }
        else
        {
            MazeShapeIndex -= 1;
        }
    }
}
