using AutoML.Web.Enums;

namespace AutoML.Web.Models
{
    public class TrainModelViewModel
    {
        public string FileName { get; set; }
        public ModelType ModelType { get; set; }
        public int TrainingSplit { get; set; } = 10;
        public string targetColumn { get; set; }
    }
}
