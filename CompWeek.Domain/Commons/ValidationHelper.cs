namespace CompWeek.Domain.Commons;

public class ValidationHelper
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; }

    public ValidationHelper()
    {
        this.IsValid = true;
        this.Errors = new List<string>();
    }

    public void AddError(string erro)
    {
        this.IsValid = false;
        this.Errors.Add(erro);
    }
}
