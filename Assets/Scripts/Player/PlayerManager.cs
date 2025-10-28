using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("Current Player Data")]
    public PlayerData currentData;

    private GameObject currentPlayerInstance;
    [SerializeField] private GameObject playerPrefab;
    private PlayerStateMachine fsm;
    [SerializeField] private CameraFollow cam;

    protected override void Awake()
    {
        base.Awake();

        if (currentPlayerInstance == null)
        {
            currentPlayerInstance = Instantiate(playerPrefab, transform);
            currentPlayerInstance.name = "Player";
        }

        fsm = currentPlayerInstance.GetComponent<PlayerStateMachine>();
        fsm.Initialize(this);
        fsm.ChangeState(new PlayerMainSceneState());

        GameManager.OnSceneChanged += OnSceneChanged;
        cam.SetTarget(currentPlayerInstance.transform);
    }


    private void OnDestroy()
    {
        GameManager.OnSceneChanged -= OnSceneChanged;
    }

    /// <summary>
    /// 처음 시작 시 새로운 플레이어를 생성한다.
    /// </summary>
    public GameObject SpawnPlayer(GameObject prefab, Vector3 position)
    {
        if (currentPlayerInstance != null)
            Destroy(currentPlayerInstance);

        currentPlayerInstance = Instantiate(prefab, position, Quaternion.identity);

        var appearance = currentPlayerInstance.GetComponent<PlayerAppearance>();
        if (appearance != null && currentData != null)
            appearance.Apply(currentData);

        fsm = currentPlayerInstance.GetComponent<PlayerStateMachine>();
        //임시 상태 초기화
        Debug.Log("[PlayerManager] FSM 초기화: HubState");
        fsm.ChangeState(new PlayerMainSceneState());
            
        return currentPlayerInstance;
    }

    private void OnSceneChanged(string sceneName)
    {
        // FSM 교체
        if (sceneName.Contains("Main"))
            fsm.ChangeState(new PlayerMainSceneState());
        else if (sceneName.Contains("Flappy"))
            fsm.ChangeState(new FlappyState());


        // 씬의 스폰 포인트로 이동
        var spawn = GameObject.FindWithTag("PlayerSpawn");
        currentPlayerInstance.transform.position = spawn ? spawn.transform.position : Vector3.zero;
    }

    /// <summary>
    /// 플레이어 데이터 변경 (ex. 색상, 이름, 장식 등)
    /// </summary>
    public void UpdateData(PlayerData newData)
    {
        currentData = newData;
    }

    /// <summary>
    /// 현재 플레이어 인스턴스를 반환
    /// </summary>
    public GameObject Player => currentPlayerInstance;
    public Rigidbody2D Rb => currentPlayerInstance.GetComponent<Rigidbody2D>();
    public Animator anim => currentPlayerInstance.GetComponent<Animator>();
}
