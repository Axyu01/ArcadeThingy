using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
    public List<Entity> Entities = new List<Entity>(); 
    Player player = null;
    [Header("Camera")]
    public Camera MainCamera;
    public float CameraDistance = 10f;
    public float CameraAngle = 45f;
    public float CameraLerpSpeed = 5.0f;
    [Header("Game Info UI References")]
    public Text Score;
    int score = 0;
    public Text Wave;
    [Header("Events")]
    public UnityEvent OnStart;
    public UnityEvent OnEnd;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
            instance = this;
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
        foreach(Entity entity in instance.Entities)
        {
            entity.gameObject.SetActive(true);
        }
        instance.score = 0;
        instance.Score.text = $"Score:\n{instance.score}";
        instance.OnStart.Invoke();
    }
    public static void EndGame()
    {
        instance.OnEnd.Invoke();
        foreach (Entity entity in instance.Entities)
        {
            entity.gameObject.SetActive(false);
        }
        SceneManager.LoadScene(0);
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
    public static void RegisterEntity(Entity entity)
    {
        instance.Entities.Add(entity);
    }
    public static void UnregisterEntity(Entity entity)
    { 
        instance.Entities.Remove(entity);
    }
}
