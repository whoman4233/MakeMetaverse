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
    /// ó�� ���� �� ���ο� �÷��̾ �����Ѵ�.
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
        //�ӽ� ���� �ʱ�ȭ
        Debug.Log("[PlayerManager] FSM �ʱ�ȭ: HubState");
        fsm.ChangeState(new PlayerMainSceneState());
            
        return currentPlayerInstance;
    }

    private void OnSceneChanged(string sceneName)
    {
        // FSM ��ü
        if (sceneName.Contains("Main"))
            fsm.ChangeState(new PlayerMainSceneState());
        else if (sceneName.Contains("Flappy"))
            fsm.ChangeState(new FlappyState());


        // ���� ���� ����Ʈ�� �̵�
        var spawn = GameObject.FindWithTag("PlayerSpawn");
        currentPlayerInstance.transform.position = spawn ? spawn.transform.position : Vector3.zero;
    }

    /// <summary>
    /// �÷��̾� ������ ���� (ex. ����, �̸�, ��� ��)
    /// </summary>
    public void UpdateData(PlayerData newData)
    {
        currentData = newData;
    }

    /// <summary>
    /// ���� �÷��̾� �ν��Ͻ��� ��ȯ
    /// </summary>
    public GameObject Player => currentPlayerInstance;
    public Rigidbody2D Rb => currentPlayerInstance.GetComponent<Rigidbody2D>();
    public Animator anim => currentPlayerInstance.GetComponent<Animator>();
}
