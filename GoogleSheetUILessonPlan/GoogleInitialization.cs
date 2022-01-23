using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace GoogleSheets
{
    class GoogleInitialization
    {
        private readonly string[] Scopes = {SheetsService.Scope.Spreadsheets};
        private readonly string ApplicationName = "Project";
        private readonly string SpreadsheetID = "10vQT-ERAhdm1lAVAD1bJooi7bmYkEzHCftr6lCFFYxc";
        private readonly string sheet1 = "Преподаватели";
        private readonly string sheet2 = "Аудитории";
        private readonly string sheet3 = "Предметы";
        private List<string> outputSheets;
        private SheetsService service;

        public GoogleInitialization()
        {
            GoogleCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public string[,] GetTeachersSchedule()
        {
            var rangeTeach = $"{sheet1}!A2:H100";
            return GetSchedule(8, rangeTeach);
        }

        public string[,] GetRoomsSchedule()
        {
            var rangeRooms = $"{sheet2}!A2:I100";
            return GetSchedule(9, rangeRooms);
        }

        public string[,] GetLessonsSchedule()
        {
            var rangeLessons = $"{sheet3}!A2:D100";
            return GetSchedule(4, rangeLessons);
        }

        private string[,] GetSchedule(int rowsCount, string range)
        {
            var request = service.Spreadsheets.Values.Get(SpreadsheetID, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null)
                throw new ArgumentNullException("Empty input schedule!");

            var schedule = new string[rowsCount, values.First().Count];

            for (var i = 0; i < values.Count; i++)
            {
                var row = values[i];
                for (var k = 0; k < row.Count; k++)
                {
                    if (row[k].Equals(""))
                        schedule[i,k] = null;
                    else schedule[k,i] = row[k].ToString();
                }
            }

            return schedule;
        }
        
        public void WriteToSheets(List<string[,]> input)
        {
            outputSheets = new List<string>();
            for (var i = 0; i < input.Count; i++)
            {
                CreateSheet(i);
                CreateSample(outputSheets[i]);
                var data = CastToMassiveOfMassives(input[i]);
                
                var resource = new ValueRange{ MajorDimension = "COLUMNS", Values = data};
                var range = $"{outputSheets[i]}!B2:G7";
                var appendRequest = service.Spreadsheets.Values.Append(resource, SpreadsheetID, range);
                appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
                var appendResponse = appendRequest.Execute();
            }
        }

        private void CreateSheet(int numSheet)
        {
            var sheetName = $"Расписание {numSheet + 1}";
            outputSheets.Add(sheetName);
            var addSheetRequest = new AddSheetRequest();
            addSheetRequest.Properties = new SheetProperties();
            addSheetRequest.Properties.Title = sheetName;
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
            batchUpdateSpreadsheetRequest.Requests = new List<Request>();
            batchUpdateSpreadsheetRequest.Requests.Add(new Request
            {
                AddSheet = addSheetRequest
            });

            var batchUpdateRequest =
                service.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, SpreadsheetID);

            batchUpdateRequest.Execute();
        }

        private string[][] CastToMassiveOfMassives(string[,] input)
        {
            var newData = new string[input.GetLongLength(0)][];
            for (var i = 0; i < input.GetLongLength(0); i++)
            {
                var lowData = new string[input.GetLongLength(1)];
                for (var j = 0; j < input.GetLongLength(1); j++)
                {
                    lowData[j] = input[i, j];
                }

                newData[i] = lowData;
            }

            return newData;
        }

        private void CreateSample(string sheet)
        {
            var days = new string[][]
            {
                new string[] {"Пн"},
                new string[] {"Вт"},
                new string[] {"Ср"},
                new string[] {"Чт"},
                new string[] {"Пт"},
                new string[] {"Сб"}
            };
            var resource = new ValueRange {MajorDimension = "COLUMNS", Values = days};
            var range = $"{sheet}!B1:G1";
            var appendRequest = service.Spreadsheets.Values.Append(resource, SpreadsheetID, range);
            appendRequest.ValueInputOption =
                SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendResponse = appendRequest.Execute();
            var numbLesson = new string[][]
            {
                new string[] {"1"},
                new string[] {"2"},
                new string[] {"3"},
                new string[] {"4"},
                new string[] {"5"},
                new string[] {"6"},
                new string[] {"7"}
            };
            var resource1 = new ValueRange {MajorDimension = "ROWS", Values = numbLesson};
            var range1 = $"{sheet}!A2:A8";
            var appendRequest1 = service.Spreadsheets.Values.Append(resource1, SpreadsheetID, range1);
            appendRequest1.ValueInputOption =
                SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendResponse1 = appendRequest1.Execute();
        }
    }
}