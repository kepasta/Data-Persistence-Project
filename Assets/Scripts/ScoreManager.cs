using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI nameSpace;
    public TextMeshProUGUI bestScore;
    public new string name;
    public string champion;
    public int score;

    [System.Serializable]
    class Player
    {
        public string name;
        public int score;
    }

    private void Awake()
    {
        if (ScoreManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();
        UpdateScore();
    }

    public void StartGame()
    {
        name = nameSpace.text;
        SceneManager.LoadScene(1);
    }

    public void SaveScore()
    {
        Player player = new();
        player.name = champion;
        player.score = score;

        string json = JsonUtility.ToJson(player);

        File.WriteAllText(Application.persistentDataPath + "/bestscore.json", json);
    }
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/bestscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Player player = JsonUtility.FromJson<Player>(json);

            champion = player.name;
            score = player.score;
        }
    }

    public void UpdateScore()
    {
        bestScore.text = $"Best score: {champion} {score}";
    }
}
