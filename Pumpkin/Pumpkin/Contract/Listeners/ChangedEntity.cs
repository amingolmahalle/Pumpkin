using System.Collections.Generic;

namespace Pumpkin.Contract.Listeners
{
    public class ChangedEntity
    {
        public EntityChangeState OldState { get; set; }
        public object Entity { get; set; }
        public EntityChangeState NewState { get; set; }
        public List<ChangedValue> ChangedValues { get; set; } = new List<ChangedValue>();
    }
}
