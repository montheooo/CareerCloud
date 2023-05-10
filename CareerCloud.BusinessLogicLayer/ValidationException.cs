namespace CareerCloud.BusinessLogicLayer
{
    public class ValidationException : Exception
    {
        public int Code { get; set; }

        public ValidationException(int code_p, string message_p) : base(message_p)
        {
            Code = code_p;
        }
    }
}