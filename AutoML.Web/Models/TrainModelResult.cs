﻿namespace AutoML.Web.Models
{
    public class TrainModelResult
    {
        public string ModelType { get; set; }
        public double Accuracy { get; set; }
        public double Precision { get; set; }
        public double Recall { get; set; }
        public double F1Score { get; set; }
    }
}
