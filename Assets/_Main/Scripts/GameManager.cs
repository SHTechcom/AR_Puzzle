using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int gameIndex;
    public GameObject[] games;
    public GameObject game;
    public PlaceItem placeItem;

    public MainView mainView;
    public JumpScareView jumpScareView;

    public AudioSource music;
    public AudioSource completedLevel;

    private void Start()
    {
        placeItem.gameObject.SetActive(true);
    }

    public void Init(Vector3 position)
    {
        music.PlayScheduled(0);
        for (int i = 0; i < games.Length; i++)
        {
            games[i].SetActive(false);
        }
        game.transform.position = position;
        game.gameObject.SetActive(true);
        Tuong.Instance.Init();
        gameIndex = -1;
        mainView.Show();
        NextGame();
    }

    public void Lose()
    {
        jumpScareView.Show();
        Debug.Log("Lose");
        OnQuit();
    }

    public void NextGame()
    {
        Debug.Log("NextGame");
        StartCoroutine(AnimNextGame());
    }

    private IEnumerator AnimNextGame()
    {
        completedLevel.PlayScheduled(0);
        yield return new WaitForSeconds(1f);
        if (gameIndex > -1)
        {
            games[gameIndex].SetActive(false);
        }
        gameIndex++;
        if (gameIndex >= games.Length)
        {
            Completed();
        }
        games[gameIndex].SetActive(true);
    }

    public void Completed()
    {
        Debug.Log("Completed");
        OnQuit();
    }

    public void OnQuit()
    {
        Debug.Log("OnQuit");
        music.Stop();
        game.SetActive(false);
        placeItem.isPlaced = false;
        mainView.Hide();
    }
}
