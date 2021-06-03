using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace TimeTableUWP
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class ErrorCodeAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string positionalString;

        // This is a positional argument
        public ErrorCodeAttribute(string positionalString)
        {
            this.positionalString = positionalString;

        }

        public string PositionalString
        {
            get { return positionalString; }
        }
    }

    [Serializable, ErrorCode("1xxx")]
    public class TimeTableException : Exception
    {
        public int ErrorCode { get; protected set; } = -1;
        public TimeTableException() { }
        public TimeTableException(string message) : base(message) { }
        public TimeTableException(string message, int errorCode) : this(message) { ErrorCode = errorCode; }
        public TimeTableException(string message, Exception inner) : base(message, inner) { }
        protected TimeTableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public static async void HandleException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            Exception exception = e.Exception;
            int? code = null;

            if (exception is TimeTableException te)
            {
                code = te.ErrorCode;
            }

            MessageDialog messageDialog = new(
                @$"에러가 발생했습니다. 개발자에게 아래 정보를 전송해주세요.
{(code is not null ? $"\nError code: {code}" : "")}
{exception}", "An error has occured.");
            await messageDialog.ShowAsync();
        }
    }

    [Serializable, ErrorCode("2xxx")]
    public class TableCellException : TimeTableException
    {
        public TableCellException() { }
        public TableCellException(string message, int errorCode = 2000) : base(message, errorCode) { }
        public TableCellException(string message, Exception inner) : base(message, inner) { }
        protected TableCellException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable, ErrorCode("3xxx")]
    public class DataAccessException : TimeTableException
    {
        public DataAccessException() { }
        public DataAccessException(string message, int errorCode = 3000) : base(message, errorCode) { }
        public DataAccessException(string message, Exception inner) : base(message, inner) { }
        protected DataAccessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable, ErrorCode("4xxx")]
    public class ComboBoxDataException : TimeTableException
    {
        public ComboBoxDataException() { }
        public ComboBoxDataException(string message, int errorCode = 4000) : base(message, errorCode) { }
        public ComboBoxDataException(string message, Exception inner) : base(message, inner) { }
        protected ComboBoxDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
