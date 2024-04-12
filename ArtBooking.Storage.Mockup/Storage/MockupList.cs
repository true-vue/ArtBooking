using System.Reflection;

public class MockupList<T> : List<T> where T : class
{
    private int _identity = 0;
    private readonly PropertyInfo _idProperty;
    private readonly object _syncLock = new object(); // Synchronization object for locking

    public MockupList(string idPropertyName)
    {
        var keyProp = typeof(T).GetProperty(idPropertyName);
        if (keyProp == null) throw new InvalidOperationException($"Identity property '{idPropertyName}' not found.");
        _idProperty = keyProp;
    }

    public new T Add(T item)
    {
        lock (_syncLock) // Ensure thread safety during add operations
        {
            _identity++;
            _idProperty.SetValue(item, _identity);
            base.Add(item);
            return item;
        }
    }

    public new IEnumerable<T> AddRange(IEnumerable<T> items)
    {
        lock (_syncLock) // Ensure thread safety for range additions
        {
            var itemList = items.ToList(); // Avoid multiple enumeration
            foreach (var item in itemList)
            {
                _identity++;
                _idProperty.SetValue(item, _identity);
            }
            base.AddRange(itemList);
            return itemList;
        }
    }
}