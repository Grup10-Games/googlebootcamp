using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportLevelFriendCount : MonoBehaviour
{
    private float hp;
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }    
    private void GameManagerOnOnGameStateChanged(GameState obj)
    {
        if (obj == GameState.FightScreen)
        {
            GameManager.Instance.increaseLevelFriendlyUnitCount();
        }
    }

    private void Update()
    {
        hp = gameObject.GetComponent<CharacterController>()._health;

        if (hp<0)
        {
            GameManager.Instance.decreaseLevelFriendlyUnitCount();

            if (GameManager.Instance.getLevelEnemyCount() == 0 ||
                GameManager.Instance.getLevelFriendlyUnitCount() == 0) 
            {
                if (GameManager.Instance.getLevelFriendlyUnitCount() == 0)
                {
                    GameManager.Instance.changeCurrentStete(GameState.GameOverScreen);
                }

                if (GameManager.Instance.getLevelEnemyCount() == 0)
                {
                    GameManager.Instance.changeCurrentStete(GameState.GameWonScreen);
                }
            }
        }
    }
}