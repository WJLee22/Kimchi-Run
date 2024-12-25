using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float jumpForce;

    // Rigidbody2D 컴포넌트를 가리키는 레퍼런스. 플레이어 Body에 물리법칙 적용 & 중력 작용 등을 위해 사용. 
    [Header("References")]
    public Rigidbody2D PlayerRigidbody;
    public Animator PlayerAnimator; // 플레이어 애니메이션을 제어하기 위한 레퍼런스.
    private bool isGrounded = true;// 플레이어가 땅에 닿아있는지 여부를 나타내는 변수.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 스페이스바 입력을 받아 점프를 처리하는 로직. 땅에 닿아있을 때만 점프 가능하도록 함.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded){
            // AddForceY: Rigidbody2D에 수직 방향으로 힘을 가함.
            // force 타입 = ForceMode2D.Impulse: 순간적인 힘. -> 플레이어에게 즉시 force 부여. 
            PlayerRigidbody.AddForceY(jumpForce, ForceMode2D.Impulse);
            isGrounded = false; // 점프 후 땅에 닿지 않은 상태로 변경.
            PlayerAnimator.SetInteger("state", 1); // 점프 애니메이션 실행.
        }   
    }

    // OnCollisionEnter2D: Collider간에 충돌이 발생했을 때 호출되는 함수.(ex.플레이어 Collider와 Platform Collider 충돌시)
    void OnCollisionEnter2D(Collision2D collision){
        // 땅에 닿았을 때, isGrounded를 true로 변경.
        if (collision.gameObject.name == "Platform")
        { 
            if(!isGrounded)
            {
                PlayerAnimator.SetInteger("state", 2); // 착지 애니메이션 실행. 
            }
            isGrounded = true;
        }
    }
}
