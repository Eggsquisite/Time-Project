using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    //[SerializeField] GameObject selectObj = null;
    public GameObject selectObj;
    [SerializeField] GameObject objPosition = null;
    [SerializeField] GameObject manaBar = null;
    [SerializeField] ButtonManager bm;

    private ManaBar mb;
    private GameObject tempObj;
    private GameObject[] allObjs;
    private SpriteRenderer[] spriteObjs;
    private Color tmp;
    private bool ready, placed, useMana, manaReady;
    private int index;

    private void Start()
    {
        manaReady = true;
        mb = manaBar.GetComponent<ManaBar>();

        allObjs = new GameObject[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
            tempObj.transform.position = objPosition.transform.position;

        if (useMana)
        {
            mb.UseMana(true);
            useMana = false;
        }

        if (selectObj != null)
        {
            objPosition.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            
            if (!ready && !placed && manaReady && index < 2)
            {
                tempObj = Instantiate(selectObj, objPosition.transform.position, Quaternion.identity);
                tempObj.GetComponent<ParticleSystem>().Stop();
                tempObj.GetComponent<CapsuleCollider2D>().enabled = false;

                spriteObjs = tempObj.GetComponentsInChildren<SpriteRenderer>();
                tmp = spriteObjs[0].color;
                tmp.a = 0.3f;
                spriteObjs[0].color = spriteObjs[1].color = tmp;

                ready = true;
            }
            else if (Input.GetButtonDown("Fire2") && ready && !placed)
            {
                Destroy(tempObj);
                selectObj = null;
                bm.Reset();
                ready = false;
            }

            if (Input.GetButtonDown("Fire1") && ready && index < 2)
            {
                tmp.a = 1f;
                spriteObjs[0].color = spriteObjs[1].color = tmp;

                tempObj.GetComponent<CapsuleCollider2D>().enabled = true;
                tempObj.GetComponent<ParticleSystem>().Play();
                allObjs[index] = tempObj;

                index++;
                ready = false;
                placed = true;
                useMana = true;
            }
        }
    }

    public List<Transform> GetAllSelectObjs(GameObject obj)
    {
        var tmp = new List<Transform>();

        foreach (Transform child in obj.transform)
        {
            tmp.Add(child);
        }

        return tmp;
    }

    public void SetSelected(GameObject obj)
    {
        selectObj = obj;
        ready = false;
        placed = false;
    }

    public void SetManaReady(bool status)
    {
        manaReady = status;
    }

    public void OutOfMana()
    {
        for (int i = 0; i < index; i++)
        {
            Destroy(allObjs[i].gameObject);
        }

        index = 0;
        //bm.Reset();
        placed = false;
        manaReady = false;
    }
}
