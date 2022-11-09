namespace CompWeek.Domain.Commons;

public class CustomException : Exception
{
    public List<string> Errors { get; set; }

    public CustomException()
    {
        this.Errors = new List<string>();
    }

    public CustomException(string error)
    {
        this.Errors = new List<string>();
        this.Errors.Add(error);
    }

    public CustomException(List<string> errors)
    {
        this.Errors = errors;
    }

    public CustomException(Exception e)
    {
        this.Errors = new List<string>();
        this.Errors.Add(e.Message);
        
        if(!string.IsNullOrEmpty(e.StackTrace))
            this.Errors.Add(e.StackTrace);
    }
}

