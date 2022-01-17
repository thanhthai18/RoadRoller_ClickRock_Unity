using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocks_RoadRollerMinigame1 : MonoBehaviour
{
    public int healRock = 10;
    public float speedRock;
    public bool isVaCham = false;
    public Text txtHPRock;


    private void Start()
    {
        healRock = Random.Range(3, 11);
        txtHPRock = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        txtHPRock.text = "x" + healRock.ToString();
        speedRock = GameController_RoadRollerMinigame1.instance.speedGame;
    }

    void Handle_OnChangeSpeed(float speedGame)
    {
        speedRock = speedGame;
    }

    private void FixedUpdate()
    {
        if (!isVaCham)
        {
            transform.Translate(Vector3.left * speedRock * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isVaCham = true;
            GameController_RoadRollerMinigame1.instance.speedGame = 0;
            collision.transform.DOMoveX(collision.transform.position.x - 20, 10).SetEase(Ease.Linear);
            transform.DOMoveX(transform.position.x - 20, 10).SetEase(Ease.Linear);
            GameController_RoadRollerMinigame1.instance.isPause = true;
        }
        if (collision.gameObject.CompareTag("MainCamera"))
        {
            GameController_RoadRollerMinigame1.instance.Lose();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isVaCham = false;
            GameController_RoadRollerMinigame1.instance.speedGame = 4;
            collision.transform.DOKill();
            collision.transform.DOMoveX(GameController_RoadRollerMinigame1.instance.roadRollerObj.startPos.x, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                GameController_RoadRollerMinigame1.instance.isPause = false;
            });
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
