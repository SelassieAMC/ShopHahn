namespace Hahn.ApplicationProcess.February2021.Domain.ResponseObjects
{
    public class UnitResult<T>
    {
        public bool EndOnSuccess { get; set; }
        public bool EndOnError { get; set; }
        public string ErrorMessage { get; set; }
        public T Result { get; set; }

        public void Failure(string error, T result)
        {
            ErrorMessage = error;
            EndOnError = true;
            Result = result;
        }

        public void Success(T result)
        {
            EndOnSuccess = true;
            Result = result;
        }
    }
}
