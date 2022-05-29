using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lives : MonoBehaviour
{

    CollisionHandler collisionHandler;
    int lives;
    [SerializeField] TextMeshProUGUI livesText;
    private void Awake()
    {
        collisionHandler = FindObjectOfType<CollisionHandler>();
    }
    void Start()
    {
        lives = collisionHandler.GetLives();
        livesText.text = "Lives: " + lives.ToString();
    }

    void Update()
    {
        lives = collisionHandler.GetLives();
        livesText.text = "Lives: " + lives.ToString();

    }
}
