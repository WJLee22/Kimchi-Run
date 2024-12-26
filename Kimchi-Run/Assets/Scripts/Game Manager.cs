using Unity.VisualScripting;
using UnityEngine;

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

    [Header("References")]

    public GameObject IntroUI; // 게임 시작 화면 UI 레퍼런스.
    public GameObject EnemySpawner; // 적 스포너 레퍼런스.
    public GameObject FoodSpawner; // 음식 스포너 레퍼런스.
    public GameObject GoldenSpawner; // 금배추 스포너 레퍼런스.
    

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
        // 게임 상태가 Intro이고, 스페이스바를 눌렀다면
        if(state == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            state = GameState.Playing; // 게임 시작 화면에서 스페이스바를 누르면 게임 시작.
            IntroUI.SetActive(false); // 게임 시작 화면 UI 비활성화.
            EnemySpawner.SetActive(true); // 적 스포너 활성화.
            FoodSpawner.SetActive(true); // 음식 스포너 활성화.
            GoldenSpawner.SetActive(true); // 금배추 스포너 활성화.
        }
    }
}
