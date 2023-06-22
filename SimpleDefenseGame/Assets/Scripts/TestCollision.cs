using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    //1) 나 혹은 상대 한테 RigidBody가 있어야 한다.(isKinematic: OFF)
    //2) 나한테 Collider가 있어야 한다(isTrigger: OFF)
    //3) 상대한테 Collider가 있어야 한다(isTrigger: OFF)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision @{collision.gameObject.name} !");
    }

    //1) 둘 다 Collider가 있어야 한다
    //2) 둘 중 하나는isTrigger : ON
    //3) 둘 중 하나는 RigidBody가 있어야 한다.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger @{other.gameObject.name}!");
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            //int mask = (1 << 8) | (1<<9);//1에서 8번 민다. 즉 8번째 비트를 킨다. 벽도 추가

            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100.0f, mask))//8번(몬스터)만 걸리는 레이캐스팅
            {
                Debug.Log($"{hit.collider.gameObject.tag}"); 
            }
        }
    }
}
