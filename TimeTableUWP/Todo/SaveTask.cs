using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Storage;
using Windows.Storage.Streams;
using RollingRess.UWP.FileIO;
using System.Threading.Tasks;
using TimeTableUWP.Pages;

namespace TimeTableUWP.Todo
{
    public static class SaveTask
    {
        private static string FileName => "TodoXML.tks";
        private static StorageFolder storageFolder => ApplicationData.Current.LocalFolder;

        public static async Task Save()
        {
            DataWriter<List<TodoTask>> writer = new(FileName, TodoPage.TaskList.List);
            await writer.WriteAsync();

            // 디버그 해볼것... 지금 파일 생성이 안 됨...
        }

        public static async Task Load()
        {
            if (await storageFolder.TryGetItemAsync(FileName) is not StorageFile)
                return;

            DataReader<List<TodoTask>> reader = new(FileName);
            TodoPage.TaskList.List = await reader.ReadAsync();
        }
    }
}