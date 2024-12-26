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
    public BoxCollider2D PlayerCollider; // 플레이어의 충돌체를 나타내는 레퍼런스.
    private bool isGrounded = true;// 플레이어가 땅에 닿아있는지 여부를 나타내는 변수.
    public bool isInvincible = false; // 플레이어가 무적 상태인지 여부를 나타내는 변수.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 스페이스바 입력을 받아 점프를 처리하는 로직. 땅에 닿아있을 때만 점프 가능하도록 함.
        if (Input.anyKeyDown && isGrounded){
            // AddForceY: Rigidbody2D에 수직 방향으로 힘을 가함.
            // force 타입 = ForceMode2D.Impulse: 순간적인 힘. -> 플레이어에게 즉시 force 부여. 
            PlayerRigidbody.AddForceY(jumpForce, ForceMode2D.Impulse);
            isGrounded = false; // 점프 후 땅에 닿지 않은 상태로 변경.
            PlayerAnimator.SetInteger("state", 1); // 점프 애니메이션 실행.
        }   
    }

    public void KillPlayer()
    {
        //플레이어가 죽었을떄의 처리.
        PlayerCollider.enabled = false; // 플레이어의 충돌체를 비활성화.
        PlayerAnimator.enabled = false; // 플레이어의 애니메이션을 비활성화.
        //플레이어 사망시, 마리오 죽을때처럼 한번 튀어오르도록.
        PlayerRigidbody.AddForceY(jumpForce, ForceMode2D.Impulse);
    }

    void Hit(){
        // 목숨이 0이 아니면 목숨을 하나 줄이고, 목숨이 0이면 게임오버.
            GameManager.instance.lives -= 1;
            // if(GameManager.instance.lives == 0)
            // {
            //     KillPlayer();
            // }
    }

    void Heal(){
        // 목숨을 하나 늘림. 단, 목숨이 3보다 커지지 않도록 함.
            GameManager.instance.lives = Mathf.Min(3, GameManager.instance.lives + 1); 
    }

    void StartInvincible(){
        // 무적 상태로 변경.
        isInvincible = true;
        CancelInvoke("StopInvincible"); // StopInvincible 함수를 취소.
        // CancelInvoke를 호출하여 이전에 설정된 Invoke 호출을 취소함으로써 무적 상태 로직이 제대로 작동. 이전에는 무적 상태가 끝나기 전에 다시 StartInvincible 메서드가 호출되면 이전 Invoke 호출이 여전히 유효하여 무적 상태가 정해진 무적시간보다 빨리 끝나는 문제가 있었음.
        Invoke("StopInvincible", 5f); // 5초 후 무적 상태 해제.
    }

    void StopInvincible(){
        // 무적 상태 해제.
        isInvincible = false;
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

    //Trigger와 충돌했을 때 호출되는 함수.
    void OnTriggerEnter2D(Collider2D collider){
        // 장애물들과 충돌했을 때의 처리.
        if (collider.gameObject.tag == "enemy")
        {
            //플레이어가 적과 닿았으면 해당 오브젝트 제거. 단, 무적 상태일 때는 제거하지 않음.
            if(!isInvincible)
            {
                Destroy(collider.gameObject);
                Hit();
            }

        } 
        // 음식들과 충돌했을 때의 처리.
        else if(collider.gameObject.tag == "food")
        {
            //닿았으면 해당 오브젝트 제거
            Destroy(collider.gameObject);
            Heal();
        }
        // 금배추와 충돌했을 때의 처리.
        else if(collider.gameObject.tag == "golden")
        {
            //닿았으면 해당 오브젝트 제거
            Destroy(collider.gameObject);
            StartInvincible();
        }
    }


}
