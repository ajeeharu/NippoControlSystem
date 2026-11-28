using System.Collections;

namespace NippoControlSystem.Domain.Tables
{
    /// <summary>
    /// 各種 DTO コレクションをテーブルとして扱う汎用モデル
    /// </summary>
    public class ItemTable<T> : IReadOnlyList<T>
    {
        private readonly List<T> _rows;

        public ItemTable(IEnumerable<T>? rows = null)
        {
            _rows = rows?.ToList() ?? new List<T>();
        }

        public int Count => _rows.Count;

        public T this[int index] => _rows[index];

        public T? FindFirst(Func<T, bool> predicate)
            => _rows.FirstOrDefault(predicate);

        public IReadOnlyList<T> Filter(Func<T, bool> predicate)
            => _rows.Where(predicate).ToList();

        public IEnumerator<T> GetEnumerator() => _rows.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}