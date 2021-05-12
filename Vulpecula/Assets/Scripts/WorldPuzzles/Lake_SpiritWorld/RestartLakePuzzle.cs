using UnityEngine;

public class RestartLakePuzzle : MonoBehaviour
{
    Transform player;
    Transform startPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = GameObject.FindGameObjectWithTag("Lake_Puzzle_Start").transform;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            RestartPuzzle();
        }
    }

    private void RestartPuzzle()
    {
        print("Restarting puzzle");
        player.position = startPos.position;
    }
}
