using System;
using System.Data.Common;

// README.md를 읽고 코드를 작성하세요.

Run();
GC.Collect();
GC.WaitForPendingFinalizers();

Seat.ShowStatus();

void Run()
{
    Seat seat1 = new Seat("김민수", 1);
    Seat seat2 = new Seat("이지영", 2);
    Seat seat3 = new Seat("박서준", 3);

    seat1.Study();
    seat2.Study();
    seat3.Study();

    Seat.ShowStatus();
}

class Seat
{
    private string _studentName;
    private int _id = 1;

    private static int _totalUsers = 0;
    private static int _currentUsers = 0;

    public Seat(string name, int id)
    {
        _studentName = name;
        _id = id;
        _totalUsers++;
        _currentUsers++;
        Console.WriteLine($"좌석 {_id}번 착석: {_studentName}");
    }

    public void Study()
    {
        Console.WriteLine($"{this._studentName}이(가) 좌석 {this._id}번에서 공부 중...");
    }

    ~Seat()
    {
        Console.WriteLine($"좌석 {this._id}번 반납: {this._studentName}");
        _currentUsers--;
    }

    public static void ShowStatus()
    {
        Console.WriteLine($"총 이용: {_totalUsers}명, 현재 착석: {_currentUsers}명");
    }
}
