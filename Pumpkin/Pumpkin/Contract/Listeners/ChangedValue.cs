namespace Pumpkin.Contract.Listeners
{
   public class ChangedValue
    {
        public string Name { get; set; }
        
        public object OldValue { get; set; }
        
        public object CurrentValue { get; set; }
    }
}
