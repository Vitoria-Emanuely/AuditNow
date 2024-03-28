namespace AuditNow.Core.Models.ValueObjects
{
    public class ReturnObject<T>
    {

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }

        public List<T> Data { get; set; }

    }
}