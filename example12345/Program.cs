using System;

new MyParaller().Foreach<int>(Enumerable.Range(0, 1000), a =>
{
    Console.WriteLine($"items:{a}  thread:{Thread.CurrentThread.ManagedThreadId}");
    for (int i = 0; i < int.MaxValue; i++)
    {

    }
}, 5);
Console.ReadKey();
class MyParaller
{
    public void Foreach<TObj>(IEnumerable<TObj> enumerable, Action<TObj?> func, int countTask)
    {
        var query = Slice(enumerable, countTask);
        var ls = new List<Task>(countTask);
        foreach (var iCollection in query)
        {
            var task = Task.Run(() =>
            {
                foreach (var item in iCollection)
                {
                    func.Invoke(item);
                }
            });
            ls.Add(task);
        }
        Task.WaitAll(ls.ToArray());
        // enumerable.Chunk
    }
    private IEnumerable<IEnumerable<TObj>> Slice<TObj>(IEnumerable<TObj> en, int count)
    {
        var enumerator = en.GetEnumerator();
        for (int i = 0; i < count; i++)
        {
            yield return GetPart(enumerator);
        }

    }
    private IEnumerable<TObj> GetPart<TObj>(IEnumerator<TObj> en)
    {
        while (true)
        {
            lock (objLock)
            {
                if (!en.MoveNext())
                    break;
            }
            yield return en.Current;
        }
    }
    static object objLock = new object();
}