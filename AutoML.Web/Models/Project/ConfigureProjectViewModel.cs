namespace AutoML.Web.Models.Project
{
    public class ConfigureProjectViewModel
    {
        public string? ModelType { get; set; }
        public string? Algorithm { get; set; }
        public int TrainingSplit { get; set; } = 10;
        public IEnumerable<string>? SelectedColumns { get; set; }
        public string? TargetColumn { get; set; }
    }
}
