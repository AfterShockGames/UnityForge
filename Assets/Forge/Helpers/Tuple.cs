/// <summary>
///     Simple tuple class
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="T2"></typeparam>
internal class Tuple<T, T2>
{
    public T Item1 { get; set; }

    public T2 Item2 { get; set; }

    public Tuple(T item1, T2 item2)
    {
        Item1 = item1;
        Item2 = item2;
    }
}