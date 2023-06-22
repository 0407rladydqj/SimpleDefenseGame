using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    //1) �� Ȥ�� ��� ���� RigidBody�� �־�� �Ѵ�.(isKinematic: OFF)
    //2) ������ Collider�� �־�� �Ѵ�(isTrigger: OFF)
    //3) ������� Collider�� �־�� �Ѵ�(isTrigger: OFF)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision @{collision.gameObject.name} !");
    }

    //1) �� �� Collider�� �־�� �Ѵ�
    //2) �� �� �ϳ���isTrigger : ON
    //3) �� �� �ϳ��� RigidBody�� �־�� �Ѵ�.
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

            //int mask = (1 << 8) | (1<<9);//1���� 8�� �δ�. �� 8��° ��Ʈ�� Ų��. ���� �߰�

            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100.0f, mask))//8��(����)�� �ɸ��� ����ĳ����
            {
                Debug.Log($"{hit.collider.gameObject.tag}"); 
            }
        }
    }
}
