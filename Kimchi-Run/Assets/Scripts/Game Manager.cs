using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Playing,
    Dead
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance; //GameManager의 인스턴스를 저장할 변수.(싱글톤)
    public GameState state = GameState.Intro; 
    public float PlayStartTime; // 게임 시작 시간을 나타내는 변수.
    public int lives = 3; // 플레이어의 목숨을 나타내는 변수.

    [Header("References")]

    public GameObject IntroUI; // 게임 시작 화면 UI 레퍼런스.
    public GameObject DeadUI; // 플레이어 사망시 화면 UI 레퍼런스.
    public GameObject EnemySpawner; // 적 스포너 레퍼런스.
    public GameObject FoodSpawner; // 음식 스포너 레퍼런스.
    public GameObject GoldenSpawner; // 금배추 스포너 레퍼런스.
    public Player playerScript; // 플레이어 스크립트 레퍼런스.
    public TMP_Text scoreText; // 점수를 나타내는 텍스트 레퍼런스.

    //Unity에 의해 자동으로 호출되는 함수로, start 함수보다 먼저 호출됨. 
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        IntroUI.SetActive(true); // 프레임 시작시 -> 게임 시작 화면 UI 활성화.
    }

    float CalculateScore() // 게임 시작 후 경과된 시간(RunningTime)을 반환하는 함수.
    {
        return Time.time - PlayStartTime; 
    }

    void SaveHighScore() // 현재까지의 최고 점수를 저장하는 함수.
    {
        int score = Mathf.FloorToInt(CalculateScore()); // 경과된 시간을 정수로 변환.
        //PlayerPrefs: 유니티에서 제공하는 데이터 저장소. (사용자의 컴퓨터 디스크에 데이터를 저장해주는 역할을 하는 클래스)
        int currentHighScore = PlayerPrefs.GetInt("highScore"); // 현재까지의 최고 점수를 디스크에서 가져옴.
        if(score > currentHighScore) // 현재 점수가 현재까지의 최고 점수보다 높으면
        {
            PlayerPrefs.SetInt("highScore", score); // 최고 점수를 현재까지의 점수로 변경.
            PlayerPrefs.Save(); // 변경된 최고 점수를 디스크에 저장.
        }
    }

    int getHighScore() // 현재까지의 최고 점수를 반환하는 함수.
    {
        return PlayerPrefs.GetInt("highScore"); // 현재까지의 최고 점수를 반환.
    }

    public float CalculateGameSpeed() // 게임 속도를 계산하는 함수.
    {
        if(state != GameState.Playing) // 게임 상태가 Playing이 아니면
        {
            return 5f; // 게임 시작전, 배경화면에서의 게임 속도.
        }
        float speed = 8f + (1f * Mathf.Floor(CalculateScore() / 10f)); // 초기 게임 속도를 8 + (경과된 시간 / 10)으로 계산. => 10초마다 속도 0.5씩 증가. 즉, 시간이 흘러감에따라 자연스럽게 난이도 증가!!

        return Mathf.Min(speed, 70f); // 게임의 최대 속도가 30을 넘지 않도록 함.
        
    }

    // Update is called once per frame
    void Update()
    {
        if(state == GameState.Playing) // 게임 상태가 Playing이면
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(CalculateScore()); // 점수 텍스트에 경과된 시간을 표시.
        }  else if(state == GameState.Dead)   {
            scoreText.text = "High Score: " + getHighScore(); // 점수 텍스트에 최고 점수를 표시.
            //SaveHighScore(); // 최고 점수를 저장. 이곳에서 호출시, 게임오버되어도 스코어가 계속 증가되는 문제발생. 
            // Dead 상태에서도 CalculateScore()는 계속 Time.time - PlayStartTime을 계산함.
            // 따라서 게임오버 상태에서도 Time.time이 계속 증가하므로 CalculateScore()의 결과값도 계속 커지게 됨.
            // + Update() 함수에서 매 프레임마다 이 로직이 실행되기 때문에 Dead 상태에서도 계속 점수가 증가하는 문제가 발생하는 것임.
        }
        // 게임 상태가 Intro이고, 스페이스바(모든키로 수정함)를 눌렀다면 (게임 상태: Intro -> Playing)
        if(state == GameState.Intro && Input.anyKeyDown)  
        {
            state = GameState.Playing; // 게임 시작 화면에서 스페이스바를 누르면 게임 시작.
            IntroUI.SetActive(false); // 게임 시작 화면 UI 비활성화.
            EnemySpawner.SetActive(true); // 적 스포너 활성화.
            FoodSpawner.SetActive(true); // 음식 스포너 활성화.
            GoldenSpawner.SetActive(true); // 금배추 스포너 활성화.
            PlayStartTime = Time.time; // 게임 시작 시간을 저장.
        }
        if(state == GameState.Playing && lives == 0) // 게임 상태가 Playing이고, 목숨이 0이면 (게임 상태: Playing -> Dead)
        {
            playerScript.KillPlayer(); // 플레이어 죽음 처리.
            EnemySpawner.SetActive(false); // 적 스포너 비활성화.
            FoodSpawner.SetActive(false); // 음식 스포너 비활성화.
            GoldenSpawner.SetActive(false); // 금배추 스포너 비활성화.
            
            DeadUI.SetActive(true); // 플레이어 사망시 화면 UI 활성화.
            SaveHighScore(); // 최고 점수 저장.
            state = GameState.Dead; // 게임 상태를 Dead로 변경.
        }
        if(state == GameState.Dead && Input.anyKeyDown)
        {
            SceneManager.LoadScene("main"); // 죽고나서 키 입력시 main 씬 다시 로드.(즉, 게임 재시작)
        }
    }
}
