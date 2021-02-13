using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    Transform _boss;
    [SerializeField]
    GameObject bullet = null;

    bool isDone;

    // Start is called before the first frame update
    void Start()
    {
        _boss = this.transform;
        isDone = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDone)
        {
            isDone = false;
            int val = Random.Range(0, 2);
            if (val == 0)
            {
                StartCoroutine(Ptt1());
            }
            else
            {
                StartCoroutine(Ptt2());
            }
        }
    }

    IEnumerator Ptt1()
    {
        Instantiate(bullet, this.transform.position + new Vector3(-0.028f, 0.2f), Quaternion.Euler(0, 0, 90));   // shoot up
        Instantiate(bullet, this.transform.position + new Vector3(-0.028f, -0.425f), Quaternion.Euler(0, 0, 270));   // shoot down

        yield return new WaitForSeconds(0.3f);

        Instantiate(bullet, this.transform.position + new Vector3(-0.028f, 0.2f), Quaternion.Euler(0, 0, 90));   // shoot up
        Instantiate(bullet, this.transform.position + new Vector3(-0.028f, -0.425f), Quaternion.Euler(0, 0, 270));   // shoot down

        yield return new WaitForSeconds(1f);
        isDone = true;
    }
    IEnumerator Ptt2()
    {
        Instantiate(bullet, this.transform.position + new Vector3(0.3f, -0.095f), Quaternion.Euler(0, 0, 0));   // shoot right
        Instantiate(bullet, this.transform.position + new Vector3(-0.3f, -0.095f), Quaternion.Euler(0, 0, 180));   // shoot left

        yield return new WaitForSeconds(0.3f);

        Instantiate(bullet, this.transform.position + new Vector3(0.3f, -0.095f), Quaternion.Euler(0, 0, 0));   // shoot right
        Instantiate(bullet, this.transform.position + new Vector3(-0.3f, -0.095f), Quaternion.Euler(0, 0, 180));   // shoot left

        yield return new WaitForSeconds(1f);
        isDone = true;
    }
}
