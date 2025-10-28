# MakeMetaverse (Unity 2D)

## 🧩 개요
플레이어가 허브 씬(MainScene)에서 이동하며 다양한 **Zone**과 **NPC**와 상호작용하고,  
특정 Zone에 진입하면 **미니게임(FlappyBird)** 으로 전환되는 2D 프로젝트입니다.  

**FSM 기반 플레이어 상태 전환**, **씬 전환 관리**, **카메라 경계 추적**, **UI 프롬프트**, **오브젝트 풀링** 등이 구현되어 있습니다.

---

## 🎮 주요 기능
- **플레이어 FSM**
  - `PlayerMainSceneState` ↔ `FlappyState` 간 상태 전환
  - 이동, 점프, 애니메이션 제어 로직 분리
- **미니게임 시스템**
  - `FlappyBird`: 파이프 스폰 및 충돌 처리
  - 오브젝트 풀링으로 성능 최적화
- **씬 전환 & 게임 흐름**
  - `GameManager`가 씬 로드 및 미니게임 시작/종료 관리
- **카메라 추적**
  - `CameraFollow`가 타일맵 경계를 감지해 화면 이탈 방지
- **UI**
  - `UIManager`에서 상호작용 프롬프트(E키) 및 간단 대화창 표시
- **상호작용 시스템**
  - `IInteractable` 기반으로 Zone/NPC의 행동 통합 관리

---

## 📁 폴더 구조
Assets
└── Scripts
├── Manager # GameManager, UIManager
├── Map
│ ├── InteractZone.cs
│ ├── ZoneData.cs
│ └── NPC/ # DialogueData, NPCInteract, TestDialogue
├── MiniGame
│ ├── IMinigameManager.cs
│ ├── FlappyBird/ # FlappyUI, ObjectPool, Pipe, PipeSpawner
│ └── Manager/ # FlappyBirdManager
├── Player
│ ├── InteractDetector.cs
│ ├── PlayerAppearance.cs
│ ├── PlayerContext.cs
│ ├── PlayerData.cs
│ ├── PlayerInputController.cs
│ ├── PlayerManager.cs
│ ├── PlayerMovementController.cs
│ └── State/ # IPlayerState, PlayerMainSceneState, FlappyState, PlayerStateMachine
├── CameraFollow.cs
└── Singleton.cs

---

## ⚙️ 셋업 & 요구사항
- Unity **2022.3 LTS**
- **Input System** 사용 (WASD/Arrow, Space, E)
- 씬 구성:
  - `MainScene`: 허브 + PlayerManager + UIManager + GameManager
  - `MiniGame_Flappy`: 미니게임용 구성 + FlappyBirdManager
- 태그:
  - `PlayerSpawn`: 씬 내 플레이어 시작 위치 지정용

---

## 🚀 실행 흐름

1. **허브(MainScene)**  
   - `GameManager` → 씬 이름 브로드캐스트  
   - `PlayerManager` → Player FSM 초기화(`PlayerMainSceneState`)  
   - `UIManager` → 프롬프트 표시 준비  

2. **상호작용(InteractZone)**  
   - 플레이어가 존에 진입 → `"E: 상호작용"` 프롬프트 표시  
   - E 입력 시 `GameManager.LaunchMiniGame("MiniGame_Flappy")` 호출  

3. **미니게임(FlappyBird)**  
   - `FlappyBirdManager`에서 게임 시작  
   - 장애물 충돌 시 `GameOver()` → `GameManager.EndMiniGame(score)` → 허브 복귀  

4. **복귀(MainScene)**  
   - 씬 로드 후 `PlayerManager`가 FSM 상태 재설정  
   - `PlayerSpawn` 위치로 이동 후 카메라 타겟 재할당  

---

## 🧠 플레이어 FSM 구조
- **PlayerStateMachine**  
  - `Initialize()` → `PlayerContext` 구성  
  - `ChangeState()` → `IPlayerState` 전환  
- **PlayerMainSceneState**  
  - 이동, 회전, 애니메이션 처리  
- **FlappyState**  
  - 중력 활성화, Space로 상승  
  - 충돌 시 `GameOver()`

---

## 🎥 카메라 추적
- `CameraFollow`  
  - `SetTarget(Transform)`으로 대상 지정  
  - 씬 내 “Boarder” 타일맵 경계 자동 감지  
  - 카메라 비율 및 orthographicSize 기준으로 Clamp 처리  

---
## 🧠 플레이어 FSM 구조

┌────────────────────────┐
│ PlayerManager │
│ - FSM 초기화 & 전환 │
└────────────┬───────────┘
│
▼
┌────────────────────────┐
│ PlayerStateMachine │
│ - OnMove, OnJump 등 │
│ - State 교체 관리 │
└────────────┬───────────┘
│
┌───────────┴─────────────────────────────┐
│ │
▼ ▼
[PlayerMainSceneState] [FlappyState]

중력 0 - 중력 활성화

이동 및 점프 - Space 점프

Zone/NPC 상호작용 - Pipe 충돌 시 GameOver

yaml
코드 복사

---

## 🚀 씬 전환 흐름

┌────────────┐
│ MainScene │
│ (허브) │
└────┬───────┘
│ 플레이어 상호작용 (E)
▼
┌────────────┐
│ MiniGame │
│ FlappyBird │
└────┬───────┘
│ GameOver()
▼
┌────────────┐
│ MainScene │
│ 복귀 │
└────────────┘

csharp
코드 복사

- `GameManager`가 씬 전환 및 `OnSceneChanged` 이벤트 브로드캐스트
- `PlayerManager`가 새 씬 이름을 감지해 FSM 상태 자동 전환  
  (`PlayerMainSceneState` ↔ `FlappyState`)

---


## 🧩 상호작용 시스템
```csharp
public enum InteractType { Zone, NPC }

public interface IInteractable {
    InteractType Type { get; }
    void OnPlayerEnter();
    void OnPlayerExit();
    void TryInteract();
}
InteractZone: ZoneData에 연결되어 씬 전환 또는 이벤트 실행

NPCInteract: 대화 데이터(DialogueData) 기반으로 UI 표시

🕹️ 미니게임 (FlappyBird)

FlappyBirdManager: 게임 진행/종료/점수 관리

PipeSpawner: 파이프 주기적 스폰

ObjectPool: 재사용 가능한 파이프 풀

FlappyUI: 점수 및 게임오버 UI

⚠️ 트러블슈팅 메모
문제	원인	해결
씬 전환 후 UI 사라짐	Camera.main 재참조 누락	UIManager에서 SceneManager.sceneLoaded 이벤트로 재할당
미니게임 무한루프	Start() 내 StartGame() 중복 호출	RegisterMiniGame()만 수행하고 외부에서 1회만 호출
카메라 이동 안됨	Target 누락 또는 Clamp 계산 오류	CameraFollow.SetTarget() 재호출
FSM 상태 전환 안됨	씬 이름 검사 불일치	"Main", "Flappy" 문자열 확인
🧭 새 미니게임 추가 가이드

MiniGame/<NewGame> 폴더 생성

NewGameManager : MonoBehaviour, IMiniGameManager 작성

새 씬 MiniGame_<Name> 생성

GameManager.LaunchMiniGame("MiniGame_<Name>") 호출

🪶 코딩 컨벤션
항목	규칙
클래스/메서드/프로퍼티	PascalCase
필드/로컬 변수	camelCase (_field or field)
주석	/// Summary 사용, 클래스/메서드 설명 명시
MonoBehaviour	Awake(초기화), Start(참조 연결), Update(로직) 구분
로그 태그	[ClassName] 접두부 사용
🧱 로드맵

 로컬 리더보드 (PlayerPrefs + JSON)

 탑승물 시스템 (속도/방어/통과 능력)

 추가 미니게임 (슈팅/러너)

 네임스페이스 구조화 (Game.*)

 코드 리팩토링 및 Summary 주석 추가

🧾 라이선스 / 참고

예시 스프라이트: TopDown 2D RPG BE3 (무료)

외부 에셋 사용 시 각 라이선스 준수

👥 크레딧

Core Systems: FSM, Camera, MiniGame, Interaction

Author: 손준형 (Unity Client Programmer Portfolio Project)
