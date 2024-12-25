using Unity.VisualScripting;
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
        // 스페이스바 입력을 받아 점프를 처리하는 로직
        if (Input.GetKeyDown(KeyCode.Space)){
            // AddForceY: Rigidbody2D에 수직 방향으로 힘을 가함.
            // force 타입 = ForceMode2D.Impulse: 순간적인 힘. -> 플레이어에게 즉시 force 부여. 
            PlayerRigidbody.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
        
        
    }
}
