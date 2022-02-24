using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace APILibrary.Core.Models
{
    public abstract class ModelBase //: ValueObject
    {
        //protected abstract override IEnumerable<object> GetEqualityComponents();
        public int ID { get; set; }
    }
}
