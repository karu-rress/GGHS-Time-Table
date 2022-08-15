#nullable enable

using MoneyType = System.UInt32;

namespace TimeTableUWP.Conet;

/// <summary>
/// Conet's money
/// </summary>
/// 

public readonly struct Won : IComparable, IComparable<Won>, IEquatable<Won>
{
    private readonly MoneyType _won;
    public Won(MoneyType won) => _won = won;
    public Won(Egg egg) => _won = egg.Value * 1000;

    public MoneyType Value => _won;
    public static explicit operator Egg(Won won) => new(won);
    public static explicit operator Won(MoneyType won) => new(won);

    public override string ToString() => $"{_won}원";

    public int CompareTo(object obj)
    {
        if (obj is null)
            return 1;

        if (obj is Won won)
            return _won.CompareTo(won._won);
        throw new ArgumentException("Received not Won.");
    }
    public int CompareTo(Won rhs) => _won.CompareTo(rhs._won);
    public bool Equals(Won rhs) => _won.Equals(rhs._won);

    public static Won operator +(Won w1, Won w2) => new(w1._won + w2._won);
    public static Won operator -(Won w1, Won w2) => new(w1._won - w2._won);
    public static bool operator <(Won w1, Won w2) => w1._won < w2._won;
    public static bool operator >(Won w1, Won w2) => w1._won > w2._won;
    public static bool operator ==(Won w1, Won w2) => w1._won == w2._won;
    public static bool operator !=(Won w1, Won w2) => w1._won != w2._won;
}

public readonly struct Egg : IComparable, IComparable<Egg>, IEquatable<Egg>
{
    private readonly MoneyType _egg;
    public Egg(MoneyType egg) => _egg = egg;
    public Egg(short egg) : this((MoneyType)egg) { }
    public Egg(Won won) => _egg = won.Value / 1000;
    

    public static explicit operator Won(Egg egg) => new(egg);
    public static implicit operator Egg(MoneyType egg) => new(egg);
    public MoneyType Value => _egg;

    public override string ToString() => $"{_egg} 에그";

    public int CompareTo(object obj)
    {
        if (obj is null)
            return 1;

        if (obj is Egg egg)
            return this._egg.CompareTo(egg._egg);
        throw new ArgumentException("Received not Egg.");
    }
    public int CompareTo(Egg rhs) => _egg.CompareTo(rhs._egg);
    public bool Equals(Egg rhs) => _egg.Equals(rhs._egg);

    public static Egg operator +(Egg e1, Egg e2) => new(e1._egg + e2._egg);
    public static Egg operator -(Egg e1, Egg e2) => new(e1._egg - e2._egg);
    public static bool operator <(Egg e1, Egg e2) => e1._egg < e2._egg;
    public static bool operator >(Egg e1, Egg e2) => e1._egg > e2._egg;
    public static bool operator ==(Egg e1, Egg e2) => e1._egg == e2._egg;
    public static bool operator !=(Egg e1, Egg e2) => e1._egg != e2._egg;
}