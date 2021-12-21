using System;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace GoogleSheets
{
    class GoogleInitialization
    {
        private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private readonly string ApplicationName = "Project";
        private readonly string SpreadsheetID = "10vQT-ERAhdm1lAVAD1bJooi7bmYkEzHCftr6lCFFYxc";
        private readonly string sheet1 = "Преподаватели";
        private readonly string sheet2 = "Аудитории";
        private readonly string sheet3 = "Предметы";
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

        public string[][] GetTeachersSchedule()
        {
            var rangeTeach = $"{sheet1}!A2:H100";
            return GetSchedule(8, rangeTeach);
        }

        public string[][] GetRoomsSchedule()
        {
            var rangeRooms = $"{sheet2}!A2:I100";
            return GetSchedule(9, rangeRooms);
        }

        public string[][] GetLessonsSchedule()
        {
            var rangeLessons = $"{sheet3}!A2:D100";
            return GetSchedule(4, rangeLessons);
        }

        private string[][] GetSchedule(int rowsCount, string range)
        {
            var request = service.Spreadsheets.Values.Get(SpreadsheetID, range);
            var response = request.Execute();
            var values = response.Values;
            if (values == null)
                throw new ArgumentNullException("Empty input schedule!");

            var schedule = new string[rowsCount][];
            for (var i = 0; i < rowsCount; i++)
            {
                schedule[i] = new string[values.Count]; 
            }

            for (var i = 0; i < values.Count; i++) 
            {
                var row = values[i]; 
                for (var k = 0; k < row.Count; k++) 
                {
                    if (row[k].Equals(""))
                        schedule[k][i] = null;
                    else schedule[k][i] = row[k].ToString();
                }
            }

            return schedule;
        }
    }

    public class Program
    {
        public static void Main()
        {
            var init = new GoogleInitialization();
            var teach = init.GetTeachersSchedule();
            var rooms = init.GetRoomsSchedule();
            var less = init.GetLessonsSchedule();
        }
    }
}