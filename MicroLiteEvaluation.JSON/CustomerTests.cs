using System;
using System.Collections.Generic;
using System.Text;

using MicroLite.TypeConverters;
using MicroLite.Listeners;

using MicroLite.Core;

namespace MicroLite.JSONconcept.Tests
{
    public class TestHarness
    {
        public void Test()
        {
            // Register a custom implementation of the JSONTypeConverter for each business object aggregate root (e.g. the top level object)

            TypeConverter.Converters.Add(new JSONTypeConverter<CustomerData>());


            // Register each listener

            Listener.Listeners.Add(new CustomerListener());
        }
    }


}
