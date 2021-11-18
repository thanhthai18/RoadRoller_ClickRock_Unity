using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBackGround_RoadRollerMinigame1 : MonoBehaviour
{
    private Vector3 startPos;
    public float speedBG;

    private void Start()
    {
        startPos = transform.position;
        speedBG = GameController_RoadRollerMinigame1.instance.speedGame;
    }

    void Handle_OnChangeSpeed(float speedGame)
    {
        speedBG = speedGame;
    }

    private void Update()
    {
        if (GameController_RoadRollerMinigame1.instance.isBegin && !GameController_RoadRollerMinigame1.instance.isLose && !GameController_RoadRollerMinigame1.instance.isWin)
        {
            transform.Translate(Vector3.left * speedBG * Time.deltaTime);
            if (transform.position.x < -35.9f)
            {
                transform.position = startPos;
            }
        }
    }

    private void OnEnable()
    {
        GameController_RoadRollerMinigame1.Event_OnChangeSpeed += Handle_OnChangeSpeed;
    }
    private void OnDisable()
    {
        GameController_RoadRollerMinigame1.Event_OnChangeSpeed -= Handle_OnChangeSpeed;
    }
}
