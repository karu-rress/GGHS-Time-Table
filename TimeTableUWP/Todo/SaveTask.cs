#nullable enable

using System;
using System.Collections.Generic;
using Windows.Storage;
using RollingRess.UWP.FileIO;
using System.Threading.Tasks;
using TimeTableUWP.Pages;

namespace TimeTableUWP.Todo
{
    public static class SaveTask
    {
        private static string FileName => "GTDTaskXML.tks";
        private static StorageFolder Storage => ApplicationData.Current.LocalFolder;

        public static async Task Save()
        {
            DataWriter<List<TodoTask>> writer = new(FileName, TodoListPage.TaskList.List);
            await writer.WriteAsync();
        }

        public static async Task Load()
        {
            if (await Storage.TryGetItemAsync(FileName) is not StorageFile)
                return;

            DataReader<List<TodoTask>> reader = new(FileName);
            TodoListPage.TaskList.List = await reader.ReadAsync();
        }
    }
}