﻿using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class pointeurController : MonoBehaviour
{
    // Radius de recherche
    public float radius = 0.25f;
    // Vitesse de déplacement du curseur
    public float moveSpeed = 20f;
    
    // Score du joueur
    private int playerScore = 0;

    // Identifiant du joueur
    private int playerId;

    GameManager gameManager;
    PlayerCollider playerColl;
    ScoreColor sc;
    Text displayedScore;

    string playerScoreName;

    Vector2 i_movement;

    void Start()
    {
        playerId = ChangeColor.getPlayerId();
        print("player" + playerId + " has joined!");

        playerScoreName = "scorePlayer" + playerId.ToString();


        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        displayedScore = GameObject.Find(playerScoreName).GetComponent<Text>();
        sc = displayedScore.GetComponentInChildren<ScoreColor>();
        sc.createScore();
        
        playerColl = this.GetComponentInChildren<PlayerCollider>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 movement = new Vector2(i_movement.x, i_movement.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
    void OnMove(InputValue value)
    {
        i_movement = value.Get<Vector2>();
    }

    public void OnFire()
    {
        // Trouve la cible la plus proche
        var go = playerColl.getTarget(radius);

        // Si une cible a été trouvé, elle est détruite et le score du joueur augmente.
        if (go != null)
        {
            IncrementScore();
            Destroy(go);
        }

    }

    public void IncrementScore()
    {
        playerScore++;
        print(playerScore);

        displayedScore.text = playerScore.ToString() + " / 20"; // + "/10" //.GetComponent<TextMesh>()

        if (playerScore >= 10)
        {
            print("player" + playerId + " winned!");
            gameManager.PlayerWin();
        }
    }

}
