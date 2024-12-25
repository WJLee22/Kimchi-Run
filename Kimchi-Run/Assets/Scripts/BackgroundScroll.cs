using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How fast should the texture scroll?")]
    public float scrollSpeed;


    [Header("References")]
    public MeshRenderer meshRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    // 매 프레임마다 호출되는 함수. 
    void Update()
    {
        // Update는 프레임마다 호출되므로, 이로인해 프레임에 의존하여 텍스쳐의 이동속도를 제어하면, 프레임이 높아질수록 텍스쳐의 이동속도도 높아지게 되는 등 문제가 발생할 수 있다. 프레임에 의존하지않으려면 Time.deltaTime을 사용하면 된다.

        // Time.deltaTime: 이전 프레임에서 지금 프레임까지의 초 단위 간격. (즉, 이전 프레임으로부터 현재 프레임까지 몇 초 걸렸는지를 나타냄)

        // 이를 통해 텍스쳐(하늘,빌딩,땅...)의 offset이, scrollSpeed 속도로 1초에 한 번만 이동하게 된다.
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);

    }
}
