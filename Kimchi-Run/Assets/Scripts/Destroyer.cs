using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 오브젝트(건물,배추 등등)의 x좌표가 -15보다 작아지면, 해당 오브젝트를 파괴한다.
        if(transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}
