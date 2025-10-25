using System;
using System.Collections.Generic;
using System.Text;

namespace AutoML.Data
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string CollectionName { get; set; } = "model-configs";
    }
}
