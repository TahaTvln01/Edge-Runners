using Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public int fruits;
    public Transform respawnPoint;
    public GameObject currentPlayer;
    public float respawnTime;
    public int choosenSkinID;
    public InGame_UI inGame_UI;
    public bool isGamePaused = false;
    public bool playerJustRespawned;

    [SerializeField] private GameObject playerPrefab;

    [Header("Camera Shake FX")]
    [SerializeField] private CinemachineImpulseSource impulse;
    [SerializeField] private Vector3 shakeDirection;
    [SerializeField] private float force = 0.25f;
    private bool willRespawned;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void ScreenShake(int direction)
    {
        impulse.m_DefaultVelocity = new Vector3(shakeDirection.x * direction, shakeDirection.y) * force;
        impulse.GenerateImpulse();
    }


    private void Update()
    {
        if (currentPlayer == null)
        {
            if (!willRespawned)
            {
                Invoke("RespawnPlayer", respawnTime);
                willRespawned = true;
            }
        }
    }
    public void RespawnPlayer()
    {
        if (currentPlayer == null && respawnPoint != null)
        {
            AudioManager.instance.PlaySFX(11);
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
            willRespawned = false;
            playerJustRespawned = true;
            Invoke("CameraDelay", .5f);
        }
    }

    private void CameraDelay()
    {
        playerJustRespawned = false;
    }

    public void KillPlayer()
    {
        AudioManager.instance.PlaySFX(0);
        Destroy(currentPlayer);
    }
}

