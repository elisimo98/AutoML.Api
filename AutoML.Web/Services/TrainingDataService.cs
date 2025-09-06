using AutoML.Web.Enums;
using AutoML.Web.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using AutoML.Domain.Models;

namespace AutoML.Web.Services
{
    public class TrainingDataService : ITrainingDataService
    {
        private readonly CsvConfiguration csvConfig;

        public TrainingDataService()
        {
            csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                BadDataFound = null
            };
        }

        public TrainingData ProcessCsv(Stream csvStream, string targetColumn)
        {
            ArgumentNullException.ThrowIfNull(csvStream);
            ArgumentException.ThrowIfNullOrEmpty(targetColumn, nameof(targetColumn));

            var csvData = ReadCsv(csvStream);

            var cleanRows = DropRowsWithMissingValues(csvData);
            var types = InferColumnTypes(cleanRows);
            var encoders = EncodeCategoricalFeatures(
                cleanRows,
                types.Where(kv => kv.Value == ColumnType.Categorical).Select(kv => kv.Key)
            );
            var trainingData = ConvertToTrainingData(cleanRows, targetColumn);

            // TODO: you could store encoders if needed for future inference

            return trainingData;
        }

        private List<Dictionary<string, string>> ReadCsv(Stream csvStream)
        {
            using var reader = new StreamReader(csvStream, leaveOpen: true);
            using var csv = new CsvReader(reader, csvConfig);

            var records = new List<Dictionary<string, string>>();
            csv.Read();
            csv.ReadHeader();
            var header = csv.HeaderRecord;

            while (csv.Read())
            {
                var row = header.ToDictionary(h => h, h => csv.GetField(h));
                records.Add(row);
            }

            return (records);
        }

        private List<Dictionary<string, string>> DropRowsWithMissingValues(List<Dictionary<string, string>> rows)
        {
            ArgumentNullException.ThrowIfNull(rows);

            if (rows.Count == 0)
                return rows;

            var columns = rows.First().Keys;
            return rows
                .Where(row => columns.All(col => !string.IsNullOrWhiteSpace(row[col])))
                .ToList();
        }

        private Dictionary<string, ColumnType> InferColumnTypes(List<Dictionary<string, string>> rows)
        {
            ArgumentNullException.ThrowIfNull(rows);

            bool IsNumeric(IEnumerable<string> values) =>
                values.All(v => double.TryParse(v, out _));

            var columns = rows.First().Keys;
            return columns.ToDictionary(
                col => col,
                col => IsNumeric(rows.Select(r => r[col])) ? ColumnType.Numeric : ColumnType.Categorical
            );
        }

        private Dictionary<string, Dictionary<string, int>> EncodeCategoricalFeatures(
            List<Dictionary<string, string>> rows,
            IEnumerable<string> categoricalColumns)
        {
            ArgumentNullException.ThrowIfNull(rows);
            ArgumentNullException.ThrowIfNull(categoricalColumns);

            var encoders = new Dictionary<string, Dictionary<string, int>>();

            foreach (var col in categoricalColumns)
            {
                var encoder = new Dictionary<string, int>();
                int index = 0;

                foreach (var row in rows)
                {
                    var value = row[col];
                    if (!encoder.ContainsKey(value))
                        encoder[value] = index++;

                    row[col] = encoder[value].ToString();
                }

                encoders[col] = encoder;
            }

            return encoders;
        }

        private TrainingData ConvertToTrainingData(List<Dictionary<string, string>> rows, string targetColumn)
        {
            ArgumentNullException.ThrowIfNull(rows);
            ArgumentException.ThrowIfNullOrEmpty(targetColumn, nameof(targetColumn));

            var features = new List<List<float>>();
            var labels = new List<float>();
            var featureColumns = rows.First().Keys.Where(k => k != targetColumn).ToList();

            foreach (var row in rows)
            {
                var featureRow = new List<float>();

                foreach (var column in featureColumns)
                {
                    string value = row[column];
                    featureRow.Add(float.TryParse(value, out float result) ? result : 0f);
                }

                features.Add(featureRow);

                string label = row[targetColumn];
                labels.Add(float.TryParse(label, out float labelValue) ? labelValue : 0f);
            }

            return new TrainingData
            {
                Features = features,
                Labels = labels
            };
        }
    }
}
