using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalCubes = 0;
    private int collectedCubes = 0;

    void Awake()
    {
        Instance = this;
    }

    public void CollectCube()
    {
        collectedCubes++;

        Debug.Log("Collected: " + collectedCubes);

        if (collectedCubes >= totalCubes)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("YOU WIN!");
        Time.timeScale = 0;
    }
}