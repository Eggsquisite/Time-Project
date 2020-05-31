using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{

    public static CameraManager Instance { get; private set; }

    [SerializeField] List<AudioClip> music = null;
    private Scene scene;
    private AudioSource audioSource;
    private bool audioBegin;
    private int level = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        level = SceneManager.GetActiveScene().buildIndex;

        if (level % 3 == 0)
        {
            audioSource.Stop();
            var num = Random.Range(0, music.Count);
            var tmp = music[num];
            audioSource.clip = tmp;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
