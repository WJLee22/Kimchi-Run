using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite onHeart;
    public Sprite offHeart;
    public SpriteRenderer spriteRenderer; // 스프라이트를 화면에 그려주는 역할을하는 렌더러.
    public int liveNumber;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.lives >= liveNumber)
        {
            spriteRenderer.sprite = onHeart;
        } 
        else
        {
            spriteRenderer.sprite = offHeart;
        }
        
    }
}
