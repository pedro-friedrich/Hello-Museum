using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Vector3 dir;
    private GameObject parentObj;

    private void Start()
    {
        parentObj = GameObject.Find("Cannon Base");
        dir = parentObj.transform.TransformDirection(Vector3.up);

        StartCoroutine(Wait());
    }
    void Update()
    {
        transform.position += dir * 10f * Time.deltaTime;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
