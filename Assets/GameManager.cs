using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject targetObject;

    private Player _player;
    private Vector3 _playerStartPosition;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;

        _player = playerObject.GetComponent<Player>();
        _playerStartPosition = _player.transform.position;
    }

    private void Start()
    {
        Invoke("MoveCubeToDestination", 2f);
    }

    private void MoveCubeToDestination()
    {
        NavMeshAgent agent = playerObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(targetObject.transform.position);
    }

    public void EnablePlayerInvincibility()
    {
        _player.SetInvincibility(true);
    }

    public void DisablePlayerInvincibility()
    {
        _player.SetInvincibility(false);
    }
    public void ResetPlayer()
    {
        _player.transform.position = _playerStartPosition;
        _player.gameObject.SetActive(true);
        Invoke("MoveCubeToDestination", 2f);
    }

    public void PauseGame(bool pause)
    {
        if (pause) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }
    public void Victory()
    {
        StartCoroutine("RestartGame");
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1f);
        _player.spawnVictoryEmmiter();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadSceneAsync(0); 
    }
    GameManager(){}
}
