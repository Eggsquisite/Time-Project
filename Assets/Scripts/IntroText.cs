using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroText : MonoBehaviour
{
    [SerializeField] GameObject civs = null, enemies = null;

    private bool portal, hidden, grav, ed, rose, oscar;

    // Start is called before the first frame update
    void Start()
    {
        civs.SetActive(false);
        enemies.SetActive(false);

        if (gameObject.name.Contains("Portal"))
            portal = true;
        else if (gameObject.name.Contains("Hidden"))
            hidden = true;
        else if (gameObject.name.Contains("Gravity"))
            grav = true;
        else if (gameObject.name.Contains("Ed"))
            ed = true;
        else if (gameObject.name.Contains("Rose"))
            rose = true;
        else if (gameObject.name.Contains("Oscar"))
            oscar = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
            Continue();

        if (PlayerPrefs.GetInt("portalText") == 1 && portal)
            Continue();
        else if (PlayerPrefs.GetInt("hiddenText") == 1 && hidden)
            Continue();
        else if (PlayerPrefs.GetInt("gravText") == 1 && grav)
            Continue();
        else if (PlayerPrefs.GetInt("edText") == 1 && ed)
            Continue();
        else if (PlayerPrefs.GetInt("roseText") == 1 && rose)
            Continue();
        else if (PlayerPrefs.GetInt("oscarText") == 1 && oscar)
            Continue();
    }

    public void Continue()
    {
        if (portal)
            PlayerPrefs.SetInt("portalText", 1);
        else if (hidden)
            PlayerPrefs.SetInt("hiddenText", 1);
        else if (grav)
            PlayerPrefs.SetInt("gravText", 1);
        else if (ed)
            PlayerPrefs.SetInt("edText", 1);
        else if (rose)
            PlayerPrefs.SetInt("roseText", 1);
        else if (oscar)
            PlayerPrefs.SetInt("oscarText", 1);

        if (!civs.activeSelf && !enemies.activeSelf && civs != null && enemies != null)
        {
            civs.SetActive(true);
            enemies.SetActive(true);
        }

        Destroy(gameObject);
    }
}
