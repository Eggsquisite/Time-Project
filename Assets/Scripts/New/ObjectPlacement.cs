using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    [SerializeField] GameObject selectObj = null;
    [SerializeField] GameObject objPosition = null;
    private GameObject tempObj;
    private Color tmp;
    private bool ready = false;
    private bool placed = false;

    // Update is called once per frame
    void Update()
    {
        if (ready)
            tempObj.transform.position = objPosition.transform.position;

        if (selectObj != null)
        {
            objPosition.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

            if (Input.GetButtonDown("Fire2") && !ready && !placed)
            {
                tempObj = Instantiate(selectObj, objPosition.transform.position, Quaternion.identity);
                tempObj.GetComponent<ParticleSystem>().Stop();
                tempObj.GetComponent<CapsuleCollider2D>().enabled = false;
                tmp = tempObj.GetComponent<SpriteRenderer>().color;
                tmp.a = 0.3f;
                tempObj.GetComponent<SpriteRenderer>().color = tmp;

                ready = true;
            }
            else if (Input.GetButtonDown("Fire2") && ready && !placed)
            {
                Destroy(tempObj);
                ready = false;
            }

            if (Input.GetButtonDown("Fire1") && ready)
            {
                tmp.a = 1f;
                tempObj.GetComponent<SpriteRenderer>().color = tmp;
                tempObj.GetComponent<CapsuleCollider2D>().enabled = true;
                tempObj.GetComponent<ParticleSystem>().Play();

                ready = false;
                placed = true;
            }
        }
    }

    public void SetSelected(GameObject obj)
    {
        selectObj = obj;
    }
}
