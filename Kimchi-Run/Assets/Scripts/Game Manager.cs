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
    public int lives = 3; // 플레이어의 목숨을 나타내는 변수.

    [Header("References")]

    public GameObject IntroUI; // 게임 시작 화면 UI 레퍼런스.
    public GameObject EnemySpawner; // 적 스포너 레퍼런스.
    public GameObject FoodSpawner; // 음식 스포너 레퍼런스.
    public GameObject GoldenSpawner; // 금배추 스포너 레퍼런스.
    public Player playerScript; // 플레이어 스크립트 레퍼런스.

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

    // Update is called once per frame
    void Update()
    {
        // 게임 상태가 Intro이고, 스페이스바(모든키로 수정함)를 눌렀다면 (게임 상태: Intro -> Playing)
        if(state == GameState.Intro && Input.anyKeyDown)  
        {
            state = GameState.Playing; // 게임 시작 화면에서 스페이스바를 누르면 게임 시작.
            IntroUI.SetActive(false); // 게임 시작 화면 UI 비활성화.
            EnemySpawner.SetActive(true); // 적 스포너 활성화.
            FoodSpawner.SetActive(true); // 음식 스포너 활성화.
            GoldenSpawner.SetActive(true); // 금배추 스포너 활성화.
        }
        if(state == GameState.Playing && lives == 0) // 게임 상태가 Playing이고, 목숨이 0이면 (게임 상태: Playing -> Dead)
        {
            playerScript.KillPlayer(); // 플레이어 죽음 처리.
            EnemySpawner.SetActive(false); // 적 스포너 비활성화.
            FoodSpawner.SetActive(false); // 음식 스포너 비활성화.
            GoldenSpawner.SetActive(false); // 금배추 스포너 비활성화.

            state = GameState.Dead; // 게임 상태를 Dead로 변경.
        }
        if(state == GameState.Dead && Input.anyKeyDown)
        {
            SceneManager.LoadScene("main"); // 죽고나서 키 입력시 main 씬 다시 로드.(즉, 게임 재시작)
            
        }
    }
}
