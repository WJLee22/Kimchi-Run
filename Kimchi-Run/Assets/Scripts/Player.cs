using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float jumpForce;

    // Rigidbody2D 컴포넌트를 가리키는 레퍼런스. 플레이어 Body에 물리법칙 적용 & 중력 작용 등을 위해 사용. 
    [Header("References")]
    public Rigidbody2D PlayerRigidbody;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
