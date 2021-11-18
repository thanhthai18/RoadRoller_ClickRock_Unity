using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_RoadRollerMinigame1 : MonoBehaviour
{
    public static GameController_RoadRollerMinigame1 instance;

    public Camera mainCamera;
    public RoadRoller_RoadRollerMinigame1 roadRollerObj;
    public bool isWin, isLose, isBegin, isPause;
    public RaycastHit2D[] hit;
    public Vector2 mousePos;
    public Rocks_RoadRollerMinigame1 rockPrefab;
    public Rocks_RoadRollerMinigame1 rockObj;
    public Slider sliderProgress;
    public MyBackGround_RoadRollerMinigame1 backGround;
    public bool isLockStageSpeed;
    public Transform posSpawnRock;
    public float speedGame;
    public static event Action<float> Event_OnChangeSpeed;
    public float posXLose;
    public GameObject tutorial;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);

        isWin = false;
        isLose = false;
        isBegin = true;
        isPause = false;
        speedGame = 4;
        isLockStageSpeed = false;
    }

    private void Start()
    {
        tutorial.SetActive(false);
        SetSizeCamera();
        SpawnRock();
        Invoke(nameof(ShowTutorial), 2);
        posXLose = mainCamera.orthographicSize * -2 + 1.5f;

    }

    void SetSizeCamera()
    {
        float f1, f2;
        f1 = 16.0f / 9;
        f2 = Screen.width * 1.0f / Screen.height;
        mainCamera.orthographicSize *= f1 / f2;
    }

    void ShowTutorial()
    {
        tutorial.transform.position = rockObj.transform.position;
        tutorial.SetActive(true);
        speedGame = 0;
        rockObj.speedRock = 0;
        Event_OnChangeSpeed?.Invoke(speedGame);
        isPause = true;
    }

    public void SpawnRock()
    {
        rockObj = Instantiate(rockPrefab, posSpawnRock.transform.position, Quaternion.identity);
    }

    void Win()
    {
        isWin = true;
        if (rockObj != null)
        {
            Destroy(rockObj.gameObject);
        }
        Debug.Log("Win");
    }

    public void Lose()
    {
        isLose = true;
        if (rockObj != null)
        {
            Destroy(rockObj.gameObject);
        }
        if (roadRollerObj != null)
        {
            Destroy(roadRollerObj.gameObject);
        }
        Debug.Log("Thua");
    }

    void ClickRock()
    {
        rockObj.healRock = (rockObj.healRock > 0) ? --rockObj.healRock : 0;
        rockObj.txtHPRock.text = "x" + rockObj.healRock.ToString();
        if (rockObj.healRock == 0)
        {
            Destroy(rockObj.gameObject);
            SpawnRock();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isWin && !isLose)
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.RaycastAll(mousePos, Vector2.zero);
            if (hit.Length != 0)
            {
                if (hit[0].collider.gameObject.CompareTag("Box"))
                {
                    ClickRock();
                    if (tutorial != null)
                    {
                        if (tutorial.activeSelf)
                        {
                            tutorial.SetActive(false);
                            Destroy(tutorial.gameObject);
                            speedGame = 4;
                            Event_OnChangeSpeed?.Invoke(speedGame);
                            isPause = false;
                        }
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isPause && !isWin && !isLose && isBegin)
        {
            if (sliderProgress.value < 1)
            {
                sliderProgress.value += Time.deltaTime * speedGame / 200;
                if (sliderProgress.value >= 0.5f && sliderProgress.value < 0.6 && !isLockStageSpeed)
                {
                    isLockStageSpeed = true;
                    speedGame *= 1.5f;
                    Event_OnChangeSpeed?.Invoke(speedGame);
                }
                else if (sliderProgress.value >= 2.0f / 3 && isLockStageSpeed)
                {
                    isLockStageSpeed = false;
                    speedGame *= 4.0f / 3;
                    Event_OnChangeSpeed?.Invoke(speedGame);
                }
            }
            else
            {
                sliderProgress.value = 1;
                Win();
                roadRollerObj.transform.DOMoveX(roadRollerObj.transform.position.x + 30, 3).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Destroy(roadRollerObj.gameObject);
                });
            }
        }
    }


}
