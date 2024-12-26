using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")] // 최소 지연 시간 & 최대 지연 시간을 설정.
    public float minSpawnDelay; // 새로운 객체를 만들기 위해서 최소 몇 초를 기다려야 되는지.
    public float maxSpawnDelay; // 새로운 객체를 만들기 위해서 최대 몇 초를 기다려야 되는지.


    [Header("References")]
    public GameObject[] gameObjects; // 생성할 오브젝트들을 담을 배열.
  
    void Start()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay)); // 프레임 시작 시, Spawn 함수를 랜덤초 뒤에 실행.
    }

    void Spawn(){
        GameObject randomObject = gameObjects[Random.Range(0,gameObjects.Length)]; // 빌딩 오브젝트 배열에서 랜덤으로 오브젝트를 선택.
        Instantiate(randomObject, transform.position, Quaternion.identity); // 선택된 오브젝트를 인스턴스화 -> main scene에 생성.
        // Quaternion.identity : 회전값을 0으로 초기화.
        //Invoke: 일정 시간이 지난 후에 특정 함수를 실행하는 함수.
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay)); // 다음 생성까지의 시간을 랜덤으로 설정.

    }
}