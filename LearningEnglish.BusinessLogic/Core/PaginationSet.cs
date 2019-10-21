using System.Collections.Generic;

namespace LearningEnglish.BusinessLogic.Core
{
    public class PaginationSet<T>
    {
        public int Total { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
