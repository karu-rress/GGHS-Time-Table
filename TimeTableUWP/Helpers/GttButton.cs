#nullable enable

namespace TimeTableUWP;

public interface IButtonData
{
}

public abstract class GttButton<T> : Button
    where T: IButtonData
{
    protected const int ButtonWidth = 2560;
    protected const int ButtonHeight = 93;

    public T Data { get; protected set; }
    /*
    public GttButton(T data)
    {
        Data = data;
        Height = ButtonHeight;
    }
    */
}