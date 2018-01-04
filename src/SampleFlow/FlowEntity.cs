using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SampleFlow
{
    public class FlowEntity
    {
        public static string DEFAULTSTATE = "NY";
        public static string BoolTestComparison = "false";

        public string State { get; set; }
        public string BoolText { get; set; }
        public ChildEntity ChildEntity { get; set; } = new ChildEntity();

        public string[,] StringList { get; set; } = new string[2, 2];

        public decimal Discount { get; set; }
        public string FirstValue { get; set; }

        public Type TheType { get; set; }
        public Collection<object> MyCollection { get; set; }

        public FirstClass FirstClass { get; set; } = new FirstClass();
        public object DClass = new ChildEntity();


        private bool BoolMethod()
        {
            return BoolText == BoolTestComparison;
        }

        private void SetFirstValue(OtherEntity entity)
        {
            this.FirstValue = entity.FirstValue;
        }

        private void SetCollection(Collection<object> obj)
        {
            MyCollection = obj;
        }

        public static void SetDefaultState(string state)
        {
            DEFAULTSTATE = state;
        }
    }
}
