namespace Hahn.ApplicationProcess.February2021.Domain.ResponseObjects
{
    public class UnitResult<T> where T : class
    {
        public bool EndOnSuccess { get; set; }
        public bool EndOnError { get; set; }
        public bool EndOnValidationError { get; set; }
        public string ErrorMessage { get; set; }
        public T Result { get; set; }

        public void Failure(string error, T result = null)
        {
            ErrorMessage = error;
            EndOnError = true;
            Result = result;
        }

        public void ValidationFailure(string error, T result = null)
        {
            ErrorMessage = error;
            EndOnValidationError = true;
            Result = result;
        }

        public void Success(T result = null)
        {
            EndOnSuccess = true;
            Result = result;
        }
    }
}
