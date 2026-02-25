using System;
using System.Runtime.CompilerServices;

// README.md를 읽고 아래에 코드를 작성하세요.


Test(); // 1. GC 강제 실행
Console.WriteLine("GC 생성 전");
GC.Collect();
GC.WaitForPendingFinalizers();
Console.WriteLine("GC 생성 후");


CharacterTest();    // 2. 생성자, 메서드, 소멸자 실행 순서
Console.WriteLine("GC 실행");
GC.Collect();
GC.WaitForPendingFinalizers();
Console.WriteLine("프로그램 종료");

CarTest();   // 3. 여러 객체의 생명주기
Console.WriteLine("=== GC 실행 ===");
GC.Collect();
GC.WaitForPendingFinalizers();
Console.WriteLine("=== 정리 완료 ===");


GameSessionTest();  // 4. 게임 세션 관리
GC.Collect();
GC.WaitForPendingFinalizers();

MonsterTest();   // 5. 리소스 사용 카운터
GC.Collect();
GC.WaitForPendingFinalizers();
Monster.ShowStatus();

GameItemTest();  // 6. 게임 아이템 관리 시스템
GC.Collect();
GC.WaitForPendingFinalizers();
Console.WriteLine("=== 정리 완료 ===");



void Test()     // 1. GC 강제 실행
{
    TestObject obj1 = new TestObject(1);
    TestObject obj2 = new TestObject(2);

    obj1 = null;
    obj2 = null;
}

void CharacterTest()    // 2. 생성자, 메서드, 소멸자 실행 순서
{
    Character character1 = new Character("용사");

    character1.Attack();

    character1 = null;
}

void CarTest()   // 3. 여러 객체의 생명주기
{
    Car car1 = new Car("검정");
    car1.Drive();
    Car car2 = new Car("하얀");
    car2.Drive();
    Car car3 = new Car("파란");
    car3.Drive();

    car1 = null;
    car2 = null;
    car3 = null;
}

void GameSessionTest()   // 4. 게임 세션 관리
{
    GameSession session1 = new GameSession("플레이어1");
    session1.Play();

    session1 = null;
}

void MonsterTest()   // 5. 리소스 사용 카운터
{
    Monster monster1 = new Monster(1, "슬라임");
    Monster monster2 = new Monster(2, "고블린");
    Monster monster3 = new Monster(3, "오크");

    Monster.ShowStatus();

    monster1 = null;
    monster2 = null;
    monster3 = null;
}

void GameItemTest()  // 6. 게임 아이템 관리 시스템
{
    Console.WriteLine("=== 아이템 생성 ===");
    GameItem item1 = new GameItem("체력 포션", 50);
    GameItem item2 = new GameItem("강철 검", 200);
    GameItem item3 = new GameItem("나무 방패", 100);

    Console.WriteLine("\n=== 아이템 사용 ===");
    item1.Use();
    item2.Use();

    item1 = null;
    item2 = null;
    item3 = null;

    Console.WriteLine("\n=== 인벤토리 정리 ===");
}

class TestObject    // 1. GC 강제 실행
{
    private int _id;

    public TestObject(int id)
    {
        _id = id;
        Console.WriteLine($"객체 {_id} 생성");
    }
    ~TestObject()
    {
        Console.WriteLine($"객체 {_id} 소멸");
    }
}

class Character     // 2. 생성자, 메서드, 소멸자 실행 순서
{
    private string _name;
    public Character(string name)
    {
        _name = name;
        Console.WriteLine($"[1] {_name} 생성");
    }
    public void Attack()
    {
        Console.WriteLine($"[2] {_name} 공격");
    }
    ~Character()
    {
        Console.WriteLine($"[3] {_name} 소멸");
    }
}

class Car   // 3. 여러 객체의 생명주기
{
    private string _color;
    public Car(string color)
    {
        _color = color;
        Console.WriteLine($"{_color}색 자동차 조립");
    }
    public void Drive()
    {
        Console.WriteLine($"{_color}색 자동차가 달림");
    }
    ~Car()
    {
        Console.WriteLine($"{_color}색 자동차 폐차");
    }
}

class GameSession   // 4. 게임 세션 관리
{
    private string _playerName;
    private DateTime _startTime;
    public GameSession(string playerName)
    {
        _playerName = playerName;
        _startTime = DateTime.Now;
        Console.WriteLine($"[{_startTime:HH:mm:ss}] {_playerName} 게임 시작");
    }
    public void Play()
    {
        Console.WriteLine($"{_playerName} 플레이 중...");
    }
    ~GameSession()
    {
        DateTime endTime = DateTime.Now;
        TimeSpan duration = endTime - _startTime;
        Console.WriteLine($"[{endTime:HH:mm:ss}] {_playerName} 게임 종료");
        Console.WriteLine($"플레이 시간: {duration.TotalSeconds:F1}초");
    }
}

class Monster   // 5. 리소스 사용 카운터
{
    private static int s_totalCount;
    private static int s_aliveCount;

    private int _id;
    private string _name;

    public Monster(int _id, string _name)
    {
        this._id = _id;
        this._name = _name;
        s_totalCount++;
        s_aliveCount++;
        Console.WriteLine($"몬스터 생성: {this._name} (ID: {this._id})");
        Console.WriteLine($"  - 현재 생존: {s_aliveCount}");
    }
    ~Monster()
    {
        s_aliveCount--;
        Console.WriteLine($"몬스터 소멸: {this._name} (ID: {this._id})");
        Console.WriteLine($"  - 현재 생존: {s_aliveCount}");
    }

    public static void ShowStatus()
    {
        Console.WriteLine($"\n총 생성: {s_totalCount}, 현재 생존: {s_aliveCount}\n");
    }
}

class GameItem  // 6. 게임 아이템 관리 시스템
{
    private static int s_nextId = 0;    // 정적

    // 인스턴스
    private int _id;
    private string _name;
    private int _price;

    public GameItem(string name, int price)
    {
        _id = ++s_nextId;
        _name = name;
        _price = price;
        Console.WriteLine($"[생성] 아이템 #{_id}: {_name} ({_price}골드)");
    }

    public void Use()
    {
        Console.WriteLine($"[사용] {_name} 아이템을 사용함");
    }
    ~GameItem()
    {
        Console.WriteLine($"[삭제] 아이템 #{_id}: {_name} 인벤토리에서 제거됨");
    }
}