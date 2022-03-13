using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyType = System.UInt32;

namespace TimeTableUWP.Conet;

/// <summary>
/// Conet's money
/// </summary>
public readonly struct Won
{
    private readonly MoneyType _won;
    public Won(MoneyType won) => _won = won;
    public Won(Egg egg) => _won = egg.Value * 1000;
    public MoneyType Value => _won;
    public static explicit operator Egg(Won won) => new(won);
    public override string ToString() => $"{_won}원";
}

public readonly struct Egg
{
    private readonly MoneyType _egg;
    public Egg(MoneyType egg) => _egg = egg;
    public Egg(Won won) => _egg = won.Value / 1000;

    public static explicit operator Won(Egg egg) => new(egg);
    public MoneyType Value => _egg;
    public override string ToString() => $"{_egg} 에그";
}
