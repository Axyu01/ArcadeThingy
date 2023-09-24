using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get { return instance; } private set { instance = value; } }
    public static Player Player { get { return instance.player; } 
        set 
        {
            if (value == null)
                return;
            if(instance.player != null)
            {
                Destroy(instance.player);
            }
            instance.player = value;
        } 
    }
    Player player = null;
    [Header("Camera")]
    public Camera MainCamera;
    public float CameraDistance = 10f;
    public float CameraAngle = 45f;
    public float CameraLerpSpeed = 5.0f;
    [Header("UI")]
    public List<RectTransform> MenuElements = new List<RectTransform>();
    public List<RectTransform> GameElements = new List<RectTransform>();
    [Header("Game Info UI References")]
    public Text Score;
    int score = 0;
    public Text Wave;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        SmoothUpdateCamera(MainCamera, Time.fixedDeltaTime);
    }
    public static void StartGame()
    {
        instance.score = 0;
        instance.Score.text = $"Score:\n{instance.score}";
    }
    public void EndGame()
    {

    }
    public static void AddScore(int score)
    {
        instance.score += score;
        instance.Score.text = $"Score:\n{instance.score}";
    }
    public void SmoothUpdateCamera(Camera camera,float deltaTime)
    {
        Quaternion destRotation = Quaternion.Euler(CameraAngle, 0f, 0f);
        float lerpVal = deltaTime * CameraLerpSpeed;
        if(lerpVal > 1f || lerpVal < 0f)
            lerpVal = 1f;
        camera.transform.position = Vector3.Lerp(camera.transform.position,player.transform.position + destRotation*Vector3.back*CameraDistance,lerpVal);
        camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, destRotation,lerpVal);
    }
}
